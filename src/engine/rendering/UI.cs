// UI handler that supports Sprites, Text and small debug text

using System;

namespace Elite
{
    public static class UI
    {

        private static Window.CharInfo[] UIBuffer = new Window.CharInfo[Settings.SCREEN_SIZE_X*Settings.SCREEN_SIZE_Y];
        private static string debugText = "";

        public static void AddSprite(Sprite sprite, int posX, int posY,char character = '#')
        {
            for (int y = 0; y < sprite.height; y++)
            {
                for (int x = 0; x < sprite.width; x++)
                {
                    if(sprite.pixels[y*sprite.width+x].Attributes == 0) continue;
                    if((x+posX) < 0 || (x+posX) >= Settings.SCREEN_SIZE_X || (y+posY) < 0 || (y+posY) >= Settings.SCREEN_SIZE_Y) continue;
                    UIBuffer[(y+posY)*Settings.SCREEN_SIZE_X+x+posX]= sprite.pixels[y*sprite.width+x];
                }
            }
  
        }

        public static void WriteText(string text, int posX, int posY, char character='#', short colour=15)
        {
            int startX = posX;
            for (int i = 0; i < text.Length; i++)
            {
                if(text[i] == '\n')
                {
                    posY += 9;
                    posX = startX;
                    continue;
                }

                // Ascii to index in font array.
                // I have no idea how this works...
                int index = char.ToUpper(text[i]) - '@' - 1;

                if(index < 0)
                {
                    index = char.ToUpper(text[i]) - '/' + 25;

                }

                if(text[i] != ' ') WriteCharacter(FontHandler.characters[index],posX,posY,character,colour);

                posX += 6;
                
            }


        }
        
        private static void WriteCharacter(string fontChar, int posX, int posY, char character, short colour)
        {
            for (int y = 0; y < 7; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if(fontChar[y*5+x] == ' ') continue;
                    UIBuffer[(y+posY)*Settings.SCREEN_SIZE_X + x+posX].Char.AsciiChar = Convert.ToByte(character);
                    UIBuffer[(y+posY)*Settings.SCREEN_SIZE_X + x+posX].Attributes = colour;

                }
            }

        }


        public static void ResetUI()
        {
            
            UIBuffer = Rasteriser.GenerateEmptyBuffer();
        }


        public static void Write(string text)
        {
            // Very hacky
            if(!Settings.DISPLAY_DEBUG_INFO) return;
            debugText += text;
        
        }

        public static void WriteFPS(string fps)
        {
            debugText += fps + "\n";
        }

        public static void WriteLine(string text)
        {
            if(!Settings.DISPLAY_DEBUG_INFO) return;
            Write(text + "\n");
        }

        

        public static Window.CharInfo[] ApplyUI(Window.CharInfo[] buffer)
        {
            
            // Debugtext
            int x = 0;
            int y = 0;
            for (int i = 0; i < debugText.Length; i++)
            {
                if (debugText[i] == '\n')
                {
                    y++;
                    x = 0;
                    continue;
                }
                buffer[y*Settings.SCREEN_SIZE_X + x].Char.AsciiChar = Convert.ToByte(debugText[i]);
                buffer[y*Settings.SCREEN_SIZE_X + x].Attributes = (short)ConsoleColor.White;
                x++;

            }
            debugText = "";

            // Main UI
            for (int b = 0; b < Settings.SCREEN_SIZE_Y; b++)
            {
                for (int a = 0; a < Settings.SCREEN_SIZE_X; a++)
                {
                    
                    if(UIBuffer[b*Settings.SCREEN_SIZE_X + a].Char.AsciiChar == 32)
                    {
                        continue;
                    }

                    buffer[b*Settings.SCREEN_SIZE_X + a].Char.AsciiChar = UIBuffer[b*Settings.SCREEN_SIZE_X + a].Char.AsciiChar;
                    buffer[b*Settings.SCREEN_SIZE_X + a].Attributes = UIBuffer[b*Settings.SCREEN_SIZE_X + a].Attributes;
                }
            }
           
            return buffer;
        }
    }
}