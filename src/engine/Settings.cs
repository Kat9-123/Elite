using System;

namespace Elite
{   
    public static class Settings
    {





        public static short SCREEN_SIZE_X = (short) SettingHandler.LoadInt("ScreenSizeX"); //180
        public static short SCREEN_SIZE_Y = (short) SettingHandler.LoadInt("ScreenSizeY");
        public static short FONT_SCALE = (short) SettingHandler.LoadInt("FontScale");

        public static bool DO_MOUSE_CONTROLS = SettingHandler.LoadBool("DoMouseControls");


        public const float FOV = 90f;


        public static bool DISPLAY_DEBUG_INFO = SettingHandler.LoadBool("Debug");

        public static string FONT = SettingHandler.LoadString("Font");

        public static int PLANET_QUALITY = SettingHandler.LoadInt("PlanetQuality");

        public static int DUST_COUNT = SettingHandler.LoadInt("DustCount");


        public static bool SHOW_COLOURS_ON_STARTUP = SettingHandler.LoadBool("ShowColoursOnStartup");


        public static int PLANET_SCALE = SettingHandler.LoadInt("PlanetScale");
        public static int MIN_PLANET_COUNT = SettingHandler.LoadInt("MinPlanetCount");
        public static int MAX_PLANET_COUNT = SettingHandler.LoadInt("MaxPlanetCount");

        public static bool SHOW_HITBOXES = SettingHandler.LoadBool("ShowHitboxes");




 
    }

}