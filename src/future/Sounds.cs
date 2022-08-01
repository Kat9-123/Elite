namespace Elite
{

    public static class Sounds
    {
        public static void Load()
        {   
            warpStart = new Sound("warpstart.wav");
            warpwait = new Sound("warpwait.wav");
            warpend = new Sound("warpend.wav");
            shoot = new Sound("laserShoot.wav");
        }
        public static Sound warpStart;
        public static Sound warpwait;
        public static Sound warpend;


        public static Sound shoot;

    }


        
}