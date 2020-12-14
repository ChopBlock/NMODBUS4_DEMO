using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;

namespace NMODBUS4_DEMO
{
    public partial class fMain :Office2007Form
    {
        public fMain()
        {

            Control.CheckForIllegalCrossThreadCalls = false;
            GlobalVariable.plc_con_prm.OK += new PLC_CON_Prm.CON_OK(GlobalVariable.modbusoperate.dt);
           // listViewEx1.BindingContext = GlobalVariable.MSGLIST_;
            this.EnableGlass = false;
            InitializeComponent();
            this.progressBarX1.DataBindings.Add("Maximum", GlobalVariable.StatusModes, "Maximum");
            this.progressBarX1.DataBindings.Add("Minimum", GlobalVariable.StatusModes, "Minimum");
            this.progressBarX1.DataBindings.Add("Value", GlobalVariable.StatusModes, "NowValue");

           
        }
     

     
       
        private void Form1_Load(object sender, EventArgs e)
        {
            GlobalVariable.Read_Write = Application.StartupPath + @"\Read&Write\PLC读写指令.xlsx";
            this.Text += Assembly.GetExecutingAssembly().GetName().Version.ToString();
          
        }

        private void bt_connect_Click(object sender, EventArgs e)
        {
            if (GlobalVariable.TH_LIST_MSG == null || !GlobalVariable.TH_LIST_MSG.IsAlive)
            {
                GlobalVariable.TH_LIST_MSG = new Thread(new ThreadStart(listmsg));
                GlobalVariable.TH_LIST_MSG.Start();
            }
            GlobalVariable.plc_con_prm.ShowDialog();
            if (GlobalVariable.conn_flag)
            {
                this.bt_connect.Enabled = false;
                this.BT_Disconn.Enabled = true;
            }
        }

        private void BT_RW_Parm_Click(object sender, EventArgs e)
        {

            GlobalVariable.plc_rw_prm.ShowDialog();
        }

        private void buttonItem13_Click(object sender, EventArgs e)
        {
            this.dataGridViewX1.DataSource = GlobalVariable.dt;
          
        }

        private  void listmsg()
        { int i = -1;
            while (true)
            {
                Thread.Sleep(1);
                if (!String.IsNullOrEmpty(GlobalVariable.Read_Value)  )
                    {
                    
                    
                        ListViewItem listitm = new ListViewItem();
                    i = (i + 1);
                    listitm.Text = i.ToString();
                        listitm.SubItems.Add(GlobalVariable.Read_Value);
                    if (listViewEx1.Items.Count >= 100)
                    {
                        listViewEx1.Items.Remove(listViewEx1.Items[0]);
                    }
                        listViewEx1.Items.Add(listitm);
                    this.listViewEx1.Items[this.listViewEx1.Items.Count-1 ].EnsureVisible();
                    // this.listViewEx1.selec;
                    this.listViewEx1.Items[this.listViewEx1.Items.Count-1].Focused = true;
                    GlobalVariable.Read_Value = "";

                    this.listViewEx1.Focus();
                    }
            }

        }

        private void fMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(2);
        }

        private void BT_Disconn_Click(object sender, EventArgs e)
        { try
            {
                this.bt_connect.Enabled = true;
                this.BT_Disconn.Enabled = false;
                GlobalVariable.conn_flag = false;
                if (GlobalVariable.TH_dt.IsAlive && GlobalVariable.TH_dt != null)
                {
                    GlobalVariable.TH_dt.Abort();
                }
                if (GlobalVariable.TH_Master_Read.IsAlive && GlobalVariable.TH_Master_Read != null)
                {
                    GlobalVariable.TH_Master_Read.Abort();
                }
                GlobalVariable.Mdapi.master.Dispose();
                GlobalVariable.Mdapi.port.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            }
    }
}
