using System;

namespace Elite
{
    public class Projectile : GameObject
    {
        private const float SPEED = 100f;


        private Timer lifeTimer = new Timer(5f);

        public Projectile(Vector3 pos, Vector3 direction, Vector3 shipMomentum,Vector3 _up)
        {
            mesh = Models.cube;
            position = pos;
            momentum = shipMomentum + Engine.cameraForward*SPEED;
            colour = 10;
            scale = new Vector3(0.005f,0.005f,2f);

            forward = Engine.cameraForward;
            up = Engine.cameraUp;

        }

        private Vector3 momentum;
        public override void Start()
        {
            
        }

        public override void Update(float deltaTime)
        {
            position += momentum*deltaTime;

            if(lifeTimer.Accumulate())
            {
                Engine.QueueDestruction(this);
            }

            for (int i = 0; i < Engine.gameManager.enemyManager.enemies.Count; i++)
            {
                Enemy enemy = Engine.gameManager.enemyManager.enemies[i];
                if(Physics.CheckLineBox(
                    enemy.boundingBox.start+enemy.position, 
                    enemy.boundingBox.end+enemy.position, 
                    position, position+forward*3f)
                )
                {
                    enemy.Hit(20f);
                }
            }

                
        }
    }


}
