using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;

namespace NMODBUS4_DEMO
{
  public static  class GlobalVariable
    {
        /// <summary>
        /// 读取excell配置线程
        /// </summary>
        public static  Thread TH_dt = null;
        /// <summary>
        /// 抓取读取状态
        /// </summary>
        public static Thread TH_LIST_MSG = null;

        public static Thread TH_Master_Read = null;
        /// <summary>
        /// 写多线圈状态
        /// </summary>
        public static bool[] coilsBuffer ;
        /// <summary>
        /// 寄存器数组
        /// </summary>
        public static ushort[] registerBuffer;
     
        public static StatusModes StatusModes = new StatusModes();
        public enum MSGTYPE
        {
            ERROR,
            OK,
            WARING

        }
        /// <summary>
        /// 0代表OK
        /// </summary>
        public static int OK =0;
        /// <summary>
        /// 警告代表1
        /// </summary>
        public static int WARNING = 1;
        /// <summary>
        /// 2错误代表
        /// </summary>
        public static int ERROR = 2;
        public  class msg

        {
            public  int Msg_ID;
            public string slave_value;
            public int type;
            public  string time { get { return DateTime.Now.ToString(); } }

           
           
            


         }

        public static msg MSG = new msg();
        public static string Read_Value = string.Empty;
        public static System.Collections.ObjectModel.ObservableCollection<msg> MSGLIST = new System.Collections.ObjectModel.ObservableCollection<msg>();
        public static System.Collections.ObjectModel.ObservableCollection<string> MSGLIST_ = new System.Collections.ObjectModel.ObservableCollection<string>();

        /// <summary>
        /// 界面绑定传消息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public static void MSG_THOD(int id, string value, int type)
        {
            GlobalVariable.MSG = new GlobalVariable.msg { Msg_ID = id, slave_value = value, type = type };
            GlobalVariable.MSGLIST.Add(GlobalVariable.MSG);
            Read_Value = value;

        }
        public static Mdbapi Mdapi = new Mdbapi();


        /// <summary>
        /// 读取和写入文件地址
        /// </summary>
        public static string Read_Write = @"\Read&Write\PLC读写指令.xlsx";

        public static PLC_CON_Prm plc_con_prm = new PLC_CON_Prm();
        public static Modbusoperate modbusoperate = new Modbusoperate();
        public static DataTable dt=null;
        public static PLC_RW_Prm plc_rw_prm = new PLC_RW_Prm();

        /// <summary>
        /// 读取线程
        /// </summary>
        public static bool conn_flag = false;




    }

    
}
