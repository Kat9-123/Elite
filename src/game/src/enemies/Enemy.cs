// Base enemy class
using System;
using System.Collections.Generic;


namespace Elite
{


    public class Enemy : GameObject
    {
        private List<EnemyLaser> lasers = new List<EnemyLaser>(4);



        // Collision
        protected Vector3 boundingBoxStart;
        protected Vector3 boundingBoxEnd;

        // Not to be set by the object that inherits from this class.
        // only set by this class
        public BoundingBox boundingBox;

        // STATS
        protected float rotationSpeed;
        protected float fireRate;
        public float maxHealth;
        public short score;
        protected float speed;
        protected float maxSpeed;

        public Vector3 displaySize = new Vector3(0.02f,0.02f,0.02f);
        protected Vector3 radarSize = new Vector3(1f,1f,1f);



        protected const float HITTIME = 0.08f;
        protected float hitTimer = 0f;



        protected bool isHit = false;
        public Vector3 momentum;

        
        protected Player player;


        public bool isAlive = true;

        public float health;

        
        

        protected void Setup()
        {
            visible = true;
            player = Engine.gameManager.player;
            position = Utils.RandomPositionExcludeCentre(200f,550f) + player.position;
            getsLit = true;
            health = maxHealth;
            forward = (Engine.cameraPosition - position).Normalise();

            Engine.gameManager.uiManager.AddRadarEnemy(this,radarSize);

            boundingBox = new BoundingBox(boundingBoxStart,boundingBoxEnd);

            if(Settings.SHOW_HITBOXES)
            {
                Engine.Instance(new BoundingBoxDisplay(this,boundingBoxStart,boundingBoxEnd));
            }


            
        }
        protected void DoMovement(float deltaTime)
        {
            if(!Settings.DO_ENEMY_AI) return;
            momentum += forward * deltaTime*speed;

            if(momentum.LengthSquared() > 2000f*2000f)
            {
                momentum = momentum.Normalise()*2000;
            }
            position += momentum*deltaTime;
        }
        protected void DoRotation(float deltaTime)
        {
            if(!Settings.DO_ENEMY_AI) return;

            Vector3 currentForward = forward;
            Vector3 currentUp = up;
            Vector3 desiredForward = (Engine.cameraPosition - position).Normalise();
            Vector3 axis = Utils.Cross(currentForward,desiredForward);

            
            currentForward = Utils.RotateAroundAxis(currentForward,axis,MathF.Min(rotationSpeed*deltaTime,currentForward.AngleTo(desiredForward)));



            currentForward = currentForward.Normalise();
            forward = currentForward;



            UI.WriteLine(Utils.FormatVector(forward,"enemy_forward"));
            UI.WriteLine(Utils.FormatVector(up,"enemy_up"));
        }

        protected void ShootLasers(float deltaTime)
        {
            if(!Settings.DO_ENEMY_AI) return;
            for (int i = 0; i < lasers.Count; i++)
            {
                lasers[i].Shoot(deltaTime);
            }
            

        }
        
        protected EnemyLaser AddLaser(
            Mesh laserMesh, Vector3 _offset, 
            float damage, float accuracy, short laserColour, 
            float fireTime, float laserVisibilityTime)
        {
      
            EnemyLaser laser = (EnemyLaser) Engine.Instance(new EnemyLaser(this,laserMesh,_offset,damage,accuracy,fireTime,laserVisibilityTime));
            laser.colour = laserColour;

            lasers.Add(laser);

            return laser;

        }
        public void Hit(float damage)
        {
            isHit = true;
            colour = 4;
            health -= damage;

            if(health <= 0 && isAlive)
            {
                Engine.QueueDestruction(this);
                isAlive = false;

                Engine.gameManager.uiManager.score = (int.Parse(Engine.gameManager.uiManager.score) + score).ToString();
                player.Heal();
                Engine.gameManager.explosionManager.DoExplosion(this);
                Engine.gameManager.enemyManager.DestroyEnemy(this);

                for (int i = 0; i < lasers.Count; i++)
                {
                    Engine.QueueDestruction(lasers[i]);
                }
            }

        }

        public override void Update(float deltaTime)
        {
            
            if(!isAlive) return;
            ShootLasers(deltaTime);        

            if(isHit)
            {
                hitTimer += deltaTime;
                if(hitTimer >= HITTIME)
                {
                    colour = 15;
                    isHit = false;
                    hitTimer = 0f;
                }
            }

 
            
            DoRotation(deltaTime);

            DoMovement(deltaTime);


            
        }



    }
}