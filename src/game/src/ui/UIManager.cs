using System;

namespace Elite
{
    public class UIManager : GameObject
    {
        private Player player;

        private ShieldDisplay enemyShieldDisplay;
        private ShieldDisplay playerShieldDisplay;

        public bool isBlinking = false;

        public string score = "0";
        public UIManager(Player _player)
        {
            player = _player;
        }

        public override void Start()
        {
            visible = false;
            if(!Settings.SHOW_UI) return;


            
            TrueVector trueVector = (TrueVector) Engine.Instance(new TrueVector());

            trueVector.player = player;
            trueVector.colour = 2;

            TrueVector behindVector = (TrueVector) Engine.Instance(new TrueVector());

            behindVector.player = player;
            behindVector.colour = 4;
            behindVector.isBehind = true;


            Target target = (Target) Engine.Instance(new Target());




        //    Engine.Instance(new PIP());




            PlayerDisplay plDis = (PlayerDisplay) Engine.Instance(new PlayerDisplay());


            ShieldDisplay bgShieldDisplay = (ShieldDisplay) Engine.Instance(new ShieldDisplay(new Vector3(0.855f,1.84f,2),true));
            bgShieldDisplay.scale.y *= 1.3f;
    
            enemyShieldDisplay = (ShieldDisplay) Engine.Instance(new ShieldDisplay(new Vector3(0.855f,1.84f,2)));
            enemyShieldDisplay.colour = 4; //12
            enemyShieldDisplay.scale.y *= 1.3f;


            




            ShieldDisplay playerShieldBg = (ShieldDisplay) Engine.Instance(new ShieldDisplay(new Vector3(-1.89f,1.7f,2),true,true));

            playerShieldBg.scale = new Vector3(0.15f,0.15f,0.15f);


            playerShieldDisplay = (ShieldDisplay) Engine.Instance(new ShieldDisplay(new Vector3(-1.89f,1.7f,2),false,true));
            playerShieldDisplay.colour = 1;
            playerShieldDisplay.scale = new Vector3(0.15f,0.15f,0.15f);

            playerShieldDisplay.maxHealth = Player.MAX_HEALTH;






            Engine.Instance(new DeathBG());


    
        }

        public void AddRadarEnemy(Enemy enemy, Vector3 size)
        {

            RadarEnemy radarEnemy = (RadarEnemy) Engine.Instance(new RadarEnemy(enemy,size));


        }

        public void DebugInfo()
        {
            UI.WriteLine("player:");
            UI.WriteLine("position: " + Engine.cameraPosition.ToString());
            
            UI.Write("forward: " + Engine.cameraForward.ToString());
            UI.Write(" | ");
            UI.WriteLine(Engine.cameraForward.Length().ToString());

            UI.Write("up: " + Engine.cameraUp.ToString());
            UI.Write(" | ");
            UI.WriteLine(Engine.cameraUp.Length().ToString());


            UI.WriteLine(Utils.FormatVector(Engine.cameraRight,"right"));

            UI.WriteLine("Health: " + player.health.ToString());

            UI.WriteLine("\ngameobjectcount: " + Engine.GameObjectCount().ToString());
            // UI.WriteText("0123456789\nabcdefg\nhijklmnop\nqrstuvw\nxyz",5,5);
        }



        public override void Update(float deltaTime)
        {
            if(!Settings.SHOW_UI) return;
            if(Engine.main.player.isDead)
            {
                return;
            }





            DebugInfo();
            

            // Points
            UI.WriteText(score,173-(6*(score.Length-1)),2);


            // Player speed
            UI.WriteText(((int)(Engine.main.player.momentum.Length()*10)).ToString(),2,170);

            // Crosshair
            if(!isBlinking) UI.AddSprite(Sprites.crosshair,Settings.SCREEN_SIZE_X/2-4,Settings.SCREEN_SIZE_Y/2-4);

            // Radar
            UI.AddSprite(Sprites.radar,89,157);

           

            if(player.target != null)
            {
                enemyShieldDisplay.currentHealth = player.target.health;
                enemyShieldDisplay.maxHealth = player.target.maxHealth;

            }

            playerShieldDisplay.currentHealth = player.health;




            // Mouse
       //     UI.AddSprite(Sprites.radar,
         //   (int)Engine.main.mouseController.spritePos.x, 
         //   (int)Engine.main.mouseController.spritePos.y);

            // Target momentum
            //if(Engine.main.player.target != null)
            //{
            //string str = ((int)Engine.main.player.target.momentum.Length()).ToString();

            //  UI.WriteText(str,173-(6*(str.Length-1)),80);
            //}
    

        }


    }
}
