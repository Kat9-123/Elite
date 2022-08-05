using System;
using System.Collections.Generic;


namespace Elite
{   
    // GameManager game manager. 
    public class GameManager : GameObject
    {


        public Player player;

        public UIManager uiManager;
        public EnemyGenerator enemyManager;

        public ExplosionManager explosionManager;

        public MouseController mouseController;

        public Blink blinkController;



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

            Engine.Instance(new Planet(new Vector3(0,0,20),(short)ConsoleColor.Yellow,1,false));

        }


        public void Setup()
        {


            player = (Player) Engine.Instance(new Player());

            explosionManager = (ExplosionManager) Engine.Instance(new ExplosionManager());

            EnemyGenerator enemyGen = (EnemyGenerator) Engine.Instance(new EnemyGenerator());

            player.enemyGen = enemyGen;
            enemyManager = enemyGen;

   
            GenerateBodies();
            

            for (int i = 0; i < Settings.DUST_COUNT; i++)
            {
                Engine.Instance(new Dust());
            }



            enemyLayer = Engine.GameObjectCount();  //    <--- ENEMIES

            blinkController = (Blink) Engine.Instance(new Blink());

            player.SetupLasers();


            mouseController = (MouseController) Engine.Instance(new MouseController());

            uiManager = (UIManager) Engine.Instance(new UIManager(player));


            Engine.cameraPosition = new Vector3(0,0,0);


            Engine.cameraUp = new Vector3(0,1,0);
            Engine.cameraForward = new Vector3(0,0,1);
            Engine.cameraRight = new Vector3(1,0,0);


        }

        public override void Start()
        {
            //  Models.cubeMesh = ModelHandler.LoadModel("test.obj");
            visible = false;

            Setup();


        }









    }
}