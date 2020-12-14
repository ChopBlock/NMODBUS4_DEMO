using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MES_CAN
{
    public class IniFile
    {
        public string Path;

        public IniFile(string inipath)
        {
            this.Path = inipath;
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder retVal = new StringBuilder(0xff);
            int num = GetPrivateProfileString(Section, Key, "", retVal, 0xff, this.Path);
            return retVal.ToString();
        }

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.Path);
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    }
}
