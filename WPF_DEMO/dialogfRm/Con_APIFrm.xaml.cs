using System;
using System.Collections.Generic;
using System.IO.Ports;
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

namespace WPF_DEMO
{
    /// <summary>
    /// Con_APIFrm.xaml 的交互逻辑
    /// </summary>
    public partial class Con_APIFrm : Window
    {
        public Con_APIFrm()
        {
            InitializeComponent();
            Com_init();
        }

        #region 界面初始化
        /// <summary>
        /// combox赋值类
        /// </summary>
        public class ComomboxItem
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }
        public class ComomboxItem_parity
        {
            public string Name { get; set; }
            public Parity Value { get; set; }
        }
        public class ComomboxItem_stop_bit
        {
            public string Name { get; set; }
            public StopBits Value { get; set; }
        }
        /// <summary>
        /// combox初始化赋值
        /// </summary>
        public void Com_init()
        {
            try
            {
                #region Modbus连接选择
                List<ComomboxItem> com_con = new List<ComomboxItem>();
                com_Modbus_chos.DisplayMemberPath = "Name";
                com_Modbus_chos.SelectedValuePath = "Value";
                com_con.Add(new ComomboxItem { Name = "Serial Port", Value = 0 });
                com_con.Add(new ComomboxItem { Name = "Modbus TCP/IP", Value = 1 });
                com_Modbus_chos.ItemsSource = com_con;
                com_Modbus_chos.SelectedValue = 0;

                #endregion
                #region 串口号添加

                string[] portname = SerialPort.GetPortNames();

                foreach (string str in portname)
                {
                    Com_PortName.Items.Add(str);
                }
                for (int i = Com_PortName.Items.Count + 1; i < 50; i++)
                {
                    Com_PortName.Items.Add("COM" + i.ToString());
                }
                Com_PortName.SelectedIndex = 0;


                #endregion

                #region 串口波特率
                List<ComomboxItem> com_Baud = new List<ComomboxItem>();
                Com_Baud.DisplayMemberPath = "Name";
                Com_Baud.SelectedValuePath = "Value";
                com_Baud.Add(new ComomboxItem { Name = "300 Baud", Value = 300 });
                com_Baud.Add(new ComomboxItem { Name = "600 Baud", Value = 600 });
                com_Baud.Add(new ComomboxItem { Name = "1200 Baud", Value = 1200 });
                com_Baud.Add(new ComomboxItem { Name = "2400 Baud", Value = 2400 });
                com_Baud.Add(new ComomboxItem { Name = "4800 Baud", Value = 4800 });
                com_Baud.Add(new ComomboxItem { Name = "9600 Baud", Value = 9600 });
                com_Baud.Add(new ComomboxItem { Name = "14400 Baud", Value = 14400 });
                com_Baud.Add(new ComomboxItem { Name = "19200 Baud", Value = 19200 });
                com_Baud.Add(new ComomboxItem { Name = "38400 Baud", Value = 38400 });
                com_Baud.Add(new ComomboxItem { Name = "56000 Baud", Value = 56000 });
                com_Baud.Add(new ComomboxItem { Name = "57600 Baud", Value = 57600 });
                com_Baud.Add(new ComomboxItem { Name = "115200 Baud", Value = 115200 });
                com_Baud.Add(new ComomboxItem { Name = "128000 Baud", Value = 128000 });
                com_Baud.Add(new ComomboxItem { Name = "256000 Baud", Value = 256000 });


                Com_Baud.ItemsSource = com_Baud;
                Com_Baud.SelectedValue = 9600;
                #endregion

                #region 数据位
                List<ComomboxItem> DateBit = new List<ComomboxItem>();
                Com_Date_bits.DisplayMemberPath = "Name";
                Com_Date_bits.SelectedValuePath = "Value";
                DateBit.Add(new ComomboxItem { Name = "7 Date bits", Value= 7});
                DateBit.Add(new ComomboxItem { Name = "8 Date bits", Value = 8});
                Com_Date_bits.ItemsSource = DateBit;
                Com_Date_bits.SelectedValue = 8;

                #endregion

                #region 校验位
                List<ComomboxItem_parity> com_parity = new List<ComomboxItem_parity>();
                Com_Parity.DisplayMemberPath = "Name";
                Com_Parity.SelectedValuePath = "Value";
                com_parity.Add(new ComomboxItem_parity { Name = "奇", Value = Parity.Odd });
                com_parity.Add(new ComomboxItem_parity { Name="偶",Value=Parity.Even});
                com_parity.Add(new ComomboxItem_parity { Name = "无",Value = Parity.None});
                Com_Parity.ItemsSource = com_parity;
                Com_Parity.SelectedValue = Parity.None;
                #endregion

                #region 停止位
                List<ComomboxItem_stop_bit> com_stopbit = new List<ComomboxItem_stop_bit>();
                Com_Stop_Bits.DisplayMemberPath = "Name";
                Com_Stop_Bits.SelectedValuePath = "Value";
              
                com_stopbit.Add(new ComomboxItem_stop_bit { Name = "1 Stop Bit", Value =StopBits.One});
                com_stopbit.Add(new ComomboxItem_stop_bit { Name = "2 Stop Bit", Value =StopBits.Two});
                Com_Stop_Bits.ItemsSource = com_stopbit;
                Com_Stop_Bits.SelectedValue = StopBits.One;

                #endregion
                TXT_IP.Text = "127.0.0.1";
                TXT_Port.Text = "502";

               

            }
            catch
            { }


        }


        #endregion

        /// <summary>
        /// 连接委托
        /// </summary>
        /// <returns></returns>
        public delegate bool CON();
        /// <summary>
        /// 连接事件
        /// </summary>
        public event CON CONNECT;
       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.CONNECT += new CON(GlobalVariable.modbusOperate.con);
        }

        private void com_Modbus_chos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (com_Modbus_chos.SelectedIndex == 0)
            {
                SerialGrid.IsEnabled = true;
                tcpipGrid.IsEnabled = false;
            }
            else
            {
                SerialGrid.IsEnabled = false;
                tcpipGrid.IsEnabled = true;

            }
        }


        private void BT_CONN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (com_Modbus_chos.SelectedIndex == 0)
                {
                    GlobalVariable.Mod = 0;

                    GlobalVariable.PortName = (string)Com_PortName.SelectedValue;
                    GlobalVariable.Baud = (int)Com_Baud.SelectedValue;
                    GlobalVariable.Date_bit = (int)Com_Date_bits.SelectedValue;
                    GlobalVariable.Parity = (Parity)Com_Parity.SelectedValue;
                    GlobalVariable.stopBits = (StopBits)Com_Stop_Bits.SelectedValue;

                }
                else
                {
                    GlobalVariable.Mod = 1;


                    GlobalVariable.IP = TXT_IP.Text;
                    GlobalVariable.port_no = Convert.ToInt32(TXT_Port.Text.ToString());
                }

                if (CONNECT())
                {
                    this.DialogResult = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
