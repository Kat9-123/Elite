namespace Elite
{
    public class EnemyDisplay : GameObject
    {


        public Enemy target;
        public override void Start()
        {
            
            
            //offset = new Vector3(0,0,0);

            scale = new Vector3(0.02f,0.02f,0.02f);
            getsLit = true;
         //   getsCulled= false;
            movesWithCamera = true;
        //    getsLit = true;
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
            mesh = target.mesh;
            position = new Vector3(1.29f,1.2f,2f);
            forward = target.forward;
            up = target.up;
            colour = target.colour;
            lightingDirection = Engine.cameraUp;
            //Vector3 add = 
           // position += Engine.cameraForward* add.z + Engine.cameraUp*add.y + Utils.Cross(Engine.cameraUp,Engine.cameraForward)*add.x;
        }




    }
}
