﻿using System;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.IO.Ports;
using System.Threading;
using System.Management;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SerialPorstSample1
{
    public class SerialBytes
    {
        public byte[] Bytes;
        public DateTime DT = DateTime.Now;
    }
    public class SerialByte
    {
        public byte Byte;
        public DateTime DT = DateTime.Now;
    }
    public class StorageOfArrayByte
    {
        volatile Queue<SerialBytes> StorageArrayByte;
        public StorageOfArrayByte()
        {
            StorageArrayByte = new Queue<SerialBytes>();
        }
        public SerialBytes Data
        {
            get
            {
                lock (this)
                {
                    while (StorageArrayByte.Count == 0) Monitor.Wait(this);
                    return StorageArrayByte.Dequeue();
                }
            }
            set
            {
                lock (this)
                {
                    StorageArrayByte.Enqueue(value);
                    Monitor.PulseAll(this);
                }
            }

        }

        public int DataCount { get { return StorageArrayByte.Count; } }
        public void DataReallocate() { StorageArrayByte.TrimExcess(); }
        public void EmptyQueue() { StorageArrayByte.Clear(); }
    }

    /// <summary>
    /// //////////////////////////////////////////////////////////////
    /// </summary>
    public class StorageOfByte
    {
        volatile Queue<byte> StorageByte;
        public StorageOfByte()
        {
            StorageByte = new Queue<byte>();
        }
        public byte Data
        {
            get
            {
                lock (this)
                {
                    while (StorageByte.Count == 0) Monitor.Wait(this);
                    return StorageByte.Dequeue();
                }
            }
            set
            {
                lock (this)
                {
                    StorageByte.Enqueue(value);
                    Monitor.PulseAll(this);
                }
            }

        }

        public int DataCount { get { return StorageByte.Count; } }
        public void DataReallocate() { StorageByte.TrimExcess(); }
        public void EmptyQueue() { StorageByte.Clear(); }
    }
    /// <summary>
    /// //////////////////////////////////////////////////////////////
    /// </summary>

    public class StorageOfSerialByte
    {
        volatile Queue<SerialByte> StorageSerialByte;
        public StorageOfSerialByte()
        {
            StorageSerialByte = new Queue<SerialByte>();
        }
        public SerialByte Data
        {
            get
            {
                lock (this)
                {
                    while (StorageSerialByte.Count == 0) Monitor.Wait(this);
                    return StorageSerialByte.Dequeue();
                }
            }
            set
            {
                lock (this)
                {
                    StorageSerialByte.Enqueue(value);
                    Monitor.PulseAll(this);
                }
            }

        }

        public int DataCount { get { return StorageSerialByte.Count; } }
        public void DataReallocate() { StorageSerialByte.TrimExcess(); }
        public void EmptyQueue() { StorageSerialByte.Clear(); }
    }

    /// <summary>
    /// /////////////////////////////////////////////////////////////////////////////
    /// </summary>
    public class Communicator_SerialPort
    {
        const int BufferSize = 1000000000;
        SerialPort SerialPort_Com = null;
        private bool m_isopen = false;
        //-------------------------------
        public static bool StopThread = false;
        public static StorageOfArrayByte Q_SerialBuffer = new StorageOfArrayByte();
        public static StorageOfSerialByte Q_Byte = new StorageOfSerialByte();
        public static ObtainDataRate TotalBoudRate = new ObtainDataRate(100000, 2000);
        //-----------------

        public static byte[] recpacket;

        //****************************************
        private static Communicator_SerialPort instance;
        public static Communicator_SerialPort Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Communicator_SerialPort();
                }

                return instance;
            }
            set { instance = value; }
        }
        //***************************************
        public static List<string> GetAllPorts()
        {
            List<String> allPorts = new List<String>();
            foreach (String portName in System.IO.Ports.SerialPort.GetPortNames())
            {
                allPorts.Add(portName);
            }
            return allPorts;
        }

        public void Create_Thread_CommunicatorSerial(SerialPort SelSerialPort)
        {
            if (SerialPort_Com == null || !SerialPort_Com.IsOpen)
            {
                SerialPort_Com = new SerialPort(SelSerialPort.PortName, SelSerialPort.BaudRate, SelSerialPort.Parity, SelSerialPort.DataBits, SelSerialPort.StopBits);
                SerialPort_Com.ReadBufferSize = BufferSize;
                SerialPort_Com.WriteBufferSize = BufferSize;

                if (!SerialPort_Com.IsOpen) //if not open, open the port
                {
                    try
                    {
                        SerialPort_Com.Open();
                        m_isopen = true;

                        Thread ThreadCommunicatorSerialPort = new Thread(ReceivedData_SerialPort_Thread_Func);
                        ThreadCommunicatorSerialPort.Start();

                        Thread ThreadConvertByte = new Thread(ConvertByte_SerialPort_Thread_Func);
                        ThreadConvertByte.Start();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("This Port Is busy Please Select another One... ");
                    }

                }
            }
        }

        public bool IsOpen
        {
            get { return this.m_isopen; }
            set { this.m_isopen = value; }
        }

        public void Close()
        {
            this.m_isopen = false;
            if (SerialPort_Com != null)
            {
                SerialPort_Com.Dispose();
                GC.SuppressFinalize(SerialPort_Com);
                SerialPort_Com = null;
            }
        }

        //****************************************
        private void ReceivedData_SerialPort_Thread_Func()
        {
            int Count_BytesToRead = 0;
            byte[] buffer;
            while (!StopThread)
            {
                if (SerialPort_Com.IsOpen)
                {
                    Count_BytesToRead = SerialPort_Com.BytesToRead;
                }
                else
                {
                    IsOpen = false;
                }
                if (m_isopen && Count_BytesToRead > 0)
                {
                    buffer = new byte[Count_BytesToRead];
                    SerialPort_Com.Read(buffer, 0, Count_BytesToRead);

                    SerialBytes SB = new SerialBytes();
                    SB.Bytes = buffer;
                    Q_SerialBuffer.Data = SB;

                    TotalBoudRate.datetime = DateTime.Now;
                    TotalBoudRate.Data = Count_BytesToRead;
                }
                else
                {
                    Thread.Sleep(10);
                }
            }
        }
        //****************************************
        private void ConvertByte_SerialPort_Thread_Func()
        {
            int Count_Buffer = 0;
            SerialBytes SB = new SerialBytes();
            while (!StopThread)
            {
                Count_Buffer = Q_SerialBuffer.DataCount;
                if (Count_Buffer > 0)
                {
                    SB = Q_SerialBuffer.Data;

                    for (int i = 0; i < SB.Bytes.Length; i++)
                    {
                        SerialByte S = new SerialByte();
                        S.Byte = SB.Bytes[i];
                        S.DT = SB.DT;
                        Q_Byte.Data = S;
                    }
                }
                else
                {
                    Thread.Sleep(10);
                }
            }
        }
        //************************************
        public void SendPacket(byte[] Packet)
        {
            //send    
            if (SerialPort_Com != null && SerialPort_Com.IsOpen)
            {
                SerialPort_Com.Write(Packet, 0, Packet.Length);
            }
        }
    }
}
