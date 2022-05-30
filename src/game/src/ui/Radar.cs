using System;

namespace Elite
{
    public class Radar : GameObject
    {

        public static GameObject enemy;

        public override void Start()
        {
            visible = false;
            SetMesh(ModelHandler.LoadModel("Cube.obj"));
            scale = new Vector3(0.01f,0.01f,0.01f);
            
            colour = 2;

        }

        public override void Update(float deltaTime)
        {
            position = Engine.cameraPosition;
            Vector3 add = new Vector3(0,1.5f,2f);
            position += Engine.cameraForward* add.z + Engine.cameraUp*add.y + Utils.Cross(Engine.cameraUp,Engine.cameraForward)*add.x;
        }




    }
}
