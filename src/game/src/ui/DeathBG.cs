
namespace Elite
{
    public class DeathBG : GameObject
    {

        public override void Start()
        {

            character = ' ';
            visible = false;

            mesh = Models.circle;
            movesWithCamera = true;
            filled = true;

            position.z = 2f;
            forward = new Vector3(0,0,-1f);

            scale = new Vector3(0f,0f,0f);
        }


        public override void Update(float deltaTime)
        {
            if(!Engine.gameManager.player.isDead) return;

            visible = true;

            scale += 3f * deltaTime;

            
        }


    }
}
