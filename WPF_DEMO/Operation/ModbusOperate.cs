using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


namespace WPF_DEMO
{
    public class ModbusOperate
    {
            
        public bool con()
        {
            try
            {
                
                #region 初始化
                switch (GlobalVariable.Mod)
                {
                    case 0:

                        GlobalVariable.Port = new SerialPort(GlobalVariable.PortName, GlobalVariable.Baud, GlobalVariable.Parity, GlobalVariable.Date_bit, GlobalVariable.stopBits);
                        GlobalVariable.Master = ModbusSerialMaster.CreateRtu(GlobalVariable.Port);
                        GlobalVariable.Master.Transport.Retries = 0;
                        GlobalVariable.Master.Transport.ReadTimeout = 1000;
                        if (GlobalVariable.Port.IsOpen == false)
                        {
                            GlobalVariable.Port.Open();
                        }
                        else
                        { GlobalVariable.Port.Close(); }
                        break;
                    case 1:
                        GlobalVariable.tcpClient = new TcpClient(GlobalVariable.IP, GlobalVariable.port_no);
                        GlobalVariable.Master = ModbusIpMaster.CreateIp(GlobalVariable.tcpClient);
                        GlobalVariable.Master.Transport.Retries = 0;
                        GlobalVariable.Master.Transport.ReadTimeout = 1000;
                        break;
                    default:
                        GlobalVariable.Port = new SerialPort(GlobalVariable.PortName, GlobalVariable.Baud, GlobalVariable.Parity, GlobalVariable.Date_bit, GlobalVariable.stopBits);
                        GlobalVariable.Master = ModbusSerialMaster.CreateRtu(GlobalVariable.Port);
                        GlobalVariable.Master.Transport.Retries = 0;
                        GlobalVariable.Master.Transport.ReadTimeout = 1000;
                        if (GlobalVariable.Port.IsOpen == false)
                        {
                            GlobalVariable.Port.Open();
                        }
                        break;


                }

                #endregion
                GlobalVariable.Connected = true;
              
                new TaskFactory().StartNew(Tsk);
                return true;

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                GlobalVariable.Connected = false;
                return false;
            }
        }

