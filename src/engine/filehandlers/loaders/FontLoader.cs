namespace Elite
{   
    public static class FontHandler
    {
        // 26 characters + 10 numbers + 2 Specials
        public static string[] characters = new string[26+10+2];

        public static void LoadFont()
        {
            string data = FileHandler.Read("Font");
             
            // Characters are separated by: newline, `, newline
            string[] splitData = data.Split("\r\n`\r\n");
          
            for (int i = 0; i < splitData.Length; i++)
            {
                // Get the curent character (split into 7 parts, height)
                string[] splitCharacter = splitData[i].Split("\r\n");

                for (int line = 0; line < splitCharacter.Length; line++)
                {
                    // Pad right so that each line is exactly 5 characters long
                    splitCharacter[line] = splitCharacter[line].PadRight(5);

                }

                string res = string.Join("",splitCharacter);
                characters[i] = res;
          
            }

        }        
    }

}