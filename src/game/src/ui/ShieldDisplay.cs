using System;

namespace Elite
{
    public class ShieldDisplay : GameObject
    {



        private Vector3 basePos;


        public float currentHealth = 1f;
        public float maxHealth = 1f;

        private bool isPlayer;
        private bool isBackground;


        public ShieldDisplay(Vector3 _basePos, bool _isBackground = false, bool _isPlayer = false)
        {
            basePos = _basePos;
            isPlayer = _isPlayer;
            isBackground = _isBackground;

            if(isBackground) 
            {
                character = '.';
                colour = 8;
            }
        }
        public override void Start()
        {
            scale = new Vector3(0.1f,0.1f,0.1f);
           

            movesWithCamera = true;

            filled = true;
        
            position = basePos;
            SetMesh(Models.quad);

        }

        public override void Update(float deltaTime)
        {
            if(Engine.gameManager.player.isDead)
            {
                Engine.QueueDestruction(this);
            }

            if(!isPlayer && Engine.gameManager.player.target == null)
            {
                visible = false;
                return;
            }
            visible = true;

            //if(isBackground) return;

            if(!isPlayer)
            {
                scale.x = (currentHealth/maxHealth);
                position.x = basePos.x + scale.x/2f;
            }
            else
            {
                scale.y = (currentHealth/maxHealth);
                position.y = basePos.y - scale.y/2f;   
            }





        }



    }
}
