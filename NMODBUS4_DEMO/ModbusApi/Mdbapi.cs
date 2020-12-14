using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NMODBUS4_DEMO;
using System.Collections;
using System.IO.Ports;
using Modbus.Device;
using System.Net.Sockets;

namespace NMODBUS4_DEMO
{
  public  class Mdbapi
    {



      public  TcpClient tcpClient;//= new TcpClient(GlobalVariable.StatusModes.IP, GlobalVariable.StatusModes.PORT);
        /// <summary>
        /// 串口对象
        /// </summary>
        public SerialPort port;//= new SerialPort(GlobalVariable.StatusModes.portName, GlobalVariable.StatusModes.baudRate, GlobalVariable.StatusModes.parity, GlobalVariable.StatusModes.dataBits, GlobalVariable.StatusModes.stopBits);


        //  public IModbusMaster master;
        /// <summary>
        /// Modbus 根据选择条件进行相应读取
        /// </summary>
        public IModbusMaster master;
        //{
          
         

        //    switch (GlobalVariable.StatusModes.Modbus_Mode_s)
        //        {
        //            case 0:
        //            port = new SerialPort(GlobalVariable.StatusModes.portName, GlobalVariable.StatusModes.baudRate, GlobalVariable.StatusModes.parity, GlobalVariable.StatusModes.dataBits, GlobalVariable.StatusModes.stopBits);
        //            return  ModbusSerialMaster.CreateRtu(port);
                       
        //            case 1:
        //            tcpClient = new TcpClient(GlobalVariable.StatusModes.IP, GlobalVariable.StatusModes.PORT);
        //            return   ModbusIpMaster.CreateIp(tcpClient);
                      
        //            default:
        //            port = new SerialPort(GlobalVariable.StatusModes.portName, GlobalVariable.StatusModes.baudRate, GlobalVariable.StatusModes.parity, GlobalVariable.StatusModes.dataBits, GlobalVariable.StatusModes.stopBits);
        //            return  ModbusSerialMaster.CreateRtu(port);
                       
                
            
        //            }
         

        //}

       

          


    }




}

    

