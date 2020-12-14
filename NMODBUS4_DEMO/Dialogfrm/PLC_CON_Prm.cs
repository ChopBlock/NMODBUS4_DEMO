using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO.Ports;
using System.Threading.Tasks;

namespace NMODBUS4_DEMO
{
    public partial class PLC_CON_Prm : Office2007Form
    {
        public PLC_CON_Prm()
        {
            this.EnableGlass = false;
            InitializeComponent();
        }
        public delegate void CON_OK();
        public event CON_OK OK;
       // public SerialPort serprt = new SerialPort();
        private void PLC_CON_Prm_Load(object sender, EventArgs e)
        {
            // this.com_conn_TYPE.DataBindings.Add("Text", GlobalVariable.StatusModes, "");
            // this.com_Prt_name.DataBindings.Add("Text", GlobalVariable.StatusModes, "");
            //  this.com_dataBits.DataBindings.Add("Text", GlobalVariable.StatusModes, "");
            //  this.COM_baudRate.DataBindings.Add("Text", GlobalVariable.StatusModes, "");
            //  this.com_parity.DataBindings.Add("Text", GlobalVariable.StatusModes, "");
       

         
            COM_baudRate.SelectedIndex = 5;
            com_parity.SelectedIndex = 2;
            com_dataBits.SelectedIndex = 1;
            com_stopBits.SelectedIndex = 0;



        }

        private void BT_con_OK_Click(object sender, EventArgs e)
        {
            //     try
            //     {
            #region 初始化连接参数
            if (com_conn_TYPE.SelectedIndex == 0)
            {
                GlobalVariable.StatusModes.portName = com_Prt_name.Items[com_Prt_name.SelectedIndex].ToString();
                GlobalVariable.StatusModes.baudRate = Convert.ToInt16(COM_baudRate.Items[COM_baudRate.SelectedIndex].ToString());
                GlobalVariable.StatusModes.com_parity = com_parity.Items[com_parity.SelectedIndex].ToString();
                GlobalVariable.StatusModes.dataBits = Convert.ToInt16(com_dataBits.Items[com_dataBits.SelectedIndex].ToString());
                GlobalVariable.StatusModes.com_stopBits = com_stopBits.Items[com_stopBits.SelectedIndex].ToString();
            }
            else
            {
                GlobalVariable.StatusModes.IP = TXT_IP.Text.ToString().Trim();
                TXT_port.Text = (TXT_port.Text.ToString()) == "" ? "0" : (TXT_port.Text.ToString());
                GlobalVariable.StatusModes.PORT = Convert.ToInt16(TXT_port.Text);
            }



            #endregion
            if (OK == null)
            { GlobalVariable.plc_con_prm.OK += new PLC_CON_Prm.CON_OK(GlobalVariable.modbusoperate.dt); }
                OK();
                this.DialogResult = DialogResult.Cancel;
        //    }
         //   catch (Exception ex)
         //   {
          //      MessageBox.Show(ex.Message);
         //   }
        }

        private void com_conn_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (com_conn_TYPE.SelectedIndex == 0)
            {
                Grp_tcp.Enabled = false;
                GRP_RTU.Enabled = true;
                GlobalVariable.StatusModes.Modbus_Mode_s = 0;

                string[] portname = SerialPort.GetPortNames();
                foreach (string str in portname)
                {
                    com_Prt_name.Items.Add(str);
                }
                com_Prt_name.SelectedIndex = 0;

            }
            else
            {
                Grp_tcp.Enabled = true;
                GRP_RTU.Enabled = false;
                GlobalVariable.StatusModes.Modbus_Mode_s = 1;
            }
        }
    }
}
