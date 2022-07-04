using System;
using System.Collections.Generic;


namespace Elite
{   
    // Main game manager. 
    public class Main : GameObject
    {
        

        public Player player;

        public UIManager uiManager;
        public EnemyGenerator enemyManager;

        public ExplosionManager explosionManager;

        public MouseController mouseController;

      //  public const int ENEMY_LAYER = 10;

        public int enemyLayer = 1;


        public void GenerateBodies()
        {
            int l = Utils.RandomInt(10,20);
            for (int i = 0; i < l; i++)
            {
                Vector3 pos = Utils.RandomPositionExcludeCentre(2f,16f);

                float s = Utils.RandomFloat(0.5f,2.7f);

                Engine.Instance(new Planet(pos,(short)Utils.RandomInt(1,14),s));
                enemyLayer++;
            }

            Engine.Instance(new Planet(new Vector3(0,0,20),(short)ConsoleColor.Yellow,1,false));
            enemyLayer++;
        }

        public override void Start()
        {
          //  Models.cubeMesh = ModelHandler.LoadModel("test.obj");
            visible = false;
           // 
            player = (Player) Engine.Instance(new Player());
            enemyLayer++;


            mouseController = (MouseController) Engine.Instance(new MouseController());



            explosionManager = (ExplosionManager) Engine.Instance(new ExplosionManager());
            enemyLayer++;

            EnemyGenerator enemyGen = (EnemyGenerator) Engine.Instance(new EnemyGenerator());
            enemyLayer++;
            //player.t = en;
            player.enemyGen = enemyGen;
            enemyManager = enemyGen;

   
            GenerateBodies();
            

            for (int i = 0; i < 80; i++)
            {

                Engine.Instance(new Dust());
                enemyLayer++;
            }


            //              <--- ENEMIES




         //   InstanceEnemy();
           // Cube cube = (Cube) Engine.Instance(new Cube());

            
            //en.target = cube;

            




            uiManager = (UIManager) Engine.Instance(new UIManager(player));




        }




        public override void Update(float deltaTime)
        {

        }

        public void GameOver()
        {
            
        }
    }
}