using System;


namespace Elite
{   

    public class Enemy : Ship
    {

        private const float HITTIME = 0.08f;
        private float hitTimer = 0f;
        private bool isHit = false;

        public Vector3 momentum;

        public GameObject target;


        private const float ROTATION_SPEED = 20f;

        public BoundingBox boundingBox;
        private Player player;
        public Enemy(Player _player)
        {
            player = _player;
        }
        
        public override void Start()
        {
            boundingBox = new BoundingBox(new Vector3(-20f,-20f,-20f),new Vector3(20f,20f,20f));
            //visible = false;
            getsLit = true;
           // getsCulled = false;
            //filled = true;
            character = 'O';
            speed = 1f;

            position = new Vector3(Utils.RandomFloat(-100,100),Utils.RandomFloat(-100,100),Utils.RandomFloat(-500,500));//issue
         //   position = new Vector3(0,0,0);
            
            scale = new Vector3(1,1,1);
          //  up = new Vector3(0,1,0);

            
            switch (Utils.RandomInt(0,2))
            {
                case 0:
                    SetMesh(ModelHandler.LoadModel("Ship.obj"));
                    break;
                
                default:
                    SetMesh(ModelHandler.LoadModel("Ship2.obj"));
                    break;
            }
            

            
            
            offset = new Vector3(0,0,0);

            forward = (Engine.cameraPosition - position).Normalise();
           /// EnemyLaser laser = (EnemyLaser) Engine.Instance(new EnemyLaser(this));
            player.AddRadarEnemy(this);
        }

        //public Player player;


        private static float shootTimer = 0f;
        private const float SHOOT_TIME = 0.1f;
        private Line lin = new Line(new Vector3(0,0,0), new Vector3(0,0,1),12);


        private const float FIRE_RATE = 0.3f;
        private static float fireTimer = 0f;
        private bool shooting = false;


        private bool isAlive = true;

        public override void Update(float deltaTime)
        {

            if(!isAlive) return;
          //  rot += deltaTime;
          //  forward = Utils.RotateAroundAxis(forward,new Vector3(0,0,1),0.12f*deltaTime);
        //    forward = Utils.RotateAroundAxis(forward,new Vector3(0,1,0),0.1f*deltaTime);
            
          //  forward = Utils.RotateAroundAxis(forward,new Vector3(1,0,0),0.2f*deltaTime);
            //return;
            //col++;
            //col %= 16;
            //colour = (short)col;

            fireTimer += deltaTime;
            if(fireTimer >= FIRE_RATE)
            {
                fireTimer = 0f;
                Shoot();
            }

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

            
            currentForward = Utils.RotateAroundAxis(currentForward,axis,MathF.Min(ROTATION_SPEED*deltaTime,currentForward.AngleTo(desiredForward)));



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

            lin.start = position;
            lin.end = forward*300f;
      //      Line line = new Line(new Vector3(0,0,0),forward*5,5);
    //        Renderer.AddLine(line);

            Renderer.WriteLine(Utils.FormatBool((forward.Dot((Engine.cameraPosition-position).Normalise()) > 0.99f) && (position.SquaredDistanceTo(Engine.cameraPosition) < 90000),"can_hit_player"));


            if(shooting)
            {
                shootTimer += deltaTime;
                if(shootTimer >= SHOOT_TIME)
                {
                    shooting = false;
                    lin.visible = false;
                    shootTimer = 0f;
                }
            }


            

        }

        private void Shoot()
        {
            shooting = true;
            lin.visible = true;


            if((forward.Dot((Engine.cameraPosition-position).Normalise()) > 0.99f) && (position.SquaredDistanceTo(Engine.cameraPosition) < 90000))
            {
                Engine.main.player.Hit(4f);   
            }


        }
        public void Hit(float damage)
        {
            isHit = true;
            colour = 7;
            currentShield -= damage;

            if(currentShield <= 0)
            {
                isAlive = false;
                lin.visible = false;
                colour = 4;
                //Engine.Destroy(this);
                //visible = false;
            }

        }



    }
}