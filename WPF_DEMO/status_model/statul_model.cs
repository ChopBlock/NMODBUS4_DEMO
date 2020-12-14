using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace WPF_DEMO

{
    /// <summary>
    /// 静态变量设定 使用
    /// </summary>
 public class statul_model: NotificationObject
    {
        private int _Maxmum=15;
        public int Maxmum
        {
            get { return _Maxmum; }
            set {
                this._Maxmum = value;
                this.RaisePropertyChanged("Maxmum");
                
            }
        }
        private int _Minimum=0;
        public int Minimum
        {
            get { return this._Minimum; }
            set { this._Minimum = value; this.RaisePropertyChanged("Minimum"); }
        }
        private int _Value=0;
        public int Vale
        {
            get { return this._Value; }
            set { this._Value = value;this.RaisePropertyChanged("Vale"); }
        }

    }

}
