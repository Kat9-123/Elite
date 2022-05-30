using System;


namespace Elite
{   
    public class Player : Ship
    {

        public Enemy? target;


        private Timer lastHitTimer;
        private Timer shieldRegenTimer;
        private const float LASER_LIFETIME = 0.2f;
        private float laserTimer = 0f;
        private bool currentLaser = false;
        public Laser laserLeft;

        public Laser laserRight;

        public EnemyGenerator enemyGen;



        public Vector3 momentum = new Vector3(0,0,0);

        public static Vector3 absoluteForward = new Vector3(0,0,1);
        public static Vector3 absoluteUp =new Vector3(0,1,0);

        public static Vector3 absoluteRight = new Vector3(1,0,0);

        public EnemyHealth shieldDisplay;




        public EnemyHealth enemyShieldDisplay;

        private const float ROTATION_SPEED = 1.5f;

        public override void Start()
        {
            visible = false;
            lastHitTimer = new Timer(2f);
            shieldRegenTimer = new Timer(0.8f);

        }


        private void Shoot(float deltaTime)
        {
            Enemy? hitEnemy = null;

            Vector3 test = new Vector3();
           
            //Console.WriteLine(Engine.main.enemies.Count);
            for (int i = 0; i < enemyGen.enemies.Count; i++)
            {
                if(Physics.CheckLineBox(enemyGen.enemies[i].boundingBox.start + enemyGen.enemies[i].position, enemyGen.enemies[i].boundingBox.end + enemyGen.enemies[i].position, Engine.cameraPosition, Engine.cameraPosition+(Engine.cameraForward*1_000_000_000),ref test))
                {

                    hitEnemy = enemyGen.enemies[i];

                }
             //   else laserIsColliding = false;             
            }


            if (InputManager.IsKeyHeld(InputMap.SHOOT))  
            {
                laserTimer += deltaTime;

                if(laserTimer >= LASER_LIFETIME)
                {

                    /*
                    Projectile laserRight =(Projectile) Engine.Instance(new Projectile());
                     laserRight.momentum = momentum;
                     laserRight.player = this;

                    laserRight.forward = forward;
                    laserRight.up = up;


                    laserLeft.position = position;
                    */
                    laserTimer = 0f;
                    

                    laserLeft.visible = !currentLaser;
                    laserRight.visible = currentLaser;

                    if(hitEnemy != null)
                    {
                        hitEnemy.Hit(5f);
                    }
                
                    laserTimer = 0f;
                    currentLaser = !currentLaser;
                    
                }
                
            }
            else
            {
                laserLeft.visible = false;
                laserRight.visible = false;
                laserTimer = 10f;
            }


           // Renderer.WriteLine(InputManager.KeyState(InputMap.SHOOT).ToString());
           // Renderer.WriteLine(Utils.FormatBool(laserIsColliding,"laser_hit")+ "\n");
        }

