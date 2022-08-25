namespace Elite
{
    public class Warp : GameObject
    {
        private const float WARP_SPEED = 800f;


        private bool loadingWarp = false;

        private bool ready = false;
        public bool isWarping = false;

        private bool reset = false;

        private Timer flashTimer = new Timer(0.6f);

        private bool currentFlash = false;


        private Timer cooldownTimer = new Timer(10f);


        private bool onCooldown;

        private bool startCoolDownTimer;


        public override void Start()
        {
            movesWithCamera = true;

            getsCulled = false;
            mesh = Models.warp;
            scale = new Vector3(1f,1f,2f);
            position = new Vector3(0,0,20.5f);
            colour = 1;
            visible = false;
        }

        public bool InitWarp()
        {

            if(isWarping) return false;
            if(reset) return false;
            if(onCooldown) return false;
            up = new Vector3(0,1,0);
            Engine.gameManager.uiManager.isWarping = true;
            ResetBools();
            loadingWarp = true;

            scale = new Vector3(1f,1f,0f);
            position = new Vector3(0,0,0.5f);

            colour = 1;
            

            visible = true;
            return true;
        }
        public bool DoWarp()
        {
            if(!ready)
            {
                ResetBools();
                reset = true;
                return false;
            }
            
        
            isWarping = true;
            return true;
        
            
        }

        public void ResetBools()
        {
            loadingWarp = false;
            ready = false;
            reset = false;
            isWarping = false;
            currentFlash = false;

        }


        public override void Update(float deltaTime)
        {
            if(Engine.gameManager.player.isDead) return;

            if(startCoolDownTimer)
            {
                if(cooldownTimer.Accumulate())
                {
                    onCooldown = false;
                    startCoolDownTimer = false;
                    SoundManager.Play(Sounds.warpCooldown);
                    cooldownTimer.Reset();
                }
            }



            if(loadingWarp)
            {
                position.z += deltaTime * 12f;
                scale.z += deltaTime * 1.2f;

                if(scale.z >= 2f)
                {
                    ResetBools();
                    ready = true;
                    SoundManager.Play(Sounds.warpLoaded);
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
                    Engine.gameManager.uiManager.isWarping = false;
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


            
            if(isWarping)
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
                    Engine.gameManager.uiManager.isWarping = false;
                }


            }


        }

    }
}
