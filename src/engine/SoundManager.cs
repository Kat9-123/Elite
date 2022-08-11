// If you have stumbled upon this code, god help you...
// I am too burnt out to make this code not absolutely atrocious.
// Loosely based on https://pastebin.com/TtrCdeER
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;



namespace Elite
{
    static class SoundManager
    {  
        private static List<string> tracks = new(4);
        private static List<int> lengths = new(4);

        private static List<int> queuedForStop = new(4);
        // Sound api functions
        [DllImport("winmm.dll")]
        private static extern Int32 mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);


        private static int totalTrackCount = 0;
  
        public static string Play(Sound sound)
        {
            string trackName = sound.trackName + totalTrackCount.ToString();

            if(trackName.StartsWith("Music") && !Settings.MUSIC) return "";
            if(!trackName.StartsWith("Music") && !Settings.SOUND_EFFECTS) return "";


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
            return trackName;

        }

        public static void Stop(string trackName)
        {
            StringBuilder sb = new StringBuilder();
            mciSendString("stop " + trackName, sb, 0, IntPtr.Zero);
            mciSendString("close " + trackName, sb, 0, IntPtr.Zero);      
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
                catch 
                {
                    pos = 100000;
                }
                
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
                if(tracks.Count-1 < queuedForStop[i])
                {
                    queuedForStop.RemoveAt(i);
                    continue;
                }
                tracks.RemoveAt(queuedForStop[i]);
                lengths.RemoveAt(queuedForStop[i]);
                queuedForStop.RemoveAt(i);
            }
        }
    }
}
