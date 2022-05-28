using System;
using System.Drawing;

namespace Elite
{   
    public static class FontHandler
    {

        public static string[] characters = new string[1+10+26];

        public static void LoadFont()
        {
            string data = FileHandler.Read("Font.txt");
            
            string[] splitData = data.Split("\r\n\r\n");
          
            for (int i = 0; i < splitData.Length; i++)
            {
                string[] splitCharacter = splitData[i].Split("\r\n");
                for (int line = 0; line < splitCharacter.Length; line++)
                {

                    splitCharacter[line] = splitCharacter[line].PadRight(5);
                   // Console.Write(splitCharacter[line]);
                    //Console.WriteLine(splitCharacter[line].Length);
                }
                string res = string.Join("",splitCharacter);
                characters[i] = res;
               // Console.Write(res);
                //Console.WriteLine("|");
                
            }

        }


        
    }

}