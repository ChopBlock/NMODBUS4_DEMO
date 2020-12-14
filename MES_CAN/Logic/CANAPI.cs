using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MES_CAN
{
    internal class CANAPI
    {
        public static readonly int STATUS_ERR = 0;
        public static readonly int STATUS_OK = 1;

        [DllImport("CANApplication.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint FD_ClearBuffer(uint DeviceType, uint DeviceInd, uint CANInd);
        [DllImport("CANApplication.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint FD_CloseDevice(uint DeviceType, uint DeviceInd);
        [DllImport("CANApplication.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint FD_GetReceiveNum(uint DeviceType, uint DeviceInd, uint CANInd);
        [DllImport("CANApplication.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint FD_InitCAN(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_INIT_CONFIG pInitConfig);
        [DllImport("CANApplication.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint FD_OpenDevice(uint DeviceType, uint DeviceInd, uint Reserved);
        [DllImport("CANApplication.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint FD_ReadBoardInfo(uint DeviceType, uint DeviceInd, ref VCI_BOARD_INFO pInfo);
        [DllImport("CANApplication.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint FD_ReadErrInfo(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_ERR_INFO pErrInfo);
        [DllImport("CANApplication.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint FD_Receive(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_CAN_OBJ pReceive, uint Len, int WaitTime);
        [DllImport("CANApplication.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint FD_ResetCAN(uint DeviceType, uint DeviceInd, uint CANInd);
        [DllImport("CANApplication.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint FD_StartCAN(uint DeviceType, uint DeviceInd, uint CANInd);
        [DllImport("CANApplication.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint FD_Transmit(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_CAN_OBJ pSend, uint Len);

        [StructLayout(LayoutKind.Sequential)]
        public struct CHGDESIPANDPORT
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string szpwd;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string szdesip;
            public int desport;
        }

        public enum ErrorType
        {
            //CAN错误码
            ERR_CAN_OVERFLOW = 0x0001,	//CAN控制器内部FIFO溢出
            ERR_CAN_ERRALARM = 0x0002,	//CAN控制器错误报警
            ERR_CAN_PASSIVE = 0x0004,	//CAN控制器消极错误
            ERR_CAN_LOSE = 0x0008,	    //CAN控制器仲裁丢失
            ERR_CAN_BUSERR = 0x0010,	//CAN控制器总线错误

            //通用错误码
            ERR_DEVICEOPENED = 0x0100,	//设备已经打开
            ERR_DEVICEOPEN = 0x0200,	//打开设备错误
            ERR_DEVICENOTOPEN = 0x0400,	//设备没有打开
            ERR_BUFFEROVERFLOW = 0x0800,//缓冲区溢出
            ERR_DEVICENOTEXIST = 0x1000,//此设备不存在
            ERR_LOADKERNELDLL = 0x2000,	//装载动态库失败
            ERR_CMDFAILED = 0x4000,	    //执行命令失败错误码
            ERR_BUFFERCREATE = 0x8000	//内存不足

        }

        public enum PCIDeviceType
        {
            CAN232 = 3,
            USBCAN1 = 1,
            USBCAN2 = 2
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VCI_BOARD_INFO
        {
            public ushort hw_Version;
            public ushort fw_Version;
            public ushort dr_Version;
            public ushort in_Version;
            public ushort irq_Num;
            public byte can_Num;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string str_Serial_Num;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string str_hw_Type;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U2)]
            public ushort[] Reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VCI_CAN_OBJ
        {
            public uint ID;         //报文ID
            public uint TimeStamp;  //接收到信息帧时的时间标识，从CAN 控制器初始化开始计时
            public byte TimeFlag;   //是否使用时间标识，为1 时TimeStamp 有效，TimeFlag 和TimeStamp 只在此帧为接收帧时有意义
            public byte SendType;   //发送帧类型，在USBCAN II 设备中未启用该功能
            public byte RemoteFlag; //是否是远程帧
            public byte ExternFlag; //是否是扩展帧
            public byte DataLen;    //数据长度(<=8)，即Data 的长度
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.U1)]
            public byte[] Data;     //报文的数据
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.U1)]
            public byte[] Reserved; //系统保留
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VCI_CAN_STATUS
        {
            public byte ErrInterrupt;
            public byte regMode;
            public byte regStatus;
            public byte regALCapture;
            public byte regECCapture;
            public byte regEWLimit;
            public byte regRECounter;
            public byte regTECounter;
            public uint Reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VCI_ERR_INFO
        {
            public uint ErrCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] Passive_ErrData;
            public byte ArLost_ErrData;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VCI_INIT_CONFIG
        {
            public uint AccCode;    //验收码，在USBCAN II 设备中未启用该功能。
            public uint AccMask;    //屏蔽码，在USBCAN II 设备中未启用该功能。
            public uint Reserved;   //保留，在USBCAN II 设备中未启用该功能。
            public byte Filter;     //滤波方式，在USBCAN II 设备中未启用该功能。
            public uint BaudRate;   //CAN 总线波特率。
            public byte Mode;       //模式。在USBCAN II 设备中未启用该功能。
        }
    }
}
