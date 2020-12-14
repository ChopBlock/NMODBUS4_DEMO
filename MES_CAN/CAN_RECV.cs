using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.IO;
using System.Threading;
using MESSecurity;
using System.Data.OleDb;

namespace MES_CAN
{
    public partial class CAN_RECV : DevComponents.DotNetBar.Office2007Form
    {

        public CAN_RECV()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.EnableGlass = false;
         
            InitializeComponent();

        }
        Thread TH_MSG = null;

        //private void list_add()
        //{
        //    while (true)

        //    {
        //        //this.list_can.Invoke((MethodInvoker)delegate
        //        //{
        //        //    //foreach (string s in GlobalVariable.now_reserver_value)
        //        //    for (int i = 0; i < GlobalVariable.now_reserver_value.Count; i++)
        //        //    {
        //        //        if (GlobalVariable.now_reserver_value[i].Length > 0)
        //        //        {
        //        //            list_can.Items[i+1].SubItems[1].Text = "77777";
        //        //            //GlobalVariable.now_reserver_value[i] = string.Empty;
        //        //        }

        //        //    }

        //        //});

        //    }

        //}
        #region  MES过站基础信息
        String F = string.Empty;
        private string sSQL;
        int allow = 0;
        private string g_sTerminalID;
        private string g_sFunctionType;
        private string g_sIniFile = (Application.StartupPath + @"\Config.ini");
        private string g_sProcess = "0";
        private string g_sPdline;
        private string g_sStage;
        private string g_sSRouteID;
        private bool StartProcessflag = false;
        private bool EndProcessflag = false;
        private bool ReelCounterflag = false;
        private SoundPlay soundPlay = new SoundPlay();
        private string user = string.Empty;
        private string C_PART_NO = string.Empty;
        int PASS = 0;
        string SN = string.Empty;
        private AutoResetEvent thre_con = new AutoResetEvent(false);
        private AutoResetEvent mes_press = new AutoResetEvent(false);
   /// <summary>
   /// 保存文本csv
   /// </summary>
        StringBuilder sb ;
        DateTime d;
      
        /// <summary>
        /// 项目配置文件
        /// </summary>
        IniFile file;
        /// <summary>
        /// 基本参数配置文件
        /// </summary>
        IniFile file2;
        Thread TH_MSG3;
        DataTable dt=new DataTable();
        Thread TH_MSG6=null;
        Thread TH_MSG7=null; 
     
        
        #endregion
        /// <summary>
        /// MES数据加载
        /// </summary>
        private void Mes_Load(string path)
        {



            user = clsPPSPublicModule.PPSUserName;
            //   allow = Convert.ToInt32(clsPPSPublicModule.PPSAccountAllow);
       
            this.g_sFunctionType = "HostPassStation";
            this.g_sTerminalID = file2.IniReadValue(this.g_sFunctionType, "Process");
            if (this.g_sTerminalID == "0")
            {
                this.Show_Message("请先设定扫描站点", MSGType.Error);
                this.txtBMSID.Enabled = false;
                return;
            }
            this.txtDefect.Text = "N/A";


            if (!this.GetProcess(this.g_sTerminalID, ref this.g_sProcess))
            {
                this.Show_Message("获取该站点制程错误", MSGType.Error);
                this.txtBMSID.Enabled = false;
                return;
            }

            if (!this.LoadStationInfo(this.g_sTerminalID))
            {
                this.txtBMSID.Enabled = false;
                return;

            }
            else
            {
                this.txtBMSID.Focus();
                this.txtBMSID.SelectAll();
                this.Show_Message("请输入序号", MSGType.Input);

            }

        }
        public enum MSGType
        {
            Error,
            Input,
            OK,
            Warning
        }

        //private void playsound()
        //{
        //    while (true)
        //    {
        //        Thread.Sleep(GlobalVariable.threadsleeptime);
        //        if (GlobalVariable.sound_PASS)
        //        {
        //            soundPlay.Stop();

        //            soundPlay.PlaySound(Application.StartupPath + @"\ERR.WAV");
        //            GlobalVariable.sound_PASS = false;
        //        }
        //    }
        //}

        /// <summary>
        /// MES过站线程
        /// </summary>
        private void press()
        {

            while (true)
            {
                Thread.Sleep(200);
                  if (F=="1")
                {

                    this.Invoke((MethodInvoker)delegate
                {

                    if (GlobalVariable.MES_PASS == true)
                    {

                        #region KEY_PRESS
                        GlobalVariable.StatusModel.ListStatus = GlobalVariable.dt.ToString("HH:mm:ss") + "\t" + "条码" + GlobalVariable.BMSID + "开始过站";

                        if (string.IsNullOrEmpty(GlobalVariable.BMSID))
                        {
                            GlobalVariable.MES_PASS = false;
                            this.Show_Message("输入的序号为空", MSGType.Error);

                        }
                        else

                        if (!this.CheckSN(SN))
                        {
                            GlobalVariable.MES_PASS = false;

                        }
                        else
                        if (!this.CheckWO(SN))
                        {
                            GlobalVariable.MES_PASS = false;

                        }
                        else
                        if (!this.CheckRoute(SN, this.g_sTerminalID))
                        {
                            GlobalVariable.MES_PASS = false;

                        }


                        else
                        {

                            if (!this.ChkWoInput(SN, this.g_sProcess, out this.StartProcessflag))
                            {
                                GlobalVariable.MES_PASS = false;


                            }
                            else
                            if (!this.ChkWoOutput(SN, this.g_sProcess, out this.EndProcessflag))
                            {
                                GlobalVariable.MES_PASS = false;


                            }


                            else
                            if (this.PassStation(SN, "N/A"))
                            {
                                GlobalVariable.MES_PASS = false;

                                this.GetPassCount(this.txtWO.Text.Trim().ToUpper());

                                this.txtDefect.Text = "N/A";

                                GlobalVariable.MES_PASS = false;

                            }


                            GlobalVariable.MES_PASS = false;




                        }


                        #endregion
                        //      this.mes_press.Reset();
                    }
                });


                }
            }


        }



        private void check_Allow()
        {
            if (allow >= 4)
            {
                this.txtBMSID.Enabled = true;
                allow -= 4;
            }

        }

        //public void Show_Message(string sText, MSGType type)
        //{

        //    GlobalVariable.StatusModel.CheckResult = sText;
        //    soundPlay.Stop();
        //    switch (type)
        //    {
        //        case MSGType.Error:

        //            this.lstSt.BackColor = Color.Red;

        //            //this.txtMsg.Text = "ERROR";
        //            this.lstSt.ForeColor = Color.Yellow;
        //            soundPlay.PlaySound(Application.StartupPath + @"\ERR.WAV");
        //            break;

        //        case MSGType.Input:
        //            this.lstSt.BackColor = Color.Blue;

        //            //this.txtMsg.Text = "INPUT";
        //            soundPlay.PlaySound(Application.StartupPath + @"\INPUT.WAV");
        //            break;
        //        case MSGType.Warning:
        //            this.lstSt.ForeColor = Color.Blue;
        //            this.lstSt.BackColor = Color.Yellow;

        //            soundPlay.PlaySound(Application.StartupPath + @"\INPUT.WAV");
        //            break;
        //        default:

        //            this.lstSt.ForeColor = Color.Yellow;
        //            // this.txtMsg.Text = "PASS";
        //            this.lstSt.BackColor = Color.Green;

        //            soundPlay.PlaySound(Application.StartupPath + @"\PASS.WAV");
        //            break;
        //    }
        //}


        public void Show_Message(string sText, MSGType type)
        {

            GlobalVariable.StatusModel.MES_MSG = sText;
            GlobalVariable.StatusModel.ListStatus = GlobalVariable.dt.ToString("HH:mm:ss") + "\t" + sText;
            switch (type)
            {
                case MSGType.Error:

                    GlobalVariable.MES_COLOR = false;

           
                    this.thre_con.Set();
                    break;



                default:

                    GlobalVariable.MES_COLOR = true;
                    break;
            }
            //});
        }
        private bool LoadStationInfo(string str)
        {
            DataTable dt = null;
            bool flag = true;
            string msg = "";
            try
            {
                if (this.g_sTerminalID == "0")
                {
                    this.Show_Message("请设定扫描站点", MSGType.Error);
                    flag = false;

                }
                dt = clsPPSPublicModule.FillDataTable("", "select c.PDLINE_NAME,b.PROCESS_NAME,a.TERMINAL_NAME from SYS_TERMINAL a inner join sys_process  b on a.PROCESS_ID=b.PROCESS_ID inner join SYS_PDLINE c on a.PDLINE_ID=c.PDLINE_ID where a.TERMINAL_ID='" + str + "'", ref msg, ref flag);
                if (flag)
                {
                    if (dt.Rows.Count > 0)
                    {
                        this.txtStation.Text = dt.Rows[0][0].ToString() + "." + dt.Rows[0][1].ToString() + "." + dt.Rows[0][2].ToString();
                    }
                    else
                    {
                        this.Show_Message("查询站点错误", MSGType.Error);
                        flag = false;
                    }
                }
                else
                {
                    this.Show_Message(msg, MSGType.Error);

                }


            }
            catch (Exception exp)
            {
                this.Show_Message("查询站点错误" + exp.Message, MSGType.Error);
                flag = false;


            }
            finally
            {
                dt.Dispose();

            }
            return flag;


        }

