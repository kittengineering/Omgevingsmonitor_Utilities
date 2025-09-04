using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omgevingsmonitor_configurator
{
    public static class TokenManager
    {
        private static string _accessToken;
        private static string _refreshToken;
        private static DateTime _expirationTime;
        private static bool _emailIsConfirmed;

        public static string AccessToken
        {
            get { return _accessToken; }
            set { _accessToken = value; }
        }

        public static string RefreshToken
        {
            get { return _refreshToken; }
            set { _refreshToken = value; }
        }

        public static DateTime ExpirationTime
        {
            get { return _expirationTime; }
            set { _expirationTime = value; }
        }

        public static bool EmailIsConfirmed
        {
            get { return _emailIsConfirmed; }
            set { _emailIsConfirmed = value; }
        }

        public static bool IsTokenExpired()
        {
            return DateTime.UtcNow >= _expirationTime;
        }

        public static void ClearTokens()
        {
            _accessToken = null;
            _refreshToken = null;
            _expirationTime = DateTime.MinValue;
            _emailIsConfirmed = false;
        }

        public static void UpdateTokens(string accessToken, string refreshToken, int expiresIn, bool emailIsConfirmed)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpirationTime = DateTime.UtcNow.AddSeconds(expiresIn);
            EmailIsConfirmed = emailIsConfirmed;
        }
    }

}
