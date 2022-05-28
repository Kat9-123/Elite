using System;

namespace Elite
{
    public class EnemyLaser : GameObject
    {

      //  private static float LIFETIME

        private static GameObject origin;
        public EnemyLaser(GameObject _origin)
        {
            origin = _origin;
        }
        public override void Start()
        {
            
            mesh = Models.laser;

            getsCulled = false;


            colour = 12;


            
        }

        public override void Update(float deltaTime)
        {
            position = origin.position;
            forward = origin.forward;

        }







    }
}
