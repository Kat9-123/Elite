using System;

namespace Elite
{
    public class Dust : GameObject
    {


        public override void Start()
        {
            scale = new Vector3(0.1f,0.1f,0.1f);
            character = '^';


            SetMesh(Models.dustMesh);
            Reset();
        }

        public override void Update(float deltaTime)
        {
            LookAt(Engine.cameraPosition);

            // Should be based on FOV
            if(((position-Engine.cameraPosition).Normalise()).Dot(Engine.cameraForward) <= 0.45)
            {
                Reset();
            } 
            if(position.SquaredDistanceTo(Engine.cameraPosition) > 175*175)
            {
                Reset();
            } 




        }
        private void Reset()
        {
            position = Utils.RandomPositionExcludeCentre(50,110);

            position += Engine.cameraPosition;

            Vector3 offset = Utils.RelativeToRotation(new Vector3(0,0,100), Engine.cameraForward, Engine.cameraUp);

            position += offset;

        }




    }
}
