using System;

namespace Elite
{
    public struct Timer
    {

        private float duration;
        private float time;

        public Timer(float _duration)
        {
            duration = _duration;
            time = 0f;
        }

        public bool Accumulate(float deltaTime)
        {
            time += deltaTime;

            if(time < duration) return false;

            return true;
        }

        public void Reset()
        {
            time = 0f;
        }


    }
}