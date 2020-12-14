using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace WPF_DEMO
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MODBUS_WPF : Window
    {
        public MODBUS_WPF()
        {
            InitializeComponent();

           // this.Convert += new Convrt(GlobalVariable.modbusOperate.read_Exce_dt);
            

            GlobalVariable.Excell_pth = Directory.GetCurrentDirectory() + GlobalVariable.Excell_pth;
            // this.Excell_DGV.ItemsSource = GlobalVariable.Modbus_dt.DefaultView;
            this.Excell_DGV.ItemsSource = GlobalVariable.Excell_Coll;
             this.LstView.ItemsSource = GlobalVariable.Message;
            //   this.Main_Grid.DataContext = GlobalVariable.stmod;
         
        }
        
       
    //    /// <summary>
    //    /// 转换委托
    //    /// </summary>
    ////    public delegate bool Convrt();
    //    /// <summary>
    //    /// 转换事件
    //    /// </summary>
    //  //  public event Convrt Convert;

        private void bt_con_Click(object sender, RoutedEventArgs e)
        {
            // GlobalVariable.con.Set();
            GlobalVariable.Bt_Cancel = false;
            if (!(bool)GlobalVariable.Con_APIFrm.ShowDialog())
            {
                this.bt_con.IsEnabled = GlobalVariable.Connected = true ? false : true;
                this.bt_con_Abort.IsEnabled = GlobalVariable.Connected = true ? true : false;

            }
        }

        private void MeItm_dt_Click(object sender, RoutedEventArgs e)
        {

            try
            {

               // ProgressFrm p = new ProgressFrm();

                if (!(bool)GlobalVariable.PrgressFrm.ShowDialog())
                {


                }

            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
            

            
            

            
         
           
        

        }

        private void LstView_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            LstView.SelectedIndex = LstView.Items.Count - 1;
            LstView.ScrollIntoView(LstView.SelectedItem);
        }

        private void LstView_LayoutUpdated(object sender, EventArgs e)
        {
            LstView.SelectedIndex = LstView.Items.Count - 1;
           
            LstView.ScrollIntoView(LstView.SelectedItem);
           
            if (GlobalVariable.Message.Count >= 1000)
            {

                GlobalVariable.Message.Clear();
            }
            if (GlobalVariable.Message.Count > 50)
            { GlobalVariable.Message.RemoveAt(0);
                //
            }

        }

        private void bt_con_Abort_Click(object sender, RoutedEventArgs e)
        {
            //   GlobalVariable.con.Reset();
            GlobalVariable.Bt_Cancel = true;

            this.bt_con.IsEnabled = true;
            this.bt_con_Abort.IsEnabled =  false;

            GlobalVariable.Port.Close();
            GlobalVariable.Port.Dispose();
            GlobalVariable.Master.Dispose();
           
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
