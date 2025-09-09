using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Omgevingsmonitor_configurator.AccountCreatorForm;

namespace Omgevingsmonitor_configurator
{
    public class OpenSenseMapApiClient
    {
        public class SenseBoxCreateRequest
        {
            public string Name { get; set; }
            //public string Description { get; set; }
            public string Grouptag { get; set; }
            public string Exposure { get; set; }
            public SetLocation Location { get; set; }
            //public string Model { get; set; }
            public List<Sensor> Sensors { get; set; }
            //public List<string> SensorTemplates { get; set; }
            //public MqttSettings Mqtt { get; set; }
            //public TtnSettings Ttn { get; set; }
            //public bool UseAuth { get; set; }
            //public bool SharedBox { get; set; }

            public class Sensor
            {
                public string title { get; set; }
                public string unit { get; set; }
                public string sensorType { get; set; }
                public string icon { get; set; }
            }

            public class SetLocation
            {
                public double lat { get; set; }
                public double lng { get; set; }
                public double? height { get; set; }
            }
        }
        public class SenseBoxUpdateRequest
        {
            public string Name { get; set; }
            public List<string> Grouptag { get; set; }
            public Box.Location Location { get; set; }
            public List<Box.Sensor> Sensors { get; set; }
            public MqttSettings Mqtt { get; set; }
            public TtnSettings Ttn { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
            public Dictionary<string, string> Addons { get; set; }
        }

        public class MqttSettings
        {
            public bool Enabled { get; set; }
            public string Url { get; set; }
            public string Topic { get; set; }
            public string MessageFormat { get; set; }
            public string DecodeOptions { get; set; }
            public string ConnectionOptions { get; set; }
        }

        public class TtnSettings
        {
            public string DevId { get; set; }
            public string AppId { get; set; }
            public string Profile { get; set; }
            public List<string> DecodeOptions { get; set; }
            public int? Port { get; set; }
        }



        public class UserInfo
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
            public string Language { get; set; }
            public bool EmailIsConfirmed { get; set; }
        }


        public class OpenSenseMapApiException : Exception
        {
            public HttpStatusCode StatusCode { get; }
            public string ResponseBody { get; }

            public OpenSenseMapApiException(HttpStatusCode statusCode, string message, string responseBody)
                : base(message + responseBody)
            {
                StatusCode = statusCode;
                ResponseBody = responseBody;
            }
        }


        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.opensensemap.org";

        public OpenSenseMapApiClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<RegistrationResult> RegisterUserAsync(string name, string email, string password, string language = "en_US")
        {
            var endpoint = $"{BaseUrl}/users/register";
            var content = new
            {
                name,
                email,
                password,
                language
            };

            try
            {
                var response = await PostAsync(endpoint, content);
                var responseBody = await response.Content.ReadAsStringAsync();
                var responseJson = JsonSerializer.Deserialize<JsonElement>(responseBody);

                if (responseJson.TryGetProperty("token", out var tokenElement) &&
                    responseJson.TryGetProperty("refreshToken", out var refreshTokenElement))
                {
                    var accessToken = tokenElement.GetString();
                    var refreshToken = refreshTokenElement.GetString();
                    var expiresIn = 3600; // Assuming 1 hour

                    TokenManager.UpdateTokens(accessToken, refreshToken, expiresIn, false);
                    return new RegistrationResult(true);
                }

                return new RegistrationResult(false, "Registration failed: Unexpected response from server.");
            }
            catch (OpenSenseMapApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    try
                    {
                        var errorJson = JsonSerializer.Deserialize<JsonElement>(ex.ResponseBody);
                        if (errorJson.TryGetProperty("code", out var codeElement) &&
                            errorJson.TryGetProperty("message", out var messageElement))
                        {
                            string errorCode = codeElement.GetString();
                            string errorMessage = messageElement.GetString();

                            if (errorCode == "BadRequest" && errorMessage == "Duplicate user detected")
                            {
                                return new RegistrationResult(false, "An account with this email already exists.");
                            }
                            else
                            {
                                return new RegistrationResult(false, $"Registration failed: {errorMessage}");
                            }
                        }
                    }
                    catch (JsonException)
                    {
                        return new RegistrationResult(false, $"Registration failed: {ex.ResponseBody}");
                    }
                }

                return new RegistrationResult(false, $"Registration failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                return new RegistrationResult(false, $"An unexpected error occurred: {ex.Message}");
            }
        }



