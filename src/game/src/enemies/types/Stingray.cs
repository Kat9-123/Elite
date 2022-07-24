using System;

namespace Elite
{
    public class Stingray : Enemy
    {





        public override void Start()
        {
            
            score = 25;
            fireRate = 0.3f;
            rotationSpeed = 20f;
            speed = 100f;

            boundingBoxStart = new Vector3(-30f,-30f,-30f);
            boundingBoxEnd = new Vector3(30f,30f,30f);


            scale = new Vector3(2,2,2);



            

            mesh = Models.stingrayMesh;
        
            AddLaser(
                laserMesh: Models.enemyLaserMesh, 
                _offset: new Vector3(0,0,0), 
                damage: 7f, 
                accuracy: 1.8f, 
                laserColour: 12, 
                fireTime: 0.5f,
                laserVisibilityTime: 0.2f
            );


            

            maxHealth = 200f;
            
 
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
