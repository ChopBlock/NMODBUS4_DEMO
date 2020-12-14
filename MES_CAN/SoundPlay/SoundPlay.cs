using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Media;
using System.Windows.Forms;

namespace MES_CAN
{
    public class SoundPlay
    {
          SoundPlayer sp = new SoundPlayer();
     
        [DllImport("winmm.DLL")]
        public static extern long sndPlaySound(string strSound, long dwFlat);

        public static int SND_SYNC = 0;
        public static int SND_ASYNC = 1;
        public static int SND_MEMORY = 4;
        public static int SND_LOOP = 8;
        public static int SND_NOSTOP = 10;

        public SoundPlay()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public long sndPlay(string strSound, long dwFlat)
        {
            return sndPlaySound(strSound, dwFlat);
        }
        public long Stop()
        {
            PlaySound(null);
            return 0;
        }
        public void PlaySound(string FileName)
        {
            this.sndPlay(FileName, SND_ASYNC);
        }

        public void sound()
        {
            sp.SoundLocation = Application.StartupPath + @"\ERR.WAV";
            sp.Load();
            sp.Play();
        }
    }
}
