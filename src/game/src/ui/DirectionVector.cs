namespace Elite
{
    public class DirectionVector : GameObject
    {
        public Player player;


        public bool isBehind = false;

        public override void Start()
        {
            character = '*';
            filled = true;
          
            SetMesh(Models.directionMesh);
            offset = new Vector3(-0.1f,0.05f,0);
            up = new Vector3(0,-1,0);
 
          
        }

        public override void Update(float deltaTime)
        {   
            visible = !Engine.gameManager.uiManager.isWarping;
   

            if(Engine.gameManager.player.isDead)
            {
                Engine.QueueDestruction(this);
            }

            position = (player.momentum).Normalise()*15;
            if(isBehind) position *= -1f;

            position += Engine.cameraPosition;
            LookAt(Engine.cameraPosition);


        }


    }
}
