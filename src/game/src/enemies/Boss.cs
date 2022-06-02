using System;

namespace Elite
{
    public class Boss : Enemy
    {


        public Boss(Player _player) : base(_player) {}

        private EnemyLaser bigLaser;

        private EnemyLaser laserRight;

        private EnemyLaser laserLeft;


        private Timer canonTimer;

        public override void Start()
        {

            fireRate = 0.3f;
        
            rotationSpeed = 1f;

            

            boundingBox = new BoundingBox(new Vector3(-80f,-80f,-80f),new Vector3(80f,80f,80f));



            position = new Vector3(Utils.RandomFloat(-100,100),Utils.RandomFloat(-100,100),Utils.RandomFloat(-500,500)) + Engine.cameraPosition;//issue
         //   position = new Vector3(0,0,0);
            
            scale = new Vector3(3,3,3);
          //  up = new Vector3(0,1,0);

            displaySize /= 3;
            

            mesh = ModelHandler.LoadModel("Boss.obj");


            forward = (Engine.cameraPosition - position).Normalise();
            bigLaser = (EnemyLaser) Engine.Instance(new EnemyLaser(this,Models.bigLaserMesh,new Vector3(0,0,150),150f,0.9f,10f,1f));
            bigLaser.colour = 5;

            bigLaser.scale = new Vector3(40,40,1);
    
            laserRight = (EnemyLaser) Engine.Instance(new EnemyLaser(this,Models.enemyLaserMesh,new Vector3(50,-18,30),15f,0.98f,0.5f,0.05f));
            laserRight.colour = 4;

            laserLeft = (EnemyLaser) Engine.Instance(new EnemyLaser(this,Models.enemyLaserMesh,new Vector3(-50,-18,30),15f,0.98f,0.5f,0.05f));
            laserLeft.colour = 4;


           // canonTimer = new Timer(0.3f);

//public EnemyProjectile(Enemy _owner,Vector3 _pos, Vector3 _momentum, float _damage, float _accuracy, float lifeTime)

          
            //forward = new Vector3(0,0,1);

           /// EnemyLaser laser = (EnemyLaser) Engine.Instance(new EnemyLaser(this));

        }
        


        //public Player player;




        public override void Update(float deltaTime)
        { 

 
            
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
            laserLeft.Shoot(deltaTime);
            laserRight.Shoot(deltaTime);
            bigLaser.Shoot(deltaTime);


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
