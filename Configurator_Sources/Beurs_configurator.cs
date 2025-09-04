using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Omgevingsmonitor_configurator
{
    public partial class Beurs_configurator : Form
    {
        Form1 mainGui = null;
        public Beurs_configurator(Form1 form)
        {
            InitializeComponent();
            mainGui = form;
        }

        

        private void configBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("While pressing the boot button (dB button), reset your gadget using the reset button (status button)", "Prepare for update");
            List<StmDevice> devices = new List<StmDevice>();
            while (devices.Count == 0)
            {
                devices = STM_Programmer.getStmDevices("USB");
            }
            foreach (StmDevice device in devices)
            {                
                Thread stmProgramThread = new Thread(() =>
                {
                    STM_Programmer.flashStm32ProgressBar(mainGui.StmFilePath, "", device.DeviceIndex, device.SN, configProgressBar, outputBox);
                    STM_Programmer.programmEepromSTM32(device.DeviceIndex, device.SN, 0x08080090, Encoding.Default.GetBytes(customNameBox.Text), (UInt16)customNameBox.MaxLength, configProgressBar, outputBox);
                    MessageBox.Show("Gadget ready!!! you can now search for your gadget on deomgevingsmonitor.nl", "Concratulations", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    //STM_Programmer.flashStm32ProgressBar(mainGui.StmFilePath, "", device.DeviceIndex, device.SN, configProgressBar, outputBox);

                    //MessageBox.Show("Reset your gadget", "Update finished");
                    //List<string> gadgetComPorts = new List<string>();
                    //while (gadgetComPorts.Count == 0)
                    //{
                    //    gadgetComPorts = USB_COM.ComPorts(USB_COM.GadgetVID, USB_COM.GadgetPID);
                    //}
                    //if (gadgetComPorts.Count != 0)
                    //{
                    //    MessageBox.Show("Gadget resetted", "Device found");
                    //    foreach (string comPort in gadgetComPorts)
                    //    {

                    //    }
                    //}
                });
                stmProgramThread.Name = "Flashing STM on:" + device;
                stmProgramThread.Start();
            }
        }

        private void Beurs_configurator_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }
    }
}
