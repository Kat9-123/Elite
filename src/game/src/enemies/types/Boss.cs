using System;

namespace Elite
{
    public class Boss : Enemy
    {





        public override void Start()
        {
            score = 40;
            fireRate = 0.3f;
        
            rotationSpeed = 1.5f;
            speed = 50f;


            boundingBoxStart = new Vector3(-120f,-120f,-120f);
            boundingBoxEnd = new Vector3(120f,120f,120f);
        


           

            scale = new Vector3(6,6,6);


            displaySize /= 2;
            

            mesh = Models.bossMesh;


           
            EnemyLaser bigLaser = AddLaser(
                laserMesh: Models.bigLaserMesh, 
                _offset: new Vector3(0,0,150), 
                damage: 150f, 
                accuracy: 4f, 
                laserColour: 5, 
                fireTime: 10f,
                laserVisibilityTime: 1f

            );
            bigLaser.scale = new Vector3(40,40,1);


            AddLaser(
                laserMesh: Models.enemyLaserMesh, 
                _offset: new Vector3(50,-18,30), 
                damage: 15f, 
                accuracy: 1.5f, 
                laserColour: 4, 
                fireTime: 0.5f,
                laserVisibilityTime: 0.05f
            );



            AddLaser(
                laserMesh: Models.enemyLaserMesh, 
                _offset: new Vector3(-50,-18,30), 
                damage: 15f, 
                accuracy: 1.5f, 
                laserColour: 4, 
                fireTime: 0.5f,
                laserVisibilityTime: 0.05f
            );






            maxHealth = 400f;


            radarSize = new Vector3(1.5f,1.5f,1.5f);
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






            

        }


    }
}
