using System;

namespace Elite
{
    public class Sun : GameObject
    {
        private Vector3 pos;
        public override void Start()
        {
            getsLit = false;
            filled = true;
            mesh = Models.circle;
            forward = new Vector3(0,0,1);
            colour = (short)ConsoleColor.Yellow;

            pos = new Vector3(0,0,20f);
        }

        public override void Update(float deltaTime)
        {
            position = pos + Engine.cameraPosition;
        }
    }
}
