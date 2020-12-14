using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;

namespace NMODBUS4_DEMO
{
    public partial class PLC_RW_Prm :Office2007Form
    {
        public PLC_RW_Prm()
        {
            this.EnableGlass = false;
            InitializeComponent();
        }

        private void PLC_RW_Prm_Load(object sender, EventArgs e)
        {
          
            this.txt_RW_Path.Text = GlobalVariable.Read_Write;
        }

        private void txt_RW_Path_ButtonCustomClick(object sender, EventArgs e)
        {
            OpenFileDialog OpfileDio = new OpenFileDialog();

            if (DialogResult.OK == OpfileDio.ShowDialog())
            {
                this.txt_RW_Path.Text = Application.StartupPath + @"\Read&Write\" + OpfileDio.SafeFileName;

                GlobalVariable.Read_Write= this.txt_RW_Path.Text;

            }
        }
    }
}
