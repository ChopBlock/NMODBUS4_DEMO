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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MES_CAN_WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GlobalVariable.StatusModel.isCheckResult = 1;

            #region 数据绑定
            this.List_MSG.ItemsSource = GlobalVariable.MSG;
            this.Can_layout.DataContext = GlobalVariable.StatusModel;
            #endregion

            GlobalVariable.MSG.CollectionChanged += MSG_CollectionChanged;
        }

        private void MSG_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.Scrollv_MSG.ScrollToEnd();
          
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)GlobalVariable.ConDiolg.ShowDialog())
            {

            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            GlobalVariable.Program_Dialog.ShowDialog();
        }
    }
}
