// Little timer struct, because timers
// get used a lot in game dev
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

        // Returns true if the current duration is
        // over the max duration. false otherwise.
        public bool Accumulate(float deltaTime=0f)
        {
            time += Engine.deltaTime;

            if(time < duration) return false;

            return true;
        }

        public void SetDuration(float _duration)
        {
            duration = _duration;
        }

        public void Reset()
        {
            time = 0f;
        }

    }
}