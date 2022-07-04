using System;


namespace Elite
{
    public class PlayerLaser : GameObject
    {

      //  private static float LIFETIME

        public PlayerLaser(bool type)
        {
            if (!type) mesh = Models.laserLeft;
            else mesh = Models.laserRight;
        }
        public override void Start()
        {
            getsClipped = false;
            getsCulled = false;

            movesWithCamera = true;
            colour = 10;//12;

            
        }







    }
}
