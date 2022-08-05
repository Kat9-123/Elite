
namespace Elite
{
    public class Target : GameObject
    {


        public override void Start()
        {
            
          
            mesh = Models.targetMesh;
            offset = new Vector3(-0.1f,0.05f,0);
            colour = 11;
            filled = true;
 
          
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
            position = (Engine.gameManager.player.target.position - Engine.cameraPosition).Normalise()*12f;

            position += Engine.cameraPosition;
            forward = (Engine.cameraPosition - position).Normalise();


        }


    }
}
