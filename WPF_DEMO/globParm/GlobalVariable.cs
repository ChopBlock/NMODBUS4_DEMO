using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using Modbus.Device;
using System.Net.Sockets;
using System.Data;
using System.Collections.ObjectModel;
using System.Threading;


namespace WPF_DEMO
{
  public static  class GlobalVariable
    {
        public static Con_APIFrm Con_APIFrm{ get{ return new Con_APIFrm();}}

        public static ModbusOperate modbusOperate { get { return new ModbusOperate(); } }

        public static SerialPort Port;


        /// <summary>
        /// 端口
        /// </summary>
        public static string PortName;
        /// <summary>
        /// 波特率
        /// </summary>
        public static int Baud;
        /// <summary>
        /// 数据位
        /// </summary>
        public static int Date_bit;
        /// <summary>
        /// 校验位
        /// </summary>
        public static Parity Parity;
        /// <summary>
        /// 停止位
        /// </summary>
        public static StopBits stopBits;

        /// <summary>
        /// Modbus 实例
        /// </summary>
        public static ModbusMaster Master;


        /// <summary>
        /// IP地址
        /// </summary>
        public static string IP;

        /// <summary>
        /// 端口号
        /// </summary>
        public static int port_no;


        public static TcpClient tcpClient;

        /// <summary>
        /// 0代表serial 1代表TCP/IP
        /// </summary>
        public static int Mod;


        /// <summary>
        /// Modbus 发送数据表格
        /// </summary>
        public static DataTable Modbus_dt =new DataTable();

        /// <summary>
        /// Modbus文件读取地址
        /// </summary>
        public static string Excell_pth = @"\Read&Write\PLC读写指令.xlsx";

        /// <summary>
        ///excell 转换成dt
        /// </summary>
        public static Task Task_convrt;

        /// <summary>
        /// dt转换成DG
        /// </summary>
        public static Task<bool> Task_dt;


        /// <summary>
        /// 线圈
        /// </summary>
        public static bool[] coilsBuffer;
        /// <summary>
        /// 寄存器
        /// </summary>
        public static ushort[] registerBuffer;

        
        /// <summary>
      /// 从机ID号
      /// </summary>
        public static byte slaveAddress;
        /// <summary>
        /// 起始地址
        /// </summary>
        public static ushort startAddress;

        /// <summary>
        /// 创建listview 数据绑定实例
        /// </summary>
        public static ObservableCollection<msg> Message = new ObservableCollection<msg>();

        public static ObservableCollection<Excell_Header> Excell_Coll = new ObservableCollection<Excell_Header>();
        public  class msg {

            public  int No { get; set; }
            public  string Time { get { return DateTime.Now.ToString(); } }
            public  string Value { get; set; }
        }

        public class Excell_Header {

            public string NO { get; set; }
            public string SLAVE_ID { get; set; }
            public string FUNCTION { get; set; }
            public string STRATADDRESS { get; set; }
            public string TYPE { get; set; }
            public string LENTH { get; set; }
            public string ARRAY { get; set; }
        }

        /// <summary>
        /// 连接状态
        /// </summary>
        /// 

        public static bool Connected = false;

        public static bool Bt_Cancel = false;

        public static int List_index = 0;


        public static ProgressFrm PrgressFrm { get { return new ProgressFrm(); } }


        public static statul_model stmod = new statul_model();


        public static Task Excell_Task;
    }
}