        public void AddRadarEnemy(Enemy en)
        {
            RadarEnemy re = (RadarEnemy) Engine.Instance(new RadarEnemy());
            re.enemy = en;

        }
        public override void Update(float deltaTime)
        {
            //Engine.cameraPosition.x += 1f*deltaTime;
            
            Renderer.WriteLine("player:");
            Renderer.WriteLine(Utils.FormatVector(Engine.cameraPosition,"position"));
            
            Renderer.Write(Utils.FormatVector(Engine.cameraForward,"forward"));
            Renderer.Write(" | ");
            Renderer.WriteLine(Engine.cameraForward.Length().ToString());

            Renderer.Write(Utils.FormatVector(Engine.cameraUp,"up"));
            Renderer.Write(" | ");
            Renderer.WriteLine(Engine.cameraUp.Length().ToString());


           // Vector3 absoluteRight = Utils.Cross(up,forward).Normalise();
            Renderer.WriteLine(Utils.FormatVector(absoluteRight,"right"));

            Renderer.WriteLine("Health: " + currentShield.ToString());

           // Renderer.WriteLine(Utils.FormatVector(n,"asdfdsafdfsdf"));

         

            if (InputManager.IsKeyHeld(InputMap.YAW_LEFT))
            {
               // absoluteForward = (((forward*100)-right).Normalise()*deltaTime*0.1f).Normalise();
                absoluteForward = Utils.RotateAroundAxis(absoluteForward,absoluteUp,-ROTATION_SPEED*deltaTime);
                absoluteRight = Utils.RotateAroundAxis(absoluteRight,absoluteUp,-ROTATION_SPEED*deltaTime);
                
            }

            
            if (InputManager.IsKeyHeld(InputMap.YAW_RIGHT))
            { 
                absoluteForward = Utils.RotateAroundAxis(absoluteForward,absoluteUp,ROTATION_SPEED*deltaTime);
                absoluteRight = Utils.RotateAroundAxis(absoluteRight,absoluteUp,ROTATION_SPEED*deltaTime);
            }
            if (InputManager.IsKeyHeld(InputMap.PITH_UP))
            {
                absoluteForward = Utils.RotateAroundAxis(absoluteForward,absoluteRight,ROTATION_SPEED*deltaTime);
                absoluteUp = Utils.RotateAroundAxis(absoluteUp,absoluteRight,ROTATION_SPEED*deltaTime);
            }
            if (InputManager.IsKeyHeld(InputMap.PITCH_DOWN))
            {
                absoluteForward = Utils.RotateAroundAxis(absoluteForward,absoluteRight,-ROTATION_SPEED*deltaTime);
                absoluteUp = Utils.RotateAroundAxis(absoluteUp,absoluteRight,-ROTATION_SPEED*deltaTime);
            }


            if (InputManager.IsKeyHeld(InputMap.ROLL_LEFT))
            {
                absoluteUp = Utils.RotateAroundAxis(absoluteUp,absoluteForward,-ROTATION_SPEED*deltaTime);
                absoluteRight = Utils.RotateAroundAxis(absoluteRight,absoluteForward,-ROTATION_SPEED*deltaTime);
            }
            if (InputManager.IsKeyHeld(InputMap.ROLL_RIGHT))
            {
                absoluteUp = Utils.RotateAroundAxis(absoluteUp,absoluteForward,ROTATION_SPEED*deltaTime);
                absoluteRight = Utils.RotateAroundAxis(absoluteRight,absoluteForward,ROTATION_SPEED*deltaTime);
            }


            absoluteUp = absoluteUp.Normalise();
            absoluteForward = absoluteForward.Normalise();
            absoluteRight = absoluteRight.Normalise();
            Engine.cameraUp = absoluteUp;//new Vector3(0,1,0);
            Engine.cameraForward = absoluteForward;//forward;
//            Engine.cameraForward = (enemy.position - Engine.cameraPosition).Normalise();

            Vector3 thrustDirection = new Vector3(0,0,0);

            if (InputManager.IsKeyHeld(InputMap.MOVE_FORWARD)) thrustDirection.z = 1f;
            if (InputManager.IsKeyHeld(InputMap.MOVE_BACK)) thrustDirection.z = -1f;
            if (InputManager.IsKeyHeld(InputMap.MOVE_LEFT)) thrustDirection.x = -1f;
            if (InputManager.IsKeyHeld(InputMap.MOVE_RIGHT)) thrustDirection.x = 1f;
            if (InputManager.IsKeyHeld(InputMap.MOVE_UP)) thrustDirection.y = -1f;
            if (InputManager.IsKeyHeld(InputMap.MOVE_DOWN)) thrustDirection.y = 1f;

           
            //position.y += 0.5f * deltaTime;
            if (InputManager.IsKeyHeld(InputMap.STOP))
            {
                momentum = new Vector3(0,0,0);
                thrustDirection = (momentum*-1f).Normalise();
          //s      if(momentum.Length() < 0.01f) momentum = new Vector3(0,0,0);
            }



            momentum += Engine.cameraForward * thrustDirection.z * 20f * deltaTime;
            momentum += Engine.cameraUp * thrustDirection.y * 20f * deltaTime;
            momentum += absoluteRight * thrustDirection.x * 20f * deltaTime;
            
           
       //     Renderer.WriteLine(momentum.Length().ToString());
            if(momentum.LengthSquared() > 100f*100f)
            {
                momentum = momentum.Normalise()*100.001f;
            }
            UI.WriteText(((int)(momentum.Length()*10)).ToString(),2,170);

            position += momentum*deltaTime;
         //   ApplyMovement(Engine.cameraForward,deltaTime);

            Engine.cameraPosition = position;

         //   Engine.cameraRotation = rotation;//Utils.RotateTowards(t.position);



            Target();



            Shoot(deltaTime);

   
            if(target != null)
            {
                enemyShieldDisplay.currentHealth = target.currentShield;
                enemyShieldDisplay.maxHealth = 200f;

            }

            shieldDisplay.currentHealth = currentShield;
            shieldDisplay.maxHealth = 200f;


            if(lastHitTimer.Accumulate(deltaTime))
            {
                if(shieldRegenTimer.Accumulate(deltaTime))
                {
                    currentShield += MathF.Min(200f-currentShield,10f);
                    shieldRegenTimer.Reset();
                }


            }


        }
        public void Hit(float damage)
        {
            currentShield -= damage;
            lastHitTimer.Reset();
        }


        public void Target()
        {
            if(target != null && !target.isAlive)
            {
                target = null;
            }
            if(!InputManager.IsKeyPressed(InputMap.TARGET)) return;

    
            float closestDot = -1000f;
            Enemy? closestEnemy = null;
            for (int i = 0; i < enemyGen.enemies.Count; i++)
            {
                // && (position.SquaredDistanceTo(Engine.cameraPosition) < 90000)
                float dot = Engine.cameraForward.Dot((enemyGen.enemies[i].position-Engine.cameraPosition).Normalise());
              //  Renderer.WriteLine(i.ToString() + "   " + dot.ToString());
                if((dot > closestDot) && (dot > 0.97f) && enemyGen.enemies[i].isAlive)
                {
                    closestDot = dot;
                    closestEnemy =enemyGen.enemies[i];
                }
            }
            if(closestEnemy != null)
            {
                if(target == closestEnemy)
                {
                    target = null;
                    return;
                }
                target = closestEnemy;
            } 


            
        }
    }
}