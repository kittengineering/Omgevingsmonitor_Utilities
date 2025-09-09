using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace Omgevingsmonitor_configurator
{
    public class STM_Programmer
    {
        public const string StLink_SN = "   ST-LINK SN  :";
        public const string StLink_FW = "   ST-LINK FW  :";
        public const string StLink_Index = "   Access Port Number  :";
        public const string StLink_BoardName = "   Board Name  :";
        public const string DFU_SN = "  Serial number          :";
        public const string DFU_FW = "  Firmware version       :";
        public const string DFU_ProductID = "  Product ID             :";
        public const string DFU_DeviceIndex = "  Device Index           :";

        static string ProgrammerPath = Environment.GetEnvironmentVariable("STM32_PROGRAMMER");

        static Form1 gui = null;
        static ToolStripProgressBar usedBarProgressBar;
        static ProgressBar usedProgressBar;
        static TextBox usedTextBox;

        public STM_Programmer(Form1 form)
        {
            gui = form;
        }

        public static bool checkStm32Install()
        {
            Process flashSTM = new Process();
            flashSTM.StartInfo.FileName = "cmd.exe";
            flashSTM.StartInfo.Arguments = $"/c \"\"{ProgrammerPath}\\STM32_Programmer_CLI.exe\" -version\"";
            flashSTM.StartInfo.RedirectStandardOutput = true;
            flashSTM.StartInfo.UseShellExecute = false;
            flashSTM.StartInfo.CreateNoWindow = true;

            flashSTM.Start();
            string output = flashSTM.StandardOutput.ReadToEnd();
            flashSTM.WaitForExit();
     
            if (output.Contains("2.17.0") || output.Contains("2.18.0"))
            {
                return true;
            }
            return false;
        }

        public static string flashStm32(string stmFile, string address, string port, string sn)
        {
            //string batchFilePath = @"STM32L072batch.bat";
            //string batchFilePathQuoted = "\"" + batchFilePath + "\"";
            //Console.WriteLine(batchFilePathQuoted);

            string binPath = stmFile;
            string binPathQuoted = "\"" + binPath + "\"";
            Console.WriteLine("binPathQouted: " + binPathQuoted);

            address = "0x" + address;

            Process flashSTM = new Process();
            flashSTM.StartInfo.FileName = "cmd.exe";
            flashSTM.StartInfo.Arguments = $"/c \"\"{ProgrammerPath}\\STM32_Programmer_CLI.exe\" -c port={port} sn={sn} mode=HOTPLUG freq=4000 speed=Reliable -w {binPathQuoted} {address} -halt\"";
            flashSTM.StartInfo.RedirectStandardOutput = true;
            flashSTM.StartInfo.UseShellExecute = false;
            flashSTM.StartInfo.CreateNoWindow = true;
            //flashSTM.OutputDataReceived += flashSTM_OutputDataReceived;

            flashSTM.Start();
            flashSTM.WaitForExit();
            string output = flashSTM.StandardOutput.ReadToEnd();
            flashSTM.Close();
            return output;
        }

        public static void flashStm32ProgressBar(string stmFile, string address, string port, string sn, ProgressBar pb, TextBox tb)
        {
            usedProgressBar = pb;
            usedTextBox = tb;
            gui.flashProcessRunning++;
            //string batchFilePath = @"STM32L072batch.bat";
            //string batchFilePathQuoted = "\"" + batchFilePath + "\"";
            //Console.WriteLine(batchFilePathQuoted);

            string binPath = stmFile;
            string binPathQuoted = "\"" + binPath + "\"";
            Console.WriteLine("binPathQouted: " + binPathQuoted);

            address = "0x" + address;

            Process flashSTM = new Process();
            flashSTM.StartInfo.FileName = "cmd.exe";
            flashSTM.StartInfo.Arguments = $"/c \"\"{ProgrammerPath}\\STM32_Programmer_CLI.exe\" -c port={port} sn={sn} mode=HOTPLUG freq=4000 speed=Reliable -w {binPathQuoted} {address} -halt\"";
            flashSTM.StartInfo.RedirectStandardOutput = true;
            flashSTM.StartInfo.UseShellExecute = false;
            flashSTM.StartInfo.CreateNoWindow = true;
            flashSTM.OutputDataReceived += flashSTM_OutputDataReceived;

            flashSTM.Start();
            flashSTM.BeginOutputReadLine();
            flashSTM.WaitForExit();
            gui.UpdateProgressBar(usedProgressBar, 100);
        }

        

        public static void flashStm32ProgressBar(string stmFile, string address, string port, string sn, ToolStripProgressBar pb, TextBox tb)
        {
            usedBarProgressBar = pb;
            usedTextBox = tb;
            gui.flashProcessRunning++;
            //string batchFilePath = @"STM32L072batch.bat";
            //string batchFilePathQuoted = "\"" + batchFilePath + "\"";
            //Console.WriteLine(batchFilePathQuoted);

            string binPath = stmFile;
            string binPathQuoted = "\"" + binPath + "\"";
            Console.WriteLine("binPathQouted: " + binPathQuoted);

            address = "0x" + address;

            Process flashSTM = new Process();
            flashSTM.StartInfo.FileName = "cmd.exe";
            //flashSTM.StartInfo.Arguments = $"/c \"{batchFilePathQuoted} {port} {sn} {binPathQuoted} {address}\"";
            flashSTM.StartInfo.Arguments = $"/c \"\"{ProgrammerPath}\\STM32_Programmer_CLI.exe\" -c port={port} sn={sn} mode=HOTPLUG freq=4000 speed=Reliable -w {binPathQuoted} {address} -halt\"";
            flashSTM.StartInfo.RedirectStandardOutput = true;
            flashSTM.StartInfo.UseShellExecute = false;
            flashSTM.StartInfo.CreateNoWindow = true;
            flashSTM.OutputDataReceived += flashSTM_OutputDataReceived;

            flashSTM.Start();
            flashSTM.BeginOutputReadLine();
            flashSTM.WaitForExit();
            gui.UpdateProgressBar(usedBarProgressBar, 100);
        }

        private static void flashSTM_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
            if (e.Data != null)
            {
                string line = General.NormalizeConsoleGlyphs(e.Data).Replace("\r", "");
             
                gui.UpdateTextBox(usedTextBox, line);
                if (line.Contains("File download complete"))
                {
                    gui.flashProcessRunning--;
                    //if (gui.flashProcessRunning == 0)
                    //{
                        //General.messageBoxLed();
                    //}
                }
                else if (line.Contains("Error:"))
                {
                    gui.flashProcessRunning--;
                    MessageBox.Show(e.Data, "STM programming error", MessageBoxButtons.OK ,MessageBoxIcon.Error);
                }
                int progress = General.parseProgressFromString(e.Data);
                if (progress > 0)
                {
                    if (usedProgressBar == null)
                    {
                        gui.UpdateProgressBar(usedBarProgressBar, progress);
                    }
                    else
                    {
                        gui.UpdateProgressBar(usedProgressBar, progress);
                    }
                }
            }
        }

        public static void resetSTM(string port, string sn)
        {
            Process flashSTM = new Process();
            flashSTM.StartInfo.FileName = "cmd.exe";
            flashSTM.StartInfo.Arguments = $"/c \"\"{ProgrammerPath}\\STM32_Programmer_CLI.exe\" -c port={port} sn={sn} mode=HOTPLUG freq=8000 -hardRST\"";
            flashSTM.StartInfo.RedirectStandardOutput = true;
            flashSTM.StartInfo.UseShellExecute = false;
            flashSTM.StartInfo.CreateNoWindow = true;
            flashSTM.OutputDataReceived += flashSTM_OutputDataReceived;

            flashSTM.Start();
            flashSTM.BeginOutputReadLine();
            flashSTM.WaitForExit();
            gui.UpdateProgressBar(usedBarProgressBar, 100);
        }

        public static void programmEepromSTM32(string port, string sn, UInt32 startaddr, byte[] data, UInt16 size, ProgressBar pb, TextBox tb)
        {
            usedTextBox = tb;
            usedProgressBar = pb;
            string eepromCmd = $" -w8 0x{startaddr:X}";
            byte[] name = new byte[30];
            Array.Copy(data, name, data.Length);
            for (int i = 0; i<size; i++)
            {
                eepromCmd = eepromCmd + $" 0x{name[i]:X}";
            }
            Process flashSTM = new Process();
            flashSTM.StartInfo.FileName = "cmd.exe";
            flashSTM.StartInfo.Arguments = $"/c \"\"{ProgrammerPath}\\STM32_Programmer_CLI.exe\" -c port={port} sn={sn} {eepromCmd}\"";
            flashSTM.StartInfo.RedirectStandardOutput = true;
            flashSTM.StartInfo.UseShellExecute = false;
            flashSTM.StartInfo.CreateNoWindow = true;
            flashSTM.OutputDataReceived += flashSTM_OutputDataReceived;

            //flashSTM.Start();
            //flashSTM.WaitForExit();
            //string output = flashSTM.StandardOutput.ReadToEnd();
            //flashSTM.Close();
            flashSTM.Start();
            flashSTM.BeginOutputReadLine();
            flashSTM.WaitForExit();

            gui.UpdateProgressBar(usedProgressBar, 100);
        }

        public static List<StmDevice> getStmDevices(string stmInterface) //List<StLinkProbes>
        {
            Process flashSTM = new Process();
            flashSTM.StartInfo.FileName = "cmd.exe";
            flashSTM.StartInfo.Arguments = $"/c \"{ProgrammerPath}\\STM32_Programmer_CLI.exe\" -l {stmInterface}";
            flashSTM.StartInfo.RedirectStandardOutput = true;
            flashSTM.StartInfo.UseShellExecute = false;
            flashSTM.StartInfo.CreateNoWindow = true;
            //flashSTM.OutputDataReceived += flashSTM_OutputDataReceivedProbes;

            flashSTM.Start();
            flashSTM.WaitForExit();
            string output = flashSTM.StandardOutput.ReadToEnd();
            //flashSTM.BeginOutputReadLine();
            //flashSTM.Close();
            List<StmDevice> probes = parseStLinkProbes(output);

            return (probes);
        }

        private static List<StmDevice> parseStLinkProbes(string input)
        {
            bool dfuDevice = false;
            bool stlinkDevice = false;
            string[] lines = input.Split(new[] { '\r', '\n', }, StringSplitOptions.RemoveEmptyEntries);
            List<StmDevice> probes = new List<StmDevice>();

            StmDevice probe = new StmDevice();
            foreach (string line in lines)
            {
                if (probe == null)
                {
                    probe = new StmDevice(); // Maak een nieuwe probe aan
                }

                if (line.Contains("DFU Interface") || dfuDevice == true)
                {
                    dfuDevice = true;
                    if (line.StartsWith(DFU_SN))
                    {
                        probe.SN = line.Substring(DFU_SN.Length).Trim();
                    }
                    if (line.StartsWith(DFU_FW))
                    {
                        probe.FW = line.Substring(DFU_FW.Length).Trim();
                    }
                    if (line.StartsWith(DFU_DeviceIndex))
                    {
                        probe.DeviceIndex = line.Substring(DFU_DeviceIndex.Length).Trim();
                    }
                    if (line.StartsWith(DFU_ProductID))
                    {
                        probe.ProductID = line.Substring(DFU_ProductID.Length).Trim();
                    }

                    if (probe.FW != "")
                    {
                        probes.Add(probe);
                        probe = null;
                    }
                }
                else if (line.Contains("STLink Interface") || stlinkDevice == true)
                {
                    stlinkDevice = true;
                    if (line.StartsWith(StLink_SN))
                    {
                        probe.SN = line.Substring(StLink_SN.Length).Trim();
                    }
                    if (line.StartsWith(StLink_FW))
                    {
                        probe.FW = line.Substring(StLink_FW.Length).Trim();
                    }
                    if (line.StartsWith(StLink_Index))
                    {
                        probe.Index = Convert.ToInt32(line.Substring(StLink_Index.Length).Trim());
                    }
                    if (line.StartsWith(StLink_BoardName))
                    {
                        probe.BoardName = line.Substring(StLink_BoardName.Length).Trim();
                    }

                    if (probe.BoardName != "")
                    {
                        probes.Add(probe);
                        probe = null;
                    }
                }
            }
            return probes;
        }

        //static public void flashSTM_OutputDataReceivedProbes(object sender, DataReceivedEventArgs e)
        //{
        //    temp += e.Data;
        //    Console.WriteLine(e.Data);
        //}

    }


    public class StmDevice
    {
        public int Index = 0;
        public string SN = "";
        public string FW = "";
        public string BoardName = "";
        public string ProductID = "";
        public string DeviceIndex = "";

        public void Clear()
        {
            Index = 0;
            SN = "";
            FW = "";
            BoardName = "";
            ProductID = "";
            DeviceIndex = "";
        }
    }

}
