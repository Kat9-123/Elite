using System;

namespace Elite
{
    public class PlayerDisplay : GameObject
    {


        public Enemy target;
        public override void Start()
        {
            
            
            offset = new Vector3(0,0,0);

            scale = new Vector3(0.02f,0.02f,0.02f);
            getsLit = true;
         //   getsCulled= false;
            movesWithCamera = true;
        //    getsLit = true;
            colour = 15;

        }

        public override void Update(float deltaTime)
        {
           
            if(Engine.main.player.target == null)
            {
                visible = false;
                return;
            }
            
            visible = true;

            target = Engine.main.player.target;
            mesh = target.mesh;
            position =new Vector3(1.3f,1.1f,2f);
            forward = target.forward;
            up = target.up;
            colour = target.colour;
            lightingDirection = Engine.cameraUp * 1f;
            //Vector3 add = 
           // position += Engine.cameraForward* add.z + Engine.cameraUp*add.y + Utils.Cross(Engine.cameraUp,Engine.cameraForward)*add.x;
        }




    }
}
