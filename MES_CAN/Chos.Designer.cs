namespace MES_CAN
{
    partial class Chos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chos));
            this.com_file = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.checkBoxX1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.style_lab = new DevComponents.DotNetBar.LabelItem();
            this.com_sty = new DevComponents.DotNetBar.ComboBoxItem();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.SuspendLayout();
            // 
            // com_file
            // 
            this.com_file.DisplayMember = "Text";
            this.com_file.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.com_file.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_file.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.com_file.FormattingEnabled = true;
            this.com_file.ItemHeight = 19;
            this.com_file.Location = new System.Drawing.Point(128, 84);
            this.com_file.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.com_file.Name = "com_file";
            this.com_file.Size = new System.Drawing.Size(278, 25);
            this.com_file.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.com_file.TabIndex = 0;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Location = new System.Drawing.Point(29, 81);
            this.labelX1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(99, 37);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "选择程序:";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(29, 176);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 2;
            this.buttonX1.Text = "OK";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // checkBoxX1
            // 
            // 
            // 
            // 
            this.checkBoxX1.BackgroundStyle.Class = "";
            this.checkBoxX1.Checked = true;
            this.checkBoxX1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxX1.CheckValue = "Y";
            this.checkBoxX1.Location = new System.Drawing.Point(29, 125);
            this.checkBoxX1.Name = "checkBoxX1";
            this.checkBoxX1.Size = new System.Drawing.Size(100, 23);
            this.checkBoxX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX1.TabIndex = 3;
            this.checkBoxX1.Text = "MES过站";
            this.checkBoxX1.CheckedChanged += new System.EventHandler(this.checkBoxX1_CheckedChanged);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(144, 176);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 23);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 0;
            this.buttonX2.Text = "CANCEL";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // bar1
            // 
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.style_lab,
            this.com_sty});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(480, 28);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 0;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // style_lab
            // 
            this.style_lab.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.style_lab.Name = "style_lab";
            this.style_lab.SingleLineColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.style_lab.Text = "风格";
            // 
            // com_sty
            // 
            this.com_sty.ComboWidth = 260;
            this.com_sty.DropDownHeight = 106;
            this.com_sty.DropDownWidth = 242;
            this.com_sty.ItemHeight = 17;
            this.com_sty.Name = "com_sty";
            this.com_sty.SelectedIndexChanged += new System.EventHandler(this.com_sty_SelectedIndexChanged);
            this.com_sty.Click += new System.EventHandler(this.com_sty_Click);
            // 
            // Chos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 352);
            this.Controls.Add(this.bar1);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.checkBoxX1);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.com_file);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Chos";
            this.Text = "Chos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Chos_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Chos_FormClosed);
            this.Load += new System.EventHandler(this.Chos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx com_file;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.LabelItem style_lab;
        private DevComponents.DotNetBar.ComboBoxItem com_sty;
    }
}