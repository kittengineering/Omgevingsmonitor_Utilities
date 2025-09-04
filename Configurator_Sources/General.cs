using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Omgevingsmonitor_configurator
{
    class General
    {
        static Form1 gui = null;

        public General(Form1 form)
        {
            gui = form;
        }

        public static byte[] ConvertHexString(string hexString)
        {
            byte[] hexbytes = new byte[hexString.Length / 2];

            for (int i = 0; i < hexString.Length; i += 2)
            {
                hexbytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return hexbytes;
        }

        public static int parseProgressFromString(string input)
        {
            if (input != null)
            {
                int percentageIndex = input.IndexOf('%');
                if (percentageIndex > 0)
                {
                    int start = percentageIndex - 1;
                    while (start > 0 && char.IsDigit(input[start - 1]))
                        start--;

                    string progressString = input.Substring(start, percentageIndex - start);
                    int progress;
                    if (int.TryParse(progressString, out progress))
                    {
                        return progress;
                    }
                }
            }
            return -1;
        }

        public static bool messageBoxLed()
        {
            //foreach (StmDevices probe in gui.probes)
            //{
            //    STM_Programmer.resetSTM("swd", probe.SN);
            //}
            DialogResult result = MessageBox.Show("Are all LEDs Green?", "Finished programming!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                gui.UpdateProgressBar(gui.generalProgressBar, 0);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
