using System;

namespace Elite
{
    public class Cube : GameObject
    {

        public GameObject target;

        public Player player;
        //private Line line;
        public override void Start()
        {
            colour = 3;


          
            SetMesh(ModelHandler.LoadModel("Cube.obj"));//Utils.GenerateCircle(50);
           // offset = new Vector3(-0.1f,0.05f,0);
         //   up = new Vector3(0,-1,0);
 
          
        }

        public override void Update(float deltaTime)
        {
            visible = false;
            position.x += 0.8f*deltaTime;
            position.y += 0.3f*deltaTime;

        }


    }
}
