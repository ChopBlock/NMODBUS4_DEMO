using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;

namespace MES_CAN
{
    public partial class Chos : Office2007Form
    {
        public Chos()
        {
            this.EnableGlass = false;
            InitializeComponent();
        }

        private void Chos_Load(object sender, EventArgs e)
        {
            this.CloseEnabled = false;
             string path = @"\\192.168.100.252\fct数据\" + GlobalVariable.filepath + "" + @"\config_file";
        //    IniFile file = new IniFile(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\config_file
             DirectoryInfo root = new DirectoryInfo(path);
             FileInfo[] files = root.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                com_file.Items.Add(files[i].Name);
            }
            com_file.SelectedIndex = 0;

            IniFile file = new IniFile(@"\\192.168.100.252\fct数据\" + GlobalVariable.filepath + "" + @"\config.ini");

            string[] skinNames = { "Office2007Blue", "Office2007Silver", "Office2007Black", "Office2010Silver", "Office2007VistaGlass", "Windows7Blue" };
            for (int i = 0; i < skinNames.Length; i++)
            {
                com_sty.Items.Add(skinNames[i]);
            }
            com_sty.Text = file.IniReadValue("style", "index"); 




        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            GlobalVariable.file_path = com_file.Text;
            this.DialogResult = DialogResult.OK;

            IniFile file = new IniFile(@"\\192.168.100.252\fct数据\" + GlobalVariable.filepath + "" + @"\config.ini");


            if (checkBoxX1.Checked)
            { file.IniWriteValue("MESEB", "FLAG", "1"); }
            else
            { file.IniWriteValue("MESEB", "FLAG", "0"); }
            this.Close();
        }

        private void Chos_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            IniFile file = new IniFile(@"\\192.168.100.252\fct数据\" + GlobalVariable.filepath + "" + @"\config.ini");


            if (checkBoxX1.Checked)
            { file.IniWriteValue("MESEB", "FLAG", "1"); }
            else
            { file.IniWriteValue("MESEB", "FLAG", "0"); }
        }

        private void Chos_FormClosing(object sender, FormClosingEventArgs e)
        {
          


        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void com_sty_Click(object sender, EventArgs e)
        {

        }

        private void com_sty_SelectedIndexChanged(object sender, EventArgs e)
        {
            IniFile file = new IniFile(@"\\192.168.100.252\fct数据\" + GlobalVariable.filepath + "" + @"\config.ini");

            CAN_RECV cr = new CAN_RECV();
            switch (this.com_sty.Text)
            {
                case "Office2007Blue":
                    cr.styleManager1.ManagerStyle = eStyle.Office2007Blue;
                    break;
                case "Office2007Silver":
                    cr.styleManager1.ManagerStyle = eStyle.Office2007Silver;
                    break;
                case "Office2007Black":
                    cr.styleManager1.ManagerStyle = eStyle.Office2007Black;
                    break;
                case "Office2010Silver":
                    cr.styleManager1.ManagerStyle = eStyle.Office2010Silver;
                    break;
                case "Office2007VistaGlass":
                    cr.styleManager1.ManagerStyle = eStyle.Office2007VistaGlass;
                    break;
                case "Windows7Blue":
                    cr.styleManager1.ManagerStyle = eStyle.Windows7Blue;
                    break;
            }
            file.IniWriteValue("style", "index", this.com_sty.Text);
        }
    }
}
