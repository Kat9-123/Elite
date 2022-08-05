using System;

using System.Media;


namespace Elite
{
    static class AudioManager
    {        
        private static void Play(string audio)
        {


            SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = @"C:\Users\trist\Desktop\ALB\Programming\C#\Elite\warp.wav";
            player.Play();
            Engine.Setup();

            Engine.Run();
        }
    }
}
