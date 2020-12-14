using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace MES_CAN_WPF
{
    public static class GlobalVariable
    {
        /// <summary>
        /// 连接界面
        /// </summary>
        public static ConDiolg ConDiolg { get {return new ConDiolg(); }  }
        public static CANOperate CANOperate = new CANOperate();
        public static StatusModel StatusModel = new StatusModel();

        public static Program_Dialog Program_Dialog = new Program_Dialog();

        internal static uint CANBaudRate;
        internal static int CANIndex;
       
        internal static int DeviceIndex;
        internal static int DeviceType = 2;

        /// <summary>
        /// 消息显示框
        /// </summary>
        public static   ObservableCollection<string> MSG = new ObservableCollection<string>();


        public static  void MSG_Show(string ms)
        {
            MSG.Add(DateTime.Now.ToString() + "\t" + ms);
        }



        public static string File_Path = string.Empty;

       
    }
}
