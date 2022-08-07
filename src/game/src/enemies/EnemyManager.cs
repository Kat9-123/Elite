using System;
using System.Collections.Generic;

namespace Elite
{
    public class EnemyGenerator : GameObject
    {

        public List<Enemy> enemies = new List<Enemy>();

        private Timer enemySpawnTimer = new Timer(10);



        private int enemiesSpawned = 0;



        public override void Start()
        {
            visible = false;
        }

        public override void Update(float deltaTime)
        {


            // Don't spawn more than 4 enemies
            if(enemies.Count > 3) return;


            if(enemySpawnTimer.Accumulate())
            {

                Enemy enemy;

                // After 3 enemies were spawned, the boss
                // can spawn as well.
                int upperBound = 2;
                if(enemiesSpawned > 2)
                {
                    upperBound = 3;
                }

                switch (Utils.RandomInt(0,upperBound))
                {
                    case 0:
                        enemy = InstanceEnemy(new Charger());

                        break;
                    case 1:
                        enemy = InstanceEnemy(new Stingray());

                        break;
                    case 2:
                        enemy = InstanceEnemy(new Boss());
                        
                        break;

                }

                enemiesSpawned++;

                enemySpawnTimer.SetDuration(Utils.RandomFloat(6,25));
                enemySpawnTimer.Reset();

                    
            }



        }


        public void DestroyEnemy(Enemy enemy)
        {
            enemies.Remove(enemy);
        }

        private Enemy InstanceEnemy(Enemy en)
        {
            Engine.Instance(en);
            enemies.Add(en);
            Engine.ChanageIndex(en,Engine.gameManager.enemyLayer);

            return en;
            

        }




    }
}
