using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Omgevingsmonitor_configurator.OpenSenseMapApiClient;

namespace Omgevingsmonitor_configurator
{
    public partial class LoginForm : Form
    {
        public class UserLoginEventArgs : EventArgs
        {
            public string Email { get; set; }
            public string Name { get; set; }
            public string Role { get; set; }
            public string Language { get; set; }
            public bool EmailIsConfirmed { get; set; }

            public UserLoginEventArgs(OpenSenseMapApiClient.UserInfo userInfo)
            {
                Email = userInfo.Email;
                Name = userInfo.Name;
                Role = userInfo.Role;
                Language = userInfo.Language;
                EmailIsConfirmed = userInfo.EmailIsConfirmed;
            }
        }

        public event EventHandler<UserLoginEventArgs> LoginSuccessful;

        public LoginForm()
        {
            InitializeComponent();
            this.AcceptButton = loginBtn;
            loginBtn.Enabled = true;
        }

        private async void loginBtn_Click(object sender, EventArgs e)
        {
            OpenSenseMapApiClient client = new OpenSenseMapApiClient();
            loginBtn.Enabled = false;
            try
            {
                (var result, UserInfo userInfo) = await client.SignInAsync(emailTextBox.Text, passwordTextBox.Text);
                if (result)
                {
                    LoginSuccessful?.Invoke(this, new UserLoginEventArgs(userInfo));

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Email or password incorrect", "Incorrect details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    loginBtn.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                loginBtn.Enabled = true;
            }
        }
    }
}
