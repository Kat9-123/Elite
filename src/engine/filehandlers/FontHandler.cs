using System;
using System.Drawing;

namespace Elite
{   
    public static class FontHandler
    {
        // 1 special, 10 numbers, 26 characters
        public static string[] characters = new string[1+10+26];

        public static void LoadFont()
        {
            string data = FileHandler.Read("Font.txt");
            
            // Characters are separated by TWO newlines
            string[] splitData = data.Split("\r\n\r\n");
          
            for (int i = 0; i < splitData.Length; i++)
            {
                // 
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