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




            player.AddRadarEnemy(this, new Vector3(1.5f,1.5f,1.5f));

            maxHealth = 300f;
            health = maxHealth;
           // canonTimer = new Timer(0.3f);

//public EnemyProjectile(Enemy _owner,Vector3 _pos, Vector3 _momentum, float _damage, float _accuracy, float lifeTime)

          
            //forward = new Vector3(0,0,1);

           /// EnemyLaser laser = (EnemyLaser) Engine.Instance(new EnemyLaser(this));

            Setup();
        }
        


        //public Player player;




        public override void Update(float deltaTime)
        { 

            return;
            visible = true;
            if(Engine.cameraPosition.SquaredDistanceTo(position) > 1050*1050)
            {
          //      visible = false;
            }

            if(!isAlive) return;


          //  rot += deltaTime;
          //  forward = Utils.RotateAroundAxis(forward,new Vector3(0,0,1),0.12f*deltaTime);
        //    forward = Utils.RotateAroundAxis(forward,new Vector3(0,1,0),0.1f*deltaTime);
            
          //  forward = Utils.RotateAroundAxis(forward,new Vector3(1,0,0),0.2f*deltaTime);
            //return;
            //col++;
            //col %= 16;
            //colour = (short)col;

           // Shoot(deltaTime,4,0.9f);

            
            
            ShootLasers(deltaTime);


           // if(canonTimer.Accumulate(deltaTime))
            //{
              //  Engine.Instance(new EnemyProjectile(this,new Vector3(50,-18,30),momentum,20,10,5));
                //Engine.Instance(new EnemyProjectile(this,new Vector3(-50,-18,30),momentum,20,10,5));
                //canonTimer.Reset();
           // }

            

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

 
            
            Vector3 currentForward = forward;
            Vector3 currentUp = up;
            Vector3 desiredForward = (Engine.cameraPosition - position).Normalise();
            Vector3 axis = Utils.Cross(currentForward,desiredForward);

            
            currentForward = Utils.RotateAroundAxis(currentForward,axis,MathF.Min(rotationSpeed*deltaTime,currentForward.AngleTo(desiredForward)));



            currentForward = currentForward.Normalise();
            forward = currentForward;



            Renderer.WriteLine(Utils.FormatVector(forward,"enemy_forward"));
            Renderer.WriteLine(Utils.FormatVector(up,"enemy_up"));
          //  Renderer.WriteLine(Utils.FormatVector(scale,"scale"));
            
            if (position.DistanceTo(Engine.cameraPosition) < 250f)
            {
                //momentum += Utils.Cross(up,forward) * deltaTime*30f;
            }

            momentum += forward * deltaTime*50f;
            
            

         //   if(momentum.LengthSquared() > 10000f*10000f)
           // {
             //   momentum = momentum.Normalise()*10000.001f;
            //}

            position += momentum*deltaTime;


      //      Line line = new Line(new Vector3(0,0,0),forward*5,5);
    //        Renderer.AddLine(line);

            Renderer.WriteLine(Utils.FormatBool((forward.Dot((Engine.cameraPosition-position).Normalise()) > 0.99f) && (position.SquaredDistanceTo(Engine.cameraPosition) < 90000),"can_hit_player"));





            

        }


    }
}