        public async Task<bool> ResendEmailConfirmationAsync()
        {
            await EnsureAuthenticatedAsync();

            var endpoint = $"{BaseUrl}/users/me/resend-email-confirmation";

            var response = await PostAsync(endpoint, null);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Box>> ListAllBoxesAsync(string phenomenon = null, string exposure = null, int? limit = null)
        {
            await EnsureAuthenticatedAsync();

            var endpoint = $"{BaseUrl}/boxes";
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(phenomenon))
                queryParams.Add($"phenomenon={Uri.EscapeDataString(phenomenon)}");
            if (!string.IsNullOrEmpty(exposure))
                queryParams.Add($"exposure={Uri.EscapeDataString(exposure)}");
            if (limit.HasValue)
                queryParams.Add($"limit={limit.Value}");

            if (queryParams.Count > 0)
                endpoint += "?" + string.Join("&", queryParams);

            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Box>>(responseBody);
        }

        public async Task<(bool Success, UserInfo UserInfo)> SignInAsync(string emailOrName, string password)
        {
            var endpoint = $"{BaseUrl}/users/sign-in";
            var content = new
            {
                email = emailOrName,
                password
            };

            var response = await PostAsync(endpoint, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseJson = JsonSerializer.Deserialize<JsonElement>(responseBody);

            if (responseJson.TryGetProperty("token", out var tokenElement) &&
                responseJson.TryGetProperty("refreshToken", out var refreshTokenElement) &&
                responseJson.TryGetProperty("data", out var dataElement))
            {
                var accessToken = tokenElement.GetString();
                var refreshToken = refreshTokenElement.GetString();
                var expiresIn = 3600; // Assuming 1 hour

                UserInfo userInfo = null;
                if (dataElement.TryGetProperty("user", out var userElement))
                {
                    userInfo = new UserInfo
                    {
                        Name = userElement.GetProperty("name").GetString(),
                        Email = userElement.GetProperty("email").GetString(),
                        Role = userElement.GetProperty("role").GetString(),
                        Language = userElement.GetProperty("language").GetString(),
                        EmailIsConfirmed = userElement.GetProperty("emailIsConfirmed").GetBoolean()
                    };
                }

                TokenManager.UpdateTokens(accessToken, refreshToken, expiresIn, userInfo.EmailIsConfirmed);
                return (true, userInfo);
            }

            return (false, null);
        }

        public async Task<bool> RefreshAuthAsync()
        {
            if (string.IsNullOrEmpty(TokenManager.RefreshToken))
            {
                throw new InvalidOperationException("No refresh token available.");
            }

            var endpoint = $"{BaseUrl}/users/refresh-auth";
            var content = new
            {
                token = TokenManager.RefreshToken
            };

            var response = await PostAsync(endpoint, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseJson = JsonSerializer.Deserialize<JsonElement>(responseBody);

            if (responseJson.TryGetProperty("token", out var tokenElement) &&
                responseJson.TryGetProperty("refreshToken", out var refreshTokenElement) &&
                responseJson.TryGetProperty("data", out var dataElement))
            {
                var accessToken = tokenElement.GetString();
                var refreshToken = refreshTokenElement.GetString();
                var expiresIn = 3600; // Assuming 1 hour

                bool emailIsConfirmed = false;
                if (dataElement.TryGetProperty("user", out var userElement) &&
                    userElement.TryGetProperty("emailIsConfirmed", out var emailConfirmedElement))
                {
                    emailIsConfirmed = emailConfirmedElement.GetBoolean();
                }

                TokenManager.UpdateTokens(accessToken, refreshToken, expiresIn, emailIsConfirmed);
                return true;
            }

            return false;
        }

        public async Task<bool> ConfirmEmailAsync(string email, string token)
        {
            var endpoint = $"{BaseUrl}/users/confirm-email";
            var content = new
            {
                email,
                token
            };

            var response = await PostAsync(endpoint, content);
            if (response.IsSuccessStatusCode)
            {
                TokenManager.EmailIsConfirmed = true;
                return true;
            }
            return false;
        }

        public async Task<bool> SignOutAsync()
        {
            if (string.IsNullOrEmpty(TokenManager.AccessToken))
            {
                throw new InvalidOperationException("No access token available. User is not signed in.");
            }

            var endpoint = $"{BaseUrl}/users/sign-out";

            var response = await PostAsync(endpoint, null);
            if (response.IsSuccessStatusCode)
            {
                TokenManager.ClearTokens();
                return true;
            }

            return false;
        }


        public async Task EnsureAuthenticatedAsync()
        {
            if (TokenManager.IsTokenExpired())
            {
                if (!await RefreshAuthAsync())
                {
                    throw new UnauthorizedAccessException("Unable to refresh authentication. Please log in again.");
                }
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.AccessToken);
        }

        public class ApiResponse
        {
            public string Code { get; set; }
            public ApiData Data { get; set; }
        }

        public class ApiData
        {
            public List<Box> Boxes { get; set; }
            public int BoxesCount { get; set; }
            public List<Box> SharedBoxes { get; set; }
        }

        public async Task<List<Box>> GetUserBoxesAsync()
        {
            try
            {
                await EnsureAuthenticatedAsync();
                var endpoint = $"{BaseUrl}/users/me/boxes";
                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                };

                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(responseBody, options);

                if (apiResponse?.Code == "Ok" && apiResponse.Data?.Boxes != null)
                {
                    return apiResponse.Data.Boxes;
                }

                return new List<Box>();
            }
            catch (HttpRequestException ex)
            {
                throw new UnauthorizedAccessException("Unable to access user boxes. Please log in again.", ex);
            }
        }


        public async Task<Box> GetUserBoxAsync(string boxId)
        {
            await EnsureAuthenticatedAsync();

            var endpoint = $"{BaseUrl}/users/me/boxes/{boxId}";

            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Box>(responseBody);
        }

        public async Task<List<Box>> ListSharedBoxesAsync()
        {
            await EnsureAuthenticatedAsync();

            var endpoint = $"{BaseUrl}/users/me/boxes";

            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Box>>(responseBody);
        }


        public async Task<Box> PostNewSenseBoxAsync(SenseBoxCreateRequest request)
        {
            await EnsureAuthenticatedAsync();

            var endpoint = $"{BaseUrl}/boxes";
            var response = await PostAsync(endpoint, request);
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Box>(responseBody);
        }

        public async Task<bool> MarkSenseBoxForDeletionAsync(string boxId, string password)
        {
            await EnsureAuthenticatedAsync();

            var endpoint = $"{BaseUrl}/boxes/{boxId}";
            var content = new { password };
            var response = await DeleteAsync(endpoint, content);
            return response.IsSuccessStatusCode;
        }


        private async Task<HttpResponseMessage> PostAsync(string endpoint, object content)
        {
            JsonSerializerOptions serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(content, serializeOptions);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(TokenManager.AccessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", TokenManager.AccessToken);
            }

            var response = await _httpClient.PostAsync(endpoint, httpContent);

            //if (!response.IsSuccessStatusCode)
            //{
            //    var responseBody = await response.Content.ReadAsStringAsync();
            //    throw new OpenSenseMapApiException(response.StatusCode, response.ReasonPhrase, responseBody);
            //}

            return response;
        }

        private async Task<HttpResponseMessage> PutAsync(string endpoint, object content)
        {
            var json = JsonSerializer.Serialize(content);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(TokenManager.AccessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.AccessToken);
            }

            var response = await _httpClient.PutAsync(endpoint, httpContent);
            response.EnsureSuccessStatusCode();

            return response;
        }

        private async Task<HttpResponseMessage> DeleteAsync(string endpoint, object content)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, endpoint);

            if (content != null)
            {
                var json = JsonSerializer.Serialize(content);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            if (!string.IsNullOrEmpty(TokenManager.AccessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.AccessToken);
            }

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return response;
        }



    }

}

