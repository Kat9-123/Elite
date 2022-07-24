using System;

namespace Elite
{
    public class PIP : GameObject
    {

        private Enemy? target;



        public override void Start()
        {
          //  mesh = Models.pip;
            colour = 6;
            scale = new Vector3(1,1,1);
        }
        public override void Update(float deltaTime)
        {

            target = Engine.main.player.target;

            if(target == null)
            {
                return;
            }
            
            position = (CalculatePIP(1000,target.momentum,target.position) - Engine.cameraPosition).Normalise()*12f;

            position += Engine.cameraPosition;
            forward = (Engine.cameraPosition - position).Normalise();
        }



        private Vector3 CalculatePIP(float projectileSpeed, Vector3 targetMomentum, Vector3 targetPosition)
        {

            Vector3 pos = targetPosition;
            for (int i = 0; i < 6; i++)
            {
                float dist = pos.DistanceTo(new Vector3(0,0,0));
                float time = dist / projectileSpeed;


                Vector3 newPos = targetMomentum*time + targetPosition;

                pos = newPos;

                
            }


            return pos;

        }


    }
}