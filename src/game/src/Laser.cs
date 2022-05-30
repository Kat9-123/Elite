using System;


namespace Elite
{
    public class Laser : GameObject
    {

      //  private static float LIFETIME

        public Laser(bool type)
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
