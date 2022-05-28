using System;

namespace Elite
{
    public class Target : GameObject
    {





        public override void Start()
        {
            
          
            SetMesh(ModelHandler.LoadModel("target.obj"));//Utils.GenerateCircle(50);
            offset = new Vector3(-0.1f,0.05f,0);
            //up = new Vector3(0,-1,0);
 
          
        }

        public override void Update(float deltaTime)
        {
            if(Engine.main.player.target == null)
            {
                visible = false;
                return;
            }
            visible = true;
            position = (Engine.main.player.target.position - Engine.cameraPosition).Normalise()*12f;

            position += Engine.cameraPosition;
            forward = (Engine.cameraPosition - position).Normalise();


        }


    }
}
