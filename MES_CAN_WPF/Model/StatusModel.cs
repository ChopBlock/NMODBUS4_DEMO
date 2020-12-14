using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_CAN_WPF
{
 public   class StatusModel: NotificationObject
    {

        private int isCheckResult_ = 1;
        /// <summary>
        /// 比对结果0正常状态下 1比对PASS 2比对错误
        /// </summary>
        public int isCheckResult
        {
            get { return isCheckResult_; }
            set { this.isCheckResult_ = value;

                switch (isCheckResult_)
                {
                    case 0:
                        CheckResult_ = "";
                        break;
                    case 1:
                        CheckResult_ = "PASS";
                        break;
                    case 2:
                        CheckResult_ = "NG..";
                        break;
                    default:
                        CheckResult_ = "";
                        break;
                }
                this.RaisePropertyChanged("isCheckResult");
            }
        }

        private string CheckResult_ = "测试";
        /// <summary>
        /// 结果PASS NG 显示
        /// </summary>
        public string CheckResult
        {
            get { return CheckResult_;  }
            set { this.CheckResult_ = value;
                this.RaisePropertyChanged("CheckResult");
            }
        }
    }
}
