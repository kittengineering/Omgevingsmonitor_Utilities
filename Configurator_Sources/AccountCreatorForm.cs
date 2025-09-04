using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Omgevingsmonitor_configurator
{
    public partial class AccountCreatorForm : Form
    {
        public class RegistrationResult
        {
            public bool Success { get; set; }
            public string ErrorMessage { get; set; }

            public RegistrationResult(bool success, string errorMessage = null)
            {
                Success = success;
                ErrorMessage = errorMessage;
            }
        }

        public AccountCreatorForm()
        {
            InitializeComponent();
        }

        private async void createBtn_Click(object sender, EventArgs e)
        {
            var client = new OpenSenseMapApiClient();

            try
            {
                var result = await client.RegisterUserAsync(nameTextBox.Text, emailTextBox.Text, passwordTextBox.Text);
                if (result.Success)
                {
                    MessageBox.Show("User registered successfully. Please check your email to confirm your account.");
                }
                else
                {
                    MessageBox.Show($"Registration failed with the following reason:\n\n{result.ErrorMessage}", "Faillure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void showPasswordcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox)sender;
            if (checkbox.Checked)
            {
                passwordTextBox.PasswordChar = '\0';
            }
            else
                passwordTextBox.PasswordChar = '•';


        }
    }
}
