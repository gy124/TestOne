using System;
using System.Collections.Generic;
using System.Linq;
using Modbus.Device;
using System.IO.Ports;
using System.Threading;

namespace AZD
{
    /// <summary>
    /// 东方马达ADZ系列Modbus API
    /// </summary>
    public class Motor
    {
        public static SerialPort serialPort = new SerialPort();
        public static IModbusSerialMaster master = null;
        public static List<byte> ListAddress = new List<byte>();
        public byte Address;
        public int ErrorCount;
        private static readonly Object LockObj = new object();

        public class STATUS
        {
            public int Inport;
            public int Outport;
            public int enc_pos;
            public int tickcnt;
        }
        public STATUS status = new STATUS();

        public class RemoteIO
        {
            public int Inport;
            public int Outport;
            public int tickcnt;

            public bool isReady
            {
                get
                {
                    if ((Outport >> 8 & 1) == 1) return true;
                    else return false;
                }
            }
            public bool isMove
            {
                get
                {
                    if ((Outport >> 9 & 1) == 1) return true;
                    else return false;
                }
            }
            public bool isALM
            {
                get
                {
                    if ((Outport >> 6 & 1) == 1) return true;
                    else return false;
                }
            }
            public bool isHomeEnd
            {
                get
                {
                    if ((Outport >> 11 & 1) == 1) return true;
                    else return false;
                }
            }
            public bool isINPOS
            {
                get
                {
                    if ((Outport >> 7 & 1) == 1) return true;
                    else return false;
                }
            }
            public bool isStart
            {
                get
                {
                    if ((Inport >> 8 & 1) == 1) return true;
                    else return false;
                }
                set
                {
                    Inport = Inport & (0x01 << 8) | (value ? 1 : 0);
                }
            }
            public bool isFWSLS
            {
                get
                {
                    if ((Outport >> 13 & 1) == 1) return true;
                    else return false;
                }
            }
            public bool isRVSLS
            {
                get
                {
                    if ((Outport >> 14 & 1) == 1) return true;
                    else return false;
                }
            }
            public bool isSVRON
            {
                get
                {
                    if ((Outport >> 10 & 1) == 1) return true;
                    else return false;
                }
            }
            public bool isHomeing
            {
                get
                {
                    if ((Outport >> 15 & 1) == 1 && isMove && !isHomeEnd) return true;
                    else return false;
                }
            }
            public int Msel
            {
                get
                {
                    return (Inport >> 6 & 0x03);
                }
                set
                {
                    Inport = Inport & (0x03 << 6) | value;
                }
            }            
        }
        public RemoteIO remoteio = new RemoteIO();

