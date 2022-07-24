
using System;

namespace Elite
{
    public class Target : GameObject
    {





        public override void Start()
        {
            
          
            mesh = Models.targetMesh;//Utils.GenerateCircle(50);
            offset = new Vector3(-0.1f,0.05f,0);
            scale = new Vector3(45,45,45);
            //up = new Vector3(0,-1,0);

            colour = 11;
            filled = true;
 
          
        }

        public override void Update(float deltaTime)
        {
            if(Engine.main.player.isDead)
            {
                Engine.QueueDestruction(this);
            }
            if(Engine.main.player.target == null)
            {
                visible = false;
                return;
            }
            Enemy target = Engine.main.player.target;
            visible = true;
            position = target.position;//(target.boundingBox.start + target.position - Engine.cameraPosition).Normalise() * 12f + Engine.cameraPosition;
            //position = (Engine.main.player.target.position - Engine.cameraPosition).Normalise()*12f;

            forward = (Engine.cameraPosition - position).Normalise();


        }


    }
}

/*
using System;

namespace Elite
{
    public class Target : GameObject
    {





        public override void Start()
        {
            
          
            mesh = Models.targetMesh;//Utils.GenerateCircle(50);
            offset = new Vector3(-0.1f,0.05f,0);
            //up = new Vector3(0,-1,0);
            colour = 11;
            filled = true;
 
          
        }

        public override void Update(float deltaTime)
        {
            if(Engine.main.player.isDead)
            {
                Engine.QueueDestruction(this);
            }
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
*/