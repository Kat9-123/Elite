using System;
using System.Globalization;

namespace Elite
{   
    public static class SettingHandler
    {

        private static string[] names;
        private static string[] values;


        private static CultureInfo culture = new CultureInfo("en-UK");

        public static void Initialise()
        {
            string text = FileHandler.Read("Settings.txt");

            string[] data = text.Split("\r\n");

            // I should use a dictionary for this
            names = new string[data.Length];
            values = new string[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                // Comments and empty lines
                if(data[i].Length == 0 || data[i][0] == '#') {names[i] = ""; values[i] = ""; continue;} 

                string[] line = data[i].Split('=', 2);
                names[i] = line[0];
                values[i] = line[1];
            }
        }

        public static float LoadFloat(string name)
        {
            return float.Parse(Load(name),culture);
        }
        public static int LoadInt(string name)
        {
            return int.Parse(Load(name),culture);
        }
        public static bool LoadBool(string name)
        {
            return bool.Parse(Load(name));
        }

        public static string LoadString(string name)
        {
            return Load(name);
        }



        private static string Load(string name)
        {
            for (int i = 0; i < names.Length; i++)
            {
                if(names[i] == name)
                {
                    return values[i];
                }
            }
            return "SETTING NOT FOUND";
        }

    }

}