        public Motor(byte Address)
        {
            this.Address = Address;
        }
        public static int InitRs485(string PortName, int BaudRate =9600)
        {
            if (serialPort == null) return -1;            

            //open port
            if (!serialPort.IsOpen)
            {
                if (!PortName.ToUpper().Contains("COM") || BaudRate == 0) return -2;
                try
                {
                    serialPort.PortName = PortName;
                    serialPort.BaudRate = BaudRate;
                    serialPort.StopBits = StopBits.One;
                    serialPort.Parity = Parity.Even;
                    serialPort.Open();
                }
                catch (Exception ex)
                {
                    //throw ex;
                    return -1;
                }
            }
            //create rtu
            if (serialPort.IsOpen && (master == null || master.Transport == null))
            {               
                try
                {
                    master = ModbusSerialMaster.CreateRtu(serialPort);
                    master.Transport.ReadTimeout = 50;
                    master.Transport.WriteTimeout = 50;
                    master.Transport.Retries = 3;
                    master.Transport.WaitToRetryMilliseconds = 10;
                }
                catch (Exception ex)
                {
                    return -2;
                    //throw ex;                    
                }
            }
            return 0;
        }
        public static bool isRs485Ready
        {
            get
            {
                if (master == null || serialPort == null || !serialPort.IsOpen) return false;
                return true;
            }
        }
        public static bool Close()
        {
            try
            {
                if (serialPort!=null && serialPort.IsOpen) serialPort.Close();
                if (master != null) master.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        public bool isMasterReady
        {
            get
            {
                if (serialPort == null || !serialPort.IsOpen || master == null) return false;
                if (Address < 1 || Address > 254 || ErrorCount >3) return false;
                return true;
            }
        }
        public bool UpdateSatus(bool bReflash = false)
        {
            if (!isMasterReady) return false;
            //lock (LockObj)
            if (Monitor.TryEnter(LockObj, 5000))
            {
                try
                {
                    //check buffer
                    if (bReflash == false && Math.Abs(status.tickcnt - Environment.TickCount) < 50)
                    {
                        Monitor.Exit(LockObj);
                        return true;
                    }

                    //get status
                    ushort[] dat = master.ReadHoldingRegisters(Address, 204, 213 - 204 + 1);
                    if (dat.Length == 0)
                    {
                        if (ListAddress.Contains(Address)) ListAddress.Remove(Address);
                        Monitor.Exit(LockObj);
                        return false;
                    }
                    else
                    {
                        if (!ListAddress.Contains(Address)) ListAddress.Add(Address);
                    }

                    //pos
                    status.enc_pos = dat[204 - 204] << 16 | dat[205 - 204];

                    //read direct input@213/output@212
                    status.Outport = dat[212 - 204];
                    status.Inport = dat[213 - 204];

                    //首次读取以直接输出状态更新远程IO
                    if (status.tickcnt == 0 && (status.Outport & 0x3F) != (remoteio.Inport & 0x3F))
                    {
                        remoteio.Inport = (remoteio.Inport & 0x3F) | (status.Outport & 0x3F);
                    }
                    status.tickcnt = Environment.TickCount;
                    ErrorCount = 0;
                    Monitor.Exit(LockObj);
                    return true;
                }
                catch (Exception ex)
                {
                    Monitor.Exit(LockObj);
                    ErrorCount++;
                    return false;
                    //throw ex;
                }
            }
            return false;
        }
        public bool UpdateRemoteIO(bool bReflash = false)
        {
            //check
            if (!isMasterReady) return false;

            //lock (LockObj)
            if (Monitor.TryEnter(LockObj, 5000))
            {

                //check buffer
                if (bReflash == false && Math.Abs(remoteio.tickcnt - Environment.TickCount) < 50)
                {
                    Monitor.Exit(LockObj);
                    return true;
                }
                //remote io/status
                try
                {
                    ushort[] dat = master.ReadHoldingRegisters(Address, 124, 4);
                    remoteio.Inport = dat[0] << 16 | dat[1];
                    remoteio.Outport = dat[2] << 16 | dat[3];
                    remoteio.tickcnt = Environment.TickCount;
                    ErrorCount = 0;
                    Monitor.Exit(LockObj);
                    return true;
                }
                catch
                {
                    Monitor.Exit(LockObj);
                    ErrorCount++;
                    return false;
                }
            }
            return false;
        }
        public bool WriteRemoteIO()
        {
            ErrorCount = 0;
            //check
            if (!isMasterReady) return false;

            //lock (LockObj)
            if (Monitor.TryEnter(LockObj, 5000))
            {
                //set remote status
                try
                {
                    ushort[] dat = new ushort[4] { (ushort)(remoteio.Inport >> 16), (ushort)(remoteio.Inport & 0xFFFF), (ushort)(remoteio.Outport >> 16), (ushort)(remoteio.Outport & 0xFFFF) };
                    master.WriteMultipleRegisters(Address, 124, dat);
                    ErrorCount = 0;
                    Monitor.Exit(LockObj);
                    return true;
                }
                catch(Exception ex)
                {
                    ErrorCount++;
                    Monitor.Exit(LockObj);
                    return false;
                }
            }
            return false;
        }
        public bool WriteReg(ushort RegAddr,ushort[] dat)
        {
            ErrorCount = 0;
            //check
            if (!isMasterReady) return false;
            //lock (LockObj)
            if (Monitor.TryEnter(LockObj, 5000))
            {
                //set remote status
                try
                {
                    master.WriteMultipleRegisters(Address, RegAddr, dat);
                    ErrorCount = 0;
                    Monitor.Exit(LockObj);
                    return true;
                }
                catch (Exception ex)
                {
                    ErrorCount++;
                    Monitor.Exit(LockObj);
                    return false;
                }
            }
            return false;
        }
        public bool ReadReg(ushort RegAddr, ushort[] dat)
        {
            //check
            if (!isMasterReady) return false;

            //lock (LockObj)
            if (Monitor.TryEnter(LockObj, 5000))
            {
                try
                {
                    ushort len = (ushort)dat.Count();
                    if (len <= 0) return false;
                    dat = master.ReadHoldingRegisters(Address, RegAddr, len);
                    Monitor.Exit(LockObj);
                    return true;
                }
                catch
                {
                    ErrorCount++;
                    Monitor.Exit(LockObj);
                    return false;
                }
            }
            return false;
        }
        public bool Stop()
        {
            //stop
            remoteio.Inport = remoteio.Inport  | 1 << 9;
            if (!WriteRemoteIO()) return false;

            Thread.Sleep(100);

            //reset stopo
            remoteio.Inport = remoteio.Inport & (~(1 << 9));
            if (!WriteRemoteIO()) return false;

            return true;
        }
        public bool Free()
        {
            remoteio.Inport = remoteio.Inport & (~(1 << 11)) | 1 << 10;
            if (!WriteRemoteIO()) return false;
            return true;
        }
        public bool CurrentON
        {
            get
            {
                UpdateRemoteIO();
                return (remoteio.Inport & (1 << 10)) == (1 << 10);
            }
            set
            {
                remoteio.Inport = remoteio.Inport & (~(1 << 10));
                WriteRemoteIO();
            }
        }
        public bool CurrentOn()
        {
            remoteio.Inport = remoteio.Inport | (1 << 10);
            if (!WriteRemoteIO()) return false;
            return true;
        }
        public bool SvrOn()
        {
            remoteio.Inport = remoteio.Inport & (~(1 << 10)) | 1 << 11;
            if (!WriteRemoteIO())
                return false;
            return true;
        }
        public bool MoveToSelDat(ref bool bquit, byte sel, int timeout =3000)
        {
            //check alm and ready
            UpdateRemoteIO();
            if (remoteio.isALM || !remoteio.isReady)
            {
                //reset alm
                remoteio.Inport = remoteio.Inport & (0x40FF) | (0x01 << 14) | (0x01 << 11);
                if (!WriteRemoteIO()) return false;
                Thread.Sleep(500);

                UpdateRemoteIO(true);
                if (remoteio.isALM || !remoteio.isReady)
                    return false;
            }

            //resetremoteio.isALM
            remoteio.Inport = remoteio.Inport & (0x40FF) | (0x01 << 14) | (0x01 << 11);
            //select
            remoteio.Inport = remoteio.Inport & (~(0x03 << 6)) | sel<<6;
            if (!WriteRemoteIO()) return false;

            //start
            remoteio.Inport = remoteio.Inport & (~(0x01 << 14)) | (0x01 << 8);
            if (!WriteRemoteIO()) return false;

            //reset start
            Thread.Sleep(100);
            remoteio.Inport = remoteio.Inport & (~(0x01 << 8));
            if (!WriteRemoteIO()) return false;

            //wait
            Thread.Sleep(100);
            while (timeout>0)
            {
                Thread.Sleep(100);
                if (bquit) return false;
                UpdateRemoteIO();
                if (!remoteio.isMove)
                {
                    //check
                    UpdateRemoteIO();
                    if (remoteio.isINPOS && !remoteio.isALM) return true;
                    else return false;
                }                
                timeout -= 100;
                if (timeout <= 0)
                    return false;
            }
            return true;            
        }
        public bool Home(ref bool bquit, int timeout = 10000)
        {
            //reset alm
            UpdateRemoteIO(true);
            if (remoteio.isALM || !remoteio.isReady)
            {
                remoteio.Inport = remoteio.Inport & (0x40FF) | (0x01 << 14) | (0x01 << 11);
                if (!WriteRemoteIO())
                    return false;
                Thread.Sleep(1000);
                //check alm and ready
                UpdateRemoteIO(true);
                if (remoteio.isALM || !remoteio.isReady)
                    return false;
            }

            try
            {
                //home
                remoteio.Inport = remoteio.Inport & (~(0x01 << 14)) | (0x01 << 15);
                if (!WriteRemoteIO())
                    return false;

                //wait
                Thread.Sleep(500);
                while (timeout > 0)
                {
                    Thread.Sleep(100);
                    if (bquit)
                        return false;
                    UpdateRemoteIO();
                    if (remoteio.isHomeEnd && !remoteio.isALM)
                        return true;

                    timeout -= 100;
                    if (timeout <= 0)
                        return false;
                }
            }
            finally
            {
                //reset home
                remoteio.Inport = remoteio.Inport & (~(0x01 << 15));
                WriteRemoteIO();
            }
            return true;
        }
        public int GetDo(int num)
        {
            if (num < 0 || num > 5) return -1;
            UpdateSatus();
            return (status.Outport >> num) & 1;
        }
        public int GetDi(int num)
        {            
            if (num < 0 || num > 9) return -1;
            UpdateSatus();
            return (status.Inport >> num) & 1;
        }
        public int SetDo(int num, bool bon)
        {
            if (num < 0 || num > 5) return -1;
            remoteio.Inport = remoteio.Inport & (~(1 << num)) | (bon ? 1 << num : 0);
            if (!WriteRemoteIO()) return -2;
            return 0;
        }
        public int DoInvert(int num)
        {
            if (num < 0 || num > 5) return -1;
            UpdateRemoteIO();
            remoteio.Inport = remoteio.Inport & (~(1 << num)) | ((remoteio.Inport & (1 << num)) ==0 ? 1 << num : 0);
            if (!WriteRemoteIO()) return -2;
            return 0;
        }
    }
}
