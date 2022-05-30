using System;


namespace Elite
{   

    public class Enemy : Ship
    {

        // STATS
        protected float rotationSpeed; //= 20f;
        public BoundingBox boundingBox;
        protected float fireRate; //= 0.3f;

        protected const float HITTIME = 0.08f;
        protected float hitTimer = 0f;
        protected bool isHit = false;
        public Vector3 momentum;

        
        protected Player player;

        private static float shootTimer = 0f;
        private const float SHOOT_TIME = 0.1f;


        protected EnemyLaser laser;


        public Vector3 displaySize = new Vector3(0.02f,0.02f,0.02f);

        
        private static float fireTimer = 0f;
        private bool shooting = false;


        public bool isAlive = true;



        public Enemy(Player _player)
        {
            player = _player;
            getsLit = true;
            //mesh _mesh = new Mesh(new Triangle[]{new Triangle(new Vector3(0,0,0),new Vector3(0,0.0001f,0), new Vector3(0,0,5))});

            laser = (EnemyLaser) Engine.Instance(new EnemyLaser(Models.enemyLaserMesh));
            laser.colour = 12;
            
            player.AddRadarEnemy(this);  
        }

        protected void Shoot(float deltaTime, float damage, float accuracy, float maxDist=90000)
        {
            laser.forward = forward;
            laser.up = up;
            laser.position = position;
            if(shooting)
            {
                shootTimer += deltaTime;
                if(shootTimer >= SHOOT_TIME)
                {
                    shooting = false;
                    laser.visible = false;
                    shootTimer = 0f;
                }
            }
    
            fireTimer += deltaTime;
            if(fireTimer < fireRate)
            {

                return;
            }
            fireTimer = 0f;
            shooting = true;
            laser.visible = true;


            if((forward.Dot((Engine.cameraPosition-position).Normalise()) > accuracy) && (position.SquaredDistanceTo(Engine.cameraPosition) < maxDist))
            {
                Engine.main.player.Hit(damage);   
            }


        }
        public void Hit(float damage)
        {
            isHit = true;
            colour = 7;
            currentShield -= damage;

            if(currentShield <= 0)
            {
                Engine.QueueDestruction(this);
                isAlive = false;

                colour = 4;
                //Engine.Destroy(this);
                //visible = false;
            }

        }



    }
}