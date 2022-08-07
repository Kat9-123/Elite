namespace Elite
{
    public class RadarEnemy : GameObject
    {
        private const int RADAR_RANGE = 3000;
        private Enemy enemy;


        public RadarEnemy(Enemy _enemy, Vector3 _scale)
        {
            enemy = _enemy;
            scale = _scale;
        }

        public override void Start()
        {
            


            mesh = Models.radarEnemyMesh;

            position = new Vector3(0,1,5);
            
            colour = 4;
            getsCulled = false;

            Vector3 axis = Utils.Cross(up,forward);

            scale = new Vector3(0.03f,0.03f,0.03f);



        }

        public override void Update(float deltaTime)
        {
            if(Engine.gameManager.player.isDead)
            {
                Engine.QueueDestruction(this);
            }
            if(!enemy.isAlive)
            {
                Engine.QueueDestruction(this);
            }

            colour = 4;
            


            forward = enemy.forward;
            up = enemy.up;

            Vector3 right = Utils.Cross(Engine.cameraUp,Engine.cameraForward);

            visible = Settings.SHOW_UI;
            if(Engine.cameraPosition.SquaredDistanceTo(enemy.position) > RADAR_RANGE*RADAR_RANGE)
            {
                visible = false;
            }

            

            if(enemy == Engine.gameManager.player.target)
            {
                colour = 3;
            }
            



            Vector3 pos = (enemy.position-Engine.cameraPosition)/(RADAR_RANGE*1.4f);
            position = pos;

            Vector3 add = new Vector3(0,1.5f,2f);
            position += Engine.cameraForward* add.z + Engine.cameraUp*add.y + right*add.x;

            position += Engine.cameraPosition;

        }

    }
}
