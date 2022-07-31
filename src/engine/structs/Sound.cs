using System;
using System.Media;

namespace Elite
{
    public struct Sound
    {
        SoundPlayer soundPlayer;

        public Sound(string path)
        {
            
            soundPlayer = new System.Media.SoundPlayer();
            soundPlayer.SoundLocation = FileHandler.originPath + "sounds\\" + path;
        }

        public void Play()
        {
            soundPlayer.Play();
        }
        public void Stop()
        {
            soundPlayer.Stop();
        }

    }
}
