
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;


namespace MES_CAN
{
    public static class GlobalVariable
    {
        public static string BMSID = string.Empty;
        public static string file_path = string.Empty;
        internal static uint CANBaudRate;
        internal static int CANIndex;
        public static CANOperate CANOperate = new CANOperate();
        internal static int DeviceIndex;
        internal static int DeviceType = 2;
        public static DateTime dt;
        public static bool IsBCU = true;
        public static bool IsCheckOK = false;
        public static bool IsFindBMSData = true;
        public static bool IsNotCheckInsulation = true;
        public static bool IsReadBMSID = false;
        public static bool IsRealVersion = true;
        public static string MES_OPEN = "";
     
        public static string RegexStr = string.Empty;
        public static string Regexint = string.Empty;
        public static string Regex = string.Empty;
        public static StatusModel StatusModel = new StatusModel();
        public static int TimeOutValue = 15;
        public static bool VoltageCoeff;
        public static bool MES_PASS;
        public static bool sound_PASS;
        public static bool MES_COLOR = true;
        public static string[] HEAD=new string[30];
        public static bool debug = true;

        public static bool TL_OPEN = false;
        public static bool mes_con = false;
        public static string[] message=new string[30];
        public static string[] message_OPEN = new string[30];
        public static List<string> mess = new List<string>();
        public static List<string> reserver_value = new List<string>();
        public static List<string> now_reserver_value = new List<string>();
        public static string mes_read_value="";
        public static List<msg> ListStatus = new List<msg>();
        public class msg{

            public string MSG { get; set; }
            public string time { get; set; }
        }

        public static bool loop = true;
        public static string[] value;
        public static AutoResetEvent JM_TH = new AutoResetEvent(false);

        public static bool JM_TH_boo = false;
        public static string mes ="";
        public static string reserver ;
        public static string NOW_reserver;
        /// <summary>
        /// 开启报文
        /// </summary>
        public static string open_mod = "";

        public static bool conn = false;

        public static bool check_result = true;
        /// <summary>
        /// 更新路径地址
        /// </summary>
        public static string filepath = "上位机更新文件";     //fct更新文件  上位机更新文件

        public static int P = 0;
        public static int threadsleeptime = 1;
        public static string bit ;

        public static SoundPlay sp = new SoundPlay();
        
    }
}
