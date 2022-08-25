namespace Elite
{
    public class ExplosionParticle : GameObject
    {

        private Vector3 momentum;

        private float deceleration = 0f;

        private Timer lifeTimer = new Timer();

        private Timer colourTimer = new Timer();

        private bool isExploding = false;
        public override void Start()
        {
            colour = 4;
            mesh = Models.cube;
            visible = false;
        }

        public override void Update(float deltaTime)
        {
            if(!isExploding) return;

            if(colourTimer.Accumulate())
            {
                colour = 8;
            }
            position += momentum*deltaTime;

            momentum -= momentum/20f;


            if(lifeTimer.Accumulate())
            {
                visible = false;
                isExploding = false;
            }            

        }

        public void DoExplosion(Vector3 _momentum, Vector3 _size, float lifeTime, float _deceleration)
        {
            momentum = _momentum;
            scale = _size;
            deceleration = _deceleration;

            lifeTimer.Reset();
            lifeTimer.SetDuration(lifeTime);
            
            colourTimer.Reset();
            colourTimer.SetDuration(Utils.RandomFloat(0.2f,0.5f));
            colour = 4;
            

            isExploding = true;
            visible = true;

        }

    }
}
