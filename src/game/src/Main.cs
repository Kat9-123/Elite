using System;
using System.Collections.Generic;


namespace Elite
{   
    // Main game manager. 
    public class Main : GameObject
    {
        
        public Player player;
        public List<Enemy> enemies = new List<Enemy>();

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
            }

            Engine.Instance(new Planet(new Vector3(0,0,20),(short)ConsoleColor.Yellow,1,false));
        }

        public override void Start()
        {
          //  Models.cubeMesh = ModelHandler.LoadModel("test.obj");
            visible = false;
           // 
            player = (Player) Engine.Instance(new Player());

            //player.t = en;
            

   
            GenerateBodies();
            

            for (int i = 0; i < 80; i++)
            {

                Engine.Instance(new Dust());
            }

            InstanceEnemy(new Charger(player));


         //   InstanceEnemy();
           // Cube cube = (Cube) Engine.Instance(new Cube());

            
            //en.target = cube;

            


            player.laserLeft = (Laser) Engine.Instance(new Laser(false));
            player.laserRight = (Laser) Engine.Instance(new Laser(true));
          //  player.enemy = en;

           // en.player = player;


            TrueVector trueVector = (TrueVector) Engine.Instance(new TrueVector());

            trueVector.player = player;
            trueVector.colour = 2;

            TrueVector behindVector = (TrueVector) Engine.Instance(new TrueVector());

            behindVector.player = player;
            behindVector.colour = 4;
            behindVector.isBehind = true;


            Target target = (Target) Engine.Instance(new Target());


//            PIP pip = (PIP) Engine.Instance(new PIP());

  //          pip.target = en;

           // Projectile laserLeft = (Projectile) Engine.Instance(new Projectile());
            Engine.Instance(new Radar());




            PlayerDisplay plDis = (PlayerDisplay) Engine.Instance(new PlayerDisplay());
           // plDis.target = en;

            EnemyHealth bgShieldDisplay = (EnemyHealth) Engine.Instance(new EnemyHealth());
            bgShieldDisplay.colour = 8;

            bgShieldDisplay.currentHealth = 1;
            bgShieldDisplay.maxHealth = 1;
            bgShieldDisplay.character = '.';
    
            EnemyHealth shieldDisplay = (EnemyHealth) Engine.Instance(new EnemyHealth());
            shieldDisplay.colour = 1;

            player.shieldDisplay = shieldDisplay;

            EnemyHealth healthDisplay = (EnemyHealth) Engine.Instance(new EnemyHealth());
            healthDisplay.colour = 2;
            healthDisplay.currentHealth = 1;
            healthDisplay.maxHealth = 1;
            healthDisplay.position.y += 0.142f;
            //player.shieldDisplay = shieldDisplay;






        }


        private Enemy InstanceEnemy(Enemy en)
        {
            Engine.Instance(en);
            enemies.Add(en);

            return en;

        }

        public override void Update(float deltaTime)
        {
            UI.WriteText("10@",161,2);
        }
    }
}