using System;

namespace Elite
{
    public class ExplosionParticle : GameObject
    {

        private Vector3 momentum;

        private float deceleration = 0f;

        private Timer lifeTimer = new Timer(0);

        private bool isExploding = false;
        public override void Start()
        {
            colour = 8;
            mesh = Models.cube;
            visible = false;
        }

        public override void Update(float deltaTime)
        {
            if(!isExploding) return;

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

            lifeTimer.SetDuration(lifeTime);

            isExploding = true;
            visible = true;

        }


    }
}