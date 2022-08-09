using System;
using System.Media;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;


//using System.Media;


namespace Elite
{
    static class SoundManager
    {  
        const string VOLUME = "0";      
        private static List<string> tracks = new(4);
        private static List<int> lengths = new(4);

        private static List<int> queuedForStop = new(4);
        // Sound api functions
        [DllImport("winmm.dll")]
        private static extern Int32 mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);


        private static int totalTrackCount = 0;
  
        public static void Play(Sound sound)
        {
            string trackName = sound.trackName + totalTrackCount.ToString();
            totalTrackCount++;

            StringBuilder sb = new StringBuilder();
            mciSendString("open \"" + sound.fileName + "\" alias " + trackName, sb, 0, IntPtr.Zero);
            mciSendString("play " + trackName, sb, 0, IntPtr.Zero);

            sb = new StringBuilder();
            mciSendString("status " + trackName + " length", sb, 255, IntPtr.Zero);




            sb = new StringBuilder();
            mciSendString("status " + trackName + " length", sb, 255, IntPtr.Zero);
            
            tracks.Add(trackName);
            lengths.Add(Convert.ToInt32(sb.ToString()));

        }

        public static void UpdateSounds()
        {
            StringBuilder sb;
            for (int i = 0; i < tracks.Count; i++)
            {

                string trackName = tracks[i];
                sb = new StringBuilder();
                mciSendString("status " + trackName + " position", sb, 255, IntPtr.Zero);
                int pos = 0;
                try
                {
                    pos = Convert.ToInt32(sb.ToString());
                } 
                catch {}
                
                if (pos >= lengths[i])
                {

                    sb = new StringBuilder();
                    mciSendString("stop " + trackName, sb, 0, IntPtr.Zero);
                    mciSendString("close " + trackName, sb, 0, IntPtr.Zero);

                    queuedForStop.Add(i);

                }
            

            }

            for (int i = 0; i < queuedForStop.Count; i++)
            {
                tracks.RemoveAt(queuedForStop[i]);
                lengths.RemoveAt(queuedForStop[i]);
                queuedForStop.RemoveAt(i);
            }
        }
    }
}
