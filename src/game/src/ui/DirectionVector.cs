using System;

namespace Elite
{
    public class TrueVector : GameObject
    {

        public GameObject target;

        public Player player;


        public bool isBehind = false;
        //private Line line;
        public override void Start()
        {
            

            character = '*';
            filled = true;
          
            SetMesh(Models.directionMesh);//Utils.GenerateCircle(50);
            offset = new Vector3(-0.1f,0.05f,0);
            up = new Vector3(0,-1,0);
 
          
        }

        public override void Update(float deltaTime)
        {
            if(Engine.main.player.isDead)
            {
                Engine.QueueDestruction(this);
            }

            position = (player.momentum).Normalise()*15;
            if(isBehind) position *= -1f;

            position += Engine.cameraPosition;
            forward = (Engine.cameraPosition - position).Normalise();


        }


    }
}
