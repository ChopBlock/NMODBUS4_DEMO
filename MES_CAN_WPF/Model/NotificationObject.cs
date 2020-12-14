using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MES_CAN_WPF
{
   

        public class NotificationObject : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public void RaisePropertyChanged(string propertyName)
            {
        
                if (this.PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
                }
            
            
            }
        }

    
}
