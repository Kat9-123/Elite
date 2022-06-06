using System;
using System.Collections.Generic;


namespace Elite
{   
    // Main game manager. 
    public class Main : GameObject
    {
        

        public Player player;

        public UpgradeManager upgradeManager;
        public UIManager uiManager;

        public int enemyLayer = 1;


        public void GenerateBodies()
        {
            int l = Utils.RandomInt(10,20);
            for (int i = 0; i < l; i++)
            {
                float x = Utils.RandomFloat(2,16);
                int sign = Utils.RandomInt(0,2);
                if(sign == 0) sign--;
                x*=sign;

                float y = Utils.RandomFloat(2,16);
                sign = Utils.RandomInt(0,2);
                if(sign == 0) sign--;
                y*=sign;

                float z = Utils.RandomFloat(2,16);
                sign = Utils.RandomInt(0,2);
                if(sign == 0) sign--;
                z*=sign;

                float s = Utils.RandomFloat(0.5f,2.4f);

                Engine.Instance(new Planet(new Vector3(x,y,z),(short)Utils.RandomInt(0,10),s));
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


            upgradeManager = (UpgradeManager) Engine.Instance(new UpgradeManager());

            EnemyGenerator enemyGen = (EnemyGenerator) Engine.Instance(new EnemyGenerator());
            enemyLayer++;
            //player.t = en;
            player.enemyGen = enemyGen;

   
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

            


            player.laserLeft = (Laser) Engine.Instance(new Laser(false));
            player.laserRight = (Laser) Engine.Instance(new Laser(true));

            uiManager = (UIManager) Engine.Instance(new UIManager(player));




        }




        public override void Update(float deltaTime)
        {

        }
    }
}