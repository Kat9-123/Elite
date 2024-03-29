// Is this file too big? absolutely
// It should at least be split up into a Weapon class and a Controller class
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
        

        public EnemyManager enemyManager;



        public Vector3 momentum = new Vector3(0,0,0);

        public static Vector3 absoluteForward = new Vector3(0,0,1);
        public static Vector3 absoluteUp = new Vector3(0,1,0);

        public static Vector3 absoluteRight = new Vector3(1,0,0);



        public BoundingBox boundingBox;

        public float health;

        // X = Pitch
        // Y = Yaw
        // Z = Roll
        public Vector3 rotationDirection = new Vector3(0,0,0);
        private bool isShooting = false;
        private static bool prepareWarp;


        // Player stats
        public const float YAW_SPEED = 1.1f;
        public const float ROLL_SPEED = 1.5f;
        public const float PITCH_SPEED = 1.3f;

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
            
            // Warp stuff
            if((InputManager.IsKeyHeld(InputMap.WARP) || InputManager.IsKeyHeld(InputMap.WARP_MOUSE)) && !prepareWarp)
            {
                if(Engine.gameManager.warpController.InitWarp()) prepareWarp = true;

            }

            if(!(InputManager.IsKeyHeld(InputMap.WARP) || InputManager.IsKeyHeld(InputMap.WARP_MOUSE)) && prepareWarp)
            {
                
                if (Engine.gameManager.warpController.DoWarp()) 
                {
                    SoundManager.Play(Sounds.warp);
                    momentum = forward * 50f;
                }
                prepareWarp = false;
            }

            if(Engine.gameManager.warpController.isWarping) return;

            if(prepareWarp) 
            {
                laserLeft.visible = false;
                laserRight.visible = false;
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

            bool isHeld = InputManager.IsKeyHeld(InputMap.SHOOT) || InputManager.IsKeyHeld(InputMap.SHOOT_MOUSE);

            if(isHeld)
            {
                for (int i = 0; i < enemyManager.enemies.Count; i++)
                {
                    if(Physics.CheckLineBox(
                        enemyManager.enemies[i].boundingBox.start + enemyManager.enemies[i].position, 
                        enemyManager.enemies[i].boundingBox.end + enemyManager.enemies[i].position, 
                        Engine.cameraPosition, Engine.cameraPosition+(Engine.cameraForward*1_000_000)))
                    {

                        hitEnemy = enemyManager.enemies[i];
                        break;
                    }         
                }
            }


            if(isShooting && laserTimer.Accumulate())
            {
                laserTimer.Reset();

                if(isHeld)
                {
                    SoundManager.Play(Sounds.shoot);
                    if(hitEnemy != null)
                    {
                        hitEnemy.Hit(DAMAGE);
                    }
                }
                


                laserLeft.visible = !currentLaser;
                laserRight.visible = currentLaser;

            
                currentLaser = !currentLaser;
                isShooting = false;
                
                
            }



            if (isHeld)  
            {

                isShooting = true;
                
            }

            if(!isShooting)
            {

                currentLaser = !currentLaser;
                laserLeft.visible = false;
                laserRight.visible = false;
                laserTimer.Accumulate();
            
            }
        }

        private float GetSmoothRotationDirection(float val, InputMap negative, InputMap positive)
        {

            if (InputManager.IsKeyHeld(negative))
            {
                val -= 3f * Engine.deltaTime;
                if(val <= -1f) val = -1f;
            } 
            else if (InputManager.IsKeyHeld(positive))
            {
                val += 3f * Engine.deltaTime;
                if(val >= 1f) val = 1f;
            } 
            else 
            {
                val -= MathF.Sign(val) * 2f * Engine.deltaTime;

                if((MathF.Abs(val) - 0.05f) <= 0) val = 0f;
            }
            return val;
        }

        public void DoMovement(float deltaTime)
        {
            // Rotation smoothing is only used for the roll when mousecontrols
            // are enabled.
            if(!Settings.DO_MOUSE_CONTROLS)
            {
                rotationDirection = new Vector3(0,0,0);


                if (InputManager.IsKeyHeld(InputMap.YAW_LEFT)) rotationDirection.y = -1f;
                if (InputManager.IsKeyHeld(InputMap.YAW_RIGHT)) rotationDirection.y = 1f;
                if (InputManager.IsKeyHeld(InputMap.PITH_UP)) rotationDirection.x = 1f;
                if (InputManager.IsKeyHeld(InputMap.PITCH_DOWN)) rotationDirection.x = -1f;
                if (InputManager.IsKeyHeld(InputMap.ROLL_LEFT)) rotationDirection.z = -1f;
                if (InputManager.IsKeyHeld(InputMap.ROLL_RIGHT)) rotationDirection.z = 1f;
            }
            else
            {
                rotationDirection.z = GetSmoothRotationDirection(rotationDirection.z,InputMap.ROLL_LEFT,InputMap.ROLL_RIGHT);
            }



            // You do not want to know how much pain rotation brought me
            absoluteForward = Utils.RotateAroundAxis(absoluteForward,absoluteUp,rotationDirection.y * YAW_SPEED*deltaTime*zoomMultiplier);
            absoluteRight = Utils.RotateAroundAxis(absoluteRight,absoluteUp,rotationDirection.y * YAW_SPEED*deltaTime*zoomMultiplier);
            
        
            absoluteForward = Utils.RotateAroundAxis(absoluteForward,absoluteRight,rotationDirection.x * PITCH_SPEED*deltaTime*zoomMultiplier);
            absoluteUp = Utils.RotateAroundAxis(absoluteUp,absoluteRight,rotationDirection.x * PITCH_SPEED*deltaTime*zoomMultiplier);
    

            absoluteUp = Utils.RotateAroundAxis(absoluteUp,absoluteForward,rotationDirection.z * ROLL_SPEED*deltaTime*zoomMultiplier);
            absoluteRight = Utils.RotateAroundAxis(absoluteRight,absoluteForward,rotationDirection.z * ROLL_SPEED*deltaTime*zoomMultiplier);
        



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
            if(isDead) return;
            health -= damage;
            lastHitTimer.Reset();
            SoundManager.Play(Sounds.playerHit);

            if(health <= 0f)
            {
                if(!isDead)
                {
                    Engine.gameManager.Gameover();
                }
                isDead = true;
                
                
                laserLeft.visible = false;
                laserRight.visible = false;
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
            if(!(InputManager.IsKeyPressed(InputMap.TARGET) || InputManager.IsKeyPressed(InputMap.TARGET_MOUSE))) return;


            // Find the enemy closest to the centre and check if it's close enough
            // to be targeted
            float closestDot = -1000f;
            Enemy? closestEnemy = null;
            for (int i = 0; i < enemyManager.enemies.Count; i++)
            {

                float dot = Engine.cameraForward.Dot((enemyManager.enemies[i].position-Engine.cameraPosition).Normalise());

                if((dot > closestDot) && (dot > 0.97f) && enemyManager.enemies[i].isAlive)
                {
                    closestDot = dot;
                    closestEnemy = enemyManager.enemies[i];
                }
            }


            if(closestEnemy != null)
            {
                // Reset target if enemy is already targeted
                if(target == closestEnemy)
                {
                    
                    target = null;
                    return;
                }
                SoundManager.Play(Sounds.target);
                target = closestEnemy;
            } 


            
        }
    }
}