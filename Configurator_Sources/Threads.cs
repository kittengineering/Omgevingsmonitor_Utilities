using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Omgevingsmonitor_configurator
{
    class Threads
    {
        Form1 gui = null;
        WoTS_Protocol protocol = null;
        const int TimeoutTime = 5000;
        
        byte[] txMessage;

        public Threads(Form1 form)
        {
            gui = form;
            protocol = new WoTS_Protocol(gui);
        }

        public void txSerialThread()
        {
            Stopwatch stopwatch = new Stopwatch();

            long elapsedTime = stopwatch.ElapsedMilliseconds;
            while (gui.SerialConfigPortOpen)
            {
                if (gui.SerialConfigPortFree) // && ((elapsedTime + TimeoutTime) > stopwatch.ElapsedMilliseconds))
                {
                    if (gui.TxQueue.Count != 0)
                    {
                        elapsedTime = stopwatch.ElapsedMilliseconds;
                        if (gui.TxQueue.TryDequeue(out txMessage))
                        {
                            
                            string text = Encoding.ASCII.GetString(txMessage);
                            Debug.WriteLine("txSerialThread: " + text);
                            Debug.WriteLine("txSerialThread: Message length: " + txMessage.Length);
                            gui.configPort.Write(txMessage, 0, txMessage.Length);
                            gui.SerialConfigPortFree = false;

                        }
                        
                    }
                }
            }
        }

        public void rxSerialThread() //ReceiveThread
        {
            //byte[] receiveBuffer = new byte[WoTS_Protocol.TOTAL_BUFFER_SIZE];
            bool preambleFound = false;
            Debug.WriteLine("rxSerialThread(): Thread Started!");

            while (gui.SerialConfigPortOpen)    //Should be keep running bool
            {
                Thread.Yield(); //Thread.Sleep(1);
                WoTS_Message rxMessage = new WoTS_Message();
                if (preambleFound == false)
                {
                    //Debug.WriteLine("Serialport.bytesToRead: " + configPort.BytesToRead);
                    if (gui.configPort.IsOpen)
                    {
                        try
                        {
                            if (gui.configPort.BytesToRead > 0)
                            {
                                //Debug.WriteLine();
                                gui.configPort.ReadTo(WoTS_Protocol.PREABMLE);
                                Debug.WriteLine("rxSerialThread(): Found a '#'");
                                preambleFound = true;
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Communication Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("rxSerialThread(): Preamble found");
                    int available = gui.configPort.BytesToRead;
                    if (available > (WoTS_Protocol.HEADER_SIZE-1))
                    {
                        rxMessage.receiveBuffer[0] = (byte)WoTS_Protocol.PREABMLE[0];
                        gui.configPort.Read(rxMessage.receiveBuffer, 1, (WoTS_Protocol.HEADER_SIZE-1));
                        rxMessage.cmd = rxMessage.receiveBuffer[1];
                        rxMessage.payloadLength = rxMessage.receiveBuffer[2];
                        gui.UpdateTextBox(gui.outputBox, $"received command: {rxMessage.cmd}\r\n");

                        while (gui.configPort.BytesToRead < (rxMessage.payloadLength + WoTS_Protocol.CRC_SIZE) && gui.SerialConfigPortOpen); //wait to receive the rest of the data
                        gui.configPort.Read(rxMessage.receiveBuffer, WoTS_Protocol.HEADER_SIZE, rxMessage.payloadLength + WoTS_Protocol.CRC_SIZE);
                        Array.Copy(rxMessage.receiveBuffer, rxMessage.payload, rxMessage.payloadLength);
                        rxMessage.CRC = (UInt16)(rxMessage.receiveBuffer[WoTS_Protocol.HEADER_SIZE + rxMessage.payloadLength] << 8);
                        rxMessage.CRC |= rxMessage.receiveBuffer[WoTS_Protocol.HEADER_SIZE + rxMessage.payloadLength + 1];
                        gui.RxQueue.Enqueue(rxMessage);
                        preambleFound = false;
                    }
                }
            }

            if (gui.configPort.IsOpen)
            {
                gui.configPort.Close();
            }
        }

        public void rxHandlingThread()
        {
            WoTS_Message rxMessage;
            while (gui.SerialConfigPortOpen)
            {
                //Debug.WriteLine("rxQueue count: " + rxQueue.Count);
                if (gui.RxQueue.TryDequeue(out rxMessage))
                {
                    if (rxMessage.CRC == WoTS_Protocol.CRC16_ARC(rxMessage.receiveBuffer, (UInt16)(rxMessage.payloadLength + WoTS_Protocol.HEADER_SIZE)))
                    {
                        if (compareArray(rxMessage.receiveBuffer, txMessage, rxMessage.payloadLength + WoTS_Protocol.HEADER_CRC_SIZE))
                        {
                            protocol.ProcessMessage(rxMessage);
                            gui.SerialConfigPortFree = true;
                            gui.UpdateTextBox(gui.outputBox, "Ready to send next message\r\n");

                        }
                        else
                        {
                            protocol.ProcessMessage(rxMessage);
                        }
                    }
                }
            }
        }

        private bool compareArray(byte[] arrayA, byte[] arrayB, int size)
        {
            if (arrayA.Length >= size && arrayB.Length >= size)
            {
                for (int i = 0; i < size; i++)
                {
                    if (arrayA[i] != arrayB[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

    }

    public class RX_MessageData
    {
        public byte[] receiveBuffer = new byte[WoTS_Protocol.TOTAL_BUFFER_SIZE];

        public byte cmd = 0;
        public byte payloadLength = 0;
        public byte[] payload = new byte[WoTS_Protocol.MAX_PAYLOAD_SIZE];
        public UInt16 CRC = 0;
    }

}
