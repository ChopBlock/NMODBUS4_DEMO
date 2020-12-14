namespace NMODBUS4_DEMO
{
    partial class PLC_CON_Prm
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
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.Grp_tcp = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.TXT_port = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.GRP_RTU = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel5 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel4 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.com_stopBits = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem17 = new DevComponents.Editors.ComboItem();
            this.comboItem18 = new DevComponents.Editors.ComboItem();
            this.com_parity = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem12 = new DevComponents.Editors.ComboItem();
            this.comboItem13 = new DevComponents.Editors.ComboItem();
            this.comboItem14 = new DevComponents.Editors.ComboItem();
            this.com_dataBits = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem15 = new DevComponents.Editors.ComboItem();
            this.comboItem16 = new DevComponents.Editors.ComboItem();
            this.COM_baudRate = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.comboItem7 = new DevComponents.Editors.ComboItem();
            this.comboItem8 = new DevComponents.Editors.ComboItem();
            this.comboItem9 = new DevComponents.Editors.ComboItem();
            this.comboItem10 = new DevComponents.Editors.ComboItem();
            this.comboItem11 = new DevComponents.Editors.ComboItem();
            this.com_Prt_name = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.BT_con_CANCEL = new DevComponents.DotNetBar.ButtonX();
            this.BT_con_OK = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.com_conn_TYPE = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.com_Mb_Rtu = new DevComponents.Editors.ComboItem();
            this.com_Mb_TCP = new DevComponents.Editors.ComboItem();
            this.TXT_IP = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panelEx1.SuspendLayout();
            this.Grp_tcp.SuspendLayout();
            this.GRP_RTU.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.Grp_tcp);
            this.panelEx1.Controls.Add(this.GRP_RTU);
            this.panelEx1.Controls.Add(this.BT_con_CANCEL);
            this.panelEx1.Controls.Add(this.BT_con_OK);
            this.panelEx1.Controls.Add(this.groupPanel1);
            this.panelEx1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(380, 398);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // Grp_tcp
            // 
            this.Grp_tcp.CanvasColor = System.Drawing.SystemColors.Control;
            this.Grp_tcp.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.Grp_tcp.Controls.Add(this.TXT_IP);
            this.Grp_tcp.Controls.Add(this.TXT_port);
            this.Grp_tcp.Controls.Add(this.labelX2);
            this.Grp_tcp.Controls.Add(this.labelX1);
            this.Grp_tcp.DrawTitleBox = false;
            this.Grp_tcp.Location = new System.Drawing.Point(12, 282);
            this.Grp_tcp.Name = "Grp_tcp";
            this.Grp_tcp.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Grp_tcp.Size = new System.Drawing.Size(365, 104);
            // 
            // 
            // 
            this.Grp_tcp.Style.BackColor = System.Drawing.SystemColors.Window;
            this.Grp_tcp.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.Grp_tcp.Style.BackColorGradientAngle = 90;
            this.Grp_tcp.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.Grp_tcp.Style.BorderBottomWidth = 1;
            this.Grp_tcp.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.Grp_tcp.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.Grp_tcp.Style.BorderLeftWidth = 1;
            this.Grp_tcp.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.Grp_tcp.Style.BorderRightWidth = 1;
            this.Grp_tcp.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.Grp_tcp.Style.BorderTopWidth = 1;
            this.Grp_tcp.Style.Class = "";
            this.Grp_tcp.Style.CornerDiameter = 4;
            this.Grp_tcp.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.Grp_tcp.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.Grp_tcp.StyleMouseDown.Class = "";
            // 
            // 
            // 
            this.Grp_tcp.StyleMouseOver.Class = "";
            this.Grp_tcp.TabIndex = 2;
            this.Grp_tcp.Text = "Tcp/ip server";
            // 
            // TXT_port
            // 
            // 
            // 
            // 
            this.TXT_port.Border.Class = "TextBoxBorder";
            this.TXT_port.Location = new System.Drawing.Point(295, 41);
            this.TXT_port.Name = "TXT_port";
            this.TXT_port.Size = new System.Drawing.Size(60, 26);
            this.TXT_port.TabIndex = 7;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.Location = new System.Drawing.Point(295, 12);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(43, 23);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "Port";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Location = new System.Drawing.Point(3, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(99, 23);
            this.labelX1.TabIndex = 4;
            this.labelX1.Text = "IP Address";
            // 
            // GRP_RTU
            // 
            this.GRP_RTU.CanvasColor = System.Drawing.SystemColors.Control;
            this.GRP_RTU.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.GRP_RTU.Controls.Add(this.groupPanel5);
            this.GRP_RTU.Controls.Add(this.groupPanel4);
            this.GRP_RTU.Controls.Add(this.com_stopBits);
            this.GRP_RTU.Controls.Add(this.com_parity);
            this.GRP_RTU.Controls.Add(this.com_dataBits);
            this.GRP_RTU.Controls.Add(this.COM_baudRate);
            this.GRP_RTU.Controls.Add(this.com_Prt_name);
            this.GRP_RTU.DrawTitleBox = false;
            this.GRP_RTU.Location = new System.Drawing.Point(12, 94);
            this.GRP_RTU.Name = "GRP_RTU";
            this.GRP_RTU.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.GRP_RTU.Size = new System.Drawing.Size(365, 182);
            // 
            // 
            // 
            this.GRP_RTU.Style.BackColor = System.Drawing.SystemColors.Window;
            this.GRP_RTU.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.GRP_RTU.Style.BackColorGradientAngle = 90;
            this.GRP_RTU.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GRP_RTU.Style.BorderBottomWidth = 1;
            this.GRP_RTU.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.GRP_RTU.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GRP_RTU.Style.BorderLeftWidth = 1;
            this.GRP_RTU.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GRP_RTU.Style.BorderRightWidth = 1;
            this.GRP_RTU.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GRP_RTU.Style.BorderTopWidth = 1;
            this.GRP_RTU.Style.Class = "";
            this.GRP_RTU.Style.CornerDiameter = 4;
            this.GRP_RTU.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.GRP_RTU.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.GRP_RTU.StyleMouseDown.Class = "";
            // 
            // 
            // 
            this.GRP_RTU.StyleMouseOver.Class = "";
            this.GRP_RTU.TabIndex = 1;
            this.GRP_RTU.Text = "Serial Settings";
            // 
            // groupPanel5
            // 
            this.groupPanel5.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel5.DrawTitleBox = false;
            this.groupPanel5.Location = new System.Drawing.Point(108, 86);
            this.groupPanel5.Name = "groupPanel5";
            this.groupPanel5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupPanel5.Size = new System.Drawing.Size(247, 71);
            // 
            // 
            // 
            this.groupPanel5.Style.BackColor = System.Drawing.SystemColors.Window;
            this.groupPanel5.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel5.Style.BackColorGradientAngle = 90;
            this.groupPanel5.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderBottomWidth = 1;
            this.groupPanel5.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel5.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderLeftWidth = 1;
            this.groupPanel5.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderRightWidth = 1;
            this.groupPanel5.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderTopWidth = 1;
            this.groupPanel5.Style.Class = "";
            this.groupPanel5.Style.CornerDiameter = 4;
            this.groupPanel5.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel5.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel5.StyleMouseDown.Class = "";
            // 
            // 
            // 
            this.groupPanel5.StyleMouseOver.Class = "";
            this.groupPanel5.TabIndex = 2;
            this.groupPanel5.Text = "Connection";
            // 
            // groupPanel4
            // 
            this.groupPanel4.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel4.DrawTitleBox = false;
            this.groupPanel4.Location = new System.Drawing.Point(108, 35);
            this.groupPanel4.Name = "groupPanel4";
            this.groupPanel4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupPanel4.Size = new System.Drawing.Size(149, 45);
            // 
            // 
            // 
            this.groupPanel4.Style.BackColor = System.Drawing.SystemColors.Window;
            this.groupPanel4.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel4.Style.BackColorGradientAngle = 90;
            this.groupPanel4.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderBottomWidth = 1;
            this.groupPanel4.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel4.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderLeftWidth = 1;
            this.groupPanel4.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderRightWidth = 1;
            this.groupPanel4.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderTopWidth = 1;
            this.groupPanel4.Style.Class = "";
            this.groupPanel4.Style.CornerDiameter = 4;
            this.groupPanel4.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel4.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel4.StyleMouseDown.Class = "";
            // 
            // 
            // 
            this.groupPanel4.StyleMouseOver.Class = "";
            this.groupPanel4.TabIndex = 1;
            this.groupPanel4.Text = "Connection";
            // 
            // com_stopBits
            // 
            this.com_stopBits.DisplayMember = "Text";
            this.com_stopBits.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.com_stopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_stopBits.FormattingEnabled = true;
            this.com_stopBits.ItemHeight = 20;
            this.com_stopBits.Items.AddRange(new object[] {
            this.comboItem17,
            this.comboItem18});
            this.com_stopBits.Location = new System.Drawing.Point(3, 131);
            this.com_stopBits.Name = "com_stopBits";
            this.com_stopBits.Size = new System.Drawing.Size(99, 26);
            this.com_stopBits.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.com_stopBits.TabIndex = 5;
            // 
            // comboItem17
            // 
            this.comboItem17.Text = "1";
            // 
            // comboItem18
            // 
            this.comboItem18.Text = "2";
            // 
            // com_parity
            // 
            this.com_parity.DisplayMember = "Text";
            this.com_parity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.com_parity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_parity.FormattingEnabled = true;
            this.com_parity.ItemHeight = 20;
            this.com_parity.Items.AddRange(new object[] {
            this.comboItem12,
            this.comboItem13,
            this.comboItem14});
            this.com_parity.Location = new System.Drawing.Point(3, 99);
            this.com_parity.Name = "com_parity";
            this.com_parity.Size = new System.Drawing.Size(99, 26);
            this.com_parity.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.com_parity.TabIndex = 4;
            // 
            // comboItem12
            // 
            this.comboItem12.Text = "奇";
            // 
            // comboItem13
            // 
            this.comboItem13.Text = "偶";
            // 
            // comboItem14
            // 
            this.comboItem14.Text = "无";
            // 
            // com_dataBits
            // 
            this.com_dataBits.DisplayMember = "Text";
            this.com_dataBits.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.com_dataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_dataBits.FormattingEnabled = true;
            this.com_dataBits.ItemHeight = 20;
            this.com_dataBits.Items.AddRange(new object[] {
            this.comboItem15,
            this.comboItem16});
            this.com_dataBits.Location = new System.Drawing.Point(3, 67);
            this.com_dataBits.Name = "com_dataBits";
            this.com_dataBits.Size = new System.Drawing.Size(99, 26);
            this.com_dataBits.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.com_dataBits.TabIndex = 3;
            // 
            // comboItem15
            // 
            this.comboItem15.Text = "7";
            // 
            // comboItem16
            // 
            this.comboItem16.Text = "8";
            // 
            // COM_baudRate
            // 
            this.COM_baudRate.DisplayMember = "Text";
            this.COM_baudRate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.COM_baudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COM_baudRate.FormattingEnabled = true;
            this.COM_baudRate.ItemHeight = 20;
            this.COM_baudRate.Items.AddRange(new object[] {
            this.comboItem2,
            this.comboItem3,
            this.comboItem4,
            this.comboItem5,
            this.comboItem6,
            this.comboItem7,
            this.comboItem8,
            this.comboItem9,
            this.comboItem10,
            this.comboItem11});
            this.COM_baudRate.Location = new System.Drawing.Point(3, 35);
            this.COM_baudRate.Name = "COM_baudRate";
            this.COM_baudRate.Size = new System.Drawing.Size(99, 26);
            this.COM_baudRate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.COM_baudRate.TabIndex = 2;
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "300";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "600";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "1200";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "2400";
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "4800";
            // 
            // comboItem7
            // 
            this.comboItem7.Text = "9600";
            // 
            // comboItem8
            // 
            this.comboItem8.Text = "14400";
            // 
            // comboItem9
            // 
            this.comboItem9.Text = "19200";
            // 
            // comboItem10
            // 
            this.comboItem10.Text = "38400";
            // 
            // comboItem11
            // 
            this.comboItem11.Text = "56000";
            // 
            // com_Prt_name
            // 
            this.com_Prt_name.DisplayMember = "Text";
            this.com_Prt_name.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.com_Prt_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_Prt_name.FormattingEnabled = true;
            this.com_Prt_name.ItemHeight = 20;
            this.com_Prt_name.Location = new System.Drawing.Point(3, 3);
            this.com_Prt_name.Name = "com_Prt_name";
            this.com_Prt_name.Size = new System.Drawing.Size(267, 26);
            this.com_Prt_name.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.com_Prt_name.TabIndex = 1;
            // 
            // BT_con_CANCEL
            // 
            this.BT_con_CANCEL.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BT_con_CANCEL.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BT_con_CANCEL.Location = new System.Drawing.Point(293, 45);
            this.BT_con_CANCEL.Name = "BT_con_CANCEL";
            this.BT_con_CANCEL.Size = new System.Drawing.Size(75, 23);
            this.BT_con_CANCEL.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BT_con_CANCEL.TabIndex = 0;
            this.BT_con_CANCEL.Text = "CANCEL";
            // 
            // BT_con_OK
            // 
            this.BT_con_OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BT_con_OK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BT_con_OK.Location = new System.Drawing.Point(293, 16);
            this.BT_con_OK.Name = "BT_con_OK";
            this.BT_con_OK.Size = new System.Drawing.Size(75, 23);
            this.BT_con_OK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BT_con_OK.TabIndex = 0;
            this.BT_con_OK.Text = "OK";
            this.BT_con_OK.Click += new System.EventHandler(this.BT_con_OK_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.com_conn_TYPE);
            this.groupPanel1.DrawTitleBox = false;
            this.groupPanel1.Location = new System.Drawing.Point(12, 16);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupPanel1.Size = new System.Drawing.Size(275, 52);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor = System.Drawing.SystemColors.Window;
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.Class = "";
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.Class = "";
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.Class = "";
            this.groupPanel1.TabIndex = 0;
            this.groupPanel1.Text = "Connection";
            // 
            // com_conn_TYPE
            // 
            this.com_conn_TYPE.DisplayMember = "Text";
            this.com_conn_TYPE.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.com_conn_TYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_conn_TYPE.FormattingEnabled = true;
            this.com_conn_TYPE.ItemHeight = 20;
            this.com_conn_TYPE.Items.AddRange(new object[] {
            this.com_Mb_Rtu,
            this.com_Mb_TCP});
            this.com_conn_TYPE.Location = new System.Drawing.Point(3, 3);
            this.com_conn_TYPE.Name = "com_conn_TYPE";
            this.com_conn_TYPE.Size = new System.Drawing.Size(267, 26);
            this.com_conn_TYPE.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.com_conn_TYPE.TabIndex = 0;
            this.com_conn_TYPE.SelectedIndexChanged += new System.EventHandler(this.com_conn_TYPE_SelectedIndexChanged);
            // 
            // com_Mb_Rtu
            // 
            this.com_Mb_Rtu.Text = "Modbus Rtu";
            // 
            // com_Mb_TCP
            // 
            this.com_Mb_TCP.Text = "Modbus_TCP/IP";
            // 
            // TXT_IP
            // 
            // 
            // 
            // 
            this.TXT_IP.Border.Class = "TextBoxBorder";
            this.TXT_IP.Location = new System.Drawing.Point(3, 41);
            this.TXT_IP.Name = "TXT_IP";
            this.TXT_IP.Size = new System.Drawing.Size(267, 26);
            this.TXT_IP.TabIndex = 8;
            // 
            // PLC_CON_Prm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 398);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PLC_CON_Prm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PLC_CON_Prm";
            this.Load += new System.EventHandler(this.PLC_CON_Prm_Load);
            this.panelEx1.ResumeLayout(false);
            this.Grp_tcp.ResumeLayout(false);
            this.GRP_RTU.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel Grp_tcp;
        private DevComponents.DotNetBar.Controls.GroupPanel GRP_RTU;
        private DevComponents.DotNetBar.ButtonX BT_con_CANCEL;
        private DevComponents.DotNetBar.ButtonX BT_con_OK;
        private DevComponents.DotNetBar.Controls.ComboBoxEx com_conn_TYPE;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel5;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx com_stopBits;
        private DevComponents.DotNetBar.Controls.ComboBoxEx com_parity;
        private DevComponents.DotNetBar.Controls.ComboBoxEx com_dataBits;
        private DevComponents.DotNetBar.Controls.ComboBoxEx COM_baudRate;
        private DevComponents.DotNetBar.Controls.ComboBoxEx com_Prt_name;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX TXT_port;
        private DevComponents.Editors.ComboItem com_Mb_Rtu;
        private DevComponents.Editors.ComboItem com_Mb_TCP;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.Editors.ComboItem comboItem6;
        private DevComponents.Editors.ComboItem comboItem7;
        private DevComponents.Editors.ComboItem comboItem8;
        private DevComponents.Editors.ComboItem comboItem9;
        private DevComponents.Editors.ComboItem comboItem10;
        private DevComponents.Editors.ComboItem comboItem11;
        private DevComponents.Editors.ComboItem comboItem15;
        private DevComponents.Editors.ComboItem comboItem16;
        private DevComponents.Editors.ComboItem comboItem17;
        private DevComponents.Editors.ComboItem comboItem18;
        private DevComponents.Editors.ComboItem comboItem12;
        private DevComponents.Editors.ComboItem comboItem13;
        private DevComponents.Editors.ComboItem comboItem14;
        private DevComponents.DotNetBar.Controls.TextBoxX TXT_IP;
    }
}