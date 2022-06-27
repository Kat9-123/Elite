using System;

namespace Elite
{
    public class DeathBG : GameObject
    {


        private bool isActivated;
        public override void Start()
        {
            character = ' ';
            colour = 0;
            mesh = Models.circle;
            movesWithCamera = true;
            filled = true;

            position.z = 2f;
            forward = new Vector3(0,0,-1f);

            scale = new Vector3(0f,0f,0f);
        }
        public void Activate()
        {
            isActivated = true;
        }

        public override void Update(float deltaTime)
        {
            if(Engine.main.player.isDead)
            {
                Activate();
            }
            if(!isActivated)
            {
                return;
            }
            scale.x += 3f *deltaTime;
            scale.y += 3f *deltaTime;
            scale.z += 3f *deltaTime;

            
        }


    }
}
