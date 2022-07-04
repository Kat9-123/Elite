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

            

            boundingBox = new BoundingBox(new Vector3(-30f,-30f,-30f),new Vector3(30f,30f,30f),this);
        
    
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
           player.AddRadarEnemy(this);

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

            //Shoot(deltaTime,4,0.9f);
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
            
            momentum += forward * deltaTime*120f;

            if(momentum.LengthSquared() > 2000f*2000f)
            {
                momentum = momentum.Normalise()*2000;
            }

            position += momentum*deltaTime;

      //      Line line = new Line(new Vector3(0,0,0),forward*5,5);
    //        Renderer.AddLine(line);

            Renderer.WriteLine(Utils.FormatBool((forward.Dot((Engine.cameraPosition-position).Normalise()) > 0.99f) && (position.SquaredDistanceTo(Engine.cameraPosition) < 90000),"can_hit_player"));


            Setup();


            

        }


    }
}
