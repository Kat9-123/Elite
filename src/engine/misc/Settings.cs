namespace Elite
{   
    public static class Settings
    {

        // Window parameters 
        public static short SCREEN_SIZE_X = (short) SettingHandler.LoadInt("ScreenSizeX");
        public static short SCREEN_SIZE_Y = (short) SettingHandler.LoadInt("ScreenSizeY");
        public static short FONT_SCALE = (short) SettingHandler.LoadInt("FontScale");
        public static string FONT = SettingHandler.LoadString("Font");


        public static bool DO_MOUSE_CONTROLS = SettingHandler.LoadBool("DoMouseControls");


        public const float FOV = 90f;


        // Planets
        public static int PLANET_QUALITY = SettingHandler.LoadInt("PlanetQuality");
        public static int PLANET_SCALE = SettingHandler.LoadInt("PlanetScale");
        public static string PLANET_LUMINANCES = SettingHandler.LoadString("PlanetLuminances");
        public static readonly int MIN_PLANET_COUNT = SettingHandler.LoadInt("MinPlanetCount");
        public static int MAX_PLANET_COUNT = SettingHandler.LoadInt("MaxPlanetCount");

        public static readonly bool FILL_PLANETS = SettingHandler.LoadBool("FillPlanets");


        public static int DUST_COUNT = SettingHandler.LoadInt("DustCount");


        public static readonly int INITIAL_DIFFICULTY = SettingHandler.LoadInt("InitialDifficulty");
        public static readonly bool DO_DIFFICULTY_SCALING = SettingHandler.LoadBool("DoDifficultyScaling");


        // Sounds

        public static readonly bool SOUND = SettingHandler.LoadBool("Sound");
        public static readonly bool MUSIC = SettingHandler.LoadBool("Music");
  
        // Debug
        public static bool SHOW_UI = SettingHandler.LoadBool("ShowUI");
        public static bool SHOW_HITBOXES = SettingHandler.LoadBool("ShowHitboxes");
        public static bool DISPLAY_DEBUG_INFO = SettingHandler.LoadBool("Debug");
        public static readonly bool SHOW_FPS = SettingHandler.LoadBool("ShowFPS");
        public static bool SHOW_COLOURS_ON_STARTUP = SettingHandler.LoadBool("ShowColoursOnStartup");

        public static bool DO_ENEMY_AI = SettingHandler.LoadBool("DoEnemyAI");




 
    }

}