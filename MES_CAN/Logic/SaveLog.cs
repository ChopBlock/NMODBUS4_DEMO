using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MES_CAN
{
    public static class SaveLog
    {
        private static DateTime dt;
        private static string Path;
        private static StringBuilder sb;

        public static void SaveLogMessage(string BMSID, string SoftWare, string HardWare, string RealSoftWare, string RealHardWare, string SOC, string CheckResult,string JHL,string SQE)
        {
           
            Path = @"\\192.168.100.252\fct数据\" + GlobalVariable.filepath + "" + @"\CheckLOG\CheckLOG.CSV";
            dt = DateTime.Now;
            sb = new StringBuilder();
            sb.Append(dt.ToString("yy-MM-dd HH:mm:ss") + ",");
            sb.Append(GlobalVariable.IsBCU ? "BCU," : "BMU,");
            sb.Append(BMSID + ",");
            sb.Append("软件版本号：" + SoftWare + ",");
            sb.Append("硬件版本号：" + HardWare + ",");
            if (GlobalVariable.IsBCU)
            {
                sb.Append("软件法规版本号：" + RealSoftWare + ",");
                sb.Append("硬件法规版本号：" + RealHardWare + ",");
                sb.Append("SOC：" + SOC + ",");
            }
            if (!GlobalVariable.IsBCU)
            {
                sb.Append("芯片数量：" + JHL + ",");
                sb.Append("从机序号：" + SQE + ",");
             

            }
            sb.Append("操作人员：" + MESSecurity.clsPPSPublicModule.PPSUserName + ",");
            sb.Append(CheckResult + "\r\n");
            File.AppendAllText(Path, sb.ToString(), Encoding.GetEncoding("GB2312"));
        }


        public static void SaveLogMessage_new(string message)
        {

            Path = @"\\192.168.100.252\fct数据\" + GlobalVariable.filepath + "" + @"\CheckLOG\CheckLOG.CSV";
            #region 不需要的
            //dt = DateTime.Now;
            //sb = new StringBuilder();
            //sb.Append(dt.ToString("yy-MM-dd HH:mm:ss") + ",");
            //sb.Append(GlobalVariable.IsBCU ? "BCU," : "BMU,");
            //sb.Append(BMSID + ",");
            //sb.Append("软件版本号：" + SoftWare + ",");
            //sb.Append("硬件版本号：" + HardWare + ",");
            //if (GlobalVariable.IsBCU)
            //{
            //    sb.Append("软件法规版本号：" + RealSoftWare + ",");
            //    sb.Append("硬件法规版本号：" + RealHardWare + ",");
            //    sb.Append("SOC：" + SOC + ",");
            //}
            //if (!GlobalVariable.IsBCU)
            //{
            //    sb.Append("芯片数量：" + JHL + ",");
            //    sb.Append("从机序号：" + SQE + ",");


            //}
            //sb.Append("操作人员：" + MESSecurity.clsPPSPublicModule.PPSUserName + ",");
            //sb.Append(CheckResult + "\r\n");
            #endregion
            File.AppendAllText(Path, message.ToString(), Encoding.GetEncoding("GB2312"));
        }
    }
}
