namespace Elite
{
    public class UIManager : GameObject
    {
        private Player player;

        private ShieldDisplay enemyShieldDisplay;
        private ShieldDisplay playerShieldDisplay;

        public bool isWarping = false;

        public string score = "0";
        
        public UIManager(Player _player)
        {
            player = _player;
        }

        public override void Start()
        {
            visible = false;
            if(!Settings.SHOW_UI) return;


            
            DirectionVector directionVector = (DirectionVector) Engine.Instance(new DirectionVector());

            directionVector.player = player;
            directionVector.colour = 2;

            DirectionVector behindVector = (DirectionVector) Engine.Instance(new DirectionVector());

            behindVector.player = player;
            behindVector.colour = 4;
            behindVector.isBehind = true;


            Target target = (Target) Engine.Instance(new Target());



            Engine.Instance(new EnemyDisplay());


            ShieldDisplay bgShieldDisplay = (ShieldDisplay) Engine.Instance(new ShieldDisplay(new Vector3(0.855f,1.84f,2),true));
            bgShieldDisplay.scale.y *= 1.3f;
    
            enemyShieldDisplay = (ShieldDisplay) Engine.Instance(new ShieldDisplay(new Vector3(0.855f,1.84f,2)));
            enemyShieldDisplay.colour = 4;
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
            if(Engine.gameManager.player.isDead)
            {
                return;
            }


            DebugInfo();

            
            short scoreColour = 15;
            if(int.Parse(score) >= int.Parse(Engine.gameManager.highscore))
            {
                scoreColour = 5;

            }
            else
            {    
                // Highscore
                UI.WriteText(Engine.gameManager.highscore,173-(6*(Engine.gameManager.highscore.Length-1)),10,'^',5);

            }
            // Score
            UI.WriteText(score,173-(6*(score.Length-1)),2,'#',scoreColour);




            // Player speed
            UI.WriteText(((int)(Engine.gameManager.player.momentum.Length()*10)).ToString(),2,170);

            // Crosshair
            if(!isWarping) UI.AddSprite(Sprites.crosshair,Settings.SCREEN_SIZE_X/2-4,Settings.SCREEN_SIZE_Y/2-4);

            // Radar
            UI.AddSprite(Sprites.radar,89,157);

           

            if(player.target != null)
            {
                enemyShieldDisplay.currentHealth = player.target.health;
                enemyShieldDisplay.maxHealth = player.target.maxHealth;

            }

            playerShieldDisplay.currentHealth = player.health;



            // Target momentum
            /*
            if(Engine.gameManager.player.target != null)
            {
                string str = ((int)Engine.gameManager.player.target.momentum.Length()).ToString();

                UI.WriteText(str,173-(6*(str.Length-1)),80);
            }
            */
    

        }
    }
}
