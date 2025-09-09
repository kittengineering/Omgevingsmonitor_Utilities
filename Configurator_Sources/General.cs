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

        public static string NormalizeConsoleGlyphs(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;

            // Meest voorkomende: CP850/437 0xDB → Unicode full block
            s = s.Replace('\u00DB', '\u2588'); // Û → █

            // (optioneel) Nog wat klassiekers:
            // CP437 176/177/178 (▒) varianten → Unicode shades
            s = s.Replace('\u00B0', '\u2591'); // ¬/° issues komen soms voor, maar vaak 176 = light shade
            s = s.Replace('\u00B1', '\u2592'); // ± → ▒ (medium shade) (afhankelijk van tool)
            s = s.Replace('\u00B2', '\u2593'); // ² → ▓ (dark shade)

            // Box-drawing fallback (soms verkeerd gedecodeerd als accenten):
            s = s.Replace('├', '├').Replace('─', '─').Replace('│', '│'); // no-op als ze al kloppen

            return s;
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
