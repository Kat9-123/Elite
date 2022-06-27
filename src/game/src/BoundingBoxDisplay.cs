using System;

namespace Elite
{
    public class BoundingBoxDisplay : GameObject
    {

        private Enemy owner;
        public BoundingBoxDisplay(Enemy _owner, Vector3 rectStart, Vector3 rectEnd)
        {
            mesh = Models.cube;
            owner = _owner;
            character = '.';

            scale.x = (MathF.Abs(rectStart.x) + MathF.Abs(rectEnd.x))/2;
            scale.y = (MathF.Abs(rectStart.y) + MathF.Abs(rectEnd.y))/2;
            scale.z = (MathF.Abs(rectStart.z) + MathF.Abs(rectEnd.z))/2;
            colour = 10;
        }

        public override void Update(float deltaTime)
        {
            position = owner.position;
            if(!owner.isAlive)
            {
                Engine.QueueDestruction(this);
            }
        }


    }
}
