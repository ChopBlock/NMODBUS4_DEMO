
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MES_CAN
{



    public class CANOperate
    {
        private ASCIIEncoding asciiEncoding = new ASCIIEncoding();
        private bool bStatus = false;
        public bool Connected;
        private int count = 0;
        private List<byte[]> DataList = new List<byte[]>();
        private DateTime dt = new DateTime();
        private DateTime dtTimeOut;
        private CANAPI.VCI_CAN_OBJ FrameData = new CANAPI.VCI_CAN_OBJ();
        private CANAPI.VCI_CAN_OBJ FrameData2 = new CANAPI.VCI_CAN_OBJ();
        private static CANAPI.VCI_INIT_CONFIG initConfig = new CANAPI.VCI_INIT_CONFIG();
        private CAN_RECV mw = new CAN_RECV();
        private List<byte[]> receiveDataList = new List<byte[]>();
        ////  private int SlaveIndex = 0;
        private Thread thMonitor = null;
        private Thread thSend = null;
        private Thread thSend2 = null;
        private Thread thSend3 = null;
        private Thread thSend4 = null;
        private Thread thSend5 = null;
        CAN_RECV cr = new CAN_RECV();
        CANAPI.VCI_CAN_OBJ[] JM_CAN = new CANAPI.VCI_CAN_OBJ[1];
        //  private Thread thSendDebugCode = null;
        /// <summary>
        /// 整除数
        /// </summary>
        private int s61_c = 0;
        /// <summary>
        /// 余数
        /// </summary>
        private int s61_y = 0;
        private string S = string.Empty;
        public void Close()
        {
            initConfig.BaudRate = GlobalVariable.CANBaudRate;
            if (this.Connected)
            {
                if (CANAPI.FD_InitCAN((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref initConfig) != CANAPI.STATUS_OK)
                {
                    CANAPI.FD_CloseDevice((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex);
                }
                CANAPI.FD_ResetCAN((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex);
                CANAPI.FD_CloseDevice((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex);
                this.Connected = false;
                GlobalVariable.conn = false;
                GlobalVariable.IsReadBMSID = false;
            }
        }

        public int Open()
        {
            //try
            {
                GlobalVariable.DeviceType = 2;
                initConfig.BaudRate = GlobalVariable.CANBaudRate;
                if (CANAPI.FD_OpenDevice((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, 0) != CANAPI.STATUS_OK)
                {
                    return 0;
                }
                if (CANAPI.FD_InitCAN((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref initConfig) != CANAPI.STATUS_OK)
                {
                    CANAPI.FD_CloseDevice((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex);
                    return 0;
                }
                if (CANAPI.FD_StartCAN((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex) != CANAPI.STATUS_OK)
                {
                    CANAPI.FD_CloseDevice((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex);
                    return 0;
                }
                this.Connected = true;
                GlobalVariable.conn = true;
                this.WriteIn();
                return 1;
            }
            //catch(Exception ex)
            //{
            //    this.Connected = false;
            //    GlobalVariable.StatusModel.ListStatus = ex.Message;
            //    return 0;
            //}
        }
     



        private void ReceiveDataProc()
        {
            try
            {

                CANAPI.VCI_ERR_INFO pErrInfo = new CANAPI.VCI_ERR_INFO
                {
                    Passive_ErrData = new byte[3]
                };
                CANAPI.VCI_CAN_OBJ[] vci_can_objArray = new CANAPI.VCI_CAN_OBJ[1];
                while (!GlobalVariable.mes_con)
                {
                    if (!this.Connected)
                    {
                        return;
                    }
                    int num = 0;
                    num = (int)CANAPI.FD_GetReceiveNum((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex);
                    if (CANAPI.FD_Receive((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref vci_can_objArray[0], 1, 200) <= 0)
                    {
                        CANAPI.FD_ReadErrInfo((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref pErrInfo);
                        if (GlobalVariable.IsFindBMSData && (this.dt.AddSeconds(1.0) < DateTime.Now))
                        {
                            int num2 = vci_can_objArray[0].Data[0];
                            //    this.mw.SetLogMsg("CAN总线上没有检测到设备 ...", true);
                            GlobalVariable.StatusModel.ListStatus = (DateTime.Now.ToString("HH:mm:ss") + "\t" + "CAN总线上没有检测到设备 ...");
                            //    GlobalVariable.StatusModel.BMSID = "000";
                            GlobalVariable.StatusModel.NowValue = 0;
                            GlobalVariable.StatusModel.C_NowSoftVer = "                                ";
                            GlobalVariable.StatusModel.C_NowRealSoftVer = "                                ";
                            GlobalVariable.StatusModel.C_NowHeadwearVer = "                    ";
                            GlobalVariable.StatusModel.C_NowRealHeadwearVer = "                    ";
                            GlobalVariable.StatusModel.C_NowInsulationVer = "                    ";
                            GlobalVariable.StatusModel.C_NowLinkVoltage = 0.0;
                            GlobalVariable.StatusModel.C_NowPackVoltage = 0.0;
                            GlobalVariable.StatusModel.CR_SOC = 0.0;
                            GlobalVariable.StatusModel.M_NowSoftVer = "                                ";
                            GlobalVariable.StatusModel.M_NowHeadWearVer = "                    ";
                            GlobalVariable.StatusModel.M_BMUID = "";
                            GlobalVariable.StatusModel.IsCheckResult = 0;
                            GlobalVariable.IsFindBMSData = false;
                            GlobalVariable.IsCheckOK = false;
                            GlobalVariable.MES_PASS = false;
                            GlobalVariable.IsReadBMSID = false;
                            GlobalVariable.StatusModel.M_jhl = "";
                            GlobalVariable.StatusModel.MES_MSG = "";
                        }
                    }
                    else if (vci_can_objArray[0].RemoteFlag == 0)
                    {
                        if (!GlobalVariable.IsFindBMSData)
                        {
                            this.dt = DateTime.Now;

                            GlobalVariable.StatusModel.ListStatus = (DateTime.Now.ToString("HH:mm:ss") + "\t" + "检测到总线上有设备接入 ...");
                            GlobalVariable.IsFindBMSData = true;
                            GlobalVariable.StatusModel.NowValue = 0;
                            this.dtTimeOut = DateTime.Now;
                        }
                        switch (vci_can_objArray[0].ID)
                        {
                            case 0x422:
                                GlobalVariable.StatusModel.CR_SOC = ((vci_can_objArray[0].Data[4] * 0x100) + vci_can_objArray[0].Data[5]) * 0.01;
                                break;

                            case 0x469:
                                GlobalVariable.StatusModel.CR_SOC = ((vci_can_objArray[0].Data[4] * 0x100) + vci_can_objArray[0].Data[5]) * 0.01;
                                break;

                            case 0x683:
                                #region 法规号
                                if (GlobalVariable.IsRealVersion)
                                {

                                    switch (vci_can_objArray[0].Data[0])
                                    {
                                        case 160:
                                            GlobalVariable.StatusModel.C_NowRealHeadwearVer = this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.C_NowRealHeadwearVer.Substring(7);
                                            break;

                                        case 0xa1:
                                            GlobalVariable.StatusModel.C_NowRealHeadwearVer = GlobalVariable.StatusModel.C_NowRealHeadwearVer.Substring(0, 7) + this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.C_NowRealHeadwearVer.Substring(14);
                                            break;

                                        case 0xa2:
                                            GlobalVariable.StatusModel.C_NowRealHeadwearVer = GlobalVariable.StatusModel.C_NowRealHeadwearVer.Substring(0, 14) + this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1).Substring(0, 6);
                                            break;

                                        case 0xb0:
                                            GlobalVariable.StatusModel.C_NowRealSoftVer = this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.C_NowRealSoftVer.Substring(7);
                                            break;

                                        case 0xb1:
                                            GlobalVariable.StatusModel.C_NowRealSoftVer = GlobalVariable.StatusModel.C_NowRealSoftVer.Substring(0, 7) + this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.C_NowRealSoftVer.Substring(14);
                                            break;

                                        case 0xb2:
                                            GlobalVariable.StatusModel.C_NowRealSoftVer = GlobalVariable.StatusModel.C_NowRealSoftVer.Substring(0, 14) + this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.C_NowRealSoftVer.Substring(0x15);
                                            break;
                                        case 0xb3:
                                            GlobalVariable.StatusModel.C_NowRealSoftVer = GlobalVariable.StatusModel.C_NowRealSoftVer.Substring(0, 0x15) + this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.C_NowRealSoftVer.Substring(0x1c);
                                            break;

                                        case 180:
                                            GlobalVariable.StatusModel.C_NowRealSoftVer = GlobalVariable.StatusModel.C_NowRealSoftVer.Substring(0, 0x1c) + this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1).Substring(0, 4);
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                #endregion
                                #region 软硬件版本号
                                switch (vci_can_objArray[0].Data[0])
                                {
                                    case 0xa0:
                                        GlobalVariable.StatusModel.C_NowHeadwearVer = this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.C_NowHeadwearVer.Substring(7);

                                        break;

                                    case 0xa1:
                                        GlobalVariable.StatusModel.C_NowHeadwearVer = GlobalVariable.StatusModel.C_NowHeadwearVer.Substring(0, 7) + this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.C_NowHeadwearVer.Substring(14);

                                        break;

                                    case 0xa2:
                                        GlobalVariable.StatusModel.C_NowHeadwearVer = GlobalVariable.StatusModel.C_NowHeadwearVer.Substring(0, 14) + this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1).Substring(0, 6);

                                        break;

                                    case 0xb0:
                                        GlobalVariable.StatusModel.C_NowSoftVer = this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.C_NowSoftVer.Substring(7);
                                        break;

                                    case 0xb1:
                                        GlobalVariable.StatusModel.C_NowSoftVer = GlobalVariable.StatusModel.C_NowSoftVer.Substring(0, 7) + this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.C_NowSoftVer.Substring(14);
                                        break;

                                    case 0xb2:
                                        GlobalVariable.StatusModel.C_NowSoftVer = GlobalVariable.StatusModel.C_NowSoftVer.Substring(0, 14) + this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.C_NowSoftVer.Substring(0x15);
                                        break;

                                    case 0xb3:
                                        GlobalVariable.StatusModel.C_NowSoftVer = GlobalVariable.StatusModel.C_NowSoftVer.Substring(0, 0x15) + this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.C_NowSoftVer.Substring(0x1c);
                                        break;

                                    case 180:
                                        GlobalVariable.StatusModel.C_NowSoftVer = GlobalVariable.StatusModel.C_NowSoftVer.Substring(0, 0x1c) + this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1).Substring(0, 4);
                                        break;
                                }
                                break;
                            #endregion
                                #region 699
                            case 0x699:

                                switch (vci_can_objArray[0].Data[0])
                                {
                                    case 0xa0:
                                        GlobalVariable.StatusModel.C_NowHeadwearVer = this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.C_NowHeadwearVer.Substring(7);

                                        break;

                                    case 0xa1:
                                        GlobalVariable.StatusModel.C_NowHeadwearVer = GlobalVariable.StatusModel.C_NowHeadwearVer.Substring(0, 7) + this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.C_NowHeadwearVer.Substring(14);
                                        break;

                                    case 0xa2:
                                        GlobalVariable.StatusModel.C_NowHeadwearVer = GlobalVariable.StatusModel.C_NowHeadwearVer.Substring(0, 14) + this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1).Substring(0, 6);
                                        break;

                                    case 0xb0:
                                        GlobalVariable.StatusModel.C_NowSoftVer = this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.C_NowSoftVer.Substring(7);
                                        break;

                                    case 0xb1:
                                        GlobalVariable.StatusModel.C_NowSoftVer = GlobalVariable.StatusModel.C_NowSoftVer.Substring(0, 7) + this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.C_NowSoftVer.Substring(14);
                                        break;

                                    case 0xb2:
                                        GlobalVariable.StatusModel.C_NowSoftVer = GlobalVariable.StatusModel.C_NowSoftVer.Substring(0, 14) + this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.C_NowSoftVer.Substring(0x15);
                                        break;

                                    case 0xb3:
                                        GlobalVariable.StatusModel.C_NowSoftVer = GlobalVariable.StatusModel.C_NowSoftVer.Substring(0, 0x15) + this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.C_NowSoftVer.Substring(0x1c);
                                        break;

                                    case 180:
                                        GlobalVariable.StatusModel.C_NowSoftVer = GlobalVariable.StatusModel.C_NowSoftVer.Substring(0, 0x1c) + this.asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1).Substring(0, 4);
                                        break;

                                    default:
                                        break;

                                }
                                break;
                            #endregion

                            case 0x18f101f3:
                                #region S0C
                                GlobalVariable.StatusModel.CR_SOC = vci_can_objArray[0].Data[0] * 0.4;
                                break;
                            #endregion


                            //菊花链
                            case 0x6C2:
                                GlobalVariable.StatusModel.M_jhl = vci_can_objArray[0].Data[0].ToString();
                                break;
                            case 0x6C1:
                                GlobalVariable.StatusModel.M_jhl = vci_can_objArray[0].Data[0].ToString();
                                break;
                            case 0x6C3:
                                GlobalVariable.StatusModel.M_jhl = vci_can_objArray[0].Data[0].ToString();
                                break;
                            case 0x6C4:
                                GlobalVariable.StatusModel.M_jhl = vci_can_objArray[0].Data[0].ToString();
                                break;
                            case 0x6C5:
                                GlobalVariable.StatusModel.M_jhl = vci_can_objArray[0].Data[0].ToString();
                                break;

                            //32s菊花链
                            case 0x0C32D200:
                                GlobalVariable.StatusModel.M_jhl = vci_can_objArray[0].Data[0].ToString();
                                break;

                            #region OLD版本号
                            case 0x6B1:
                            case 0x0C31D201:

                                #region 一号机
                                GlobalVariable.StatusModel.M_BMUID = "1";
                                switch (vci_can_objArray[0].Data[0])
                                {



                                    case 0xB0:
                                        GlobalVariable.StatusModel.M_NowSoftVer = asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.M_NowSoftVer.Substring(7);

                                        break;
                                    case 0xB1:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 7) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1)
                                            + GlobalVariable.StatusModel.M_NowSoftVer.Substring(14);

                                        break;
                                    case 0xB2:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 14) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1)
                                             + GlobalVariable.StatusModel.M_NowSoftVer.Substring(21);

                                        break;
                                    case 0xB3:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 21) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1)
                                            + GlobalVariable.StatusModel.M_NowSoftVer.Substring(28);

                                        break;
                                    case 0xB4:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 28) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1).Substring(0, 4);

                                        break;
                                    case 0xA0:
                                        GlobalVariable.StatusModel.M_NowHeadWearVer = asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(7);

                                        break;
                                    case 0xA1:
                                        GlobalVariable.StatusModel.M_NowHeadWearVer = GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(0, 7) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1)
                                            + GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(14);

                                        break;
                                    case 0xA2:
                                        GlobalVariable.StatusModel.M_NowHeadWearVer = GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(0, 14) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1).Substring(0, 6);

                                        break;

                                }
                                break;
                            #endregion
                            case 0xc31d202:
                            case 0x6b2:
                                #region 2号机
                                GlobalVariable.StatusModel.M_BMUID = "2";
                                switch (vci_can_objArray[0].Data[0])
                                {



                                    case 0xB0:
                                        GlobalVariable.StatusModel.M_NowSoftVer = asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.M_NowSoftVer.Substring(7);

                                        break;
                                    case 0xB1:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 7) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1)
                                            + GlobalVariable.StatusModel.M_NowSoftVer.Substring(14);

                                        break;
                                    case 0xB2:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 14) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1)
                                             + GlobalVariable.StatusModel.M_NowSoftVer.Substring(21);

                                        break;
                                    case 0xB3:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 21) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1)
                                            + GlobalVariable.StatusModel.M_NowSoftVer.Substring(28);

                                        break;
                                    case 0xB4:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 28) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1).Substring(0, 4);

                                        break;
                                    case 0xA0:
                                        GlobalVariable.StatusModel.M_NowHeadWearVer = asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(7);

                                        break;
                                    case 0xA1:
                                        GlobalVariable.StatusModel.M_NowHeadWearVer = GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(0, 7) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1)
                                            + GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(14);

                                        break;
                                    case 0xA2:
                                        GlobalVariable.StatusModel.M_NowHeadWearVer = GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(0, 14) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1).Substring(0, 6);

                                        break;
                                }
                                break;
                            #endregion
                            case 0xc31d203:
                            case 0x6b3:
                                #region 三号机
                                GlobalVariable.StatusModel.M_BMUID = "3";
                                switch (vci_can_objArray[0].Data[0])
                                {



                                    case 0xB0:
                                        GlobalVariable.StatusModel.M_NowSoftVer = asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.M_NowSoftVer.Substring(7);
                                        break;
                                    case 0xB1:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 7) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1)
                                            + GlobalVariable.StatusModel.M_NowSoftVer.Substring(14);
                                        break;
                                    case 0xB2:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 14) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1)
                                             + GlobalVariable.StatusModel.M_NowSoftVer.Substring(21);
                                        break;
                                    case 0xB3:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 21) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1)
                                            + GlobalVariable.StatusModel.M_NowSoftVer.Substring(28);
                                        break;
                                    case 0xB4:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 28) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1).Substring(0, 4);
                                        break;
                                    case 0xA0:
                                        GlobalVariable.StatusModel.M_NowHeadWearVer = asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(7);
                                        break;
                                    case 0xA1:
                                        GlobalVariable.StatusModel.M_NowHeadWearVer = GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(0, 7) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1)
                                            + GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(14);
                                        break;
                                    case 0xA2:
                                        GlobalVariable.StatusModel.M_NowHeadWearVer = GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(0, 14) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1).Substring(0, 6);
                                        break;
                                }
                                break;
                            #endregion
                            case 0xc31d204:
                            case 0x6b4:
                                #region 四号机
                                GlobalVariable.StatusModel.M_BMUID = "4";
                                switch (vci_can_objArray[0].Data[0])
                                {



                                    case 0xB0:
                                        GlobalVariable.StatusModel.M_NowSoftVer = asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.M_NowSoftVer.Substring(7);
                                        break;
                                    case 0xB1:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 7) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1)
                                            + GlobalVariable.StatusModel.M_NowSoftVer.Substring(14);
                                        break;
                                    case 0xB2:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 14) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1)
                                             + GlobalVariable.StatusModel.M_NowSoftVer.Substring(21);
                                        break;
                                    case 0xB3:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 21) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1)
                                            + GlobalVariable.StatusModel.M_NowSoftVer.Substring(28);
                                        break;
                                    case 0xB4:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 28) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1).Substring(0, 4);
                                        break;
                                    case 0xA0:
                                        GlobalVariable.StatusModel.M_NowHeadWearVer = asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(7);
                                        break;
                                    case 0xA1:
                                        GlobalVariable.StatusModel.M_NowHeadWearVer = GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(0, 7) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1)
                                            + GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(14);
                                        break;
                                    case 0xA2:
                                        GlobalVariable.StatusModel.M_NowHeadWearVer = GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(0, 14) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1).Substring(0, 6);
                                        break;
                                }
                                break;
                            #endregion
                            case 0xc31d205:
                            case 0x6b5:
                                #region 5号机
                                GlobalVariable.StatusModel.M_BMUID = "5";
                                switch (vci_can_objArray[0].Data[0])
                                {



                                    case 0xB0:
                                        GlobalVariable.StatusModel.M_NowSoftVer = asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.M_NowSoftVer.Substring(7);
                                        break;
                                    case 0xB1:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 7) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1)
                                            + GlobalVariable.StatusModel.M_NowSoftVer.Substring(14);
                                        break;
                                    case 0xB2:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 14) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1)
                                             + GlobalVariable.StatusModel.M_NowSoftVer.Substring(21);
                                        break;
                                    case 0xB3:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 21) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1)
                                            + GlobalVariable.StatusModel.M_NowSoftVer.Substring(28);
                                        break;
                                    case 0xB4:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 28) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1).Substring(0, 4);
                                        break;
                                    case 0xA0:
                                        GlobalVariable.StatusModel.M_NowHeadWearVer = asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1) + GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(7);
                                        break;
                                    case 0xA1:
                                        GlobalVariable.StatusModel.M_NowHeadWearVer = GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(0, 7) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1)
                                            + GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(14);
                                        break;
                                    case 0xA2:
                                        GlobalVariable.StatusModel.M_NowHeadWearVer = GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(0, 14) + asciiEncoding.GetString(vci_can_objArray[0].Data).Substring(1).Substring(0, 6);
                                        break;
                                }
                                break;
                                #endregion
                                #endregion


                        }
                    }
                }

            }
            catch (Exception)
            {
            }
        }

        private void Send()
        {
           
            this.receiveDataList.Clear();
            this.FrameData.Data = new byte[8];
            this.FrameData.DataLen = 8;
            this.FrameData.RemoteFlag = 0;
            this.FrameData.ExternFlag = 0;
            while (!GlobalVariable.mes_con)
            {
                Thread.Sleep(1);
                if (!this.Connected)
                {
                    return;
                }
                if ((GlobalVariable.IsFindBMSData && GlobalVariable.IsReadBMSID) && !GlobalVariable.IsCheckOK)
                {
                    if (GlobalVariable.StatusModel.NowValue < GlobalVariable.StatusModel.Maximum)
                    {

                        #region BCU发送的读取信号
                        if (GlobalVariable.IsBCU)
                        {
                            GlobalVariable.IsRealVersion = true;
                            this.FrameData.ExternFlag = 0;
                            this.FrameData.RemoteFlag = 0;
                            this.FrameData.Data = new byte[8];
                            this.FrameData.ExternFlag = 0;
                            this.FrameData.RemoteFlag = 0;
                            this.FrameData.ID = 0x704;
                            this.FrameData.Data[0] = 0x20;
                            this.FrameData.Data[1] = 0x15;
                            this.FrameData.Data[2] = 6;
                            this.FrameData.Data[3] = 0x26;
                            this.FrameData.Data[4] = 0x9c;
                            this.FrameData.Data[5] = 0x8f;
                            this.FrameData.Data[6] = 0xa1;
                            this.FrameData.Data[7] = 0x37;
                            CANAPI.FD_Transmit((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref this.FrameData, 1);
                            Thread.Sleep(700);
                            GlobalVariable.IsRealVersion = false;
                            Thread.Sleep(TimeSpan.FromMilliseconds(500.0));
                            this.FrameData.ID = 0x704;
                            this.FrameData.Data[0] = 0x55;
                            this.FrameData.Data[1] = 170;
                            this.FrameData.Data[2] = 0xcc;
                            this.FrameData.Data[3] = 0x33;
                            this.FrameData.Data[4] = 0x88;
                            this.FrameData.Data[5] = 0x77;
                            this.FrameData.Data[6] = 1;
                            this.FrameData.Data[7] = 0;
                            CANAPI.FD_Transmit((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref this.FrameData, 1);
                            Thread.Sleep(1);
                            this.FrameData.ExternFlag = 0;
                            this.FrameData.RemoteFlag = 0;
                            this.FrameData.ID = 0x704;
                            this.FrameData.Data[0] = 0x20;
                            this.FrameData.Data[1] = 0x15;
                            this.FrameData.Data[2] = 6;
                            this.FrameData.Data[3] = 0x26;
                            this.FrameData.Data[4] = 0x9c;
                            this.FrameData.Data[5] = 0x8f;
                            this.FrameData.Data[6] = 0xa1;
                            this.FrameData.Data[7] = 0x37;
                            CANAPI.FD_Transmit((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref this.FrameData, 1);
                            Thread.Sleep(1);
                            this.FrameData.Data[0] = 0x55;
                            this.FrameData.Data[1] = 170;
                            this.FrameData.Data[2] = 0xcc;
                            this.FrameData.Data[3] = 0x33;
                            this.FrameData.Data[4] = 0x88;
                            this.FrameData.Data[5] = 0x77;
                            this.FrameData.Data[6] = 1;
                            this.FrameData.Data[7] = 0;
                            CANAPI.FD_Transmit((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref this.FrameData2, 1);
                            GlobalVariable.StatusModel.NowValue++;
                            Thread.Sleep(100);
                            if ((((GlobalVariable.StatusModel.C_NowSoftVer.Trim() == GlobalVariable.StatusModel.C_ReserveSoftVer.Trim()) && (GlobalVariable.StatusModel.C_NowRealSoftVer.Trim() == GlobalVariable.StatusModel.C_ReserveRealSoftVer.Trim())) && ((GlobalVariable.StatusModel.C_NowRealHeadwearVer.Trim() == GlobalVariable.StatusModel.C_ReserveRealHeadwearVer.Trim()) && (GlobalVariable.StatusModel.C_NowHeadwearVer.Trim() == GlobalVariable.StatusModel.C_ReserveHeadwearVer.Trim()))) && (Math.Abs((double)(GlobalVariable.StatusModel.C_SOC - GlobalVariable.StatusModel.CR_SOC)) <= GlobalVariable.StatusModel.C_OffSet))
                            {
                                GlobalVariable.StatusModel.IsCheckResult = 1;
                                GlobalVariable.StatusModel.NowValue = 0;
                                GlobalVariable.IsCheckOK = true;
                                GlobalVariable.MES_PASS = true;
                                SaveLog.SaveLogMessage(GlobalVariable.BMSID, GlobalVariable.StatusModel.C_NowSoftVer.ToUpper().Trim(), GlobalVariable.StatusModel.C_NowHeadwearVer.Trim(),
                                    GlobalVariable.StatusModel.C_NowRealSoftVer.ToUpper().Trim(), GlobalVariable.StatusModel.C_NowRealHeadwearVer.Trim(), GlobalVariable.StatusModel.CR_SOC.ToString(), GlobalVariable.StatusModel.CheckResult, GlobalVariable.StatusModel.M_jhl, GlobalVariable.StatusModel.M_BMUID);
                            }

                            //else
                            //{if ((GlobalVariable.StatusModel.C_NowSoftVer.Trim() != GlobalVariable.StatusModel.C_ReserveSoftVer.Trim()))
                            //    { GlobalVariable.StatusModel.ListStatus = (DateTime.Now.ToString("HH:mm:ss") + " " + GlobalVariable.StatusModel.C_NowSoftVer.Trim() + " 软件版本号不一致!"); }
                            //    if ((GlobalVariable.StatusModel.C_NowRealSoftVer.Trim() == GlobalVariable.StatusModel.C_ReserveRealSoftVer.Trim()))
                            //    { GlobalVariable.StatusModel.ListStatus = (DateTime.Now.ToString("HH:mm:ss") + " " + GlobalVariable.StatusModel.C_NowRealSoftVer.Trim() + " 软件法规版本号不一致!"); }
                            //    if (((GlobalVariable.StatusModel.C_NowRealHeadwearVer.Trim() == GlobalVariable.StatusModel.C_ReserveRealHeadwearVer.Trim())))
                            //    { GlobalVariable.StatusModel.ListStatus = (DateTime.Now.ToString("HH:mm:ss") + " " + GlobalVariable.StatusModel.C_NowRealHeadwearVer.Trim() + " 硬件法规版本号不一致!"); }
                            //    if ((GlobalVariable.StatusModel.C_NowHeadwearVer.Trim() == GlobalVariable.StatusModel.C_ReserveHeadwearVer.Trim()))
                            //    { GlobalVariable.StatusModel.ListStatus = (DateTime.Now.ToString("HH:mm:ss") + " " + GlobalVariable.StatusModel.C_NowHeadwearVer.Trim() + " 硬件版本号不一致!"); }
                            //    if ((Math.Abs((double)(GlobalVariable.StatusModel.C_SOC - GlobalVariable.StatusModel.CR_SOC))) <= GlobalVariable.StatusModel.C_OffSet)
                            //    { GlobalVariable.StatusModel.ListStatus = (DateTime.Now.ToString("HH:mm:ss") + " " + GlobalVariable.StatusModel.CR_SOC.ToString() + " SOC不在范围!"); }
                              

                            //}
                        }
                        #endregion
                        else
                        {
                            this.FrameData.ExternFlag = 0;
                            this.FrameData.RemoteFlag = 0;
                            this.FrameData.ID = 0x690;
                            this.FrameData.Data = new byte[] { 0x20, 0x15, 6, 0x26, 0x9c, 0x8f, 0xa1, 0x37 };
                            CANAPI.FD_Transmit((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref this.FrameData, 1);
                            //Thread.Sleep(280);
                            //this.FrameData.ID = 0xC3100D2;
                            //this.FrameData.Data = new byte[] { 0x20, 0x15, 6, 0x26, 0x9c, 0x8f, 0xa1, 0x37 };
                            //this.FrameData.ExternFlag = 1;
                            //CANAPI.FD_Transmit((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref this.FrameData, 1);

                            Thread.Sleep(280);
                            this.FrameData.ID = 0xC3100D2;
                            this.FrameData.Data = new byte[] { 0x20, 0x15, 0x06, 0x26, 0x00, 0x00, 0x00, 0x00 };
                            this.FrameData.ExternFlag = 1;
                            CANAPI.FD_Transmit((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref this.FrameData, 1);
                            Thread.Sleep(280);
                            //60S菊花链
                            this.FrameData.ExternFlag = 0;
                            this.FrameData.RemoteFlag = 0;
                            this.FrameData.ID = 0x6C0;
                            this.FrameData.Data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01 };
                            CANAPI.FD_Transmit((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref this.FrameData, 1);
                            Thread.Sleep(280);
                            //30S菊花链
                            this.FrameData.ExternFlag = 1;
                            this.FrameData.RemoteFlag = 1;
                            this.FrameData.ID = 0x0C32D200;
                            this.FrameData.Data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };



                            CANAPI.FD_Transmit((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref this.FrameData, 1);


                            GlobalVariable.StatusModel.NowValue++;
                            if (((GlobalVariable.StatusModel.M_NowSoftVer.Trim() == GlobalVariable.StatusModel.M_ReserveSoftVer.Trim()) && (GlobalVariable.StatusModel.M_NowHeadWearVer.Trim() == GlobalVariable.StatusModel.M_ReserveHeadwearVer.Trim())) &&
                                (GlobalVariable.StatusModel.M_ReserveBMUID.Trim() == GlobalVariable.StatusModel.M_BMUID.Trim()) && (GlobalVariable.StatusModel.M_ReserveJHL == GlobalVariable.StatusModel.M_jhl))
                            {
                                GlobalVariable.StatusModel.IsCheckResult = 1;
                                GlobalVariable.StatusModel.NowValue = 0;
                                GlobalVariable.IsCheckOK = true;
                                GlobalVariable.MES_PASS = true;
                                //   public static void SaveLogMessage(string BMSID, string SoftWare, string HardWare, string RealSoftWare, string RealHardWare, string SOC, string CheckResult,string JHL,string SQE)
                                SaveLog.SaveLogMessage(GlobalVariable.BMSID, GlobalVariable.StatusModel.M_NowSoftVer.ToUpper().Trim(), GlobalVariable.StatusModel.M_NowHeadWearVer.Trim(),
                                      GlobalVariable.StatusModel.C_NowRealSoftVer.ToUpper().Trim(), GlobalVariable.StatusModel.C_NowRealHeadwearVer.Trim(), GlobalVariable.StatusModel.CR_SOC.ToString(), GlobalVariable.StatusModel.CheckResult, GlobalVariable.StatusModel.M_jhl, GlobalVariable.StatusModel.M_BMUID);
                            }

                          
                        }
                    }
                    else
                    {
                        GlobalVariable.sp.sound();

                        GlobalVariable.StatusModel.IsCheckResult = 2;
                        // this.mw.SetLogMsg("请求相应超时，数据比对失败 ...", true);
                        GlobalVariable.StatusModel.ListStatus = (DateTime.Now.ToString("HH:mm:ss") + "\t" + "请求相应超时，数据比对失败 ...");
                        GlobalVariable.IsCheckOK = true;
                        if (GlobalVariable.IsBCU)
                        {

                            SaveLog.SaveLogMessage(GlobalVariable.BMSID, GlobalVariable.StatusModel.C_NowSoftVer.ToUpper().Trim(), GlobalVariable.StatusModel.C_NowHeadwearVer.Trim(),
                                         GlobalVariable.StatusModel.C_NowRealSoftVer.ToUpper().Trim(), GlobalVariable.StatusModel.C_NowRealHeadwearVer.Trim(), GlobalVariable.StatusModel.CR_SOC.ToString(), GlobalVariable.StatusModel.CheckResult, GlobalVariable.StatusModel.M_jhl, GlobalVariable.StatusModel.M_BMUID);

 
                            {
                                if ((GlobalVariable.StatusModel.C_NowSoftVer.Trim() != GlobalVariable.StatusModel.C_ReserveSoftVer.Trim()))
                                { S = (DateTime.Now.ToString("HH:mm:ss") + " " + GlobalVariable.StatusModel.C_NowSoftVer.Trim() + " 软件版本号不一致!")+Environment.NewLine; }
                                if ((GlobalVariable.StatusModel.C_NowRealSoftVer.Trim() != GlobalVariable.StatusModel.C_ReserveRealSoftVer.Trim()))
                                {S+= (DateTime.Now.ToString("HH:mm:ss") + " " + GlobalVariable.StatusModel.C_NowRealSoftVer.Trim() + " 软件法规版本号不一致!") + Environment.NewLine; }
                                if (((GlobalVariable.StatusModel.C_NowRealHeadwearVer.Trim() != GlobalVariable.StatusModel.C_ReserveRealHeadwearVer.Trim())))
                                { S+= (DateTime.Now.ToString("HH:mm:ss") + " " + GlobalVariable.StatusModel.C_NowRealHeadwearVer.Trim() + " 硬件法规版本号不一致!") + Environment.NewLine; }
                                if ((GlobalVariable.StatusModel.C_NowHeadwearVer.Trim() != GlobalVariable.StatusModel.C_ReserveHeadwearVer.Trim()))
                                { S+= (DateTime.Now.ToString("HH:mm:ss") + " " + GlobalVariable.StatusModel.C_NowHeadwearVer.Trim() + " 硬件版本号不一致!") + Environment.NewLine; }
                                if ((Math.Abs((double)(GlobalVariable.StatusModel.C_SOC - GlobalVariable.StatusModel.CR_SOC))) > GlobalVariable.StatusModel.C_OffSet)
                                { S+= (DateTime.Now.ToString("HH:mm:ss") + " " + GlobalVariable.StatusModel.CR_SOC.ToString() + " SOC不在范围!"); }
                                GlobalVariable.StatusModel.ListStatus = S;

                                S = string.Empty;

                            }

                        }
                        else
                        {
                            SaveLog.SaveLogMessage(GlobalVariable.BMSID, GlobalVariable.StatusModel.M_NowSoftVer.ToUpper().Trim(), GlobalVariable.StatusModel.M_NowHeadWearVer.Trim(),
                                     GlobalVariable.StatusModel.C_NowRealSoftVer.ToUpper().Trim(), GlobalVariable.StatusModel.C_NowRealHeadwearVer.Trim(), GlobalVariable.StatusModel.CR_SOC.ToString(), GlobalVariable.StatusModel.CheckResult, GlobalVariable.StatusModel.M_jhl, GlobalVariable.StatusModel.M_BMUID);


                            {
                               


                                if ((GlobalVariable.StatusModel.M_NowSoftVer.Trim() != GlobalVariable.StatusModel.M_ReserveSoftVer.Trim()))
                                { S= (DateTime.Now.ToString("HH:mm:ss") + " " + GlobalVariable.StatusModel.M_NowSoftVer.Trim() + " 软件版本号不一致!") + Environment.NewLine; }
                                if ((GlobalVariable.StatusModel.M_NowHeadWearVer.Trim() != GlobalVariable.StatusModel.M_ReserveHeadwearVer.Trim()))
                                {S+= (DateTime.Now.ToString("HH:mm:ss") + " " + GlobalVariable.StatusModel.M_NowHeadWearVer.Trim() + "  硬件版本号不一致!") + Environment.NewLine; }
                                if (GlobalVariable.StatusModel.M_ReserveBMUID.Trim() != GlobalVariable.StatusModel.M_BMUID.Trim())
                                { S+= (DateTime.Now.ToString("HH:mm:ss") + " " + GlobalVariable.StatusModel.M_ReserveBMUID.Trim() + " 从机ID不一致!") + Environment.NewLine; }
                                if ((GlobalVariable.StatusModel.M_ReserveJHL != GlobalVariable.StatusModel.M_jhl))
                                { S+= (DateTime.Now.ToString("HH:mm:ss") + " " + GlobalVariable.StatusModel.M_jhl + " 芯片数不一致!") ; }

                                GlobalVariable.StatusModel.ListStatus = S;

                                S = string.Empty;
                            }

                        }
                    }
                }
                Thread.Sleep(300);
            }
        }
      
        private void JM_ReceiveDataProc()
        {
         
                CANAPI.VCI_ERR_INFO JMER = new CANAPI.VCI_ERR_INFO
                {
                    Passive_ErrData = new byte[3]
                };
             
                while (GlobalVariable.mes_con)
            {
                Thread.Sleep(0);
                try
              {


                    if (!this.Connected )
                    {
                        return;
                    }

                    int num = 0;
                    num = (int)CANAPI.FD_GetReceiveNum((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex);
                  //  GlobalVariable.StatusModel.ListStatus = (DateTime.Now.ToString("HH:mm:ss") +"COUNT_SEND"+ CANAPI.FD_Receive((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref JM_CAN[0], 1, 200).ToString()+"NUM"+ num);
                  
                    if (CANAPI.FD_Receive((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref JM_CAN[0], 1, 200) <= 0)
                    {
                        CANAPI.FD_ReadErrInfo((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref JMER);
                        if (GlobalVariable.IsFindBMSData && (this.dt.AddSeconds(1.0) < DateTime.Now))
                        {
                            int num2 = JM_CAN[0].Data[0];
                            //    this.mw.SetLogMsg("CAN总线上没有检测到设备 ...", true);
                            GlobalVariable.StatusModel.ListStatus = (DateTime.Now.ToString("HH:mm:ss") + "\t" + "CAN总线上没有检测到设备 ...");
                            //    GlobalVariable.StatusModel.BMSID = "000";
                            GlobalVariable.StatusModel.NowValue = 0;
                            GlobalVariable.StatusModel.C_NowSoftVer = "                                ";
                            GlobalVariable.StatusModel.C_NowRealSoftVer = "                                ";
                            GlobalVariable.StatusModel.C_NowHeadwearVer = "                    ";
                            GlobalVariable.StatusModel.C_NowRealHeadwearVer = "                    ";
                            GlobalVariable.StatusModel.C_NowInsulationVer = "                    ";
                            GlobalVariable.StatusModel.C_NowLinkVoltage = 0.0;
                            GlobalVariable.StatusModel.C_NowPackVoltage = 0.0;
                            GlobalVariable.StatusModel.CR_SOC = 0.0;
                            GlobalVariable.StatusModel.M_NowSoftVer = "                                ";
                            GlobalVariable.StatusModel.M_NowHeadWearVer = "                    ";
                            GlobalVariable.StatusModel.M_BMUID = "";
                            GlobalVariable.StatusModel.IsCheckResult = 0;
                            GlobalVariable.IsFindBMSData = false;
                            GlobalVariable.IsCheckOK = false;
                            GlobalVariable.MES_PASS = false;
                            GlobalVariable.IsReadBMSID = false;
                            GlobalVariable.StatusModel.M_jhl = "";
                            GlobalVariable.StatusModel.MES_MSG = "";
                            GlobalVariable.conn = false;
                            GlobalVariable.mes = "";
                            // cr.p = 0;
                            GlobalVariable.P = 0;

                            GlobalVariable.mes_read_value = "";
                        GlobalVariable.bit = "";

                        }
                    }
                    else if (JM_CAN[0].RemoteFlag == 0)
                    {//2020.11.16
                        if (!GlobalVariable.IsFindBMSData)
                        {
                            this.dt = DateTime.Now;

                            GlobalVariable.StatusModel.ListStatus = (DateTime.Now.ToString("HH:mm:ss") + "\t" + "检测到总线上有设备接入 ...");
                            GlobalVariable.IsFindBMSData = true;
                            GlobalVariable.StatusModel.NowValue = 0;
                            this.dtTimeOut = DateTime.Now;
                            GlobalVariable.conn = true;
                           // GlobalVariable.mes = "";
                            //GlobalVariable.CANOperate.Close();
                         //   GlobalVariable.CANOperate.Open();
                           

                            //   CANAPI.FD_ClearBuffer((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex);
                        }


                    if  (GlobalVariable.message.Length>1&& Convert.ToUInt32("0x" + GlobalVariable.message[2].Split('/')[0], 16).Equals(JM_CAN[0].ID))
                        {
                        #region 软件版本号
                            //GlobalVariable.StatusModel.ListStatus = (DateTime.Now.ToString("HH:mm:ss") + "\t" + "JM_RESV" + GlobalVariable.mes_read_value);
                            if (GlobalVariable.message[2].Split('/')[1].ToString() == "软件版本号")
                            {
                                switch (JM_CAN[0].Data[0])
                                {


                                    case 0xB0:

                                        GlobalVariable.StatusModel.M_NowSoftVer = asciiEncoding.GetString(JM_CAN[0].Data).Substring(1) + GlobalVariable.StatusModel.M_NowSoftVer.Substring(7);
                                    
                                  
                                        break;
                                    case 0xB1:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 7) + asciiEncoding.GetString(JM_CAN[0].Data).Substring(1)
                                                + GlobalVariable.StatusModel.M_NowSoftVer.Substring(14);
                                  
                                   
                                        break;
                                    case 0xB2:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 14) + asciiEncoding.GetString(JM_CAN[0].Data).Substring(1)
                                                 + GlobalVariable.StatusModel.M_NowSoftVer.Substring(21);
                                     
                                 
                                        break;
                                    case 0xB3:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 21) + asciiEncoding.GetString(JM_CAN[0].Data).Substring(1)
                                                + GlobalVariable.StatusModel.M_NowSoftVer.Substring(28);
                                       
                                    
                                        break;
                                    case 0xB4:
                                        GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 28) + asciiEncoding.GetString(JM_CAN[0].Data).Substring(1).Substring(0, 6);
                                         break;
                                }
                                GlobalVariable.mes_read_value =  GlobalVariable.StatusModel.M_NowSoftVer;
                            }
                            #endregion
                        #region 鸿日软件版本号
                            //GlobalVariable.StatusModel.ListStatus = (DateTime.Now.ToString("HH:mm:ss") + "\t" + "JM_RESV" + GlobalVariable.mes_read_value);
                            if (GlobalVariable.message[2].Split('/')[1].ToString() == "鸿日软件版本号")
                            {
                                
                                        GlobalVariable.StatusModel.M_NowSoftVer = asciiEncoding.GetString(JM_CAN[0].Data);


                                 
                                
                                GlobalVariable.mes_read_value = GlobalVariable.StatusModel.M_NowSoftVer;
                            }
                            #endregion
                        #region 硬件版本号
                            if (GlobalVariable.message[2].Split('/')[1].ToString() == "硬件版本号")
                            {
                                switch (JM_CAN[0].Data[0])
                                {



                                    case 0xA0:
                                     
                                        GlobalVariable.StatusModel.M_NowHeadWearVer = asciiEncoding.GetString(JM_CAN[0].Data).Substring(1) + GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(7);
                                    
                                        
                                        break;
                                    case 0xA1:
                                        GlobalVariable.StatusModel.M_NowHeadWearVer = GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(0, 7) + asciiEncoding.GetString(JM_CAN[0].Data).Substring(1)
                                            + GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(14);
                                         break;
                                    case 0xA2:
                                        GlobalVariable.StatusModel.M_NowHeadWearVer = GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(0, 14) + asciiEncoding.GetString(JM_CAN[0].Data).Substring(1).Substring(0, 7);


                                    
                                     
                                        break;
                                    default:  


                                        break;

                                }
                                GlobalVariable.mes_read_value = GlobalVariable.StatusModel.M_NowHeadWearVer;
                            }
                            #endregion
                        #region 软件法规版本号
                        

                                //GlobalVariable.StatusModel.ListStatus = (DateTime.Now.ToString("HH:mm:ss") + "\t" + "JM_RESV" + GlobalVariable.mes_read_value);
                                if (GlobalVariable.message[2].Split('/')[1].ToString() == "软件法规版本号")
                                {
                                    switch (JM_CAN[0].Data[0])
                                    {


                                        case 0xB0:

                                            GlobalVariable.StatusModel.M_NowSoftVer = asciiEncoding.GetString(JM_CAN[0].Data).Substring(1) + GlobalVariable.StatusModel.M_NowSoftVer.Substring(7);


                                            break;
                                        case 0xB1:
                                            GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 7) + asciiEncoding.GetString(JM_CAN[0].Data).Substring(1)
                                                    + GlobalVariable.StatusModel.M_NowSoftVer.Substring(14);


                                            break;
                                        case 0xB2:
                                            GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 14) + asciiEncoding.GetString(JM_CAN[0].Data).Substring(1)
                                                     + GlobalVariable.StatusModel.M_NowSoftVer.Substring(21);


                                            break;
                                        case 0xB3:
                                            GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 21) + asciiEncoding.GetString(JM_CAN[0].Data).Substring(1)
                                                    + GlobalVariable.StatusModel.M_NowSoftVer.Substring(28);


                                            break;
                                        case 0xB4:
                                            GlobalVariable.StatusModel.M_NowSoftVer = GlobalVariable.StatusModel.M_NowSoftVer.Substring(0, 28) + asciiEncoding.GetString(JM_CAN[0].Data).Substring(1).Substring(0, 6);
                                            break;
                                    }
                                    GlobalVariable.mes_read_value = GlobalVariable.StatusModel.M_NowSoftVer;
                                }
                                #endregion
                        #region 硬件法规版本号
                                if (GlobalVariable.message[2].Split('/')[1].ToString() == "硬件法规版本号")
                                {
                                    switch (JM_CAN[0].Data[0])
                                    {



                                        case 0xA0:
                                            GlobalVariable.StatusModel.M_NowHeadWearVer = asciiEncoding.GetString(JM_CAN[0].Data).Substring(1) + GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(7);



                                            break;
                                        case 0xA1:
                                            GlobalVariable.StatusModel.M_NowHeadWearVer = GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(0, 7) + asciiEncoding.GetString(JM_CAN[0].Data).Substring(1)
                                                + GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(14);
                                            break;
                                        case 0xA2:
                                            GlobalVariable.StatusModel.M_NowHeadWearVer = GlobalVariable.StatusModel.M_NowHeadWearVer.Substring(0, 14) + asciiEncoding.GetString(JM_CAN[0].Data).Substring(1).Substring(0, 7);




                                            break;
                                        default:


                                            break;

                                    }
                                    GlobalVariable.mes_read_value = GlobalVariable.StatusModel.M_NowHeadWearVer;
                                }
                                #endregion
                        #region 索引读取 包含SOC、从机ID、芯片数
                                if (GlobalVariable.message[2].Split('/')[1].ToString() == "索引读取")
                            {

                                if (GlobalVariable.message[2].Split('/')[2].ToString().Length == 2)
                                {

                                    GlobalVariable.mes_read_value = (((JM_CAN[0].Data[Convert.ToInt32(GlobalVariable.message[2].Split('/')[2].ToString().Substring(0, 1))] * 0x100) + JM_CAN[0].Data[Convert.ToInt32(GlobalVariable.message[2].Split('/')[2].ToString().Substring(1, 1))]) * 0.01).ToString();
                                  //  GlobalVariable.StatusModel.CR_SOC = Convert.ToDouble(GlobalVariable.mes_read_value);

                                    //if (GlobalVariable.StatusModel.CR_SOC)
                                }
                                else
                                    if (GlobalVariable.message[2].Split('/')[2].ToString().Length == 1)
                                {

                                    // GlobalVariable.StatusModel.ListStatus = GlobalVariable.message[2].Split('/')[2].ToString()+":"+ JM_CAN[0].Data[Convert.ToInt32(GlobalVariable.message[2].Split('/')[2].ToString().Substring(0, 1))].ToString();
                                   GlobalVariable.mes_read_value = JM_CAN[0].Data[Convert.ToInt32(GlobalVariable.message[2].Split('/')[2].ToString().Substring(0, 1))].ToString();

                                }
                                if (GlobalVariable.message[2].Split('/')[2].ToString().Length >=3&& GlobalVariable.message[2].Split('/')[2].ToString().Contains("+"))
                                {



                                    for (int j = 0; j < JM_CAN[0].Data.Length; j++)
                                    {
                                        for (int i = 0; i<8 ; i++)
                                        {

                                            Convert.ToString(JM_CAN[0].Data[j], 2).PadLeft(8, '0');
                                            GlobalVariable.bit += Convert.ToString(JM_CAN[0].Data[j], 2).PadLeft(8, '0').Substring(i, 1);
                                        }
                                    }



                                if (GlobalVariable.message[2].Split('/')[2].ToString().Split('+').Length > 1)


                                {
                                    GlobalVariable.mes_read_value = ((Convert.ToInt32((GlobalVariable.bit.Substring(Convert.ToInt32(GlobalVariable.message[2].Split('/')[2].ToString().Split('+')[0]) - 1, Convert.ToInt32(GlobalVariable.message[2].Split('/')[2].ToString().Split('+')[1])).ToString()), 2) * 0.01)).ToString();

                                }



                            
                                }

                            }
                            #endregion
                        }
                    }
                }
                catch (Exception ex)
                {
                    GlobalVariable.StatusModel.ListStatus = ex.Message;
                }
            }

        
        }


     
       

     
        private void JM_Send()
        {

            this.receiveDataList.Clear();
           this.FrameData.Data = new byte[8];
            this.FrameData.DataLen = 8;
            this.FrameData.RemoteFlag = 0;
            this.FrameData.ExternFlag = 0;
            GlobalVariable.message = GlobalVariable.mes.Split(';');
            while (GlobalVariable.mes_con)
            {
                Thread.Sleep(1);
                try
                {
                    if (!this.Connected)
                    {
                        return;
                    }
                    {
                        if ((GlobalVariable.IsFindBMSData && GlobalVariable.IsReadBMSID && (GlobalVariable.mes.Split(';').Length >= 3)))

                        {


                            GlobalVariable.IsCheckOK = false;
                            GlobalVariable.StatusModel.NowValue = 0;

                            #region  单个循环发送
                            while (this.Connected && GlobalVariable.StatusModel.NowValue < GlobalVariable.StatusModel.Maximum && (GlobalVariable.mes.Split(';').Length >= 3))
                            {
                                //   GlobalVariable.StatusModel.ListStatus = (DateTime.Now.ToString("HH:mm:ss") + "\t" + "M发送" + GlobalVariable.mes.ToString());



                                GlobalVariable.message = GlobalVariable.mes.Split(';');


                                // string ss =Convert.ToByte (GlobalVariable.message[0].Split('/')[2]);
                                //   GlobalVariable.StatusModel.ListStatus = (DateTime.Now.ToString("HH:mm:ss") + "\t" + "message" + GlobalVariable.message[0].ToString());
                                this.FrameData.RemoteFlag = Convert.ToByte(GlobalVariable.message[0].Split('/')[3]);
                                this.FrameData.ExternFlag = Convert.ToByte(GlobalVariable.message[0].Split('/')[2]);
                                this.FrameData.Data = new byte[8];
                                string s = "0x" + GlobalVariable.message[1].Split('/')[0];
                                this.FrameData.ID = Convert.ToUInt32("0x" + GlobalVariable.message[1].Split('/')[0], 16);
                                if (GlobalVariable.message[1].Split('/')[1].Split('&').Length <= 1)
                                {
                                    this.FrameData.Data[0] = Convert.ToByte(GlobalVariable.message[1].Split('/')[1].Split(' ')[0], 16);
                                    this.FrameData.Data[1] = Convert.ToByte(GlobalVariable.message[1].Split('/')[1].Split(' ')[1], 16);
                                    this.FrameData.Data[2] = Convert.ToByte(GlobalVariable.message[1].Split('/')[1].Split(' ')[2], 16);
                                    this.FrameData.Data[3] = Convert.ToByte(GlobalVariable.message[1].Split('/')[1].Split(' ')[3], 16);
                                    this.FrameData.Data[4] = Convert.ToByte(GlobalVariable.message[1].Split('/')[1].Split(' ')[4], 16);
                                    this.FrameData.Data[5] = Convert.ToByte(GlobalVariable.message[1].Split('/')[1].Split(' ')[5], 16);
                                    this.FrameData.Data[6] = Convert.ToByte(GlobalVariable.message[1].Split('/')[1].Split(' ')[6], 16);
                                    this.FrameData.Data[7] = Convert.ToByte(GlobalVariable.message[1].Split('/')[1].Split(' ')[7], 16);



                                    if (CANAPI.FD_Transmit((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref this.FrameData, 1) == 0)
                                    { GlobalVariable.StatusModel.ListStatus = "0x" + GlobalVariable.message[1].Split('/')[0] + "发送错误"; }
                                }
                                else

                                {
                                    string s1 = "0x" + GlobalVariable.message[1].Split('/')[1].Split('&')[0].Split(' ')[0];
                                    this.FrameData.Data[0] = Convert.ToByte("0x" + GlobalVariable.message[1].Split('/')[1].Split('&')[0].Split(' ')[0], 16);
                                    this.FrameData.Data[1] = Convert.ToByte("0x" + GlobalVariable.message[1].Split('/')[1].Split('&')[0].Split(' ')[1], 16);
                                    this.FrameData.Data[2] = Convert.ToByte("0x" + GlobalVariable.message[1].Split('/')[1].Split('&')[0].Split(' ')[2], 16);
                                    this.FrameData.Data[3] = Convert.ToByte("0x" + GlobalVariable.message[1].Split('/')[1].Split('&')[0].Split(' ')[3], 16);
                                    this.FrameData.Data[4] = Convert.ToByte("0x" + GlobalVariable.message[1].Split('/')[1].Split('&')[0].Split(' ')[4], 16);
                                    this.FrameData.Data[5] = Convert.ToByte("0x" + GlobalVariable.message[1].Split('/')[1].Split('&')[0].Split(' ')[5], 16);
                                    this.FrameData.Data[6] = Convert.ToByte("0x" + GlobalVariable.message[1].Split('/')[1].Split('&')[0].Split(' ')[6], 16);
                                    this.FrameData.Data[7] = Convert.ToByte("0x" + GlobalVariable.message[1].Split('/')[1].Split('&')[0].Split(' ')[7], 16);
                                    if (CANAPI.FD_Transmit((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref this.FrameData, 1) == 0)
                                    { GlobalVariable.StatusModel.ListStatus = "0x" + GlobalVariable.message[1].Split('/')[0] + "发送错误"; }

                                    //   Thread.Sleep(200);
                                    // Thread.Sleep(200);
                                    this.FrameData.Data[0] = Convert.ToByte("0x" + GlobalVariable.message[1].Split('/')[1].Split('&')[1].Split(' ')[0], 16);
                                    this.FrameData.Data[1] = Convert.ToByte("0x" + GlobalVariable.message[1].Split('/')[1].Split('&')[1].Split(' ')[1], 16);
                                    this.FrameData.Data[2] = Convert.ToByte("0x" + GlobalVariable.message[1].Split('/')[1].Split('&')[1].Split(' ')[2], 16);
                                    this.FrameData.Data[3] = Convert.ToByte("0x" + GlobalVariable.message[1].Split('/')[1].Split('&')[1].Split(' ')[3], 16);
                                    this.FrameData.Data[4] = Convert.ToByte("0x" + GlobalVariable.message[1].Split('/')[1].Split('&')[1].Split(' ')[4], 16);
                                    this.FrameData.Data[5] = Convert.ToByte("0x" + GlobalVariable.message[1].Split('/')[1].Split('&')[1].Split(' ')[5], 16);
                                    this.FrameData.Data[6] = Convert.ToByte("0x" + GlobalVariable.message[1].Split('/')[1].Split('&')[1].Split(' ')[6], 16);
                                    this.FrameData.Data[7] = Convert.ToByte("0x" + GlobalVariable.message[1].Split('/')[1].Split('&')[1].Split(' ')[7], 16);
                                    if (CANAPI.FD_Transmit((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref this.FrameData, 1) == 0)
                                    { GlobalVariable.StatusModel.ListStatus = "0x" + GlobalVariable.message[1].Split('/')[0] + "发送错误"; }


                                    // Thread.Sleep(80);


                                }

                                Thread.Sleep(80);
                                GlobalVariable.StatusModel.NowValue++;
                            }
                            #endregion
                            Thread.Sleep(200);
                            GlobalVariable.JM_TH.Set();
                            GlobalVariable.JM_TH_boo = true;
                            GlobalVariable.mes = "";




                        }


                    }

                }
                catch (Exception ex)
                {
                    GlobalVariable.StatusModel.ListStatus = ex.Message;
                }
            }

        }



        private void JM_Send_Open()
        {

          

            while (GlobalVariable.mes_con)
            {
                if (!GlobalVariable.TL_OPEN)
                { return; }
                this.FrameData.Data = new byte[8];
                this.FrameData.DataLen = 8;
                this.FrameData.RemoteFlag = 0;
                this.FrameData.ExternFlag = 0;
                GlobalVariable.message_OPEN = GlobalVariable.open_mod.Split(';');
                Thread.Sleep(1000);
                if (!this.Connected)
                { return; }
               
                {
                    if (!GlobalVariable.IsFindBMSData && (GlobalVariable.open_mod.Split(';').Length >= 2))
                    {


                     

                        #region  单个循环发送
                       
                        {





                            this.FrameData.RemoteFlag = Convert.ToByte(GlobalVariable.message_OPEN[0].Split('/')[3]);
                            this.FrameData.ExternFlag = Convert.ToByte(GlobalVariable.message_OPEN[0].Split('/')[2]);
                            this.FrameData.Data = new byte[8];
                            string s = "0x" + GlobalVariable.message_OPEN[1].Split('/')[0];
                            this.FrameData.ID = Convert.ToUInt32("0x" + GlobalVariable.message_OPEN[1].Split('/')[0], 16);
                            if (GlobalVariable.message_OPEN[1].Split('/')[1].Split('&').Length <= 1)
                            {
                                this.FrameData.Data[0] = Convert.ToByte(GlobalVariable.message_OPEN[1].Split('/')[1].Split(' ')[0], 16);
                                this.FrameData.Data[1] = Convert.ToByte(GlobalVariable.message_OPEN[1].Split('/')[1].Split(' ')[1], 16);
                                this.FrameData.Data[2] = Convert.ToByte(GlobalVariable.message_OPEN[1].Split('/')[1].Split(' ')[2], 16);
                                this.FrameData.Data[3] = Convert.ToByte(GlobalVariable.message_OPEN[1].Split('/')[1].Split(' ')[3], 16);
                                this.FrameData.Data[4] = Convert.ToByte(GlobalVariable.message_OPEN[1].Split('/')[1].Split(' ')[4], 16);
                                this.FrameData.Data[5] = Convert.ToByte(GlobalVariable.message_OPEN[1].Split('/')[1].Split(' ')[5], 16);
                                this.FrameData.Data[6] = Convert.ToByte(GlobalVariable.message_OPEN[1].Split('/')[1].Split(' ')[6], 16);
                                this.FrameData.Data[7] = Convert.ToByte(GlobalVariable.message_OPEN[1].Split('/')[1].Split(' ')[7], 16);



                                CANAPI.FD_Transmit((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref this.FrameData, 1);
                               

                            }
                           

                        }
                        #endregion
                       // Thread.Sleep(200);
                     
                     


               

                    }


                }

            }
        }
        private void SendBMUInfo()
        {
            while (true)
            {
                Thread.Sleep(1);
                if (!this.Connected)
                {
                    return;
                }
                if (!GlobalVariable.IsBCU && GlobalVariable.debug)
                {
                    FrameData.ExternFlag = 1;
                    FrameData.RemoteFlag = 1;
                    FrameData.ID = 0x0C37D004;
                    CANAPI.FD_Transmit((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref FrameData, 1);

                    FrameData.ID = 0x1821D000;
                    CANAPI.FD_Transmit((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref FrameData, 1);
                    FrameData.ID = 0x1822D000;
                    CANAPI.FD_Transmit((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref FrameData, 1);
                    FrameData.ID = 0x1823D000;
                    CANAPI.FD_Transmit((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref FrameData, 1);
                    FrameData.ID = 0x1824D000;
                    CANAPI.FD_Transmit((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref FrameData, 1);
                    FrameData.ID = 0x1825D000;
                    CANAPI.FD_Transmit((uint)GlobalVariable.DeviceType, (uint)GlobalVariable.DeviceIndex, (uint)GlobalVariable.CANIndex, ref FrameData, 1);
                }

            }
        }
        private ulong byteArrToUint64Moto(byte[] Data)
        {
            ulong num = 0L;
            foreach (byte num2 in Data)
            {
                num = (num << 8) + num2;
            }
            return num;
        }
        private void SendDebugCode()
        {
            this.FrameData2 = new CANAPI.VCI_CAN_OBJ();
            while (true)
            {
                if (!this.Connected)
                {
                    return;
                }
                if (GlobalVariable.IsBCU)
                {
                    this.FrameData2.ExternFlag = 0;
                    this.FrameData2.RemoteFlag = 0;
                    this.FrameData2.Data = new byte[8];
                    this.FrameData2.ID = 0x704;
                    this.FrameData2.Data[0] = 0x55;
                    this.FrameData2.Data[1] = 170;
                    this.FrameData2.Data[2] = 0xcc;
                    this.FrameData2.Data[3] = 0x33;
                    this.FrameData2.Data[4] = 0x88;
                    this.FrameData2.Data[5] = 0x77;
                    this.FrameData2.Data[6] = 1;
                    this.FrameData2.Data[7] = 0;
                }
                Thread.Sleep(50);
            }
        }

        public void WriteIn()
        {
            if ((this.thSend == null) || !this.thSend.IsAlive)
            {
                this.thSend = new Thread((this.Send));
                this.thSend.Start();
            }
            if ((this.thSend2 == null) || !this.thSend2.IsAlive)
            {
                this.thSend2 = new Thread((this.SendBMUInfo));
                this.thSend2.Start();
            }
            if ((this.thMonitor == null) || !this.thMonitor.IsAlive)
            {
                this.thMonitor = new Thread((this.ReceiveDataProc));
                this.thMonitor.Start();
            }


            if ((this.thSend3 == null) || !this.thSend3.IsAlive)
            {
                this.thSend3 = new Thread((this.JM_Send));
                this.thSend3.Start();
            }
            if ((this.thSend4 == null) || !this.thSend4.IsAlive)
            {
                this.thSend4 = new Thread((this.JM_ReceiveDataProc));
                this.thSend4.Start();
            }
            if ((this.thSend5 == null) || !this.thSend5.IsAlive)
            {
                this.thSend5 = new Thread((this.JM_Send_Open));
                this.thSend5.Start();
            }

        }
    }
}

