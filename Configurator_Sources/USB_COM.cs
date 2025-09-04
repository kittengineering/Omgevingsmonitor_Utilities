using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Text.RegularExpressions;

namespace Omgevingsmonitor_configurator
{
    class USB_COM
    {
        public const string EspVID = "303A";
        public const string EspPID = "1001";

        public const string GadgetVID = "0483";
        public const string GadgetPID = "5740";

        public static List<string> ComPorts(string request_vid, string request_pid)
        {
            List<string> comPorts = new List<string>();
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"SELECT * FROM Win32_PnPEntity WHERE Caption LIKE '%(COM%'"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    string caption = obj["Caption"].ToString();
                    string deviceId = obj["DeviceID"].ToString();

                    // Gebruik een reguliere expressie om de VID en PID uit het DeviceID te halen
                    Match match = Regex.Match(deviceId, @"VID_([0-9A-F]{4})&PID_([0-9A-F]{4})", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        string vid = match.Groups[1].Value;
                        string pid = match.Groups[2].Value;

                        // Controleer of VID en PID overeenkomen met het doelapparaat
                        if (vid.Equals(request_vid, StringComparison.OrdinalIgnoreCase) && pid.Equals(request_pid, StringComparison.OrdinalIgnoreCase))
                        {
                            Match comPortMatch = Regex.Match(caption, @"\((COM\d+)\)");
                            if (comPortMatch.Success)
                            {
                                string comPort = comPortMatch.Groups[1].Value;
                                comPorts.Add(comPort);
                                Console.WriteLine($"Found device with VID: {vid} PID: {pid} - COM Port: {comPort}");
                            }
                        }
                    }
                }
            }
            return comPorts;
        }

    }
}
