using System;


namespace Elite
{   
    public class Player : GameObject
    {

        public Enemy? target;


        private Timer lastHitTimer = new Timer(2f);
        private Timer shieldRegenTimer = new Timer(0.8f);
        private Timer laserTimer = new Timer(0.2f);
        private bool currentLaser = false;
        private PlayerLaser laserLeft;
        private PlayerLaser laserRight;


        public bool isDead = false;
        

        public EnemyGenerator enemyGen;



        public Vector3 momentum = new Vector3(0,0,0);

        public static Vector3 absoluteForward = new Vector3(0,0,1);
        public static Vector3 absoluteUp = new Vector3(0,1,0);

        public static Vector3 absoluteRight = new Vector3(1,0,0);




        public BoundingBox boundingBox;

        public float health;


        private const float ROTATION_SPEED = 1.5f;

        // Player stats
        public const float YAW_SPEED = 1.2f;
        public const float ROLL_SPEED = 1.5f;
        public const float PITCH_SPEED = 1.5f;

        public float zoomMultiplier = 1f;
        
        public const float SPEED = 20f;
        public const float BREAK_SPEED = 30f;

        public const float MAX_HEALTH = 200f;
        public const float REGEN = 10f;

        public const float DAMAGE = 10f;


        public override void Start()
        {
            visible = false;


            boundingBox = new BoundingBox(new Vector3(-20,-20,-20), new Vector3(20,20,20));

        
            health = MAX_HEALTH;


        }

        public void SetupLasers()
        {
            laserLeft = (PlayerLaser) Engine.Instance(new PlayerLaser(false));
            laserRight = (PlayerLaser) Engine.Instance(new PlayerLaser(true));
        }

        public override void Update(float deltaTime)
        {
            // Always apply momentum.
            // When the player dies, their spaceship shouldn't stop instantly.
            position += momentum*deltaTime;

            Engine.cameraPosition = position;
           
            if(health <= 0f)
            {
                UI.WriteText("YOU DIED",66,87);
                return;
            }


            DoMovement(deltaTime);
            Target();

            Shoot(deltaTime); 




            // Start regenerating health every shieldRegenTimer seconds if the player hasn't been hit for lastHitTimer seconds
            if(lastHitTimer.Accumulate())
            {
                if(shieldRegenTimer.Accumulate())
                {

                    health += MathF.Min(MAX_HEALTH-health,REGEN);
                    shieldRegenTimer.Reset();
                }


            }


        }



        private void Shoot(float deltaTime)
        {
            Enemy? hitEnemy = null;

        
            for (int i = 0; i < enemyGen.enemies.Count; i++)
            {
                if(Physics.CheckLineBox(
                    enemyGen.enemies[i].boundingBox.start + enemyGen.enemies[i].position, 
                    enemyGen.enemies[i].boundingBox.end + enemyGen.enemies[i].position, 
                    Engine.cameraPosition, Engine.cameraPosition+(Engine.cameraForward*1_000_000)))
                {

                    hitEnemy = enemyGen.enemies[i];

                }         
            }


            if (InputManager.IsKeyHeld(InputMap.SHOOT))  
            {
                

                if(laserTimer.Accumulate())
                {
                    laserTimer.Reset();


                    laserLeft.visible = !currentLaser;
                    laserRight.visible = currentLaser;

                    if(hitEnemy != null)
                    {
                        hitEnemy.Hit(DAMAGE);
                    }
                
                    currentLaser = !currentLaser;
                    
                    
                }
                
            }
            else
            {
                laserLeft.visible = false;
                laserRight.visible = false;
            }


           // Renderer.WriteLine(InputManager.KeyState(InputMap.SHOOT).ToString());
           // Renderer.WriteLine(Utils.FormatBool(laserIsColliding,"laser_hit")+ "\n");
        }

