using System;

namespace Elite
{
    public class Blink : GameObject
    {
        private const float WARP_SPEED = 800f;



        private bool loadingBlink = false;

        private bool ready = false;
        public bool isBlinking = false;

        private bool reset = false;

        private Timer flashTimer = new Timer(0.6f);

        private bool currentFlash = false;


        private Timer cooldownTimer = new Timer(5f);


        private bool onCooldown;

        private bool startCoolDownTimer;


        public override void Start()
        {
            movesWithCamera = true;
           // getsClipped = false;
            getsCulled = false;
            mesh = Models.blink;
            scale = new Vector3(1f,1f,2f);
            position = new Vector3(0,0,20.5f);
            colour = 1;
            visible = false;
        }

        public bool InitBlink()
        {

            if(isBlinking) return false;
            if(reset) return false;
            if(onCooldown) return false;
            up = new Vector3(0,1,0);
            Engine.gameManager.uiManager.isBlinking = true;
            ResetBools();
            loadingBlink = true;
            //scale = new Vector3(1f,1f,2f);
            scale = new Vector3(1f,1f,0f);
            position = new Vector3(0,0,0.5f);

            colour = 1;
            
            //position = new Vector3(0,0,20.5f);
            visible = true;
            return true;
        }
        public bool DoBlink()
        {
            if(!ready)
            {
                ResetBools();
                reset = true;
                return false;
            }
            else
            {
                isBlinking = true;
                return true;
            }
            
        }

        public void ResetBools()
        {
            loadingBlink = false;
            ready = false;
            reset = false;
            isBlinking = false;
            currentFlash = false;

        }


        public override void Update(float deltaTime)
        {

            if(startCoolDownTimer)
            {
                if(cooldownTimer.Accumulate())
                {
                    onCooldown = false;
                    startCoolDownTimer = false;
                    cooldownTimer.Reset();
                }
            }



            if(loadingBlink)
            {
                position.z += deltaTime * 12f;
                scale.z += deltaTime * 1.2f;

                if(scale.z >= 2f)
                {
                    ResetBools();
                    ready = true;

                    flashTimer.Reset();
                    colour = 3;

                    

                }

            }

            if(reset)
            {
                position.z -= deltaTime * 20;
                scale.z -= deltaTime * 2f;

                if(scale.z <= -0.2f)
                {
                    ResetBools();
                    Engine.gameManager.uiManager.isBlinking = false;
                }
            }

            if(ready)
            {
                up = Utils.RotateAroundAxis(up, forward, -deltaTime*0.4f);
                if(flashTimer.Accumulate())
                {
                    currentFlash = !currentFlash;
                    flashTimer.Reset();
                }
                if(currentFlash) colour = 1;
                else colour = 3;

            }


            
            if(isBlinking)
            {
                colour = 13;
                ready = false;
                up = Utils.RotateAroundAxis(up, forward, deltaTime*1f);

                Engine.gameManager.player.position += Engine.cameraForward * deltaTime * WARP_SPEED;
                position.z -= deltaTime * 20;
                scale.z -= deltaTime * 2f;
                if(scale.z < -0.5f) 
                {
                    ResetBools();
                    startCoolDownTimer = true;
                    onCooldown = true;
                    Engine.gameManager.uiManager.isBlinking = false;
                }


            }



        }


    }
}
