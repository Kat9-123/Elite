using System;

namespace Elite
{
    public class EnemyHealth : GameObject
    {



        private Vector3 basePos =  new Vector3(0.855f,1.64f,2);


        public float currentHealth;
        public float maxHealth;
        public override void Start()
        {
            scale = new Vector3(0.1f,0.1f,0.1f);
            
          //  filled = true;



            //getsCulled = false;
            //  filled = true;
            //direction = true;

            movesWithCamera = true;

            filled = true;
        
            position = basePos;
            SetMesh(Models.quad);

        }

        public override void Update(float deltaTime)
        {
            if(Engine.main.player.target == null)
            {
                visible = false;
                return;
            }
            visible = true;
            scale.x = (currentHealth/maxHealth);
            position.x = basePos.x + scale.x/2f;




        }



    }
}
