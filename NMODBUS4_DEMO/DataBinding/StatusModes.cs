using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;
using Modbus.Device;

namespace NMODBUS4_DEMO
{
    public class StatusModes : NotificationObject
    {
     
        //======================= 串口的连接设置
        public  string com_parity = string.Empty;
        public  string com_stopBits = string.Empty;
        //=======================

        //=======================TCP/IP的连接设置

        //=======================
        /*连接界面的属性{
         * 连接模式
         * 1. (串口模式)=>{(portName, baudRate, parity, dataBits, stopBits}
         *                          //参数(分别为站号 1,起始地址2,长度 2)
         * 2. (TCP/IP模式)=>{  byte slaveAddress; ushort startAddress; ushort numberOfPoints; 
      
        }}*/



        private int _Modbus_Mode_s = 0;
        private ArrayList _Serial_ports = new ArrayList();
        #region 串口相关参数
        private string _portName = string.Empty;
        private int _baudRate =9600;

        private Parity _parity;
        private int _dataBits;
        private StopBits _stopBits;
        /// <summary>
        /// modbus模式 (按照com index排序)
        /// </summary>
        public int Modbus_Mode_s
        {
            get { return _Modbus_Mode_s; }
            set { this._Modbus_Mode_s = value;
                base.RaisePropertyChanged("Modbus_Mode_s");
            }

        }

        public string portName

        {
            get { return this._portName; }
            set { this._portName = value;
                base.RaisePropertyChanged("portName");
            }
        }
        /// <summary>
        /// 串口波特率
        /// </summary>
        public int baudRate
        {
            get { return this._baudRate; }
            set { this._baudRate = value;
                base.RaisePropertyChanged("baudRate");
}
        }
        /// <summary>
        /// 串口奇偶校验
        /// </summary>
        public Parity parity
        {
            get {
                switch (com_parity)
                {
                    case "奇":
                        this._parity = Parity.Odd;
                        break;
                    case "偶":
                        this._parity = Parity.Even;
                        break;
                    case "无":
                        this._parity = Parity.None;
                        break;
                    default:
                        break;
                }
                return this._parity;
            }
            set { this._parity = value;
                base.RaisePropertyChanged("parity");
            }
        }
        /// <summary>
        ///串口 数据位数
        /// </summary>
        public int dataBits
        {
            get { return this._dataBits; }
            set { this._dataBits = value;
                base.RaisePropertyChanged("dataBits");
            }
        }
        /// <summary>
        /// 串口停止位
        /// </summary>
        public StopBits stopBits
        {
            get
            {
                switch (com_stopBits)
                {
                    case "1":
                        this._stopBits = StopBits.One;
                        break;
                    case "2":
                        this._stopBits = StopBits.Two;
                        break;
                    default:
                        break;
                }
                return this._stopBits;
            }
            set { this._stopBits = value;
                base.RaisePropertyChanged("stopBits");
            }
        }
        #endregion
        #region TCP/IP相关参数
        private string _IP = string.Empty;
        private int _PORT = 0;
        /// <summary>
        /// TCP/IP模式的IP地址
        /// </summary>
        public string IP
        {
            get { return this._IP; }
            set { this._IP = value;
                base.RaisePropertyChanged("IP");
            }
        }
        /// <summary>
        /// TCP/IP模式端口号
        /// </summary>
        public int PORT
        {
            get { return this._PORT; }
            set { this._PORT = value;
                base.RaisePropertyChanged("PORT");
            }
        }

        #endregion
       
        #region   NModbus4 相关参数
        /// <summary>
        /// 写线圈
        /// </summary>
        public static bool[] coilsBuffer;
        /// <summary>
        /// 写寄存器数组
        /// </summary>
        public static ushort[] registerBuffer;
      
        private string _functionCode;  //功能码
        //参数(分别为站号,起始地址,长度)
        private byte _slaveAddress;
        private ushort _startAddress;
        private ushort _numberOfPoints;

        /// <summary>
        /// 从站的ID号
        /// </summary>
        public byte slaveAddress
        {
            get { return this._slaveAddress; }
            set { this._slaveAddress = value;
            base.RaisePropertyChanged("slaveAddress");
            }
        }
        
        
        /// <summary>
        /// 起始地址
        /// </summary>
        public ushort startAddress
        {
            get{ return this._startAddress; }
            set{ this._startAddress = value;
            base.RaisePropertyChanged("startAddress");
            }
        }


        /// <summary>
        /// 长度
        /// </summary>
        public ushort numberOfPoints
        {
            get { return this._numberOfPoints; }
            set { this._numberOfPoints = value;
            base.RaisePropertyChanged("numberOfPoints");
            }

        }
        #endregion


        private int _Maximum = 15;
        private int _Minimum = 0;
        private int _NowValue;
        public int Maximum
        {
            get
            {
                return this._Maximum;
            }
            set
            {
                this._Maximum = value;
                base.RaisePropertyChanged("Maximum");
            }
        }

        public int Minimum
        {
            get
            {
                return this._Minimum;
            }
            set
            {
                this._Minimum = value;
                base.RaisePropertyChanged("Minimum");
            }
        }

        public int NowValue
        {
            get
            {
                return this._NowValue;
            }
            set
            {
                this._NowValue = value;
                base.RaisePropertyChanged("NowValue");
            }
        }
    }
}
    

