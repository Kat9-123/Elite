
namespace Elite
{
    public class Charger : Enemy
    {

        public override void Start()
        {
            score = 10;
            fireRate = 0.3f;
            rotationSpeed = 25f;
            speed = 125f;

            boundingBoxStart = new Vector3(-30f,-30f,-30f);
            boundingBoxEnd = new Vector3(30f,30f,30f);
            
 
            maxHealth = 100f;
       
            scale = new Vector3(2,2,2);        

            mesh = Models.chargerMesh;


            AddLaser(
                laserMesh: Models.enemyLaserMesh, 
                _offset: new Vector3(0,0,0), 
                damage: 10f, 
                accuracy: 1.8f, 
                laserColour: 12, 
                fireTime: 1f,
                laserVisibilityTime: 0.2f
            );

            Setup();
            
        }
        





        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);     
        }


    }
}
