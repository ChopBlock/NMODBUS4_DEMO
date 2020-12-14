namespace NMODBUS4_DEMO
{
    partial class PLC_RW_Prm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_RW_Path = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // txt_RW_Path
            // 
            // 
            // 
            // 
            this.txt_RW_Path.Border.Class = "TextBoxBorder";
            this.txt_RW_Path.ButtonCustom.Visible = true;
            this.txt_RW_Path.Location = new System.Drawing.Point(7, 39);
            this.txt_RW_Path.Name = "txt_RW_Path";
            this.txt_RW_Path.Size = new System.Drawing.Size(265, 21);
            this.txt_RW_Path.TabIndex = 0;
            this.txt_RW_Path.ButtonCustomClick += new System.EventHandler(this.txt_RW_Path_ButtonCustomClick);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(55, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "文件地址";
            // 
            // PLC_RW_Prm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txt_RW_Path);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PLC_RW_Prm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PLC_RW_Prm";
            this.Load += new System.EventHandler(this.PLC_RW_Prm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX txt_RW_Path;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}