using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MES_CAN
{
    public class StatusModel : NotificationObject
    {
        private string _BMSID;
        private string _C_NowHeadwearVer = "                    ";
        private string _C_NowInsulationHeadWearVer = "                    ";
        private string _C_NowInsulationVer = "                    ";
        private double _C_NowLinkVoltage;
        private double _C_NowPackVoltage;
        private string _C_NowRealHeadwearVer = "                    ";
        private string _C_NowRealSoftVer = "                                ";
        private string _C_NowSoftVer = "                                ";
        private double _C_OffSet;
        private string _C_RaiseInsulationVer = string.Empty;
        private string _C_ReserveHeadwearVer ;
        private string _C_ReserveRealHeadwearVer = string.Empty;
        private string _C_ReserveRealSoftVer = string.Empty;
        private string _C_ReserveSoftVer = string.Empty;
        private double _C_SOC ;
        private string _CheckResult = "NULL..";
        private double _CR_SOC ;
        private byte _IsCheckResult=0 ;
        private string _M_BMUID = "";
        private string _M_NowHeadWearVer = "                               ";
        private string _M_NowSoftVer = "                                               ";
        private string _M_ReserveBMUID = "";
        private string _M_ReserveHeadwearVer = string.Empty;
        private string _M_ReserveSoftVer = string.Empty;
        private string _M_ReserveJHL = string.Empty;

        private string _M_Reservename = string.Empty;

        private string _MES_MSG = string.Empty;
        private string _MES_listvalue = string.Empty;



        private string _M_jhl = "";
        private int _Maximum = 15;
        private int _Minimum = 0;
        private int _NowValue ;

        public string BMSID
        {
            get
            {
                return this._BMSID;
            }
            set
            {
                this._BMSID = value;
                base.RaisePropertyChanged("BMSID");
            }
        }
        public string MES_listvalue
        {
            get
            {
                return this._MES_listvalue;
            }
            set
            {
                this._MES_listvalue = value;
                base.RaisePropertyChanged("MES_listvalue");
            }
        }
        public string M_jhl
        {
            get
            {
                return this._M_jhl;
            }
            set
            {
                this._M_jhl = value;
                base.RaisePropertyChanged("M_jhl");
            }
        }
        public string M_ReserveJHL
        {
            get
            {
                return this._M_ReserveJHL;
            }
            set
            {
                this._M_ReserveJHL = value;
                base.RaisePropertyChanged("M_ReserveJHL");
            }
        }
        public string M_Reservename
        {
            get
            {
                return this._M_Reservename;
            }
            set
            {
                this._M_Reservename = value;
                base.RaisePropertyChanged("M_Reservename");
            }
        }

        public string MES_MSG
        {
            get
            {
                return this._MES_MSG;
            }
            set
            {
                this._MES_MSG = value;
                base.RaisePropertyChanged("MES_MSG");
            }
        }
        private string _ListStatus="0" ;
      
        public  string ListStatus 

         {
            get
            {
                return _ListStatus;
            }
            set
            {
                this._ListStatus=value ;
                base.RaisePropertyChanged("ListStatus");
            }
        }


public string C_NowHeadwearVer
        {
            get
            {
                return this._C_NowHeadwearVer;
            }
            set
            {
                this._C_NowHeadwearVer = value;
                base.RaisePropertyChanged("C_NowHeadwearVer");
            }
        }

        public string C_NowInsulationHeadWearVer
        {
            get
            {
                return this._C_NowInsulationHeadWearVer;
            }
            set
            {
                this._C_NowInsulationHeadWearVer = value;
                base.RaisePropertyChanged("C_NowInsulationHeadWearVer");
            }
        }

        public string C_NowInsulationVer
        {
            get
            {
                return this._C_NowInsulationVer;
            }
            set
            {
                this._C_NowInsulationVer = value;
                base.RaisePropertyChanged("C_NowInsulationVer");
            }
        }

        public double C_NowLinkVoltage
        {
            get
            {
                return this._C_NowLinkVoltage;
            }
            set
            {
                this._C_NowLinkVoltage = value;
                base.RaisePropertyChanged("C_NowLinkVoltage");
            }
        }

        public double C_NowPackVoltage
        {
            get
            {
                return this._C_NowPackVoltage;
            }
            set
            {
                this._C_NowPackVoltage = value;
                base.RaisePropertyChanged("C_NowPackVoltage");
            }
        }

        public string C_NowRealHeadwearVer
        {
            get
            {
                return this._C_NowRealHeadwearVer;
            }
            set
            {
                this._C_NowRealHeadwearVer = value;
                base.RaisePropertyChanged("C_NowRealHeadwearVer");
            }
        }

        public string C_NowRealSoftVer
        {
            get
            {
                return this._C_NowRealSoftVer;
            }
            set
            {
                this._C_NowRealSoftVer = value;
                base.RaisePropertyChanged("C_NowRealSoftVer");
            }
        }

        public string C_NowSoftVer
        {
            get
            {
                return this._C_NowSoftVer;
            }
            set
            {
                this._C_NowSoftVer = value;
                base.RaisePropertyChanged("C_NowSoftVer");
            }
        }

        public double C_OffSet
        {
            get
            {
                return this._C_OffSet;
            }
            set
            {
                this._C_OffSet = value;
                base.RaisePropertyChanged("C_OffSet");
            }
        }

        public string C_RaiseInsulationVer
        {
            get
            {
                return this._C_RaiseInsulationVer;
            }
            set
            {
                this._C_RaiseInsulationVer = value;
                base.RaisePropertyChanged("C_RaiseInsulationVer");
            }
        }

        public string C_ReserveHeadwearVer
        {
            get
            {
                return this._C_ReserveHeadwearVer;
            }
            set
            {
                this._C_ReserveHeadwearVer = value;
                base.RaisePropertyChanged("C_ReserveHeadwearVer");
            }
        }

        public string C_ReserveRealHeadwearVer
        {
            get
            {
                return this._C_ReserveRealHeadwearVer;
            }
            set
            {
                this._C_ReserveRealHeadwearVer = value;
                base.RaisePropertyChanged("C_ReserveRealHeadwearVer");
            }
        }

        public string C_ReserveRealSoftVer
        {
            get
            {
                return this._C_ReserveRealSoftVer;
            }
            set
            {
                this._C_ReserveRealSoftVer = value;
                base.RaisePropertyChanged("C_ReserveRealSoftVer");
            }
        }

        public string C_ReserveSoftVer
        {
            get
            {
                return this._C_ReserveSoftVer;
            }
            set
            {
                this._C_ReserveSoftVer = value;
                base.RaisePropertyChanged("C_ReserveSoftVer");
            }
        }


        public double C_SOC
        {
            get
            {
                return this._C_SOC;
            }
            set
            {
                this._C_SOC = value;
                base.RaisePropertyChanged("C_SOC");
            }
        }

        public string CheckResult
        {
            get
            {
                return this._CheckResult;
            }
             set
            {
                this._CheckResult = value;
                base.RaisePropertyChanged("CheckResult");
            }
        }

        public double CR_SOC
        {
            get
            {
                return this._CR_SOC;
            }
            set
            {
                this._CR_SOC = value;
                base.RaisePropertyChanged("CR_SOC");
            }
        }

        public byte IsCheckResult
        {
            get
            {
                return this._IsCheckResult;
            }
            set
            {
                this._IsCheckResult = value;
                switch (value)
                {
                    case 0:
                        this.CheckResult = "NULL..";
                        break;

                    case 1:
                        this.CheckResult = "PASS";
                        break;

                    case 2:
                        this.CheckResult = "NG";
                        break;

                    default:
                        this.CheckResult = "";
                        break;
                }
                base.RaisePropertyChanged("IsCheckResult");
            }
        }

        public string M_BMUID
        {
            get
            {
                return this._M_BMUID;
            }
            set
            {
                this._M_BMUID = value;
                base.RaisePropertyChanged("M_BMUID");
            }
        }

        public string M_NowHeadWearVer
        {
            get
            {
                return this._M_NowHeadWearVer;
            }
            set
            {
                this._M_NowHeadWearVer = value;
                base.RaisePropertyChanged("M_NowHeadWearVer");
            }
        }

        public string M_NowSoftVer
        {
            get
            {
                return this._M_NowSoftVer;
            }
            set
            {
                this._M_NowSoftVer = value;
                base.RaisePropertyChanged("M_NowSoftVer");
            }
        }

        public string M_ReserveBMUID
        {
            get
            {
                return this._M_ReserveBMUID;
            }
            set
            {
                this._M_ReserveBMUID = value;
                base.RaisePropertyChanged("M_ReserveBMUID");
            }
        }

        public string M_ReserveHeadwearVer
        {
            get
            {
                return this._M_ReserveHeadwearVer;
            }
            set
            {
                this._M_ReserveHeadwearVer = value;
                base.RaisePropertyChanged("M_ReserveHeadwearVer");
            }
        }

        public string M_ReserveSoftVer
        {
            get
            {
                return this._M_ReserveSoftVer;
            }
            set
            {
                this._M_ReserveSoftVer = value;
                base.RaisePropertyChanged("M_ReserveSoftVer");
            }
        }

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
