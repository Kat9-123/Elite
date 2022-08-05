// Very basic because the game only uses two very simple sprites. 
// (that could also just be hard coded tbh)
using System;
using System.Drawing;

namespace Elite
{   
    public static class SpriteLoader
    {


        private static Colour[] colours = new Colour[] {
            new Colour(12,12,12),
            new Colour(0,55,218),
            new Colour(19,161,14),
            new Colour(58,150,221),
            new Colour(197,15,31),
            new Colour(136,23,152),
            new Colour(193,156,0),
            new Colour(255,128,0),
            new Colour(118,118,118),
            new Colour(59,120,255),
            new Colour(22,198,12),
            new Colour(97,214,214),
            new Colour(231,72,86),
            new Colour(180,0,158),
            new Colour(249,241,165),
            new Colour(242,242,242),
            
        };


        private static int DistanceBetweenColours(Colour col1, Colour col2)
        {
            return Math.Abs(col1.R - col2.R) + Math.Abs(col1.G - col2.G) + Math.Abs(col1.B - col2.B);
        }

        public static void SetSprite(Sprite sprite, string path)
        {

            Bitmap img = new Bitmap(FileHandler.originPath + "sprites\\" + path);
            ConsoleInterface.CharInfo[] pixels = new ConsoleInterface.CharInfo[img.Width*img.Height];
            sprite.width = img.Width;
            sprite.height = img.Height;
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    Color pixel = img.GetPixel(x,y);

                    if(pixel.A > 0)
                    {
                        int dist = 10000;
                        int colourIndex = 0;
                        Colour col = new Colour(pixel.R,pixel.G,pixel.B);
                        for (int i = 0; i < colours.Length; i++)
                        {
                            if(DistanceBetweenColours(col,colours[i]) < dist)
                            {
                                colourIndex = i;
                                dist = DistanceBetweenColours(col,colours[i]);
                            }
                        }

                        pixels[y*img.Width+x].Char.AsciiChar = Convert.ToByte('#');
                        pixels[y*img.Width+x].Attributes = (short)colourIndex;

                    }
                }
            } 
            
            sprite.pixels = pixels;


        }
    }

}