using System;
using System.Collections.Generic;


namespace Elite
{


    public class Enemy : GameObject
    {
        private List<EnemyLaser> lasers = new List<EnemyLaser>(4);

        // STATS
        protected float rotationSpeed; //= 20f;
        public BoundingBox boundingBox;
        protected float fireRate; //= 0.3f;

        protected const float HITTIME = 0.08f;
        protected float hitTimer = 0f;
        protected bool isHit = false;
        public Vector3 momentum;

        
        private Player player;

        protected Vector3 boundingBoxStart;
        protected Vector3 boundingBoxEnd;




        public Vector3 displaySize = new Vector3(0.02f,0.02f,0.02f);

        



        public bool isAlive = true;


        public float health;

        public float maxHealth;


        public short score;




        public void Setup()
        {
            player = Engine.main.player;
            getsLit = true;
            health = maxHealth;
            
        }

        protected void ShootLasers(float deltaTime)
        {

            for (int i = 0; i < lasers.Count; i++)
            {
                lasers[i].Shoot(deltaTime);
            }
            


        }
        //Enemy _owner, Mesh _mesh,Vector3 _pos, float _damage, float _accuracy,float fireTime, float laserVisibilityTim
        protected EnemyLaser AddLaser(Mesh laserMesh, Vector3 _offset, float damage, float accuracy, short laserColour, float fireTime, float laserVisibilityTime)
        {
      
            EnemyLaser laser = (EnemyLaser) Engine.Instance(new EnemyLaser(this,laserMesh,_offset,damage,accuracy,fireTime,laserVisibilityTime));
            laser.colour = laserColour;

            lasers.Add(laser);

            return laser;

        }
        public void Hit(float damage)
        {
            isHit = true;
            colour = 7;
            health -= damage;

            if(health <= 0 && isAlive)
            {
                Engine.QueueDestruction(this);
                isAlive = false;

                colour = 4;
                Engine.main.upgradeManager.AddPoints(score);
                Engine.main.player.Heal();
                Engine.main.explosionManager.DoExplosion(this);
                Engine.main.enemyManager.DestroyEnemy(this);

                for (int i = 0; i < lasers.Count; i++)
                {
                    Engine.QueueDestruction(lasers[i]);
                }
                //Engine.Destroy(this);
                //visible = false;
            }

        }



    }
}