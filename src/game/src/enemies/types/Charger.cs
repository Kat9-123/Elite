using System;

namespace Elite
{
    public class Charger : Enemy
    {
        public const short SCORE = 10;


        public override void Start()
        {
            score = 10;
            fireRate = 0.3f;
            rotationSpeed = 25f;
            speed = 125f;

            boundingBoxStart = new Vector3(-30f,-30f,-30f);
            boundingBoxEnd = new Vector3(30f,30f,30f);
            
 
             
            scale = new Vector3(2,2,2);        

            mesh = Models.chargerMesh;



            AddLaser(
                laserMesh: Models.enemyLaserMesh, 
                _offset: new Vector3(0,0,0), 
                damage: 10f, 
                accuracy: 1.8f, 
                laserColour: 12, 
                fireTime: 1f,
                laserVisibilityTime: 0.2f
            );


            

            maxHealth = 100f;

           /// EnemyLaser laser = (EnemyLaser) Engine.Instance(new EnemyLaser(this));

            
            Setup();

            
        }
        


        //public Player player;




        public override void Update(float deltaTime)
        {


            visible = true;


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




            Renderer.WriteLine(Utils.FormatBool((forward.Dot((Engine.cameraPosition-position).Normalise()) > 0.99f) && (position.SquaredDistanceTo(Engine.cameraPosition) < 90000),"can_hit_player"));





            

        }


    }
}