        private async Task ExecuteFunction(DataTable dt)
        {
            try
            {
                while (GlobalVariable.Connected)
                {
                    if (GlobalVariable.Modbus_dt.Rows.Count < 1) { continue; }

                 
                    for (int k = 0; k < dt.Rows.Count; k++)

                    {
                      
                      //  GlobalVariable.con.WaitOne();

                        Thread.Sleep(100);
                        if (GlobalVariable.Excell_Coll[k].FUNCTION.ToString() == "E" || GlobalVariable.Excell_Coll[k].FUNCTION.ToString() == "H")
                        {
                            for (int i = 0; i < GlobalVariable.Excell_Coll[k].ARRAY.ToString().Split(' ').Length; i++)

                            {
                                GlobalVariable.coilsBuffer = new bool[GlobalVariable.Excell_Coll[k].ARRAY.ToString().Split(' ').Length];
                                // strarr[i] == "0" ? coilsBuffer[i] = true : coilsBuffer[i] = false;
                                if (GlobalVariable.Excell_Coll[k].ARRAY.ToString().Split(' ')[i] == "0")
                                {
                                    GlobalVariable.coilsBuffer[i] = false;
                                }
                                else
                                {
                                    GlobalVariable.coilsBuffer[i] = true;
                                }
                            }
                        }
                        if (GlobalVariable.Excell_Coll[k].FUNCTION.ToString() == "F" || GlobalVariable.Excell_Coll[k].FUNCTION.ToString() == "I")
                        {
                            //转化ushort数组
                            string[] strarr = GlobalVariable.Excell_Coll[k].ARRAY.ToString().Split(' ');
                            GlobalVariable.registerBuffer = new ushort[strarr.Length];
                            for (int i = 0; i < strarr.Length; i++)
                            {
                                GlobalVariable.registerBuffer[i] = ushort.Parse(strarr[i]);
                            }
                        }
                        string s = string.Empty;
                        switch (GlobalVariable.Excell_Coll[k].FUNCTION.ToString())
                        {

                            case "A"://读取单个线圈

                                GlobalVariable.coilsBuffer = GlobalVariable.Master.ReadCoils(Convert.ToByte(GlobalVariable.Excell_Coll[k].SLAVE_ID.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].STRATADDRESS.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].LENTH.ToString()));

                                for (int i = 0; i < GlobalVariable.coilsBuffer.Length; i++)
                                {

                                    //  GlobalVariable.MSG_THOD(i, GlobalVariable.coilsBuffer[i].ToString(),GlobalVariable.OK);
                                    s += GlobalVariable.coilsBuffer[i].ToString() + " ";

                                }
                             //   GlobalVariable.Message.Add(new GlobalVariable.msg { No = GlobalVariable.Message.Count() + 1, Value = s.ToString() });
await msg(new GlobalVariable.msg { Value = s.ToString() });
                                break;
                            case "B"://读取输入线圈/离散量线圈


                                GlobalVariable.coilsBuffer = GlobalVariable.Master.ReadInputs(Convert.ToByte(GlobalVariable.Excell_Coll[k].SLAVE_ID.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].STRATADDRESS.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].LENTH.ToString()));

                                for (int i = 0; i < GlobalVariable.coilsBuffer.Length; i++)
                                {

                                    //  GlobalVariable.MSG_THOD(i, GlobalVariable.coilsBuffer[i].ToString(),GlobalVariable.OK);
                                    s += GlobalVariable.coilsBuffer[i].ToString() + " ";

                                }
                              //  GlobalVariable.Message.Add(new GlobalVariable.msg { No = GlobalVariable.Message.Count() + 1, Value = s.ToString() });
                               await msg(new GlobalVariable.msg {  Value = s.ToString() });
                                break;
                            case "C"://读取保持寄存器
                               
                                GlobalVariable.registerBuffer = GlobalVariable.Master.ReadHoldingRegisters(Convert.ToByte(GlobalVariable.Excell_Coll[k].SLAVE_ID.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].STRATADDRESS.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].LENTH.ToString()));
                                for (int i = 0; i < GlobalVariable.registerBuffer.Length; i++)
                                {

                                    //  GlobalVariable.MSG_THOD(i, GlobalVariable.coilsBuffer[i].ToString(),GlobalVariable.OK);
                                    s += GlobalVariable.registerBuffer[i].ToString() + " ";

                                }
                              //  App.Current.Dispatcher.BeginInvoke(new Action(() => { GlobalVariable.Message.Add(new GlobalVariable.msg { No = GlobalVariable.Message.Count() + 1, Value = s.ToString() }); }));
                              
                               await msg(new GlobalVariable.msg {  Value = s.ToString() });
                                break;
                            case "D"://读取输入寄存器

