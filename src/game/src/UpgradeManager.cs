using System;

namespace Elite
{
    public class UpgradeManager : GameObject
    {

        private static int points = 0;

       // private static int shieldLevel = 1;

        public void AddPoints(int n)
        {
            points += n;
            Engine.main.uiManager.points = points.ToString();
        }
        public override void Start()
        {
            
            visible = false;
        }

        public override void Update(float deltaTime)
        {

            
            //if(InputManager.IsKeyPressed(InputMap.UPGRADE_SHIELD))
            //{
               // shieldLevel++;
            //}
        }


    }
}
