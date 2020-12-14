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
using System.IO;

namespace MES_CAN_WPF
{
    /// <summary>
    /// Program_Dialog.xaml 的交互逻辑
    /// </summary>
    public partial class Program_Dialog : Window
    {
        public Program_Dialog()
        {
            InitializeComponent();
            GlobalVariable.File_Path = AppDomain.CurrentDomain.BaseDirectory+ @"\Config_File";
            DirectoryInfo file = new DirectoryInfo(GlobalVariable.File_Path);
            foreach (var fle in file.GetFiles())
            {
                this.Com_Program.Items.Add(fle.Name);

            }
        }
    }
}
