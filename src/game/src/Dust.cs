using System;

namespace Elite
{
    public class Dust : GameObject
    {


        public override void Start()
        {
            scale = new Vector3(0.1f,0.1f,0.1f);
            
          //  filled = true;



            //getsCulled = false;
            //  filled = true;
            //direction = true;


            SetMesh(Models.dustMesh);
            Reset();
        }

        public override void Update(float deltaTime)
        {
            forward = (Engine.cameraPosition - position).Normalise();
            if(((position-Engine.cameraPosition).Normalise()).Dot(Engine.cameraForward) <= 0.45)
            {
                Reset();
            } 
            if(position.SquaredDistanceTo(Engine.cameraPosition) > 30000)
            {
                Reset();
            } 




        }
        private void Reset()
        {
            
            position = new Vector3(Utils.RandomFloat(-100,100), Utils.RandomFloat(-100,100), Utils.RandomFloat(-100,100));

            position += Engine.cameraPosition;
            if(position.SquaredDistanceTo(Engine.cameraPosition) < 50f)
            {
       
                Reset();
            }
            //position *= Engine.cameraForward;
        }




    }
}
