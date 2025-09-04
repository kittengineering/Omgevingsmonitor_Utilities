using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Omgevingsmonitor_configurator
{
    class WoTS_Protocol
    {
        public const string PREABMLE = "#";
        public const int MAX_PAYLOAD_SIZE = 256;
        public const int HEADER_SIZE = 3;
        public const int CRC_SIZE = 2;
        public const int HEADER_CRC_SIZE = HEADER_SIZE + CRC_SIZE;
        public const int TOTAL_BUFFER_SIZE = (HEADER_SIZE + MAX_PAYLOAD_SIZE + CRC_SIZE);

        public enum Command
        {
            BoxConfig = 0,
            TempConfig = 1,
            HumidConfig = 2,
            NOxConfig = 3,
            VocIndexConfig = 4,
            dBaConfig = 5,
            dBcConfig = 6,
            PM2Config = 7,
            PM10Config = 8,
            BatVoltConfig = 9,
            SolVoltConfig = 10,
            ChargerStatConfig = 11,
            CustomNameConfig = 12,
            SSIDConfig = 13,
            PasswordConfig = 14,
            ClearConfig = 253,
            ClearEEprom = 254,
            Error = 255
        }

        Form1 gui = null;

        public WoTS_Protocol(Form1 form)
        {
            gui = form;
        }

        public static Byte[] Create_Message(byte cmd, Byte[] payload)
        {
            Byte[] message = new byte[HEADER_SIZE + payload.Length + CRC_SIZE];
            message[0] = (byte)PREABMLE[0];
            message[1] = cmd;
            message[2] = (byte)payload.Length;
            Array.Copy(payload, 0, message, 3, payload.Length);
            UInt16 calc_crc = CRC16_ARC(message, (UInt16)(HEADER_SIZE + payload.Length));
            message[HEADER_SIZE + payload.Length] = (byte)(calc_crc >> 8);
            message[HEADER_SIZE + payload.Length + 1] = (byte)(calc_crc & 0xFF);
            return message;
        }

        public void ProcessMessage(WoTS_Message message)
        {
            switch (message.cmd)
            {
                case (byte)WoTS_Protocol.Command.BoxConfig:

                    break;
                case (byte)WoTS_Protocol.Command.TempConfig:

                    break;
                case (byte)WoTS_Protocol.Command.HumidConfig:

                    break;
                case (byte)WoTS_Protocol.Command.NOxConfig:

                    break;
                case (byte)WoTS_Protocol.Command.VocIndexConfig:

                    break;
                case (byte)WoTS_Protocol.Command.dBaConfig:

                    break;
                case (byte)WoTS_Protocol.Command.dBcConfig:

                    break;
                case (byte)WoTS_Protocol.Command.PM2Config:

                    break;
                case (byte)WoTS_Protocol.Command.PM10Config:

                    break;
                case (byte)WoTS_Protocol.Command.BatVoltConfig:

                    break;
                case (byte)WoTS_Protocol.Command.SolVoltConfig:

                    break;
                case (byte)WoTS_Protocol.Command.ChargerStatConfig:

                    break;
                case (byte)WoTS_Protocol.Command.CustomNameConfig:

                    break;
                case (byte)WoTS_Protocol.Command.SSIDConfig:

                    break;
                case (byte)WoTS_Protocol.Command.PasswordConfig:

                    break;
                case (byte)WoTS_Protocol.Command.Error:
                    MessageBox.Show(Encoding.ASCII.GetString(message.payload), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gui.SerialConfigPortFree = true;
                    break;
            }
        }



        //public static bool Retrieve_Message(ref WoTS_Message message, Byte[] buffer)
        //{
        //    int index = FindPreamble(buffer);
        //    if (index >= 0)
        //    {
        //        ReadByte(buffer, ref index, out message.cmd);
        //        ReadByte(buffer, ref index, out message.payloadSize);
        //        Array.Copy(buffer, index, message.payload, 0, message.payloadSize);
        //        return true;
        //    }
        //    return false;
        //}

        //private static void ReadByte(byte[] buffer, ref int index, out byte dest)
        //{
        //    dest = buffer[index];
        //    index++;
        //}

        //private static int FindPreamble(Byte[] buffer)
        //{
        //    for (int i = 0; i < buffer.Length; i++)
        //    {
        //        if (buffer[i] == (byte)PREABMLE[0])
        //        {
        //            return i+1;                  //return
        //        }
        //    }
        //    return -1;
        //}

        public static UInt16 CRC16_ARC(Byte[] data, UInt16 size)
        {
            UInt16 crc = 0;

            for (int i = 0; i < size; i++)
            {
                crc ^= data[i];

                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            return crc;
        }
    }

    public class WoTS_Message
    {
        public byte[] receiveBuffer = new byte[WoTS_Protocol.TOTAL_BUFFER_SIZE];

        public byte cmd = 0;
        public byte payloadLength = 0;
        public byte[] payload = new byte[WoTS_Protocol.MAX_PAYLOAD_SIZE];
        public UInt16 CRC = 0;
    }
}
