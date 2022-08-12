// Loosely based on https://pastebin.com/TtrCdeER
using System;
using System.Runtime.InteropServices;
using System.Text;



namespace Elite
{
    static class SoundManager
    {  

        private const int MAX_CONCURRENT_TRACKS = 8;

        private static int[] tracks = new int[MAX_CONCURRENT_TRACKS];
        private static int[] lengths = new int[MAX_CONCURRENT_TRACKS];

        // Magic sound API function
        [DllImport("winmm.dll")]
        private static extern Int32 mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);


        private static int totalTrackCount = 0;


        private static StringBuilder SendString(string str, int bufferSize=0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            mciSendString(str, stringBuilder, bufferSize, IntPtr.Zero);

            return stringBuilder;
        }
  
        public static string Play(Sound sound)
        {
            if(!Settings.SOUND) return "";
            int trackInt = totalTrackCount;

            totalTrackCount++;

            string trackName = trackInt.ToString();



            // Play
            SendString("open \"" + sound.fileName + "\" alias " + trackName);
            SendString("play " + trackName);


            // Get length
            StringBuilder sb = SendString("status " + trackName + " length",255);
            
            for (int i = 0; i < MAX_CONCURRENT_TRACKS; i++)
            {
                if(tracks[i] == -1)
                {
                    tracks[i] = trackInt;
                    lengths[i] = Convert.ToInt32(sb.ToString());
                    return trackName;
                }
            }

            return "";

            

        }

        public static void Setup()
        {
            for (int i = 0; i < MAX_CONCURRENT_TRACKS; i++)
            {
                tracks[i] = -1;
            }
        }

        public static void Stop(string trackName)
        {
            SendString("stop " + trackName);
            SendString("close " + trackName);
            for (int i = 0; i < MAX_CONCURRENT_TRACKS; i++)
            {
                if(tracks[i] == Int32.Parse(trackName))
                {
                    tracks[i] = -1;
                    return;
                }
            }
        }




        public static void SoundUpate()
        {
            UI.WriteLine("Sounds:");

            for (int i = 0; i < MAX_CONCURRENT_TRACKS; i++)
            {
                UI.WriteLine(tracks[i].ToString() + " : " + lengths[i].ToString());
                if(tracks[i] == -1) continue;

                string trackName = tracks[i].ToString();

                // Get current position
                StringBuilder sb = SendString("status " + trackName + " position",255);
                int pos = Int32.Parse(sb.ToString());

  
                
                if (pos >= lengths[i])
                {
                    Stop(trackName);
                }
            
            }
        }
    }
}
