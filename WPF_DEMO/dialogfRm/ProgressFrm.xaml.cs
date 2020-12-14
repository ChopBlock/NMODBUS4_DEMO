using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_DEMO
{
    /// <summary>
    /// ProgressFrm.xaml 的交互逻辑
    /// </summary>
    public partial class ProgressFrm : Window
    {
        public ProgressFrm()
        {
            GlobalVariable.stmod.Vale = 0;
            InitializeComponent();
            this.Convert += new Convrt(GlobalVariable.modbusOperate.read_Exce_dt);
            Prgress.DataContext = GlobalVariable.stmod;
        }

        /// <summary>
        /// 转换委托
        /// </summary>
        public delegate bool Convrt();
        /// <summary>
        /// 转换事件
        /// </summary>
        public event Convrt Convert;
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
               
                Convert();
                // GlobalVariable.Excell_Task.Wait();

                //if (Convert())
                //{

                //   this.DialogResult = false;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Prgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.Prgress.Value == this.Prgress.Maximum)
            {


                //this.DialogResult = false;
                this.Hide();
               // GlobalVariable.PrgressFrm.DialogResult = false;
            }
        }
    }
}
