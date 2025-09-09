using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Text.RegularExpressions;
using static Omgevingsmonitor_configurator.LoginForm;

namespace Omgevingsmonitor_configurator
{
    public partial class Form1 : Form
    {
        private AccountCreatorForm accountCreatorForm;
        private LoginForm loginForm;
        private Beurs_configurator beursForm;

        private List<Box> userBoxesList; 

        private OpenSenseMapApiClient apiClient;

        public bool SerialConfigPortOpen = false;
        public bool LoggedIn = false;
        public bool SerialConfigPortFree = true;

        public ConcurrentQueue<byte[]> TxQueue = new ConcurrentQueue<byte[]>();
        public ConcurrentQueue<WoTS_Message> RxQueue = new ConcurrentQueue<WoTS_Message>();

        
        static Threads threads = null;
        public STM_Programmer StmProgrammer = null;
        static General general = null;

        public int flashProcessRunning = 0;

        public string StmFilePath;

        public Form1()
        {
            InitializeComponent();
            apiClient = new OpenSenseMapApiClient();
            threads = new Threads(this);
            StmProgrammer = new STM_Programmer(this);
            general = new General(this);

            if (!STM_Programmer.checkStm32Install())
            {
                groupBox2.Visible = false;
                labelBlockedReason.Visible = true;
                labelBlockedReason.Text =
                    $"De STM32 programmeerfunctie is uitgeschakeld.\r\n" +
                    $"Er is een niet correcte versie of geen installatie van\r\n" +
                    $"STM32CubeProgrammer gevonden.\r\n\r\n" +
                    $"Vereist: STM32CubeProgrammer 2.17.0 of 2.18.0\r\n" +
                    $"Installeer, update STM32CubeProgrammer of \r\n" +
                    $"controlleer de omgevingsvariabele %STM32_PROGRAMMER%\r\n" +
                    $"en probeer opnieuw.";
            }
            else
            {
                groupBox2.Visible = true;
                labelBlockedReason.Visible = false;
            }
        }

        public void UpdateProgressBar(ToolStripProgressBar pb, int value)
        {
            try
            {
                if (pb.GetCurrentParent().InvokeRequired) this.BeginInvoke((MethodInvoker)(() => UpdateProgressBar(pb, value)));
                else
                {
                    pb.Value = value;
                }
            }
            catch { }
        }

        public void UpdateProgressBar(ProgressBar pb, int value)
        {
            try
            {
                if (pb.InvokeRequired) this.BeginInvoke((MethodInvoker)(() => UpdateProgressBar(pb, value)));
                else
                {
                    pb.Value = value;
                }
            }
            catch { }
        }

        public void UpdateTextBox(TextBox tb, string text)
        {
            try
            {
                if (tb.InvokeRequired) this.BeginInvoke((MethodInvoker)(() => UpdateTextBox(tb, text)));
                else
                {
                    tb.AppendText(text + "\r\n");
                    //tb.ScrollToCaret();
                }
            }
            catch { }
        }

        private async Task LoadUserBoxesAsync()
        {
            try
            {
                userBoxesList = await apiClient.GetUserBoxesAsync(); // Store the boxes
                DisplayUserBoxes(userBoxesList);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Your session has expired. Please log in again.", "Session Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user's boxes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayUserBoxes(List<Box> boxes)
        {
            userBoxesListView.Items.Clear();
            foreach (var box in boxes)
            {
                var item = new ListViewItem(new[]
                {
                box.Name,
                box.Exposure,
                box.Model,
                box.LastMeasurementAt.ToString("yyyy-MM-dd HH:mm:ss"),
                box.Sensors.Count.ToString()
            });
                userBoxesListView.Items.Add(item);
            }
        }


        private async void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           
            if (SerialConfigPortOpen)
            {
                SerialConfigPortOpen = false;
            }
            await LogoutUser();
        }
        private async void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (configPort.IsOpen)
            {
                configPort.Close();
            }
            if (SerialConfigPortOpen)
            {
                configPort.Close();
                SerialConfigPortOpen = false;
            }
            await LogoutUser();
            this.Close();
        }
        private async Task LogoutUser()
        {
            if (string.IsNullOrEmpty(TokenManager.RefreshToken))
                return;
            try
            {
                await apiClient.SignOutAsync();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("www.kitt.nl");
        }

        private void createNewAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (accountCreatorForm == null || accountCreatorForm.IsDisposed)
            {
                accountCreatorForm = new AccountCreatorForm();
                accountCreatorForm.Show();
            }
            else
            {
                accountCreatorForm.BringToFront();
            }
        }

