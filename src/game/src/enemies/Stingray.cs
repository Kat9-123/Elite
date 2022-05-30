using System;

namespace Elite
{
    public class Stingray : Enemy
    {


        public Stingray(Player _player) : base(_player) {}


        public override void Start()
        {

            fireRate = 0.3f;
            rotationSpeed = 20f;

            

            boundingBox = new BoundingBox(new Vector3(-20f,-20f,-20f),new Vector3(20f,20f,20f));



            position = new Vector3(Utils.RandomFloat(-100,100),Utils.RandomFloat(-100,100),Utils.RandomFloat(-500,500)) + Engine.cameraPosition;//issue
         //   position = new Vector3(0,0,0);
            
            scale = new Vector3(1,1,1);
          //  up = new Vector3(0,1,0);


            

            mesh = ModelHandler.LoadModel("Ship.obj");


            forward = (Engine.cameraPosition - position).Normalise();
 
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

            Shoot(deltaTime,4,0.9f);

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
