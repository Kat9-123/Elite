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



            Engine.Instance(new Radar());




            PlayerDisplay plDis = (PlayerDisplay) Engine.Instance(new PlayerDisplay());


            EnemyHealth bgShieldDisplay = (EnemyHealth) Engine.Instance(new EnemyHealth(new Vector3(0.855f,1.84f,2),true));

    
            EnemyHealth shieldDisplay = (EnemyHealth) Engine.Instance(new EnemyHealth(new Vector3(0.855f,1.84f,2)));
            shieldDisplay.colour = 4; //12

            player.enemyShieldDisplay = shieldDisplay;




            EnemyHealth playerShieldBg = (EnemyHealth) Engine.Instance(new EnemyHealth(new Vector3(-1.89f,1.7f,2),true,true));

            playerShieldBg.scale = new Vector3(0.15f,0.15f,0.15f);


            EnemyHealth playerShieldDisplay = (EnemyHealth) Engine.Instance(new EnemyHealth(new Vector3(-1.89f,1.7f,2),false,true));
            playerShieldDisplay.colour = 1;
            playerShieldDisplay.scale = new Vector3(0.15f,0.15f,0.15f);

            player.shieldDisplay = playerShieldDisplay;



    
        }

        public override void Update(float deltaTime)
        {
            UI.WriteText(points,173-(6*(points.Length-1)),2);

        }


    }
}
