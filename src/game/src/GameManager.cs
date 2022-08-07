namespace Elite
{   
    // GameManager game manager. 
    public class GameManager : GameObject
    {


        public Player player;

        public UIManager uiManager;
        public EnemyManager enemyManager;

        public ExplosionManager explosionManager;

        public MouseController mouseController;

        public Warp warpController;



        public int enemyLayer;


        public void GenerateBodies()
        {
            int l = Utils.RandomInt(Settings.MIN_PLANET_COUNT,Settings.MAX_PLANET_COUNT);
            for (int i = 0; i < l; i++)
            {
                Vector3 pos = Utils.RandomPositionExcludeCentre(2f,16f);

                float s = Utils.RandomFloat(0.5f,2.7f);

                Engine.Instance(new Planet(pos,(short)Utils.RandomInt(1,14),s));

            }

            // Two suns are needed because blender messed up my circle mesh
            // so the filling algorithm doesn't entirely work which
            // makes the sun look really bad. A quick and dirty fix
            // is to just spawn two.
            Engine.Instance(new Sun());
            Engine.Instance(new Sun()).up = new Vector3(1,0,0);
        }


        public void Setup()
        {


            player = (Player) Engine.Instance(new Player());

            explosionManager = (ExplosionManager) Engine.Instance(new ExplosionManager());

            EnemyManager enemyGen = (EnemyManager) Engine.Instance(new EnemyManager());

            player.enemyGen = enemyGen;
            enemyManager = enemyGen;

   
            GenerateBodies();
            

            for (int i = 0; i < Settings.DUST_COUNT; i++)
            {
                Engine.Instance(new Dust());
            }



            enemyLayer = Engine.GameObjectCount();  //    <--- ENEMIES


            warpController = (Warp) Engine.Instance(new Warp());

            player.SetupLasers();


            mouseController = (MouseController) Engine.Instance(new MouseController());

            uiManager = (UIManager) Engine.Instance(new UIManager(player));


            Engine.cameraPosition = new Vector3(0,0,0);


            Engine.cameraUp = new Vector3(0,-1,0);
            Engine.cameraForward = new Vector3(0,0,1);
            Engine.cameraRight = new Vector3(1,0,0);


        }

        public override void Start()
        {
            visible = false;
            Engine.Instance(new Titlescreen());

        }




    }
}