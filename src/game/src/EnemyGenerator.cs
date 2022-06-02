using System;
using System.Collections.Generic;

namespace Elite
{
    public class EnemyGenerator : GameObject
    {

        public List<Enemy> enemies = new List<Enemy>();


        private int spawnDist = 300;

        private Vector3 lastPlayerPos = new Vector3(0,0,0);




        public override void Start()
        {
            visible = false;

        }

        public override void Update(float deltaTime)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if(Engine.cameraPosition.SquaredDistanceTo(enemies[i].position) > 5000*5000)
                {
                    Engine.QueueDestruction(enemies[i]);
                }
            }

            if(enemies.Count > 3) return;


            if(lastPlayerPos.SquaredDistanceTo(Engine.cameraPosition) > spawnDist*spawnDist)
            {
                lastPlayerPos = Engine.cameraPosition;

                Enemy enemy;
                switch (Utils.RandomInt(0,3))
                {
                    case 0:
                        enemy = InstanceEnemy(new Charger(Engine.main.player));

                        break;
                    case 1:
                        enemy = InstanceEnemy(new Stingray(Engine.main.player));

                        break;
                    case 2:
                        enemy = InstanceEnemy(new Boss(Engine.main.player));
                        
                        break;

                }

                spawnDist = Utils.RandomInt(500,2000);

                    
            }



        }

        private Enemy InstanceEnemy(Enemy en)
        {
            Engine.Instance(en);
            enemies.Add(en);
            Engine.MoveLayer(en,Engine.main.enemyLayer);

            return en;

        }




    }
}
