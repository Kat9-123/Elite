using System;


namespace Elite
{
    public static class UI
    {

        public static void AddSprite(Sprite sprite, int posX, int posY,char character = '#')
        {
            for (int y = 0; y < sprite.height; y++)
            {
                for (int x = 0; x < sprite.width; x++)
                {
                    if(sprite.pixels[y*sprite.width+x].Attributes == 0) continue;
                    if((x+posX) < 0 || (x+posX) >= Settings.SCREEN_SIZE_X || (y+posY) < 0 || (y+posY) >= Settings.SCREEN_SIZE_Y) continue;
                   // buffer[(y+posY)*Settings.SCREEN_SIZE_X+x+posX].Attributes = sprite.pixels[y*sprite.width+x].Attributes;
                    //buffer[(y+posY)*Settings.SCREEN_SIZE_X+x+posX].Char.AsciiChar = Convert.ToByte(character);
                    UIBuffer[(y+posY)*Settings.SCREEN_SIZE_X+x+posX]= sprite.pixels[y*sprite.width+x];
                }
            }
  
        }

        public static void WriteText(string text, int posX, int posY)
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

                int index = char.ToUpper(text[i]) - '@';

                if(index < 0)
                {
                    index = char.ToUpper(text[i]) - '/' + 26;

                }

                if(text[i] != ' ') WriteCharacter(FontHandler.characters[index],posX,posY);

                posX += 6;
                
            }


        }
        
        private static void WriteCharacter(string character, int posX, int posY)
        {
            for (int y = 0; y < 7; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if(character[y*5+x] == ' ') continue;
                    UIBuffer[(y+posY)*Settings.SCREEN_SIZE_X + x+posX].Char.AsciiChar = Convert.ToByte(character[y*5+x]);
                    UIBuffer[(y+posY)*Settings.SCREEN_SIZE_X + x+posX].Attributes = (short)ConsoleColor.White;

                }
            }

        }

        private static ConsoleInterface.CharInfo[] UIBuffer = new ConsoleInterface.CharInfo[Settings.SCREEN_SIZE_X*Settings.SCREEN_SIZE_Y];

        public static void ResetUI()
        {
            UIBuffer = Renderer.GenerateEmptyBuffer();
        }


        public static ConsoleInterface.CharInfo[] ApplyUI(ConsoleInterface.CharInfo[] buffer, string text)
        {

            
        //    buffer = WriteCharacter(buffer,FontHandler.characters[0],20,20);
          //  buffer = WriteCharacter(buffer,FontHandler.characters[1],26,20);

            //buffer = WriteText(buffer,"abcdefg\nhijklmnop\nqrstuvw\nxyz\n0123456789",0,20);

            //buffer = WriteText(buffer,"vel",20,20);
            

           // Sprite sp = SpriteLoader.Load("Crosshair.png");


          //  buffer = AddSprite(buffer,Sprites.radar2,60,140,'.');
            //Sprite cockpit = SpriteLoader.Load("test.png");
//            buffer = AddSprite(buffer,Sprites.cockpit,0,0);



          //  buffer = WriteText(buffer, "100",0,170);
            int x = 0;
            int y = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '\n')
                {
                    y++;
                    x = 0;
                    continue;
                }
                buffer[y*Settings.SCREEN_SIZE_X + x].Char.AsciiChar = Convert.ToByte(text[i]);
                buffer[y*Settings.SCREEN_SIZE_X + x].Attributes = (short)ConsoleColor.White;
                x++;

            }


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