        private bool CheckSN(string str)
        {
            bool flag = true;
            string msg = "";
            string sql = "select CURRENT_STATUS,ROUTE_ID from G_SN_STATUS with(nolock) where SERIAL_NUMBER='" + str + "'";
            DataTable dt = null;
            try
            {
                dt = clsPPSPublicModule.FillDataTable("", sql, ref msg, ref flag);
                if (flag)
                {
                    if (dt.Rows.Count == 0)
                    {
                        this.Show_Message(str + "输入的序号错误", MSGType.Error);
                        flag = false;


                    }
                    else
                    {
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            this.Show_Message(str + "序号的状态错误", MSGType.Error);
                            flag = false;

                        }
                        else
                        {
                            this.g_sSRouteID = dt.Rows[0][1].ToString();
                        }
                    }
                }
                else
                {
                    this.Show_Message(msg, MSGType.Error);
                }


            }
            catch (Exception exp)
            {
                flag = false;
                this.Show_Message(exp.Message, MSGType.Error);

            }
            finally
            {
                dt.Dispose();
            }
            return flag;
        }

        private bool CheckWO(string str)
        {
            bool flag = true;
            string msg = "";
            string sql = "select WO_STATUS from g_wo_base a WITH (NOLOCK) inner join g_sn_status b WITH (NOLOCK) on a.WORK_ORDER=b.WORK_ORDER where b.SERIAL_NUMBER='" + str + "'";
            DataTable dt = null;
            try
            {
                dt = clsPPSPublicModule.FillDataTable("", sql, ref msg, ref flag);
                if (flag)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() == "6")
                        {
                            this.Show_Message(str + "序号对应的工单已完工", MSGType.Error);
                            flag = false;

                        }
                        else if (dt.Rows[0][0].ToString() == "4")
                        {
                            this.Show_Message(str + "序号对应的工单被HOLD", MSGType.Error);
                            flag = false;

                        }

                    }
                    else
                    {
                        this.Show_Message(str + "未找到序号对应的工单号", MSGType.Error);
                        flag = false;

                    }


                }
                else
                {
                    this.Show_Message(msg, MSGType.Error);

                }

            }
            catch (Exception exp)
            {
                this.Show_Message(str + "查询工单失败" + exp.Message, MSGType.Error);
                flag = false;


            }
            finally
            {
                dt.Dispose();
            }
            return flag;

        }

        private bool CheckRoute(string str, string sTerminalID)
        {

            if (this.g_sTerminalID == "0")
            {
                this.Show_Message("Please Config Terminal", MSGType.Error);
                return false;
            }
            bool flag = true;
            DataTable dt = null;
            string msg = "";
            DataTable dts = null;
            try
            {
                dts = clsPPSPublicModule.FillDataTable("", "Select CONVERT(varchar(100), GETDATE(), 20)", ref msg, ref flag);
                if (!flag)
                {
                    this.Show_Message(msg, MSGType.Error);
                    return false;

                }
                object[][] @params = new object[][] { new object[] { ParameterDirection.Input, SqlDbType.Int, "@TERMINALID", Convert.ToInt32(this.g_sTerminalID) }, new object[] { ParameterDirection.Input, SqlDbType.VarChar, "@TSN", str.Trim().ToUpper() }, new object[] { ParameterDirection.Output, SqlDbType.VarChar, "@ErrMsg", "" } };

                dt = clsPPSPublicModule.ExecuteProc("", "UDP_CHKT_ROUTE", @params, ref flag, ref msg);
                if (flag)
                {
                    if (dt.Rows[0][0].ToString().Substring(0, 2) != "OK")
                    {

                        this.Show_Message(dt.Rows[0][0].ToString(), MSGType.Error);
                        flag = false;

                    }
                }
                else
                {
                    this.Show_Message(msg, MSGType.Error);
                }
            }
            catch (Exception exp)
            {

                this.Show_Message(str + "UDP_CHKT_ROUTE Error" + exp, MSGType.Error);
                flag = false;

            }
            finally
            {
                dt.Dispose();
            }
            return flag;




        }

        private bool GetPassCount(string wo)
        {
            bool flag = true;
            DataTable dt = null;
            string msg = "";
            try
            {
                dt = clsPPSPublicModule.FillDataTable("", @" SELECT COUNT(*) PassQty FROM  ( select count(1) as PassQty from g_sn_travel x,sys_terminal y where x.process_id=y.process_id and x.work_order='" + wo + "' and y.terminal_id='" + this.g_sTerminalID + "' and x.current_status='0' group by x.WORK_ORDER,x.SERIAL_NUMBER)t", ref msg, ref flag);
                if (flag)
                {
                    if (dt.Rows.Count > 0)
                    {
                        this.labCount.Text = dt.Rows[0]["PassQty"].ToString();

                    }
                    else
                    {
                        this.labCount.Text = "0";
                    }
                }
                else
                {
                    this.Show_Message(msg, MSGType.Error);

                }
            }
            catch (Exception ex)
            {
                this.Show_Message(wo + "GetPassCount Error:" + ex.Message, MSGType.Error);
                flag = false;
            }
            finally
            {
                dt.Dispose();
            }

            return flag;
        }

        private bool GetSNInfo(string sn)
        {

            bool flag = true;
            DataTable dt = null;
            string msg = "";
            try
            {
                String SQL = "select a.work_order,a.customer_sn,b.target_qty,b.input_qty,c.part_no from g_sn_status a,g_wo_base  b,sys_part c where c.part_id=a.part_id and b.work_order=a.work_order and a.serial_number='" + sn + "' or a.customer_sn='" + sn + "'";
                dt = clsPPSPublicModule.FillDataTable("", SQL, ref msg, ref flag);
                if (!flag)
                {
                    this.Show_Message(msg, MSGType.Error);
                    return false;
                }

                if (dt.Rows.Count == 0)
                {
                    this.Show_Message(sn + "SN No Part No Or SN Error", MSGType.Error);
                    return false;
                }
                this.txtPN.Text = dt.Rows[0]["PART_NO"].ToString().Trim();
                this.txtWO.Text = dt.Rows[0]["WORK_ORDER"].ToString().Trim();
                this.txtWorkPlan.Text = dt.Rows[0]["target_qty"].ToString().Trim();

                return true;
            }
            catch (Exception exception)
            {
                this.Show_Message(sn + "Get SN Info Error:" + exception.Message, MSGType.Error);
                return false;
            }
        }
        private void txtDefect_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (string.IsNullOrEmpty(this.txtDefect.Text.Trim()))
                {
                    this.txtDefect.Focus();
                    this.txtDefect.SelectAll();
                    this.Show_Message(this.txtDefect.Text.Trim() + "输入的不良代码为空", MSGType.Error);
                    return;
                }
                if (!this.CheckDefectCode(this.txtDefect.Text.Trim().ToUpper()))
                {
                    this.txtDefect.Focus();
                    this.txtDefect.SelectAll();
                    return;
                }
                else
                {
                    this.txtBMSID.Focus();
                    this.txtBMSID.SelectAll();
                    this.Show_Message("请输入序号", MSGType.OK);

                }

            }

        }
        private bool CheckDefectCode(string str)
        {
            bool flag = true;
            string msg = "";
            DataTable dt = null;
            this.sSQL = "select * from SYS_DEFECT where defect_code='" + str.Trim().ToUpper() + "'";
            try
            {
                dt = clsPPSPublicModule.FillDataTable("", this.sSQL, ref msg, ref flag);
                if (flag)
                {
                    if (dt.Rows.Count == 0)
                    {
                        this.Show_Message(str.Trim().ToUpper() + "输入的不良代码不存在", MSGType.Error);
                        flag = false;

                    }

                }
                else
                {
                    this.Show_Message(msg, MSGType.Error);
                    flag = false;

                }


            }
            catch (Exception exp)
            {
                this.Show_Message(exp.Message, MSGType.Error);
                flag = false;

            }
            finally
            {
                dt.Dispose();
            }
            return flag;
        }
        private bool GetProcess(string str, ref string sProcess)
        {
            string msg = "";
            g_sProcess = "0";
            bool flag = true;
            string sSQL = "SELECT A.PROCESS_ID,a.STAGE_ID,a.PDLINE_ID  FROM SYS_TERMINAL A WHERE A.TERMINAL_ID='" + str + "' and enabled='Y'";
            DataTable dt = null;
            try
            {
                dt = clsPPSPublicModule.FillDataTable("", sSQL, ref msg, ref flag);
                if (flag)
                {
                    if (dt.Rows.Count == 0)
                    {
                        this.Show_Message(str + "站点信息不存在", MSGType.Error);
                        flag = false;

                    }
                    else
                    {
                        g_sProcess = dt.Rows[0][0].ToString();
                        this.g_sStage = dt.Rows[0][1].ToString();
                        this.g_sPdline = dt.Rows[0][2].ToString();
                    }

                }
                else
                {
                    this.Show_Message(msg, MSGType.Error);
                }
            }
            catch (Exception exp)
            {
                this.Show_Message(exp.Message, MSGType.Error);
                flag = false;

            }
            finally
            {
                dt.Dispose();
            }
            return flag;



        }


        private bool GetProcessName(string str, out string sProcessName)
        {
            bool flag = true;
            sProcessName = "";
            string msg = "";
            string sql = "select PROCESS_NAME from SYS_PROCESS where PROCESS_ID='" + str + "'";
            DataTable dt = null;
            try
            {
                dt = clsPPSPublicModule.FillDataTable("", sql, ref msg, ref flag);
                if (flag)
                {
                    if (dt.Rows.Count == 0)
                    {
                        this.Show_Message(str + "未查询到该站点", MSGType.Error);
                        flag = false;



                    }
                    else
                    {
                        sProcessName = dt.Rows[0][0].ToString();
                    }

                }
                else
                {
                    this.Show_Message(msg, MSGType.Error);
                }


            }
            catch (Exception exp)
            {
                this.Show_Message(exp.Message, MSGType.Error);
                flag = false;

            }
            finally
            {
                dt.Dispose();
            }
            return flag;


        }

        private bool GetNextProcess(string crouteid, string tprocessid, string tStatus, out string cnextprocess)
        {
            bool flag = true;
            cnextprocess = "";
            string msg = "";
            DataTable dt = null;
            //去除判断  下一站是否必要判断
            string sSQL = "select next_process_id from sys_route_detail where route_id = '" + crouteid + "' and process_id = '" + tprocessid + "' and result = '" + tStatus + "'  and enabled = 'Y'";
            try
            {
                dt = clsPPSPublicModule.FillDataTable("", sSQL, ref msg, ref flag);
                if (flag)
                {
                    if (dt.Rows.Count == 0)
                    {
                        if (tStatus == "0")
                        { cnextprocess = "0"; }
                        else
                        {
                            this.Show_Message("未找到下一站站点信息", MSGType.Error);
                            flag = false;
                        }


                    }
                    else
                    {
                        cnextprocess = dt.Rows[0][0].ToString();
                    }
                }
                else
                {
                    this.Show_Message(msg, MSGType.Error);
                }
            }
            catch (Exception exp)
            {
                this.Show_Message("GetNextProcess Error" + exp.Message, MSGType.Error);
                flag = false;

            }
            finally
            {
                dt.Dispose();
            }
            return flag;


        }

        private bool ChkWoInput(string str, string sProcess, out bool bFlag)
        {
            bool flag = true;
            bFlag = false;
            DataTable dt = null;
            string msg = "";
            string sql = @"SELECT b.work_order,start_process_id,end_process_id,
      wo_status,in_pdline_time,out_pdline_time,a.route_id,b.route_id,b.process_id 
         FROM G_WO_BASE a,G_SN_STATUS b
            WHERE b.serial_number ='" + str + "' AND a.work_order = b.work_order";
            try
            {
                dt = clsPPSPublicModule.FillDataTable("", sql, ref msg, ref flag);
                if (flag)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][6].ToString() != dt.Rows[0][7].ToString())
                        {
                            if ((dt.Rows[0][8].ToString() == "0") && (string.IsNullOrEmpty(dt.Rows[0][4].ToString())))
                            {
                                bFlag = true;

                            }


                        }
                        else
                        {
                            if ((dt.Rows[0][1].ToString() == this.g_sProcess) && (string.IsNullOrEmpty(dt.Rows[0][4].ToString())))
                            {
                                bFlag = true;
                            }
                        }

                    }
                    else
                    {
                        this.Show_Message("ChkWoInput 未找到数据", MSGType.Error);
                        flag = false;
                    }
                }
                else
                {
                    this.Show_Message("ChkWoInput" + msg, MSGType.Error);
                    flag = false;
                }

            }
            catch (Exception exp)
            {
                this.Show_Message("ChkWoInput Error" + exp.Message, MSGType.Error);
                flag = false;

            }
            finally
            {
                dt.Dispose();

            }
            return flag;


        }

        private bool ChkWoOutput(string str, string sProcess, out bool bFlag)
        {
            bool flag = true;
            bFlag = false;
            DataTable dt = null;
            DataTable dts = null;
            string msg = "";
            string sql = " SELECT b.work_order,b.current_status,start_process_id,end_process_id, wo_status,target_qty,output_qty,in_pdline_time,out_pdline_time,a.route_id,b.route_id  FROM G_WO_BASE a,G_SN_STATUS b WHERE b.serial_number ='" + str + "' AND a.work_order = b.work_order";
            string ssql = "";
            try
            {
                dt = clsPPSPublicModule.FillDataTable("", sql, ref msg, ref flag);
                if (flag)
                {
                    if (dt.Rows.Count == 0)
                    {
                        this.Show_Message("ChkWoOutput 未查询到数据", MSGType.Error);
                        flag = false;


                    }
                    else
                    {
                        if (dt.Rows[0][10].ToString() != dt.Rows[0][9].ToString())
                        {
                            ssql = @"sELECT a.next_process_id 
	   FROM ( SELECT top 1 s1.* FROM SYS_ROUTE_DETAIL s1 inner join (SELECT MIN(seq) seq,step
                    FROM SYS_ROUTE_DETAIL
                   WHERE route_id='" + dt.Rows[0][10].ToString() + "' GROUP BY step ) s2 on s1.seq=s2.seq and s1.STEP=s2.STEP WHERE route_id='" + dt.Rows[0][10].ToString() + "' AND necessary='Y'  ORDER BY s1.seq DESC) a";
                            dts = clsPPSPublicModule.FillDataTable("", ssql, ref msg, ref flag);
                            if (flag)
                            {
                                if (dts.Rows.Count > 0)
                                {
                                    if ((dts.Rows[0][0].ToString() == this.g_sProcess) && (string.IsNullOrEmpty(dt.Rows[0][8].ToString())))
                                    {
                                        bFlag = true;

                                    }

                                }
                            }
                            else
                            {
                                this.Show_Message("ChkWoOutput 未查询到数据2", MSGType.Error);
                                flag = false;
                            }

                        }
                        else
                        {
                            if ((dt.Rows[0][3].ToString() == this.g_sProcess) && (string.IsNullOrEmpty(dt.Rows[0][8].ToString())))
                            {
                                bFlag = true;
                            }
                        }

                    }
                }

            }
            catch (Exception exp)
            {
                this.Show_Message("ChkWoOutput Error" + exp.Message, MSGType.Error);
                flag = false;
            }
            finally
            {
                dt.Dispose();
                if (dts != null)
                {
                    dts.Dispose();
                }
            }
            return flag;

        }

        private string GetDefectID(string str)
        {
            string sDefectId = "";
            DataTable dt = null;
            bool flag = true;
            string msg = "";
            string ssql = "select DEFECT_ID from sys_defect where defect_code='" + str + "'";
            try
            {
                dt = clsPPSPublicModule.FillDataTable("", ssql, ref msg, ref flag);
                if (flag)
                {

                    if (dt.Rows.Count == 0)
                    {

                        sDefectId = "0";
                    }
                    else
                    {
                        sDefectId = dt.Rows[0][0].ToString();
                    }

                }
                else
                {
                    sDefectId = "0";

                }



            }
            catch (Exception EXP)
            {
                sDefectId = "0";

            }
            finally
            {


            }
            return sDefectId;






        }

        private bool PassStation(string str, string serrorcode)
        {

            if (this.g_sTerminalID == "0")
            {
                this.Show_Message("Please Config Terminal", MSGType.Error);
                return false;
            }
            bool flag = true;
            DataTable dt = null;
            string msg = "";
            try
            {

                object[][] @params = new object[][] {
                    new object[] { ParameterDirection.Input, SqlDbType.Int, "@TERMINALID", Convert.ToInt32(this.g_sTerminalID) },
                    new object[] { ParameterDirection.Input, SqlDbType.VarChar, "@TSN", str.Trim().ToUpper() },
                    new object[] { ParameterDirection.Input, SqlDbType.Int, "@tempid", Convert.ToInt32(clsPPSPublicModule.PPSUserID) },
                    new object[] { ParameterDirection.Input, SqlDbType.VarChar, "@zdefect", serrorcode.Trim() },
                    new object[] { ParameterDirection.Output, SqlDbType.VarChar, "@tres", "" },
                    new object[] { ParameterDirection.Output, SqlDbType.VarChar, "@tnextproc", "" } };

                dt = clsPPSPublicModule.ExecuteProc("", "UDT_PASS_STATION", @params, ref flag, ref msg);
                if (flag)
                {
                    if (dt.Rows[0][0].ToString().Substring(0, 2) != "OK")
                    {

                        this.Show_Message(dt.Rows[0][0].ToString(), MSGType.Error);
                        flag = false;
                    }
                    else
                    {
                        string sProcessName = "";
                        sProcessName = this.GetNextProcssName(dt.Rows[0]["@tnextproc"].ToString());
                        this.Show_Message("条码" + str + "过站" + dt.Rows[0][0].ToString() + "下一站： " + sProcessName, MSGType.OK);
                    }
                }
                else
                {
                    this.Show_Message(msg, MSGType.Error);
                }
            }
            catch (Exception exp)
            {

                this.Show_Message("UDT_PASS_STATION Error" + exp, MSGType.Error);
                flag = false;

            }
            finally
            {
                dt.Dispose();
            }
            return flag;



        }

        private string GetNextProcssName(string str)
        {
            bool flag = true;
            string msg = "";
            DataTable dt = null;
            string sql = "select process_name from sys_process where process_id='" + str + "'";
            try
            {
                dt = clsPPSPublicModule.FillDataTable("", sql, ref msg, ref flag);
                if (flag)
                {
                    if (dt.Rows.Count > 0)
                    {

                        return dt.Rows[0][0].ToString();
                    }
                    else
                    {

                        return "";
                    }
                }
                else
                {
                    return "";
                }


            }
            catch (Exception exp)
            {
                return "";


            }
            finally
            {
                dt.Dispose();
            }


        }

        private bool UpdateWoInfo(bool Inputflag, bool Outputflag, string str)
        {
            bool flag = false;
            string msg = "";
            string sql1 = "UPDATE G_WO_BASE SET output_qty = output_qty+1 WHERE work_order in (select work_order from g_sn_status where serial_number='" + str + "')";
            string sql3 = " UPDATE G_WO_BASE SET input_qty = input_qty+1 WHERE work_order in (select work_order from g_sn_status where serial_number='" + str + "')";
            if (Inputflag)
            {
                flag = clsPPSPublicModule.ExecuteNoneQuery("", sql3, ref msg);
            }
            if (Outputflag)
            {
                flag = clsPPSPublicModule.ExecuteNoneQuery("", sql1, ref msg);

            }
            return flag;

        }

        private void txtSN_KeyPress()
        {


            {
                if (string.IsNullOrEmpty(this.txtBMSID.Text.Trim()))
                {

                    this.Show_Message("输入的序号为空", MSGType.Error);
                    return;
                }

                if (!this.CheckSN(SN))
                {

                    return;
                }

                if (!this.CheckWO(SN))
                {

                    return;
                }

                if (!this.CheckRoute(SN, this.g_sTerminalID))
                {

                    return;
                }
                if (this.txtDefect.Text != "N/A")
                {
                    if (!this.CheckDefectCode(this.txtDefect.Text.Trim().ToUpper()))
                    {
                        this.txtDefect.Text = "N/A";

                        return;
                    }
                    else
                    {

                        string sNextProcess = "";
                        if (!this.GetNextProcess(this.g_sSRouteID, this.g_sProcess, "1", out sNextProcess))
                        {
                            this.txtDefect.Text = "N/A";

                            return;
                        }
                        if (!this.PassStation(SN, this.txtDefect.Text.Trim()))
                        {

                            return;
                        }

                        this.txtDefect.Text = "N/A";

                    }


                }
                else
                {

                    if (!this.ChkWoInput(SN, this.g_sProcess, out this.StartProcessflag))
                    {

                        return;
                    }
                    if (!this.ChkWoOutput(SN, this.g_sProcess, out this.EndProcessflag))
                    {

                        return;
                    }



                    if (this.PassStation(SN, "N/A"))
                    {

                        this.GetPassCount(this.txtWO.Text.Trim().ToUpper());

                        this.txtDefect.Text = "N/A";

                    }



                }

            }

        }
        /// <summary>
        /// 加载上位机信息
        /// </summary>
        private void msg()
        {
            while (true)
            {
                {
                    Thread.Sleep(GlobalVariable.threadsleeptime);
                    if (GlobalVariable.StatusModel.ListStatus != "0")
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            if (this.lstStatus.Lines.Length > 1000)
                            {
                                this.lstStatus.Clear();
                            }
                            this.lstStatus.AppendText(System.Environment.NewLine + GlobalVariable.StatusModel.ListStatus);

                            this.lstStatus.ScrollToCaret();
                            GlobalVariable.StatusModel.ListStatus = "0";

                        });
                    }
                }

            }
        }
        /// <summary>
        /// 更新上位机信息背景颜色
        /// </summary>
        private void lsback()
        {
            while (true)
            {
                {
                    Thread.Sleep(GlobalVariable.threadsleeptime);
                    this.lstSt.Invoke((MethodInvoker)delegate ()
                    {

                        switch (GlobalVariable.StatusModel.IsCheckResult)
                        {
                            case 0:

                                this.lstSt.BackColor = Color.White;
                                //  
                                break;

                            case 1:
                                this.lstSt.BackColor = Color.LightGreen;
                                break;
                            case 2:
                                this.lstSt.BackColor = Color.Red;

                                break;
                            default:

                                this.lstSt.BackColor = Color.White;
                                break;

                        }
                        //switch (GlobalVariable.StatusModel.IsCheckResult)
                        //    {
                        //        case 0:
                        //            this.lstSt.BackColor = Color.White;
                        //            break;

                        //        case 1:
                        //            this.lstSt.BackColor = Color.Blue;






                        //        break;

                        //        case 2:
                        //            this.lstSt.BackColor = Color.Red;
                        //            break;

                        //        default:
                        //            this.lstSt.BackColor = Color.White;
                        //            break;
                        //    }


                    });

                }

            }
        }
        /// <summary>
        /// 更新mes信息背景颜色
        /// </summary>
        private void MES_COLOR()
        {

            while (true)
            {
                Thread.Sleep(GlobalVariable.threadsleeptime);
                if (this.thre_con.WaitOne())
                {

                    this.Invoke((MethodInvoker)delegate
                    {

                        if (GlobalVariable.MES_COLOR == false)
                        {



                            txtMsg.BackColor = Color.Red;
                            GlobalVariable.MES_COLOR = true;



                        }
                        this.thre_con.Reset();
                    });

                }
            }
        }
        /// <summary>
        /// 更新MES过站标志
        /// </summary>
        private void MES_PA()
        {

          

        
            F = file2.IniReadValue("MESEB", "FLAG");


            if (F == "1")
            {
                mes_inf.ForeColor = Color.Black;
                mes_inf.Text = "MES过站已开启";

            }
            else
            {
                mes_inf.ForeColor = Color.Red;
                mes_inf.Text = "MES过站已关闭";

            }
        }
        private void ReadExcel()
        {



           
            while (true)
            {
                   Thread.Sleep(100);
                Thread.Sleep(GlobalVariable.threadsleeptime);
                if (GlobalVariable.JM_TH_boo && GlobalVariable.mes_con&& GlobalVariable.IsReadBMSID)
                {
                    try
                   {
                        this.Invoke((MethodInvoker)delegate
                        {

                            for (int i=GlobalVariable.P ;i < dt.Rows.Count&& GlobalVariable.JM_TH_boo; i++)
                            {



                                GlobalVariable.loop = true;


                               // list_can.Rows[i].Selected=true;
                                list_can.CurrentCell = list_can.Rows[i].Cells[0];
                               // GlobalVariable.StatusModel.ListStatus = dt.Rows[i][0].ToString();
                                list_can.Rows[i].Cells[0].Value = dt.Rows[i][0].ToString();
                                list_can.Rows[i].Cells[1].Value = dt.Rows[i][1].ToString();
                                list_can.Rows[i].Cells[2].Value = dt.Rows[i][3].ToString();


                                //// 报文
                                //    GlobalVariable.mess.Add (((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[i, 3]).Text.ToString());
                                GlobalVariable.mes = dt.Rows[i][2].ToString();

                           //     GlobalVariable.StatusModel.ListStatus = (DateTime.Now.ToString("HH:mm:ss") +"\t"+ "listcan_cou" + i.ToString() + "报文" + GlobalVariable.mes);
                                //预设值相同



                                // GlobalVariable.reserver_value.Add(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[i, 4]).Text.ToString());
                                GlobalVariable.reserver = dt.Rows[i][3].ToString();


                                // GlobalVariable.JM_TH.Reset();
                                GlobalVariable.JM_TH_boo = false;

                                GlobalVariable.StatusModel.NowValue = 0;
                                #region
                                //if (p++ >= dt.Rows.Count&&GlobalVariable.StatusModel.NowValue>=GlobalVariable.StatusModel.Maximum)
                                //{

                                //    GlobalVariable.mes = "";
                                //    GlobalVariable.StatusModel.NowValue = 0;
                                //    GlobalVariable.StatusModel.C_NowSoftVer = "                                ";
                                //    GlobalVariable.StatusModel.C_NowRealSoftVer = "                                ";
                                //    GlobalVariable.StatusModel.C_NowHeadwearVer = "                    ";
                                //    GlobalVariable.StatusModel.C_NowRealHeadwearVer = "                    ";
                                //    GlobalVariable.StatusModel.C_NowInsulationVer = "                    ";
                                //    GlobalVariable.StatusModel.C_NowLinkVoltage = 0.0;
                                //    GlobalVariable.StatusModel.C_NowPackVoltage = 0.0;
                                //    GlobalVariable.StatusModel.CR_SOC = 0.0;
                                //    GlobalVariable.StatusModel.M_NowSoftVer = "                                ";
                                //    GlobalVariable.StatusModel.M_NowHeadWearVer = "                    ";
                                //    GlobalVariable.StatusModel.M_BMUID = "";


                                //    #region 结束显示结果和MES

                                //    for (int k = 0; k < list_can.Rows.Count; k++)
                                //    {
                                //        if(list_can.Rows[k].Cells[3].Value.ToString()=="")
                                //        {
                                //            GlobalVariable.check_result = false;
                                            
                                          
                                //        }

                                //    }
                                //    if (GlobalVariable.check_result == true)
                                //    {


                                //        GlobalVariable.StatusModel.IsCheckResult = 1;
                                //        GlobalVariable.MES_PASS = true;
                                //    }
                                //    else
                                //    {
                                //        GlobalVariable.StatusModel.IsCheckResult = 2;
                                //        GlobalVariable.MES_PASS = false;
                                //    }

                                //    #endregion


                                //    break;
                                //}
                                #endregion 

                            }

                    


                            #region
                          
                            
                                if (GlobalVariable.P++ >= dt.Rows.Count && GlobalVariable.StatusModel.NowValue >= GlobalVariable.StatusModel.Maximum)
                                {
                                    GlobalVariable.mes = "";
                                    GlobalVariable.StatusModel.NowValue = 0;
                                    GlobalVariable.StatusModel.C_NowSoftVer = "                                ";
                                    GlobalVariable.StatusModel.C_NowRealSoftVer = "                                ";
                                    GlobalVariable.StatusModel.C_NowHeadwearVer = "                    ";
                                    GlobalVariable.StatusModel.C_NowRealHeadwearVer = "                    ";
                                    GlobalVariable.StatusModel.C_NowInsulationVer = "                    ";
                                    GlobalVariable.StatusModel.C_NowLinkVoltage = 0.0;
                                    GlobalVariable.StatusModel.C_NowPackVoltage = 0.0;
                                    GlobalVariable.StatusModel.CR_SOC = 0.0;
                                    GlobalVariable.StatusModel.M_NowSoftVer = "                                ";
                                    GlobalVariable.StatusModel.M_NowHeadWearVer = "                    ";
                                    GlobalVariable.StatusModel.M_BMUID = "";
                                ///2020 11 30 去除不按就输入
                                GlobalVariable.IsReadBMSID = false;

                                sb = new StringBuilder();
                                d = DateTime.Now;
                                sb = new StringBuilder();
                                sb.Append(d.ToString("yy-MM-dd HH:mm:ss") + ",");
                                sb.Append("条码:"+GlobalVariable.BMSID.ToString() + ",");
                                #region 结束显示结果和MES
                                GlobalVariable.check_result = true;
                                for (int k = 0; k < list_can.Rows.Count; k++)
                                    {
                                        if (list_can.Rows[k].Cells[3].Value.ToString() == "")
                                        {
                                            GlobalVariable.check_result = false;
                                     
                                      
                                        }
                                    if (list_can.Rows[k].Cells[4].Value.ToString() == "FAIL")
                                    {
                                        GlobalVariable.check_result = false;


                                    }
                                    sb.Append(list_can.Rows[k].Cells[1].Value.ToString() + ":" + list_can.Rows[k].Cells[3].Value.ToString() + ",");

                                    }
                                sb.Append("操作人员" + ":" + clsPPSPublicModule.PPSUserName + ",");
                                if (GlobalVariable.check_result == true)
                                    {

                                    sb.Append("结果" + ": PASS" + "\r\n");
                                        GlobalVariable.StatusModel.IsCheckResult = 1;
                                        GlobalVariable.MES_PASS = true;
                                    }
                                    else
                                {
                                    GlobalVariable.sp.sound();//声音
                                    sb.Append("结果" + ": NG" + "\r\n");
                                    GlobalVariable.StatusModel.IsCheckResult = 2;
                                        GlobalVariable.MES_PASS = false;
                                    }


                                
                                SaveLog.SaveLogMessage_new(sb.ToString());
                                #endregion
                                
                            }
                              
                            
                            #endregion

                            GlobalVariable.loop = false;


                        });


                   }
                    catch (Exception ex)
                    {//2020/11/23
                        GlobalVariable.StatusModel.ListStatus = (DateTime.Now.ToString("HH:mm:ss") + "\t" + ex.Message);
                   }

                }
            }


        }
        /// <summary>
        /// 新增开启模式
        /// </summary>
        private void ReadExcel_open()
        {




            while (true)
            {
                Thread.Sleep(100);
                Thread.Sleep(GlobalVariable.threadsleeptime);
                if (GlobalVariable.JM_TH_boo && GlobalVariable.mes_con)
                {
                    try
                    {
                        this.Invoke((MethodInvoker)delegate
                        {

                            for (int i = GlobalVariable.P; i < dt.Rows.Count && GlobalVariable.JM_TH_boo; i++)
                            {



                                GlobalVariable.loop = true;


                              

                              
                                GlobalVariable.mes = dt.Rows[i][2].ToString();

                                //     GlobalVariable.StatusModel.ListStatus = (DateTime.Now.ToString("HH:mm:ss") +"\t"+ "listcan_cou" + i.ToString() + "报文" + GlobalVariable.mes);
                                //预设值相同



                                // GlobalVariable.reserver_value.Add(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[i, 4]).Text.ToString());
                                GlobalVariable.reserver = dt.Rows[i][3].ToString();


                                // GlobalVariable.JM_TH.Reset();
                                GlobalVariable.JM_TH_boo = false;

                                GlobalVariable.StatusModel.NowValue = 0;
                                #region
                                //if (p++ >= dt.Rows.Count&&GlobalVariable.StatusModel.NowValue>=GlobalVariable.StatusModel.Maximum)
                                //{

                                //    GlobalVariable.mes = "";
                                //    GlobalVariable.StatusModel.NowValue = 0;
                                //    GlobalVariable.StatusModel.C_NowSoftVer = "                                ";
                                //    GlobalVariable.StatusModel.C_NowRealSoftVer = "                                ";
                                //    GlobalVariable.StatusModel.C_NowHeadwearVer = "                    ";
                                //    GlobalVariable.StatusModel.C_NowRealHeadwearVer = "                    ";
                                //    GlobalVariable.StatusModel.C_NowInsulationVer = "                    ";
                                //    GlobalVariable.StatusModel.C_NowLinkVoltage = 0.0;
                                //    GlobalVariable.StatusModel.C_NowPackVoltage = 0.0;
                                //    GlobalVariable.StatusModel.CR_SOC = 0.0;
                                //    GlobalVariable.StatusModel.M_NowSoftVer = "                                ";
                                //    GlobalVariable.StatusModel.M_NowHeadWearVer = "                    ";
                                //    GlobalVariable.StatusModel.M_BMUID = "";


                                //    #region 结束显示结果和MES

                                //    for (int k = 0; k < list_can.Rows.Count; k++)
                                //    {
                                //        if(list_can.Rows[k].Cells[3].Value.ToString()=="")
                                //        {
                                //            GlobalVariable.check_result = false;


                                //        }

                                //    }
                                //    if (GlobalVariable.check_result == true)
                                //    {


                                //        GlobalVariable.StatusModel.IsCheckResult = 1;
                                //        GlobalVariable.MES_PASS = true;
                                //    }
                                //    else
                                //    {
                                //        GlobalVariable.StatusModel.IsCheckResult = 2;
                                //        GlobalVariable.MES_PASS = false;
                                //    }

                                //    #endregion


                                //    break;
                                //}
                                #endregion

                            }




                            #region


                            if (GlobalVariable.P++ >= dt.Rows.Count && GlobalVariable.StatusModel.NowValue >= GlobalVariable.StatusModel.Maximum)
                            {
                                GlobalVariable.mes = "";
                                GlobalVariable.StatusModel.NowValue = 0;
                                GlobalVariable.StatusModel.C_NowSoftVer = "                                ";
                                GlobalVariable.StatusModel.C_NowRealSoftVer = "                                ";
                                GlobalVariable.StatusModel.C_NowHeadwearVer = "                    ";
                                GlobalVariable.StatusModel.C_NowRealHeadwearVer = "                    ";
                                GlobalVariable.StatusModel.C_NowInsulationVer = "                    ";
                                GlobalVariable.StatusModel.C_NowLinkVoltage = 0.0;
                                GlobalVariable.StatusModel.C_NowPackVoltage = 0.0;
                                GlobalVariable.StatusModel.CR_SOC = 0.0;
                                GlobalVariable.StatusModel.M_NowSoftVer = "                                ";
                                GlobalVariable.StatusModel.M_NowHeadWearVer = "                    ";
                                GlobalVariable.StatusModel.M_BMUID = "";

                                sb = new StringBuilder();
                                d = DateTime.Now;
                                sb = new StringBuilder();
                                sb.Append(d.ToString("yy-MM-dd HH:mm:ss") + ",");
                                sb.Append("条码:" + GlobalVariable.BMSID.ToString() + ",");
                                #region 结束显示结果和MES
                                GlobalVariable.check_result = true;
                                for (int k = 0; k < list_can.Rows.Count; k++)
                                {
                                    if (list_can.Rows[k].Cells[3].Value.ToString() == "")
                                    {
                                        GlobalVariable.check_result = false;


                                    }
                                    if (list_can.Rows[k].Cells[4].Value.ToString() == "FAIL")
                                    {
                                        GlobalVariable.check_result = false;


                                    }
                                    sb.Append(list_can.Rows[k].Cells[1].Value.ToString() + ":" + list_can.Rows[k].Cells[3].Value.ToString() + ",");

                                }
                                sb.Append("操作人员" + ":" + clsPPSPublicModule.PPSUserName + ",");
                                if (GlobalVariable.check_result == true)
                                {

                                    sb.Append("结果" + ": PASS" + "\r\n");
                                    GlobalVariable.StatusModel.IsCheckResult = 1;
                                    GlobalVariable.MES_PASS = true;
                                }
                                else
                                {
                                    GlobalVariable.sp.sound();//声音
                                    sb.Append("结果" + ": NG" + "\r\n");
                                    GlobalVariable.StatusModel.IsCheckResult = 2;
                                    GlobalVariable.MES_PASS = false;
                                }



                                SaveLog.SaveLogMessage_new(sb.ToString());
                                #endregion

                            }


                            #endregion

                            GlobalVariable.loop = false;


                        });


                    }
                    catch (Exception ex)
                    {

                    }

                }
            }


        }
        private DataTable ReadExcel_dt()
        {
          
            DataTable dtt = new DataTable();
         
            GlobalVariable.mes_con = true;
            list_can.Rows.Clear();
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook wbook = app.Workbooks.Open(file.Path, Type.Missing, Type.Missing,

                 Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,

                 Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,

                 Type.Missing, Type.Missing);


            Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)wbook.Worksheets[1];
            if (wbook.Worksheets.Count > 1)
            {
                Microsoft.Office.Interop.Excel.Worksheet workSheet2 = (Microsoft.Office.Interop.Excel.Worksheet)wbook.Worksheets[2];

                GlobalVariable.open_mod = ((Microsoft.Office.Interop.Excel.Range)workSheet2.Cells[2, 3]).Text.ToString();
                GlobalVariable.TL_OPEN = true;

            }
            else
            { GlobalVariable.open_mod = "";
                GlobalVariable.TL_OPEN = false;
            }



            try
            {
                for (int k = 1; k < workSheet.UsedRange.Columns.Count + 1; k++)
                {

                    dtt.Columns.Add(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[1, k]).Text.ToString());
                }

                for (int i = 2; i < workSheet.UsedRange.Rows.Count + 1; i++)
                {


                    dtt.Rows.Add(1);

                    //  for (int p = 1; p < workSheet.UsedRange.Columns.Count + 1; p++)
                    //  {



                    //dt.Rows[i - 2][p - 1] = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[i, p]).Text.ToString();

                    //no 0
                    dtt.Rows[i - 2][1 - 1] = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[i, 1]).Text.ToString();
                    //name 1
                    dtt.Rows[i - 2][2 - 1] = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[i, 2]).Text.ToString();
                    //    }

                    //报文 2
                    dtt.Rows[i - 2][3 - 1] = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[i, 3]).Text.ToString();


                    //预设值 3
                    if (((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[i, 4]).Text.ToString() == ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[i, 5]).Text.ToString())
                    {
                        dtt.Rows[i - 2][4 - 1] = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[i, 4]).Text.ToString();

                    }
                    else
                    {
                        dtt.Rows[i - 2][4 - 1] = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[i, 4]).Text.ToString() + "至" + ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[i, 5]).Text.ToString();

                    }

                    if (list_can.Rows.Count <= workSheet.UsedRange.Rows.Count - 2)
                    {
                        list_can.Rows.Add();
                    }
                }

                wbook.Close(false, Type.Missing, Type.Missing); wbook = null;            //quit excel app          
                app.Quit();

         
            return dtt;

        }
                    catch (Exception ex)
                    {
                        wbook.Close(false, Type.Missing, Type.Missing); wbook = null;            //quit excel app          
                        app.Quit();


           //     GlobalVariable.StatusModel.ListStatus = "ReadExcel_dt" + ex.Message;
                        return dtt;
                   
            }

 
        }
        public bool ReadExcel_1(string strExcelPath,string tab)
        {
            //try
            //{
               DataTable dtExcel = new DataTable();
                //数据表
                DataSet ds = new DataSet();
                //获取文件扩展名
                string strExtension = System.IO.Path.GetExtension(strExcelPath);
                string strFileName = System.IO.Path.GetFileName(strExcelPath);
                string strFilename = System.IO.Path.GetFileNameWithoutExtension(strExcelPath);
                //Excel的连接
                OleDbConnection objConn = null;
                switch (strExtension)
                {
                    case ".xls":
                        objConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strExcelPath + ";" + "Extended Properties=\"Excel 8.0;HDR=yes;IMEX=1;\"");
                        break;
                    case ".xlsx":
                        objConn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strExcelPath + ";" + "Extended Properties=\"Excel 12.0;HDR=yes;IMEX=1;\"");
                        break;
                    default:
                        objConn = null;
                        break;
                }
                if (objConn == null)
                {
                    return false;
                }
                objConn.Open();

                string strSql = "select * from [ "+tab+"]";
                //获取Excel指定Sheet表中的信息
                OleDbCommand objCmd = new OleDbCommand(strSql, objConn);
                OleDbDataAdapter myData = new OleDbDataAdapter(strSql, objConn);
                myData.Fill(ds, tab);//填充数据
                objConn.Close();
                //dtExcel即为excel文件中指定表中存储的信息
                dtExcel = ds.Tables[0];



                if (dtExcel.Rows.Count > 0)
                {

                    for (int i = 0; i < dtExcel.Rows.Count; i++)
                    {
                        ListViewItem lv = new ListViewItem();
                        lv.Text = dtExcel.Rows[i][0].ToString();
                        lv.SubItems.Add(dtExcel.Rows[i][1].ToString());
                        lv.SubItems.Add(dtExcel.Rows[i][2].ToString());
                        lv.SubItems.Add(dtExcel.Rows[i][3].ToString());
                        lv.SubItems.Add(dtExcel.Rows[i][4].ToString());

                    }


                }

                return true;
          //  }
            //catch (Exception ex)
            //{
            //    Show_Message(ex.Message, MSGType.Error);
            //    return false;
            //}
        }

        /// <summary>
        /// 读取值和清除值
        /// </summary>
        private void LOAD4()
        {

            while (true)
            {

                Thread.Sleep(GlobalVariable.threadsleeptime);
                try {


                    if (list_can.Rows.Count > 1 && GlobalVariable.mes_con)
                    {
                        if (GlobalVariable.mes_read_value.Trim().Length >= 1 && GlobalVariable.IsReadBMSID)
                        {

                            {

                                ///2020 11 30   
                                if (GlobalVariable.P > 0 && GlobalVariable.P <= list_can.Rows.Count)
                                {
                                    list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1].Value = GlobalVariable.mes_read_value;
                                    //  list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1].Value.ToString().PadRight(list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1 - 1].Value.ToString().Length);

                                    if (list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1 - 1].Value.ToString().Trim() == (Regex.Replace(list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1].Value.ToString().Trim(), GlobalVariable.Regex, "").Trim().ToString().Replace('\0', ' ').Trim()))
                                    {
                                        list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1 + 1].Style.BackColor = Color.Green;
                                        list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1 + 1].Value = "PASS";
                                        GlobalVariable.StatusModel.NowValue = GlobalVariable.StatusModel.Maximum;
                                    }
                                    else
                                    {
                                        if (list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1 - 1].Value.ToString().Contains("至"))
                                        {
                                            double id;
                                            if (double.TryParse(list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1].Value.ToString(), out id))
                                            {

                                                if ((Convert.ToDouble(list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1].Value) >= Convert.ToDouble(list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1 - 1].Value.ToString().Split('至')[0])) &&

                                                    (Convert.ToDouble(list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1].Value) <= Convert.ToDouble(list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1 - 1].Value.ToString().Split('至')[1])))
                                                {
                                                    list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1 + 1].Style.BackColor = Color.Green;
                                                    list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1 + 1].Value = "PASS";

                                                    GlobalVariable.StatusModel.NowValue = GlobalVariable.StatusModel.Maximum;
                                                }
                                                else
                                                {

                                                    list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1 + 1].Style.BackColor = Color.Red;
                                                    list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1 + 1].Value = "FAIL";
                                                    //     GlobalVariable.check_result = false;
                                                }
                                            }
                                            else
                                            {


                                                list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1 + 1].Style.BackColor = Color.Red;
                                                list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1 + 1].Value = "FAIL";

                                                //  GlobalVariable.check_result = false;
                                            }

                                        }
                                        else
                                        {


                                            {
                                                list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1 + 1].Style.BackColor = Color.Red;
                                                list_can.Rows[GlobalVariable.P - 1].Cells[4 - 1 + 1].Value = "FAIL";



                                            }
                                        }
                                    }
                                    GlobalVariable.mes_read_value = "";

                                }
                            }


                        }
                        if (!GlobalVariable.conn)
                        {

                            for (int i = 0; i < list_can.Rows.Count; i++)
                            {
                                list_can.Rows[i].Cells[4 - 1].Value = "";
                                list_can.Rows[i].Cells[4 + 1 - 1].Style.BackColor = Color.White;
                                list_can.Rows[i].Cells[4 + 1 - 1].Value = "";

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    GlobalVariable.StatusModel.ListStatus = ex.Message;
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
         
         
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            AssemblyCopyrightAttribute customAttribute = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCopyrightAttribute));
            AssemblyDescriptionAttribute attribute2 = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyDescriptionAttribute));
            Version version = executingAssembly.GetName().Version;
            GlobalVariable.dt = DateTime.Now;
            // this.lstStatus.DataSource = mod.ListStatus;
            GlobalVariable.StatusModel.ListStatus = ("程序当前版本\tV " + version.ToString());
            // \\192.168.100.252\fct数据\fct更新文件  上位机更新文件
            file2 = new IniFile(@"\\192.168.100.252\fct数据\"+ GlobalVariable.filepath + "" + @"\config.ini");
            GlobalVariable.StatusModel.M_Reservename = (file2.IniReadValue("reload", "name"));
            file = new IniFile(@"\\192.168.100.252\fct数据\" + GlobalVariable.filepath + "" + @"\config_file\" + GlobalVariable.StatusModel.M_Reservename);
            GlobalVariable.Regex = (file2.IniReadValue("Regex", "RX"));
          
            this.Text += Assembly.GetExecutingAssembly().GetName().Version.ToString();
            txtBMSID.Focus();
            txtBMSID.SelectAll();

            #region MES登录信息
            //clsPPSPublicModule.WCSConnectionstring = @"Data Source = 192.168.100.251; Initial Catalog = ZMES; Persist Security Info = True; User ID = sa; Password = Gbmes2018";
            //clsPPSPublicModule.PPSUserID = "2111";
            //clsPPSPublicModule.PPSUserName = "龚根胜";
            //clsPPSPublicModule.PPSUserNo = "GB0561";
            #endregion

            databing();
            MES_PA();
            TH_MSG = new Thread(new ThreadStart(msg));
            if ((this.TH_MSG == null) || !this.TH_MSG.IsAlive)
            {
               // GlobalVariable.StatusModel.ListStatus = "TH_MSG开启";
                TH_MSG.Start();
            }
                
            Thread TH_MSG2 = new Thread(new ThreadStart(lsback));
            if ((TH_MSG2 == null) || !TH_MSG2.IsAlive)
            {// GlobalVariable.StatusModel.ListStatus = "TH_MSG2开启";
                TH_MSG2.Start(); }

            Thread TH_MSG4 = new Thread(new ThreadStart(MES_COLOR));
            if ((TH_MSG4 == null) || !TH_MSG4.IsAlive)
            {
             //   GlobalVariable.StatusModel.ListStatus = "TH_MSG4开启";
                TH_MSG4.Start();
            }

            //Thread TH_MSG5 = new Thread(new ThreadStart(list_add));
            //if ((TH_MSG5 == null) || !TH_MSG5.IsAlive)
            //{
            //    GlobalVariable.StatusModel.ListStatus = "TH_MSG5开启"; TH_MSG5.Start();
            //}
           // if (F == "1")
            {
               
                Mes_Load("");
                 TH_MSG3 = new Thread(new ThreadStart(press));
                if ((TH_MSG3 == null) || !TH_MSG3.IsAlive)
                {
                   // GlobalVariable.StatusModel.ListStatus = "TH_MSG3+MES开启";
                    TH_MSG3.Start();
                }

            }

            {

                //  this.DataContext = GlobalVariable.StatusModel;
                for (int i = 0; i < 8; i++)
                {
                    this.cbIndex.Items.Add(i);
                }
                this.cbIndex.SelectedIndex = 0;
                List<ComomboxItem> list = new List<ComomboxItem>();
                this.cbChannel.DisplayMember = "Name";
                this.cbChannel.ValueMember = "Value";
                ComomboxItem item = new ComomboxItem
                {
                    Name = "通道一",
                    Value = 0
                };
                list.Add(item);
                ComomboxItem item2 = new ComomboxItem
                {
                    Name = "通道二",
                    Value = 1
                };
                list.Add(item2);
                this.cbChannel.DataSource = list;
                this.cbChannel.SelectedIndex = 0;
                list = new List<ComomboxItem>();
                this.cbBaudRate.DisplayMember = "Name";
                this.cbBaudRate.ValueMember = "Value";
                ComomboxItem item3 = new ComomboxItem
                {
                    Name = "5 Kbps",
                    Value = 5
                };
                list.Add(item3);
                ComomboxItem item4 = new ComomboxItem
                {
                    Name = "10 Kbps",
                    Value = 10
                };
                list.Add(item4);
                ComomboxItem item5 = new ComomboxItem
                {
                    Name = "20 Kbps",
                    Value = 20
                };
                list.Add(item5);
                ComomboxItem item6 = new ComomboxItem
                {
                    Name = "50 Kbps",
                    Value = 50
                };
                list.Add(item6);
                ComomboxItem item7 = new ComomboxItem
                {
                    Name = "100 Kbps",
                    Value = 100
                };
                list.Add(item7);
                ComomboxItem item8 = new ComomboxItem
                {
                    Name = "125 Kbps",
                    Value = 0x7d
                };
                list.Add(item8);
                ComomboxItem item9 = new ComomboxItem
                {
                    Name = "250 Kbps",
                    Value = 250
                };
                list.Add(item9);
                ComomboxItem item10 = new ComomboxItem
                {
                    Name = "500 Kbps",
                    Value = 500
                };
                list.Add(item10);
                ComomboxItem item11 = new ComomboxItem
                {
                    Name = "800 Kbps",
                    Value = 800
                };
                list.Add(item11);
                this.cbBaudRate.DataSource = list;
                this.cbBaudRate.SelectedValue = 500;
                list = new List<ComomboxItem>();
                this.cbObjectChannel.DisplayMember = "Name";
                this.cbObjectChannel.ValueMember = "Value";
                ComomboxItem item12 = new ComomboxItem
                {
                    Name = "BCU",
                    Value = 0
                };
                list.Add(item12);
                ComomboxItem item13 = new ComomboxItem
                {
                    Name = "BMU",
                    Value = 1
                };
                list.Add(item13);


                ComomboxItem item14 = new ComomboxItem
                {
                    Name = "NEW",
                    Value = 2
                };
                list.Add(item14);
                this.cbObjectChannel.DataSource = list;
                this.cbObjectChannel.SelectedIndex = 0;
           

               
                //try
                //{
                if (!File.Exists(file.Path))

                { MessageBox.Show("不存在路径"); return; }
                if (GlobalVariable.StatusModel.M_Reservename.Split('.')[1] == "ini")
                {
                    try
                    {
                        // GlobalVariable.StatusModel.M_Reservename = "config.ini";
                        GlobalVariable.TimeOutValue = Convert.ToInt32(file2.IniReadValue("TimeOut", "TimeOutValue"));
                        if (GlobalVariable.TimeOutValue <= 0)
                        {
                            GlobalVariable.TimeOutValue = 5;
                        }
                    }
                    catch
                    {
                        GlobalVariable.TimeOutValue = 15;
                    }
                    GlobalVariable.StatusModel.Maximum = GlobalVariable.TimeOutValue;
                    list_can.Visible = false;
                    GlobalVariable.mes_con = false;
                    GlobalVariable.StatusModel.C_ReserveRealSoftVer = file.IniReadValue("BCU", "RealSoftWearVer");
                    GlobalVariable.StatusModel.C_ReserveRealHeadwearVer = file.IniReadValue("BCU", "RealHardWearVer");
                    GlobalVariable.StatusModel.C_ReserveSoftVer = file.IniReadValue("BCU", "SoftWearVer");
                    GlobalVariable.StatusModel.C_ReserveHeadwearVer = file.IniReadValue("BCU", "HardWearVer");
                    GlobalVariable.StatusModel.C_SOC = Convert.ToDouble(file.IniReadValue("BCU", "SOC"));
                    GlobalVariable.StatusModel.C_OffSet = Convert.ToDouble(file.IniReadValue("BCU", "Offset"));
                    GlobalVariable.StatusModel.M_ReserveSoftVer = file.IniReadValue("BMU", "SoftWearVer ");
                    GlobalVariable.StatusModel.M_ReserveHeadwearVer = file.IniReadValue("BMU", "HardWearVer  ");
                    GlobalVariable.StatusModel.M_ReserveJHL = file.IniReadValue("BMU", "JHL");
                    GlobalVariable.debug = file.IniReadValue("debug", "result") == "0" ? false : true;
                    GlobalVariable.RegexStr = "^[0-9|A-Z]+$";
                    GlobalVariable.Regexint = "^[0-9]+$";
                   
                 
                }
                else
                {

          
              
                    GlobalVariable.TimeOutValue = Convert.ToInt32(file2.IniReadValue("JMTimeOut", "TimeOutValue"));
                        GlobalVariable.StatusModel.Maximum = GlobalVariable.TimeOutValue;
                        list_can.Visible = true;
                        this.PANEL_BCU.Visible = false;
                        this.PANEL_BMU.Visible = false;
                
                        dt = ReadExcel_dt();
                    //  GlobalVariable.StatusModel.ListStatus = "TH_MSG6开启" + dt.Rows.Count.ToString();

               

                    //ReadExcel();




                    cbObjectChannel.SelectedIndex = 2;
                    
                    this.PANEL_BCU.Visible = false;
                    this.PANEL_BMU.Visible = false;
                    list_can.Visible = true;
                 
                  
                }
              
                try
                {
                    GlobalVariable.VoltageCoeff = Convert.ToInt32(file2.IniReadValue("System", "VoltageCoeff")) == 1;
                    this.ChangedBCUGrid();
                }
                catch
                {
                    GlobalVariable.VoltageCoeff = false;
                }
                GlobalVariable.StatusModel.M_ReserveBMUID = "1";
                this.cbBMUID.Items.Add(1);
                this.cbBMUID.Items.Add(2);
                this.cbBMUID.Items.Add(3);
                this.cbBMUID.Items.Add(4);
                this.cbBMUID.Items.Add(5);
                this.cbBMUID.SelectedIndex = 0;
                this.SetLogMsg("上位机程序初始化成功 ...", false);
                this.SetLogMsg("超时设置为  " + GlobalVariable.TimeOutValue + "  秒", false);
            }


            TH_MSG6 = new Thread(new ThreadStart(ReadExcel));
            if ((TH_MSG6 == null) || !TH_MSG6.IsAlive)
            {
                //  GlobalVariable.StatusModel.ListStatus = "TH_MSG开启";
                TH_MSG6.Start();
            }
            TH_MSG7 = new Thread(new ThreadStart(LOAD4));
            if ((TH_MSG7 == null) || !TH_MSG7.IsAlive)
            {
                // GlobalVariable.StatusModel.ListStatus = "TH_MSG开启";
                TH_MSG7.Start();
            }
        }
        private void res_load(string path)
        {
      
            file2.IniWriteValue("reload", "name", path);
          
         
            GlobalVariable.StatusModel.M_Reservename = path;
            file = new IniFile(@"\\192.168.100.252\fct数据\" + GlobalVariable.filepath + "" + @"\config_file\" + GlobalVariable.StatusModel.M_Reservename);
            if (!File.Exists(file.Path))

            { MessageBox.Show(file.Path+"路径不存在"); return; }
            if (path.Split('.')[1] == "ini")
            {
                GlobalVariable.TimeOutValue = Convert.ToInt32(file2.IniReadValue("TimeOut", "TimeOutValue"));
                GlobalVariable.StatusModel.Maximum = GlobalVariable.TimeOutValue;
                this.list_can.Visible = false;
                GlobalVariable.mes_con = false;
                GlobalVariable.StatusModel.C_ReserveRealSoftVer = file.IniReadValue("BCU", "RealSoftWearVer");
                GlobalVariable.StatusModel.C_ReserveRealHeadwearVer = file.IniReadValue("BCU", "RealHardWearVer");
                GlobalVariable.StatusModel.C_ReserveSoftVer = file.IniReadValue("BCU", "SoftWearVer");
                GlobalVariable.StatusModel.C_ReserveHeadwearVer = file.IniReadValue("BCU", "HardWearVer");
                GlobalVariable.StatusModel.C_SOC = Convert.ToDouble(file.IniReadValue("BCU", "SOC"));
                GlobalVariable.StatusModel.C_OffSet = Convert.ToDouble(file.IniReadValue("BCU", "Offset"));
                GlobalVariable.StatusModel.M_ReserveSoftVer = file.IniReadValue("BMU", "SoftWearVer ");
                GlobalVariable.StatusModel.M_ReserveHeadwearVer = file.IniReadValue("BMU", "HardWearVer  ");
                GlobalVariable.StatusModel.M_ReserveJHL = file.IniReadValue("BMU", "JHL");
                GlobalVariable.debug = file.IniReadValue("debug", "result") == "0" ? false : true;
                GlobalVariable.RegexStr = "^[0-9|A-Z]+$";
            }
            else
            {
                GlobalVariable.TimeOutValue = Convert.ToInt32(file2.IniReadValue("JMTimeOut", "TimeOutValue"));
                GlobalVariable.StatusModel.Maximum = GlobalVariable.TimeOutValue;
                list_can.Visible = true;
                this.PANEL_BCU.Visible = false;
                this.PANEL_BMU.Visible = false;
                
                dt = ReadExcel_dt();
             //   GlobalVariable.StatusModel.ListStatus = "TH_MSG6开启"+dt.Rows.Count.ToString(); 
                // TH_MSG6 = new Thread(new ThreadStart(ReadExcel));
                //if ((TH_MSG6 == null) || !TH_MSG6.IsAlive)
                //{
                //  //  GlobalVariable.StatusModel.ListStatus = "TH_MSG开启";
                //    TH_MSG6.Start();
                //}
                // TH_MSG7 = new Thread(new ThreadStart(LOAD4));
                //if ((TH_MSG7 == null) || !TH_MSG7.IsAlive)
                //{
                //  //  GlobalVariable.StatusModel.ListStatus = "TH_MSG开启";
                //    TH_MSG7.Start();
                //}


                cbObjectChannel.SelectedIndex = 2;
                this.PANEL_BCU.Visible = false;
                this.PANEL_BMU.Visible = false;
                list_can.Visible = true;
              
              

            }
          try
            {
                GlobalVariable.VoltageCoeff = Convert.ToInt32(file.IniReadValue("System", "VoltageCoeff")) == 1;
                this.ChangedBCUGrid();
            }
            catch
            {
                GlobalVariable.VoltageCoeff = false;
            }
         
            this.SetLogMsg("选择程序名"+path+"成功 ...", false);
            this.SetLogMsg("超时设置为  " + GlobalVariable.TimeOutValue + "  秒", false);
        
    

    }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void databing()
        {
         
            this.C_NOW_HEADER.DataBindings.Add("Text", GlobalVariable.StatusModel, "C_NowHeadwearVer");
            this.C_NOW_L_HEADER.DataBindings.Add("Text", GlobalVariable.StatusModel, "C_NowRealHeadwearVer");
            this.C_NOW_L_SOFT.DataBindings.Add("Text", GlobalVariable.StatusModel, "C_NowRealSoftVer");
             this.C_NOW_SOC.DataBindings.Add("Text", GlobalVariable.StatusModel, "CR_SOC");
            this.C_NOW_SOFT.DataBindings.Add("Text", GlobalVariable.StatusModel, "C_NowSoftVer");
            this.C_REV_HEADER.DataBindings.Add("Text", GlobalVariable.StatusModel, "C_ReserveHeadwearVer");
            this.C_REV_L_HEADER.DataBindings.Add("Text", GlobalVariable.StatusModel, "C_ReserveRealHeadwearVer");
            this.C_REV_SOC.DataBindings.Add("Text", GlobalVariable.StatusModel, "C_SOC");
            this.C_RE_L_SOFT.DataBindings.Add("Text", GlobalVariable.StatusModel, "C_ReserveRealSoftVer");
            this.C_RE_SOFT.DataBindings.Add("Text", GlobalVariable.StatusModel, "C_ReserveSoftVer");


            this.M_NOW_HEADER.DataBindings.Add("Text", GlobalVariable.StatusModel, "M_NowHeadWearVer");//S_txt_wo
        
            this.M_NOW_SOFT.DataBindings.Add("Text", GlobalVariable.StatusModel, "M_NowSoftVer");
            this.M_REV_HEADER.DataBindings.Add("Text", GlobalVariable.StatusModel, "M_ReserveHeadwearVer");
            
              this.M_RE_SOFT.DataBindings.Add("Text", GlobalVariable.StatusModel, "M_ReserveSoftVer");
            this.M_BMU_ID.DataBindings.Add("Text", GlobalVariable.StatusModel, "M_BMUID");


           // this.lstStatus.DataBindings.Add("Text", GlobalVariable.StatusModel, "ListStatus");

            this.lstSt.DataBindings.Add("Text", GlobalVariable.StatusModel, "CheckResult");

            this.PBAR.DataBindings.Add("Maximum", GlobalVariable.StatusModel, "Maximum");
            this.PBAR.DataBindings.Add("Minimum", GlobalVariable.StatusModel, "Minimum");
            this.PBAR.DataBindings.Add("Value", GlobalVariable.StatusModel, "NowValue");
            this.C_OFF.DataBindings.Add("Text", GlobalVariable.StatusModel, "C_OffSet");

            this.M_JHL.DataBindings.Add("Text", GlobalVariable.StatusModel, "M_jhl");

            this.M_RES_JHL.DataBindings.Add("Text", GlobalVariable.StatusModel, "M_ReserveJHL");
            this.filename.DataBindings.Add("Text", GlobalVariable.StatusModel, "M_Reservename");
            this.txtMsg.DataBindings.Add("Text", GlobalVariable.StatusModel, "MES_MSG");
            //this.txtBMSID.DataBindings.Add("Text", GlobalVariable.StatusModel, "BMSID");
          
           // this.soc.DataBindings.Add("Text", GlobalVariable.StatusModel, "CR_SOC");

          //  this.header.DataBindings.Add("Text", GlobalVariable.StatusModel, "M_NowHeadWearVer");//S_txt_wo

          //  this.soft.DataBindings.Add("Text", GlobalVariable.StatusModel, "M_NowSoftVer");
            // this.listBox1.DataBindings.Add("Text", GlobalVariable.StatusModel, "ListStatus");
        }


        private void Connect()
            {
                GlobalVariable.CANIndex = (int)this.cbChannel.SelectedValue;
                GlobalVariable.DeviceIndex = this.cbIndex.SelectedIndex;
                GlobalVariable.CANBaudRate = Convert.ToUInt32(this.cbBaudRate.SelectedValue);
                if (GlobalVariable.CANOperate.Open() == 1)
                {
                    this.cbBaudRate.Enabled = false;
                    this.cbChannel.Enabled = false;
                    this.cbIndex.Enabled = false;
                    this.btnConnect.Enabled = false;
                    this.SetLogMsg("CAN设备连接成功...", true);
                }
                else
                {
                    this.SetLogMsg("CAN设备连接失败...", true);
                }
            }

        private void DisConnect_Click()
            {
                if (GlobalVariable.CANOperate.Connected)
                {
                    GlobalVariable.CANOperate.Close();
                    this.SetLogMsg("CAN设备成功断开连接...", true);
                    this.cbBaudRate.Enabled = true;
                    this.cbChannel.Enabled = true;
                    this.cbIndex.Enabled = true;
                    this.btnConnect.Enabled = true;
                }
                else
                {
                    this.SetLogMsg("CAN设备尚未建立连接...", true);
                }
            }

        private void cbBMUID_SelectionChanged()
            {
                GlobalVariable.StatusModel.M_ReserveBMUID = Convert.ToInt32((int)(this.cbBMUID.SelectedIndex + 1)).ToString();
          
            GlobalVariable.StatusModel.M_ReserveJHL = file.IniReadValue("BMU", "JHL"+ GlobalVariable.StatusModel.M_ReserveBMUID);
        }

        private void cbObjectChannel_SelectionChanged()
            {
                this.ChangedBCUGrid();
            if (this.cbObjectChannel.SelectedIndex == 0)
            {
                GlobalVariable.IsBCU = true;
                this.SetLogMsg("当前目标设备 BCU 主机 ...", true);
            }
            else if (this.cbObjectChannel.SelectedIndex == 1)
            {
                GlobalVariable.IsBCU = false;
                this.SetLogMsg("当前目标设备 BMU 从机 ...", true);
            }
            else if (this.cbObjectChannel.SelectedIndex == 2)
            {
               // GlobalVariable.IsBCU = false;
                this.SetLogMsg("当前为EXCELL读取方式 ...", true);
            }
            }

        private void ChangedBCUGrid()
        {
            if ((this.cbObjectChannel.SelectedIndex == 1))
            { this.PANEL_BMU.Visible = true; }
            else if ((this.cbObjectChannel.SelectedIndex == 0))
            { this.PANEL_BMU.Visible = false; }
           
            else if ((this.cbObjectChannel.SelectedIndex == 2))
            {
                this.PANEL_BCU.Visible = false;
                this.PANEL_BMU.Visible = false;
                list_can.Visible = true;

            }


            this.PANEL_BCU.Visible = (this.cbObjectChannel.SelectedIndex == 0) ? true : false;

        }

      
        private void ResetBuff()
            {
                GlobalVariable.StatusModel.NowValue = 0;
                GlobalVariable.StatusModel.C_NowSoftVer = "                                ";
                GlobalVariable.StatusModel.C_NowHeadwearVer = "                    ";
                GlobalVariable.StatusModel.C_NowInsulationVer = "                    ";
                GlobalVariable.StatusModel.C_NowLinkVoltage = 0.0;
                GlobalVariable.StatusModel.C_NowPackVoltage = 0.0;
                GlobalVariable.StatusModel.M_NowSoftVer = "                                ";
                GlobalVariable.StatusModel.M_NowHeadWearVer = "                    ";
                GlobalVariable.StatusModel.M_BMUID = "";
                GlobalVariable.StatusModel.IsCheckResult = 0;
                GlobalVariable.IsFindBMSData = false;
                GlobalVariable.IsCheckOK = false;
                GlobalVariable.IsReadBMSID = false;
            }

        public void SetLogMsg(string msg, bool? isTimeVariable = true)
        {
           
        

            this.Invoke((MethodInvoker)delegate { 
            
                 

                   GlobalVariable.dt = DateTime.Now;


                if (this.lstStatus.Lines.Length > 1000)
                {
                    this.lstStatus.Clear();
                }
                if (isTimeVariable == true)
                {
                        GlobalVariable.StatusModel.ListStatus=(GlobalVariable.dt.ToString("HH:mm:ss") + "\t" + msg);
                }
                else
                {
                        GlobalVariable.StatusModel.ListStatus=("\t\t" + msg );
                }
             
             
            });
        }







        private void TextBox_TextChanged()
            {
         
            {
               
                if (F == "0")
                {
                    if (Regex.IsMatch(this.txtBMSID.Text.Trim(), GlobalVariable.RegexStr))
                    {


                        if (GlobalVariable.IsBCU)
                        {
                            if (GlobalVariable.BMSID.Substring(0, 2) != "01")
                            {
                                MessageBox.Show("条码非BCU编码");
                                this.txtBMSID.Focus();
                                this.txtBMSID.SelectAll();
                                return;
                            }
                        }
                        else if (GlobalVariable.BMSID.Substring(0, 2) != "02")
                        {
                            MessageBox.Show("条码非BMU编码");
                            this.txtBMSID.Focus();
                            this.txtBMSID.SelectAll();
                            return;
                        }
                        GlobalVariable.IsReadBMSID = true;
                    }

                    else
                    {
                        GlobalVariable.IsReadBMSID = false;
                        MessageBox.Show("BMS编码不符合规则");
                    }
                }
                else { GlobalVariable.IsReadBMSID = true; }

                }

                this.txtBMSID.Focus();
                this.txtBMSID.SelectAll();
            
        }

        private void Window_Closed()
            {
                Environment.Exit(2);
            }

        private void Window_Loaded()
            {
          
            //  this.DataContext = GlobalVariable.StatusModel;
            for (int i = 0; i < 8; i++)
                {
                    this.cbIndex.Items.Add(i);
                }
                this.cbIndex.SelectedIndex = 0;
                List<ComomboxItem> list = new List<ComomboxItem>();
                this.cbChannel.DisplayMember = "Name";
                this.cbChannel.ValueMember = "Value";
                ComomboxItem item = new ComomboxItem
                {
                    Name = "通道一",
                    Value = 0
                };
                list.Add(item);
                ComomboxItem item2 = new ComomboxItem
                {
                    Name = "通道二",
                    Value = 1
                };
                list.Add(item2);
                this.cbChannel.DataSource = list;
                this.cbChannel.SelectedIndex = 0;
                list = new List<ComomboxItem>();
                this.cbBaudRate.DisplayMember = "Name";
                this.cbBaudRate.ValueMember = "Value";
                ComomboxItem item3 = new ComomboxItem
                {
                    Name = "5 Kbps",
                    Value = 5
                };
                list.Add(item3);
                ComomboxItem item4 = new ComomboxItem
                {
                    Name = "10 Kbps",
                    Value = 10
                };
                list.Add(item4);
                ComomboxItem item5 = new ComomboxItem
                {
                    Name = "20 Kbps",
                    Value = 20
                };
                list.Add(item5);
                ComomboxItem item6 = new ComomboxItem
                {
                    Name = "50 Kbps",
                    Value = 50
                };
                list.Add(item6);
                ComomboxItem item7 = new ComomboxItem
                {
                    Name = "100 Kbps",
                    Value = 100
                };
                list.Add(item7);
                ComomboxItem item8 = new ComomboxItem
                {
                    Name = "125 Kbps",
                    Value = 0x7d
                };
                list.Add(item8);
                ComomboxItem item9 = new ComomboxItem
                {
                    Name = "250 Kbps",
                    Value = 250
                };
                list.Add(item9);
                ComomboxItem item10 = new ComomboxItem
                {
                    Name = "500 Kbps",
                    Value = 500
                };
                list.Add(item10);
                ComomboxItem item11 = new ComomboxItem
                {
                    Name = "800 Kbps",
                    Value = 800
                };
                list.Add(item11);
                this.cbBaudRate.DataSource = list;
                this.cbBaudRate.SelectedValue = 500;
                list = new List<ComomboxItem>();
                this.cbObjectChannel.DisplayMember = "Name";
                this.cbObjectChannel.ValueMember = "Value";
                ComomboxItem item12 = new ComomboxItem
                {
                    Name = "BCU",
                    Value = 0
                };
                list.Add(item12);
                ComomboxItem item13 = new ComomboxItem
                {
                    Name = "BMU",
                    Value = 1
                };
                list.Add(item13);
                this.cbObjectChannel.DataSource = list;
                this.cbObjectChannel.SelectedIndex = 0;
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                AssemblyCopyrightAttribute customAttribute = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCopyrightAttribute));
                AssemblyDescriptionAttribute attribute2 = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyDescriptionAttribute));
                Version version = executingAssembly.GetName().Version;
                GlobalVariable.dt = DateTime.Now;
            // this.lstStatus.DataSource = mod.ListStatus;
            GlobalVariable.StatusModel.ListStatus=("程序当前版本\tV " + version.ToString());
          
                try
                {
                    GlobalVariable.TimeOutValue = Convert.ToInt32(file.IniReadValue("TimeOut", "TimeOutValue"));
                    if (GlobalVariable.TimeOutValue <= 0)
                    {
                        GlobalVariable.TimeOutValue = 5;
                    }
                }
                catch
                {
                    GlobalVariable.TimeOutValue = 15;
                }
                GlobalVariable.StatusModel.Maximum = GlobalVariable.TimeOutValue;
                //try
                //{
                if (!File.Exists(file.Path))

                { MessageBox.Show(file.Path+"路径不存在");return; }

                    GlobalVariable.StatusModel.C_ReserveRealSoftVer = file.IniReadValue("BCU", "RealSoftWearVer");
                    GlobalVariable.StatusModel.C_ReserveRealHeadwearVer = file.IniReadValue("BCU", "RealHardWearVer");
                    GlobalVariable.StatusModel.C_ReserveSoftVer = file.IniReadValue("BCU", "SoftWearVer");
                    GlobalVariable.StatusModel.C_ReserveHeadwearVer = file.IniReadValue("BCU", "HardWearVer");
                    GlobalVariable.StatusModel.C_SOC = Convert.ToDouble(file.IniReadValue("BCU", "SOC"));
                    GlobalVariable.StatusModel.C_OffSet = Convert.ToDouble(file.IniReadValue("BCU", "Offset"));
                    GlobalVariable.StatusModel.M_ReserveSoftVer = file.IniReadValue("BMU", "SoftWearVer ");
                    GlobalVariable.StatusModel.M_ReserveHeadwearVer = file.IniReadValue("BMU", "HardWearVer  ");
                    GlobalVariable.RegexStr = "^[0-9|A-Z]+$";
              //  }
                //catch(Exception ex)
                //{
                //MessageBox.Show(ex.Message);
                //    this.SetLogMsg("加载配置文件出现严重的错误 ...", true);
                //}
                try
                {
                    GlobalVariable.VoltageCoeff = Convert.ToInt32(file.IniReadValue("System", "VoltageCoeff")) == 1;
                    this.ChangedBCUGrid();
                }
                catch
                {
                    GlobalVariable.VoltageCoeff = false;
                }
                GlobalVariable.StatusModel.M_ReserveBMUID = "1";
                this.cbBMUID.Items.Add(1);
                this.cbBMUID.Items.Add(2);
                this.cbBMUID.Items.Add(3);
                this.cbBMUID.Items.Add(4);
                this.cbBMUID.Items.Add(5);
                this.cbBMUID.SelectedIndex = 0;
                this.SetLogMsg("上位机程序初始化成功 ...", false);
                this.SetLogMsg("超时设置为  " + GlobalVariable.TimeOutValue + "  秒", false);
            }
        public class ComomboxItem
        {
            public string Name { set; get; }
            public int Value { set; get; }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Connect();
            txtBMSID.Focus();
            txtBMSID.SelectAll();

        }

        private void btnDisConnect_Click(object sender, EventArgs e)
        {
            DisConnect_Click();
            DataTable dd = new DataTable();
            dd = null;
            list_can.DataSource = dd;
        }

        private void CAN_RECV_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Window_Closed();
            
        }


        private void cbBMUID_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbBMUID_SelectionChanged();
        }

        private void cbObjectChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbObjectChannel_SelectionChanged();
            txtBMSID.Focus();
            txtBMSID.SelectAll();
        }

        private void txtBMSID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
               // this.thre_con.Set();
              //  this.mes_press.Set();
                this.txtMsg.Text = " ";
                this.txtMsg.BackColor = Color.White;


                if (this.txtBMSID.Text.Length < 9)
                {
                    return;
                }

                else
                {
                    // TextBox_TextChanged();
                    
                        GlobalVariable.BMSID = this.txtBMSID.Text.Trim().ToUpper().ToString();

                        if ( F == "0")
                        {
                            if (Regex.IsMatch(this.txtBMSID.Text.Trim(), GlobalVariable.RegexStr))
                            {

                            
                             if (GlobalVariable.BMSID.Substring(0, 2) != "01"&&GlobalVariable.BMSID.Substring(0, 2) != "02"&&GlobalVariable.BMSID.Substring(0, 2) != "03"&& GlobalVariable.BMSID.Substring(0, 2) != "04")
                            {
                                MessageBox.Show("条码非规则编码");
                                this.txtBMSID.Focus();
                                this.txtBMSID.SelectAll();
                                return;
                            }
                           
                            GlobalVariable.IsReadBMSID = true;
                            if (GlobalVariable.mes_con)
                            {
                                GlobalVariable.loop = true;
                                //GlobalVariable.JM_TH.Set();
                                GlobalVariable.JM_TH_boo = true;
                                GlobalVariable.P = 0;
                            }
                        }

                            else
                            {
                                GlobalVariable.IsReadBMSID = false;
                                MessageBox.Show("BMS编码不符合规则");
                            }
                      
                       
                        
                        this.txtBMSID.Focus();
                        this.txtBMSID.SelectAll();

                    }


                    if (F == "1")
                    {
                        this.txtBMSID.Focus();
                        this.txtBMSID.SelectAll();
                        GlobalVariable.IsReadBMSID = true;



                        if (GlobalVariable.mes_con)
                        {
                            GlobalVariable.loop = true;
                            //   GlobalVariable.JM_TH.Set();
                            //GlobalVariable.JM_TH.Set();
                            GlobalVariable.JM_TH_boo = true;
                            GlobalVariable.P = 0;
                        }
                        if (this.txtBMSID.Text.ToUpper().Length > 9)
                        {
                            SN = this.txtBMSID.Text.ToUpper().Substring(0, 9);
                        }
                        else
                        { SN = this.txtBMSID.Text.Trim().ToUpper().ToString(); }
                        if (!this.GetSNInfo(SN))
                        {

                        }

                    }
                }
                
            }
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem1_Click_1(object sender, EventArgs e)
        {
            Chos cs = new Chos();
            if (cs.ShowDialog() == DialogResult.OK)
            {
                res_load(GlobalVariable.file_path);
                MES_PA();
                cbBMUID_SelectionChanged();

            }
            else
            {
            }
        }

        private void txtBMSID_TextChanged(object sender, EventArgs e)
        {

        }

        private void filename_Click(object sender, EventArgs e)
        {

        }

        private void CAN_RECV_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisConnect_Click();
        }
    }
    }