        public void DoMovement(float deltaTime)
        {




            if (InputManager.IsKeyHeld(InputMap.YAW_LEFT))
            {

                absoluteForward = Utils.RotateAroundAxis(absoluteForward,absoluteUp,-YAW_SPEED*deltaTime*zoomMultiplier);
                absoluteRight = Utils.RotateAroundAxis(absoluteRight,absoluteUp,-YAW_SPEED*deltaTime*zoomMultiplier);
                
            }

            
            if (InputManager.IsKeyHeld(InputMap.YAW_RIGHT))
            { 

                absoluteForward = Utils.RotateAroundAxis(absoluteForward,absoluteUp,YAW_SPEED*deltaTime*zoomMultiplier);
                absoluteRight = Utils.RotateAroundAxis(absoluteRight,absoluteUp,YAW_SPEED*deltaTime*zoomMultiplier);
            }
            if (InputManager.IsKeyHeld(InputMap.PITH_UP))
            {

                absoluteForward = Utils.RotateAroundAxis(absoluteForward,absoluteRight,PITCH_SPEED*deltaTime*zoomMultiplier);
                absoluteUp = Utils.RotateAroundAxis(absoluteUp,absoluteRight,PITCH_SPEED*deltaTime*zoomMultiplier);
            }
            if (InputManager.IsKeyHeld(InputMap.PITCH_DOWN))
            { 
                absoluteForward = Utils.RotateAroundAxis(absoluteForward,absoluteRight,-PITCH_SPEED*deltaTime*zoomMultiplier);
                absoluteUp = Utils.RotateAroundAxis(absoluteUp,absoluteRight,-PITCH_SPEED*deltaTime*zoomMultiplier);
            }


            if (InputManager.IsKeyHeld(InputMap.ROLL_LEFT))
            {
                absoluteUp = Utils.RotateAroundAxis(absoluteUp,absoluteForward,-ROLL_SPEED*deltaTime*zoomMultiplier);
                absoluteRight = Utils.RotateAroundAxis(absoluteRight,absoluteForward,-ROLL_SPEED*deltaTime*zoomMultiplier);
            }
            if (InputManager.IsKeyHeld(InputMap.ROLL_RIGHT))
            {
                absoluteUp = Utils.RotateAroundAxis(absoluteUp,absoluteForward,ROLL_SPEED*deltaTime*zoomMultiplier);
                absoluteRight = Utils.RotateAroundAxis(absoluteRight,absoluteForward,ROLL_SPEED*deltaTime*zoomMultiplier);
            }


            absoluteUp = absoluteUp.Normalise();
            absoluteForward = absoluteForward.Normalise();
            absoluteRight = absoluteRight.Normalise();

            Engine.cameraUp = absoluteUp;//new Vector3(0,1,0);
            Engine.cameraForward = absoluteForward;//forward;
            Engine.cameraRight = absoluteRight;


            forward = Engine.cameraForward;
            up = Engine.cameraUp;



            Vector3 thrustDirection = new Vector3(0,0,0);

            if (InputManager.IsKeyHeld(InputMap.MOVE_FORWARD)) thrustDirection.z = 1f;
            if (InputManager.IsKeyHeld(InputMap.MOVE_BACK)) thrustDirection.z = -1f;
            if (InputManager.IsKeyHeld(InputMap.MOVE_LEFT)) thrustDirection.x = -1f;
            if (InputManager.IsKeyHeld(InputMap.MOVE_RIGHT)) thrustDirection.x = 1f;
            if (InputManager.IsKeyHeld(InputMap.MOVE_UP)) thrustDirection.y = -1f;
            if (InputManager.IsKeyHeld(InputMap.MOVE_DOWN)) thrustDirection.y = 1f;


            if (InputManager.IsKeyHeld(InputMap.STOP))
            {

                momentum += (momentum*-1f).Normalise() * BREAK_SPEED *deltaTime;
                if(momentum.Length() < 0.5f) momentum = new Vector3(0,0,0);
            }
            else
            {
                momentum += Engine.cameraForward * thrustDirection.z * SPEED * deltaTime;
                momentum += Engine.cameraUp *      thrustDirection.y * SPEED * deltaTime;
                momentum += Engine.cameraRight *   thrustDirection.x * SPEED * deltaTime;
            }




            
           
            // Clamp momentum
            if(momentum.LengthSquared() > 100f*100f)
            {
                momentum = momentum.Normalise()*100.001f;
            }



        }





        public void Hit(float damage)
        {
            health -= damage;
            lastHitTimer.Reset();

            if(health <= 0f)
            {
                isDead = true;
            }

        }

        public void Heal()
        {
            health = MAX_HEALTH;
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

                float dot = Engine.cameraForward.Dot((enemyGen.enemies[i].position-Engine.cameraPosition).Normalise());

                if((dot > closestDot) && (dot > 0.97f) && enemyGen.enemies[i].isAlive)
                {
                    closestDot = dot;
                    closestEnemy = enemyGen.enemies[i];
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