using System;

namespace Elite
{
    public class UIManager : GameObject
    {
        private Player player;

        public string points = "";
        public UIManager(Player _player)
        {
            player = _player;
        }

        public override void Start()
        {
            visible = false;



            
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


            EnemyHealth bgShieldDisplay = (EnemyHealth) Engine.Instance(new EnemyHealth(new Vector3(0.855f,1.84f,2),true));
            bgShieldDisplay.scale.y *= 1.3f;
    
            EnemyHealth shieldDisplay = (EnemyHealth) Engine.Instance(new EnemyHealth(new Vector3(0.855f,1.84f,2)));
            shieldDisplay.colour = 4; //12
            shieldDisplay.scale.y *= 1.3f;

            player.enemyShieldDisplay = shieldDisplay;




            EnemyHealth playerShieldBg = (EnemyHealth) Engine.Instance(new EnemyHealth(new Vector3(-1.89f,1.7f,2),true,true));

            playerShieldBg.scale = new Vector3(0.15f,0.15f,0.15f);


            EnemyHealth playerShieldDisplay = (EnemyHealth) Engine.Instance(new EnemyHealth(new Vector3(-1.89f,1.7f,2),false,true));
            playerShieldDisplay.colour = 1;
            playerShieldDisplay.scale = new Vector3(0.15f,0.15f,0.15f);

            player.shieldDisplay = playerShieldDisplay;



            Engine.Instance(new DeathBG());



    
        }



        public override void Update(float deltaTime)
        {
            if(Engine.main.player.isDead)
            {
                return;
            }
            

            // Points
            UI.WriteText(points,173-(6*(points.Length-1)),2);


            // Player speed
            UI.WriteText(((int)(Engine.main.player.momentum.Length()*10)).ToString(),2,170);

            // Crosshair
            UI.AddSprite(Sprites.crosshair,Settings.SCREEN_SIZE_X/2-4,Settings.SCREEN_SIZE_Y/2-4);

            // Radar
            UI.AddSprite(Sprites.radar,89,157);

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