                                GlobalVariable.registerBuffer = GlobalVariable.Master.ReadInputRegisters(Convert.ToByte(GlobalVariable.Excell_Coll[k].SLAVE_ID.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].STRATADDRESS.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].LENTH.ToString()));
                                for (int i = 0; i < GlobalVariable.registerBuffer.Length; i++)
                                {

                                    //  GlobalVariable.MSG_THOD(i, GlobalVariable.coilsBuffer[i].ToString(),GlobalVariable.OK);
                                    s += GlobalVariable.registerBuffer[i].ToString() + " ";

                                }
                             //   GlobalVariable.Message.Add(new GlobalVariable.msg { No = GlobalVariable.Message.Count() + 1, Value = s.ToString() });
                                await msg(new GlobalVariable.msg { No = GlobalVariable.Message.Count() + 1, Value = s.ToString() });
                                break;
                            case "E"://写单个线圈

                                await GlobalVariable.Master.WriteSingleCoilAsync(Convert.ToByte(GlobalVariable.Excell_Coll[k].SLAVE_ID.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].STRATADDRESS.ToString()), GlobalVariable.coilsBuffer[0]);
                                break;
                            case "F"://写单个输入线圈/离散量线圈

                                await GlobalVariable.Master.WriteSingleRegisterAsync(Convert.ToByte(GlobalVariable.Excell_Coll[k].SLAVE_ID.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].STRATADDRESS.ToString()), GlobalVariable.registerBuffer[0]);
                                break;
                            case "H"://写一组线圈

                                await GlobalVariable.Master.WriteMultipleCoilsAsync(Convert.ToByte(GlobalVariable.Excell_Coll[k].SLAVE_ID.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].STRATADDRESS.ToString()), GlobalVariable.coilsBuffer);
                                break;
                            case "I"://写一组保持寄存器

                                await GlobalVariable.Master.WriteMultipleRegistersAsync(Convert.ToByte(GlobalVariable.Excell_Coll[k].SLAVE_ID.ToString()), Convert.ToUInt16(GlobalVariable.Excell_Coll[k].STRATADDRESS.ToString()), GlobalVariable.registerBuffer);
                                break;
                            default:
                                break;



                        }
                        if (GlobalVariable.Bt_Cancel == true) { break; }
                    }
                }
               
            }
            catch (Exception ex)
            {
                GlobalVariable.Port.Close();
                GlobalVariable.Port.Dispose();
                GlobalVariable.Master.Dispose();
              //  GlobalVariable.Message.Add(new GlobalVariable.msg { No = GlobalVariable.Message.Count() + 1, Value = ex.ToString() });
                msg(new GlobalVariable.msg {  Value = ex.ToString() });
            }
        }
      
        public bool read_Exce_dt()
        {
            Microsoft.Office.Interop.Excel.Application app;
            Microsoft.Office.Interop.Excel.Workbook wbook;
          
           GlobalVariable.Excell_Task=   new TaskFactory().StartNew(() =>
            {

                GlobalVariable.Modbus_dt.Clear();

                DataTable dtt = GlobalVariable.Modbus_dt;



                app = new Microsoft.Office.Interop.Excel.Application();
                wbook = app.Workbooks.Open(GlobalVariable.Excell_pth, Type.Missing, Type.Missing,

                     Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,

                     Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,

                     Type.Missing, Type.Missing);


                Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)wbook.Worksheets[1];





             


                    {
                        if (!(dtt.Columns.Count > 0))
                        {
                            for (int k = 1; k < workSheet.UsedRange.Columns.Count + 1; k++)
                            {

                                dtt.Columns.Add(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[1, k]).Text.ToString());
                            }

                        }

                        GlobalVariable.stmod.Maxmum = workSheet.UsedRange.Rows.Count;
                        for (int i = 2; i < workSheet.UsedRange.Rows.Count + 1; i++)
                        {
                            Thread.Sleep(100);
                            GlobalVariable.stmod.Vale = i+1;
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

                        Excell_Coll(new GlobalVariable.Excell_Header { NO = dtt.Rows[i - 2][1 - 1].ToString(), SLAVE_ID = dtt.Rows[i - 2][2 - 1].ToString(), FUNCTION = dtt.Rows[i - 2][3 - 1].ToString(), STRATADDRESS = dtt.Rows[i - 2][4 - 1].ToString(), TYPE = dtt.Rows[i - 2][5 - 1].ToString(), LENTH = dtt.Rows[i - 2][6 - 1].ToString(), ARRAY = dtt.Rows[i - 2][7 - 1].ToString() });
                        }
                        int cou = dtt.Rows.Count;
                        GlobalVariable.Modbus_dt = dtt;
                        wbook.Close(false, Type.Missing, Type.Missing); wbook = null;            //quit excel app          
                        app.Quit();

               



                    }
                  




                    return true;
            });

            return true;
     
        }



        public async Task<bool> Tsk()
        {

          await   ExecuteFunction(GlobalVariable.Modbus_dt);



          return true;
        }





        #region  异步加载
        /// <summary>
        /// ListView异步加载
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public async  Task  msg(GlobalVariable.msg ms)
        {
            
            ms.No= GlobalVariable.List_index++;
            await   App.Current.Dispatcher.BeginInvoke(new Action(() => { GlobalVariable.Message.Add(ms); })); ;
        }

        /// <summary>
        /// Dg异步加载
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public  void Excell_Coll(GlobalVariable.Excell_Header ms)
        {

           
             App.Current.Dispatcher.BeginInvoke(new Action(() => { GlobalVariable.Excell_Coll.Add(ms); })); ;
        }
        #endregion
    }
}
