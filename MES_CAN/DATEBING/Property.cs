using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MES_CAN
{
    public class Property : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
                //this.PropertyChanged.(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
