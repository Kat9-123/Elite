namespace Elite
{
    public class EnemyDisplay : GameObject
    {


        public Enemy target;
        public override void Start()
        {
            
            scale = new Vector3(0.02f,0.02f,0.02f);
            getsLit = true;

            movesWithCamera = true;

            colour = 15;

        }

        public override void Update(float deltaTime)
        {
            if(Engine.gameManager.player.isDead)
            {
                Engine.QueueDestruction(this);
            }
           
            if(Engine.gameManager.player.target == null)
            {
                visible = false;

                return;
            }


            
            visible = true;

            target = Engine.gameManager.player.target;
            scale = target.displaySize;

            mesh = target.displayMesh;
            position = new Vector3(1.29f,1.2f,2f);
            forward = target.forward;
            up = target.up;
            colour = target.colour;
            lightingDirection = Engine.cameraUp;
        }




    }
}
