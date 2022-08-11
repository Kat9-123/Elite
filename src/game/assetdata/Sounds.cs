namespace Elite
{

    public static class Sounds
    {

        // A highly questionable fugue that I wrote in one hour
        // https://www.youtube.com/watch?v=5w8NRdETNpA
        public static readonly Sound music = new Sound("Music.wav");


        public static readonly Sound shoot = new Sound("Laser.wav");
        public static readonly Sound death = new Sound("Gameover.wav");

        // Warp
        public static readonly Sound warp = new Sound("warp\\Warp.wav");
        public static readonly Sound warpLoaded = new Sound("warp\\WarpDoneCharging.wav");
        public static readonly Sound warpCooldown = new Sound("warp\\WarpCooldown.wav");

        // Enemy
        public static readonly Sound explosion = new Sound("enemy\\Explosion.wav");

        public static readonly Sound hit = new Sound("enemy\\Hit.wav");
        public static readonly Sound target = new Sound("enemy\\Target.wav");

    }


        
}