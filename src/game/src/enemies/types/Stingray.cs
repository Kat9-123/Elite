namespace Elite
{
    public class Stingray : Enemy
    {
        public override void Start()
        {

            score = 25;
            fireRate = 0.3f;
            rotationSpeed = 20f;
            speed = 100f+2*difficulty;

            boundingBoxStart = new Vector3(-35f,-35f,-35f);
            boundingBoxEnd = new Vector3(35f,35f,35f);


            scale = new Vector3(2,2,2);


            lod = Models.stringrayLOD;
            displayMesh = Models.stingrayMesh;


            maxHealth = 180f+10*difficulty;
            
        
            AddLaser(
                laserMesh: Models.enemyLaserMesh, 
                _offset: new Vector3(0,0,0), 
                damage: 7f+1*difficulty, 
                accuracy: 1.8f, 
                laserColour: 12, 
                fireTime: 0.5f,
                laserVisibilityTime: 0.2f
            );


            
           Setup();

        }
        
        public override void Update(float deltaTime) {base.Update(deltaTime);}

    }
}