        private void loginAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loginForm == null || loginForm.IsDisposed)
            {
                loginForm = new LoginForm();
                loginForm.LoginSuccessful += OnLoginSuccessful;
                loginForm.Show();
            }
            else
            {
                loginForm.BringToFront();
            }
        }

        private async void OnLoginSuccessful(object sender, UserLoginEventArgs e)
        {

            string emailconfirmedText = e.EmailIsConfirmed ? "is confirmed." : "has to be confirmed via the website.";
            loginToolStrilLbl.Text = $"Logged in as: {e.Email}, {e.Name}. Email {emailconfirmedText}";
            loginToolStrilLbl.BackColor = Color.Green;
            loginToolStripMenuItem.BackColor = Color.Green;
            addBoxBtn.Enabled = true;
            addBoxBtn.BackColor = Color.White;
            LoggedIn = true;

            if (SerialConfigPortOpen)
            {
                configBtn.Enabled = true;
                configBtn.BackColor = Color.White;
                deleteConfigBtn.Enabled = true;
                deleteConfigBtn.BackColor = Color.White;
            }
            

            await LoadUserBoxesAsync();
        }

        private async void addBoxBtn_Click(object sender, EventArgs e)
        {
            using (var addSenseBoxForm = new AddSenseBoxForm())
            {
                if (addSenseBoxForm.ShowDialog() == DialogResult.OK)
                {
                    await LoadUserBoxesAsync();
                }
        }
    }

        private void userBoxesListView_DoubleClick_1(object sender, EventArgs e)
        {
            if (userBoxesListView.SelectedItems.Count > 0)
            {
                int selectedIndex = userBoxesListView.SelectedIndices[0];
                Box selectedBox = userBoxesList[selectedIndex];

                BoxDetailsForm detailsForm = new BoxDetailsForm(selectedBox);
                detailsForm.Show();
            }
        }

        private void userBoxesListView_Click(object sender, EventArgs e)
        {
            idGridView.Rows.Clear();
            if (userBoxesListView.SelectedItems.Count > 0)
            {
                int selectedIndex = userBoxesListView.SelectedIndices[0];
                Box selectedBox = userBoxesList[selectedIndex];

                idGridView.Rows.Add("Box ID", selectedBox._id);
                foreach (Box.Sensor sensor in selectedBox.Sensors)
                {
                    int rowIndex = idGridView.Rows.Add(sensor.title, sensor._id);
                    idGridView.Rows[rowIndex].Tag = sensor;
                }
            }

        }

        private void configBtn_Click(object sender, EventArgs e)
        {
            if (configPort.IsOpen)
            {
                byte[] message = new byte[WoTS_Protocol.TOTAL_BUFFER_SIZE];

                foreach (DataGridViewRow r in idGridView.Rows)
                {
                    switch (r.Cells[0].Value)
                    {
                        case "Box ID":
                            message = WoTS_Protocol.Create_Message((byte)WoTS_Protocol.Command.BoxConfig, General.ConvertHexString((string)r.Cells[1].Value));
                            break;
                        case "Temperature":
                            message = WoTS_Protocol.Create_Message((byte)WoTS_Protocol.Command.TempConfig, General.ConvertHexString((string)r.Cells[1].Value));
                            break;
                        case "Humidity":
                            message = WoTS_Protocol.Create_Message((byte)WoTS_Protocol.Command.HumidConfig, General.ConvertHexString((string)r.Cells[1].Value));
                            break;
                        case "NOx":
                            message = WoTS_Protocol.Create_Message((byte)WoTS_Protocol.Command.NOxConfig, General.ConvertHexString((string)r.Cells[1].Value));
                            break;
                        case "VOCindex":
                            message = WoTS_Protocol.Create_Message((byte)WoTS_Protocol.Command.VocIndexConfig, General.ConvertHexString((string)r.Cells[1].Value));
                            break;
                        case "Soundpresure dBa":
                            message = WoTS_Protocol.Create_Message((byte)WoTS_Protocol.Command.dBaConfig, General.ConvertHexString((string)r.Cells[1].Value));
                            break;
                        case "Soundpresure dBc":
                            message = WoTS_Protocol.Create_Message((byte)WoTS_Protocol.Command.dBcConfig, General.ConvertHexString((string)r.Cells[1].Value));
                            break;
                        case "Battery voltage":
                            message = WoTS_Protocol.Create_Message((byte)WoTS_Protocol.Command.BatVoltConfig, General.ConvertHexString((string)r.Cells[1].Value));
                            break;
                        case "Solar voltage":
                            message = WoTS_Protocol.Create_Message((byte)WoTS_Protocol.Command.SolVoltConfig, General.ConvertHexString((string)r.Cells[1].Value));
                            break;
                        case "PM2.5":
                            message = WoTS_Protocol.Create_Message((byte)WoTS_Protocol.Command.PM2Config, General.ConvertHexString((string)r.Cells[1].Value));
                            break;
                        case "PM10":
                            message = WoTS_Protocol.Create_Message((byte)WoTS_Protocol.Command.PM10Config, General.ConvertHexString((string)r.Cells[1].Value));
                            break;
                    }
                    TxQueue.Enqueue(message);

                }
            }
            else
            {
                MessageBox.Show("Please select the correct COM port", "Communication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void configPortBox_DropDown(object sender, EventArgs e)
        {
            //string[] ports = SerialPort.GetPortNames();
            configPortBox.Items.Clear();
            //foreach (string port in ports)
            //{
            //    configPortBox.Items.Add(port);
            //}

            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%(COM%'"))
            {
                foreach (var obj in searcher.Get())
                {
                    string name = obj["Name"]?.ToString(); // bijv. "USB-SERIAL CH340 (COM3)"
                    configPortBox.Items.Add(name);
                }
            }

        }

        private void configPortBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var match = Regex.Match(configPortBox.Text, @"(COM\d+)");
            if (match.Success)
            {
                configPort.PortName = match.Value; // configPortBox.Text;
                configPort.BaudRate = 115200; // Convert.ToInt32(configBaudBox.Text);
                configPort.Open();

                SerialConfigPortOpen = true;

                Thread serialReceive = new Thread(threads.rxSerialThread);
                serialReceive.Name = "ReceiveData";
                serialReceive.Start();

                Thread serialTransmit = new Thread(threads.txSerialThread);
                serialTransmit.Name = "TransmitData";
                serialTransmit.Start();

                Thread handleReceived = new Thread(threads.rxHandlingThread);
                handleReceived.Name = "HandleRxData";
                handleReceived.Start();

                configPortBox.BackColor = Color.Green;
                if (LoggedIn)
                {
                    configBtn.Enabled = true;
                    configBtn.BackColor = Color.White;
                    deleteConfigBtn.Enabled = true;
                    deleteConfigBtn.BackColor = Color.White;
                }
            }


        }

        private void deleteConfigBtn_Click(object sender, EventArgs e)
        {
            if (configPort.IsOpen)
            {
                byte[] temp = new byte[1];
                byte[] message = WoTS_Protocol.Create_Message((byte)WoTS_Protocol.Command.ClearConfig, temp);
                Console.WriteLine(Encoding.ASCII.GetString(message));
                TxQueue.Enqueue(message);
            }
            else
            {
                MessageBox.Show("Please select the correct COM port", "Communication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void beursMenuItem_Click(object sender, EventArgs e)
        {
            if (beursForm == null || beursForm.IsDisposed)
            {
                beursForm = new Beurs_configurator(this);
                //beursForm.LoginSuccessful += OnLoginSuccessful;
                beursForm.Show();
            }
            else
            {
                loginForm.BringToFront();
            }


            //if (!beursMenuItem.Checked)
            //{
            //    beursMenuItem.Checked = true;
            //    stmFlashBtn.Text = "Update your Gadget";
            //}
            //else
            //{
            //    beursMenuItem.Checked = false;
            //    stmFlashBtn.Text = "Flash";
            //}
        }

        private void clearEEPROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] temp = {0xFF, 0xFF};
            byte[] message = WoTS_Protocol.Create_Message((byte)WoTS_Protocol.Command.ClearEEprom, temp);
            Console.WriteLine(Encoding.ASCII.GetString(message));
            TxQueue.Enqueue(message);
        }

        private void WiFiConfigButton_Click(object sender, EventArgs e)
        {
            if (configPort.IsOpen)
            {
                byte[] message = new byte[WoTS_Protocol.TOTAL_BUFFER_SIZE];

                for (int i = 0; i < 2; i++)
                {
                    switch (i)
                    {
                        case 0:
                            byte[] bytes = Encoding.ASCII.GetBytes(SSIDBox.Text);
                            message = WoTS_Protocol.Create_Message((byte)WoTS_Protocol.Command.SSIDConfig, bytes);
                            break;
                        case 1:
                            byte[] bytes2 = Encoding.ASCII.GetBytes(PasswordBox.Text);
                            message = WoTS_Protocol.Create_Message((byte)WoTS_Protocol.Command.PasswordConfig, bytes2);
                            break;
                    }
                    TxQueue.Enqueue(message);

                }
            }
            else
            {
                MessageBox.Show("Please select the correct COM port", "Communication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void openStmElfBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openProgramDialog = new OpenFileDialog();
            openProgramDialog.Filter = "ELF(*.elf;*.ELF)|*.elf;*.ELF";
            if (openProgramDialog.ShowDialog() == DialogResult.OK)
            {
                StmFilePath = openProgramDialog.FileName;
                //Properties.Settings.Default.StmFilePath = openProgramDialog.FileName;
                selectFileStmBox.Text = Path.GetFileName(StmFilePath);
                //Properties.Settings.Default.Save();
            }
        }

        private void stmDeviceBox_DropDown(object sender, EventArgs e)
        {
            stmDeviceBox.Items.Clear();

            STM_Programmer programmer = new STM_Programmer(this);
            List<StmDevice> devices = STM_Programmer.getStmDevices(stmInterfaceBox.Text);

            foreach (StmDevice device in devices)
            {
                if (stmInterfaceBox.Text == "USB (DFU)")
                {
                    stmDeviceBox.Items.Add(device.DeviceIndex);
                }
                else if (stmInterfaceBox.Text == "ST-Link")
                {
                    stmDeviceBox.Items.Add(device.SN);
                }
            }
        }

        private void stmFlashBtn_Click(object sender, EventArgs e)
        {
            if (selectFileStmBox.Text != "")
            {
                List<StmDevice> devices = STM_Programmer.getStmDevices(stmInterfaceBox.Text);
                StmDevice device = null;
                if (stmInterfaceBox.Text == "USB (DFU)")
                {
                    device = devices.FirstOrDefault(dev => dev.DeviceIndex == stmDeviceBox.Text);
                    if (device != null)
                    {
                        Thread stmProgramThread = new Thread(() =>
                        {
                            STM_Programmer.flashStm32ProgressBar(StmFilePath, "", device.DeviceIndex, device.SN, generalProgressBar, outputBox);
                        });
                        stmProgramThread.Name = "Flashing STM on:" + device;
                        stmProgramThread.Start();
                    }
                }
                else if (stmInterfaceBox.Text == "ST-Link")
                {
                    device = devices.FirstOrDefault(dev => dev.SN == stmDeviceBox.Text);
                    if (device != null)
                    {
                        Thread stmProgramThread = new Thread(() =>
                        {
                            STM_Programmer.flashStm32ProgressBar(StmFilePath, "", "swd", device.SN, generalProgressBar, outputBox);
                        });
                        stmProgramThread.Name = "Flashing STM on:" + device;
                        stmProgramThread.Start();
                    }
                }
            }
        }
    }
}
