using System;

namespace Elite
{
    public class RadarEnemy : GameObject
    {

        public Enemy enemy;

        public override void Start()
        {
            
            
          //  movesWithCamera = true;
            SetMesh(Models.radarEnemyMesh);
            offset = new Vector3(0,0,0);
            position = new Vector3(0,1,5);
            
            float angle = MathF.PI/6;
            getsCulled = false;

            Vector3 axis = Utils.Cross(up,forward);

            scale = new Vector3(0.03f,0.03f,0.03f);

          //  forward = Utils.RotateAroundAxis(forward,axis,angle);
            
        //    up = Utils.RotateAroundAxis(up,axis,angle);

            colour = 4;

          //  getsClipped = false;
            //getsClipped = false

            

        }

        public override void Update(float deltaTime)
        {
          if(!enemy.isAlive)
          {
            Engine.QueueDestruction(this);
          }
            


            forward = enemy.forward;
            up = enemy.up;

            Vector3 right = Utils.Cross(Engine.cameraUp,Engine.cameraForward);

            visible = true;
            if(Engine.cameraPosition.SquaredDistanceTo(enemy.position) > 1050*1050)
            {
                visible = false;
            }

            colour = 4;

            if(enemy == Engine.main.player.target)
            {
                colour = 7;
            }
            



            Vector3 pos = (enemy.position-Engine.cameraPosition)/1500f;
            position = pos;//pos.z + Engine.cameraUp*pos.y + right*pos.x;

            Vector3 add = new Vector3(0,1.5f,2f);
            position += Engine.cameraForward* add.z + Engine.cameraUp*add.y + right*add.x;

            position += Engine.cameraPosition;


          //  position = (enemy.position)/400f + new Vector3(0,2,4);

        }




    }
}
