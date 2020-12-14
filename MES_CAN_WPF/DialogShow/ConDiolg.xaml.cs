using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MES_CAN_WPF
{
    /// <summary>
    /// ConDiolg.xaml 的交互逻辑
    /// </summary>
    public partial class ConDiolg : Window
    {
        public ConDiolg()
        {
            InitializeComponent();
            #region 初始化界面
            #region 设备索引号
            List<comombox> list = new List<comombox>();
            list.Add(new comombox { Name = "设备索引号0", Value = 0 });
            list.Add(new comombox { Name = "设备索引号1", Value = 1 });
            list.Add(new comombox { Name = "设备索引号2", Value = 2 });
            list.Add(new comombox { Name = "设备索引号3", Value = 3 });
            list.Add(new comombox { Name = "设备索引号4", Value = 4 });
            list.Add(new comombox { Name = "设备索引号5", Value = 5 });
            list.Add(new comombox { Name = "设备索引号6", Value = 6 });
            list.Add(new comombox { Name = "设备索引号7", Value = 7 });
            Comb_FixIdx.ItemsSource = list;
            Comb_FixIdx.DisplayMemberPath = "Name";
            Comb_FixIdx.SelectedValuePath = "Value";
            Comb_FixIdx.SelectedValue = 0;
            #endregion
            #region CAN通道号
            list = new List<comombox>();
            list.Add(new comombox { Name = "通道一", Value = 0 });
            list.Add(new comombox { Name = "通道2", Value = 1 });
            Comb_CANPass.ItemsSource = list;
            Comb_CANPass.DisplayMemberPath = "Name";
            Comb_CANPass.SelectedValuePath = "Value";
            Comb_CANPass.SelectedValue = 0;
            #endregion
            #region CAN波特率
            list = new List<comombox>();
            list.Add(new comombox { Name = "5 Baud", Value = 5 });
            list.Add(new comombox { Name = "10 Baud", Value = 10 });
            list.Add(new comombox { Name = "20 Baud", Value = 20 });
            list.Add(new comombox { Name = "50 Baud", Value = 50 });
            list.Add(new comombox { Name = "100 Baud", Value = 100 });
            list.Add(new comombox { Name = "125 Baud", Value = 125 });
            list.Add(new comombox { Name = "250 Baud", Value = 250 });
            list.Add(new comombox { Name = "500 Baud", Value = 500 });
            list.Add(new comombox { Name = "800 Baud", Value = 800 });
            Comb_CANBaud.ItemsSource = list;
            Comb_CANBaud.DisplayMemberPath = "Name";
            Comb_CANBaud.SelectedValuePath = "Value";
            Comb_CANBaud.SelectedValue = 500;
            #endregion
            #region 界面
           
            list = new List<comombox>();
            list.Add(new comombox { Name = "CUSTOM", Value = 0 });
            list.Add(new comombox { Name="EXCL",Value=1});
            Comb_Target.ItemsSource = list;
            Comb_Target.DisplayMemberPath = "Name";
            Comb_Target.SelectedValuePath = "Value";
            Comb_Target.SelectedValue = 0;
            #endregion


            this.Bt_CON.IsEnabled = true;
            this.Bt_DisCON.IsEnabled = false;
            #endregion

            this.connect += new conn(GlobalVariable.CANOperate.Open);
            this.DisConnect += new DisConn(GlobalVariable.CANOperate.Close);
        }

        public delegate int conn();
        /// <summary>
        /// 连接事件
        /// </summary>
        public event conn connect;

        public delegate bool DisConn();
        /// <summary>
        /// 取消连接
        /// </summary>
        public event DisConn DisConnect;

        /// <summary>
        /// combox类
        /// </summary>
        private class comombox
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }
           

        private void Bt_CON_Click(object sender, RoutedEventArgs e)
        {
            if (connect() > 0)
            {
                this.Bt_CON.IsEnabled = false;
                this.Bt_DisCON.IsEnabled = true;
            }
        }

        private void Bt_DisCON_Click(object sender, RoutedEventArgs e)
        {
            if (DisConnect())
            {
                this.Bt_CON.IsEnabled = true;
                this.Bt_DisCON.IsEnabled = false;
            }

        }
    }
}
