using Modbus.Device;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NMODBUS4_DEMO
{
public    class Modbusoperate
    {

       

        /// <summary>
        /// 枚举功能码
        /// </summary>
        enum functionCode
        {   [Description("01 Read Coils")]
            A,
            [Description("02 Read DisCrete Inputs")]
            B,
            [Description("03 Read Holding Registers")]
            C,
            [Description("04 Read Input Registers")]
            D,
            [Description("05 Write Single Coil")]
            E,
            [Description("06 Write Single Registers")]
            F,
            [Description("0F Write Multiple Coils")]
            H,
            [Description("10 Write Multiple Registers")]
            I

        }

        /// <summary>
        /// 根据功能码传值
        /// </summary>
        /// <param name="functionCode"></param>
        private async  void ExecuteFunction(DataTable dt)
        {
            try
            {
              
          

                for (int k=0;k<dt.Rows.Count;k++)

                {
                    Thread.Sleep(100);
                    if (dt.Rows[k]["FUNCTION"].ToString() == "E" || dt.Rows[k]["FUNCTION"].ToString() == "H")
                    {
                        for (int i = 0; i < dt.Rows[k]["ARRAY[Ushort]"].ToString().Split(' ').Length; i++)

                        {
                            GlobalVariable.coilsBuffer = new bool[dt.Rows[k]["ARRAY[Ushort]"].ToString().Split(' ').Length];
                            // strarr[i] == "0" ? coilsBuffer[i] = true : coilsBuffer[i] = false;
                            if (dt.Rows[k]["ARRAY[Ushort]"].ToString().Split(' ')[i] == "0")
                            {
                                GlobalVariable.coilsBuffer[i] = false;
                            }
                            else
                            {
                                GlobalVariable.coilsBuffer[i] = true;
                            }
                        }
                    }
                    if (dt.Rows[k]["FUNCTION"].ToString() == "F" || dt.Rows[k]["FUNCTION"].ToString() == "I")
                    {
                        //转化ushort数组
                        string[] strarr = dt.Rows[k]["ARRAY[Ushort]"].ToString().Split(' ');
                       GlobalVariable.registerBuffer = new ushort[strarr.Length];
                        for (int i = 0; i < strarr.Length; i++)
                        {
                            GlobalVariable.registerBuffer[i] = ushort.Parse(strarr[i]);
                        }
                    }
                    string s = string.Empty;
                    switch (dt.Rows[k]["FUNCTION"].ToString())
                    {
                      
                        case "A"://读取单个线圈
                        
                            GlobalVariable.coilsBuffer =GlobalVariable.Mdapi.master.ReadCoils(Convert.ToByte( dt.Rows[k]["SLAVE_ID"].ToString()),Convert.ToUInt16( dt.Rows[k]["STRATADDRESS"].ToString()),Convert.ToUInt16( dt.Rows[k]["LENTH"].ToString()));

                            for (int i = 0; i < GlobalVariable.coilsBuffer.Length; i++)
                            {

                                //  GlobalVariable.MSG_THOD(i, GlobalVariable.coilsBuffer[i].ToString(),GlobalVariable.OK);
                                s += GlobalVariable.coilsBuffer[i].ToString()+" ";

                            }
                            GlobalVariable.Read_Value = s;
                            s = "";
                            break;
                        case "B"://读取输入线圈/离散量线圈


                            GlobalVariable.coilsBuffer = GlobalVariable.Mdapi.master.ReadInputs(Convert.ToByte(dt.Rows[k]["SLAVE_ID"].ToString()), Convert.ToUInt16(dt.Rows[k]["STRATADDRESS"].ToString()), Convert.ToUInt16(dt.Rows[k]["LENTH"].ToString()));

                            for (int i = 0; i < GlobalVariable.coilsBuffer.Length; i++)
                            {

                                //  GlobalVariable.MSG_THOD(i, GlobalVariable.coilsBuffer[i].ToString(),GlobalVariable.OK);
                                s += GlobalVariable.coilsBuffer[i].ToString() + " ";

                            }
                            GlobalVariable.Read_Value = s;
                            s = "";

                            break;
                        case "C"://读取保持寄存器
                            GlobalVariable.StatusModes.slaveAddress = Convert.ToByte(dt.Rows[k]["SLAVE_ID"].ToString());
                            GlobalVariable.registerBuffer = GlobalVariable.Mdapi.master.ReadHoldingRegisters(Convert.ToByte(dt.Rows[k]["SLAVE_ID"].ToString()), Convert.ToUInt16(dt.Rows[k]["STRATADDRESS"].ToString()), Convert.ToUInt16(dt.Rows[k]["LENTH"].ToString()));
                            for (int i = 0; i < GlobalVariable.registerBuffer.Length; i++)
                            {

                                //  GlobalVariable.MSG_THOD(i, GlobalVariable.coilsBuffer[i].ToString(),GlobalVariable.OK);
                                s += GlobalVariable.registerBuffer[i].ToString() + " ";

                            }
                            GlobalVariable.Read_Value = s;
                            s = "";

                            break;
                        case "D"://读取输入寄存器

                            GlobalVariable.registerBuffer = GlobalVariable.Mdapi.master.ReadInputRegisters(Convert.ToByte(dt.Rows[k]["SLAVE_ID"].ToString()), Convert.ToUInt16(dt.Rows[k]["STRATADDRESS"].ToString()), Convert.ToUInt16(dt.Rows[k]["LENTH"].ToString()));
                            for (int i = 0; i < GlobalVariable.registerBuffer.Length; i++)
                            {

                                //  GlobalVariable.MSG_THOD(i, GlobalVariable.coilsBuffer[i].ToString(),GlobalVariable.OK);
                                s += GlobalVariable.registerBuffer[i].ToString() + " ";

                            }
                            GlobalVariable.Read_Value = s;
                            s = "";

                            break;
                        case "E"://写单个线圈

                          await   GlobalVariable.Mdapi.master.WriteSingleCoilAsync(Convert.ToByte(dt.Rows[k]["SLAVE_ID"].ToString()), Convert.ToUInt16(dt.Rows[k]["STRATADDRESS"].ToString()), GlobalVariable.coilsBuffer[0]);
                            break;
                        case "F"://写单个输入线圈/离散量线圈

                          await   GlobalVariable.Mdapi.master.WriteSingleRegisterAsync(GlobalVariable.StatusModes.slaveAddress, GlobalVariable.StatusModes.startAddress, GlobalVariable.registerBuffer[0]);
                            break;
                        case "H"://写一组线圈

                          await GlobalVariable.Mdapi.master.WriteMultipleCoilsAsync(GlobalVariable.StatusModes.slaveAddress, GlobalVariable.StatusModes.startAddress, GlobalVariable.coilsBuffer);
                            break;
                        case "I"://写一组保持寄存器

                          await GlobalVariable.Mdapi.master.WriteMultipleRegistersAsync(GlobalVariable.StatusModes.slaveAddress, GlobalVariable.StatusModes.startAddress, GlobalVariable.registerBuffer);
                            break;
                        default:
                            break;
                    }
                  
                }

                #region  去除释放

                //if (GlobalVariable.StatusModes.Modbus_Mode_s == 0)
                //{
                //    if (GlobalVariable.Mdapi.port != null & GlobalVariable.Mdapi.port.IsOpen)
                //    {
                //        GlobalVariable.Mdapi.port.Close();
                //      //  GlobalVariable.Mdapi.port.Dispose();
                //    }
                //}
                //else
                //{if (GlobalVariable.Mdapi.tcpClient != null & GlobalVariable.Mdapi.tcpClient.Connected)
                //    {

                //        GlobalVariable.Mdapi.tcpClient.Close();
                //        GlobalVariable.Mdapi.tcpClient = null;
                //    }


                //}
                #endregion
            }
            catch (Exception ex)
            {
                GlobalVariable.Read_Value = ex.Message;
                }
        }

        public void read_DT()
        {
          //  while (GlobalVariable.conn_flag)
            {
                Thread.Sleep(1000);
                if (GlobalVariable.conn_flag)
                {
                    DataTable dtt = new DataTable();



                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook wbook = app.Workbooks.Open(GlobalVariable.Read_Write, Type.Missing, Type.Missing,

                         Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,

                         Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,

                         Type.Missing, Type.Missing);


                    Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)wbook.Worksheets[1];





                    try
                    {
                        GlobalVariable.StatusModes.Maximum = workSheet.UsedRange.Rows.Count;
                        //  await     Task.Run(() =>
                        {
                            for (int k = 1; k < workSheet.UsedRange.Columns.Count + 1; k++)
                            {

                                dtt.Columns.Add(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[1, k]).Text.ToString());
                            }

                            for (int i = 2; i < workSheet.UsedRange.Rows.Count + 1; i++)
                            {

                                GlobalVariable.StatusModes.NowValue = i;
                                dtt.Rows.Add(1);

                                //step_no
                                dtt.Rows[i - 2][1 - 1] = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[i, 1]).Text.ToString();
                                //slave_ID
                                dtt.Rows[i - 2][2 - 1] = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[i, 2]).Text.ToString();
                                //function
                                dtt.Rows[i - 2][3 - 1] = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[i, 3]).Text.ToString();
                                //strataddress
                                dtt.Rows[i - 2][4 - 1] = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[i, 4]).Text.ToString();
                                //type
                                dtt.Rows[i - 2][5 - 1] = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[i, 5]).Text.ToString();
                                //lenth
                                dtt.Rows[i - 2][6 - 1] = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[i, 6]).Text.ToString();
                                //array[ushort]
                                dtt.Rows[i - 2][7 - 1] = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[i, 7]).Text.ToString();


                            }

                            wbook.Close(false, Type.Missing, Type.Missing); wbook = null;            //quit excel app          
                            app.Quit();



                        }
                        //);

                        GlobalVariable.dt = dtt;

                


                    }

                    catch (Exception ex)
                    {
                        wbook.Close(false, Type.Missing, Type.Missing); wbook = null;            //quit excel app          
                        app.Quit();


                        //     GlobalVariable.StatusModel.ListStatus = "ReadExcel_dt" + ex.Message;
                        GlobalVariable.conn_flag = false;
                        GlobalVariable.Read_Value = ex.Message;

                    }

                    GlobalVariable.conn_flag = false;
                }
            }


        }


        public void dt()
        {
            GlobalVariable.conn_flag = true;
            thrd_run();
        }


        public void thrd_run()
        {


            if (GlobalVariable.TH_dt == null || !GlobalVariable.TH_dt.IsAlive)
            {
                GlobalVariable.TH_dt = new Thread(new ThreadStart(read_DT));
                GlobalVariable.TH_dt.Start();
            }

            if (GlobalVariable.TH_Master_Read == null||! GlobalVariable.TH_Master_Read.IsAlive)
            {
                GlobalVariable.TH_Master_Read = new Thread(new ThreadStart(read_Master));
                GlobalVariable.TH_Master_Read.Start();
            }
        }

        public void read_Master()
        {
            GlobalVariable.TH_dt.Join();
            #region 初始化
            switch (GlobalVariable.StatusModes.Modbus_Mode_s)
            {
                case 0:

                    GlobalVariable.Mdapi.port = new SerialPort(GlobalVariable.StatusModes.portName, GlobalVariable.StatusModes.baudRate, GlobalVariable.StatusModes.parity, GlobalVariable.StatusModes.dataBits, GlobalVariable.StatusModes.stopBits);
                    GlobalVariable.Mdapi.master = ModbusSerialMaster.CreateRtu(GlobalVariable.Mdapi.port);
                    GlobalVariable.Mdapi.master.Transport.Retries = 0;
                    GlobalVariable.Mdapi.master.Transport.ReadTimeout = 1000;
                    if (GlobalVariable.Mdapi.port.IsOpen == false)
                    {
                        GlobalVariable.Mdapi.port.Open();
                    }
                    else
                    { GlobalVariable.Mdapi.port.Close(); }
                    break;
                case 1:
                    GlobalVariable.Mdapi.tcpClient = new TcpClient(GlobalVariable.StatusModes.IP, GlobalVariable.StatusModes.PORT);
                    GlobalVariable.Mdapi.master = ModbusIpMaster.CreateIp(GlobalVariable.Mdapi.tcpClient);
                    GlobalVariable.Mdapi.master.Transport.Retries = 0;
                    GlobalVariable.Mdapi.master.Transport.ReadTimeout = 1000;
                    break;
                default:
                    GlobalVariable.Mdapi.port = new SerialPort(GlobalVariable.StatusModes.portName, GlobalVariable.StatusModes.baudRate, GlobalVariable.StatusModes.parity, GlobalVariable.StatusModes.dataBits, GlobalVariable.StatusModes.stopBits);
                    GlobalVariable.Mdapi.master = ModbusSerialMaster.CreateRtu(GlobalVariable.Mdapi.port);
                    GlobalVariable.Mdapi.master.Transport.Retries = 0;
                    GlobalVariable.Mdapi.master.Transport.ReadTimeout = 1000;
                    if (GlobalVariable.Mdapi.port.IsOpen == false)
                    {
                        GlobalVariable.Mdapi.port.Open();
                    }
                    break;


            }
            #endregion
            while (true)

            {
          
                //  if (GlobalVariable.dt)
                Thread.Sleep(100);
                ExecuteFunction(GlobalVariable.dt);
            }
        }

    }
}
