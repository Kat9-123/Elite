// Takes 2D triangles from the renderer and writes those to an output buffer,
// Which it then sends to the Window to be dumped onto the screen.
using System;

namespace Elite
{
    public static class Rasteriser
    {

        public static Window.CharInfo[] buffer;

        public static Window.CharInfo[] GenerateEmptyBuffer()
        {
            Window.CharInfo[] result = new Window.CharInfo[Settings.SCREEN_SIZE_Y*Settings.SCREEN_SIZE_X];
            for (int y = 0; y < Settings.SCREEN_SIZE_Y; y++)
            {
                for (int x = 0; x < Settings.SCREEN_SIZE_X; x++)
                {
                    result[y*Settings.SCREEN_SIZE_X + x].Char.AsciiChar = Convert.ToByte(' ');
                }
            }
            return result;
        }


        public static void DrawBufferToScreen()
        {
            UI.ApplyUI(ref buffer);
            Window.Write(ref buffer);
        }
        public static void Reset()
        {
            buffer = GenerateEmptyBuffer();
        }


// NON-FILLED
        public static void DrawTriangle(Triangle triangle,char character,short colour)
        {
            IntVector2 a = new IntVector2(triangle.a);
            IntVector2 b = new IntVector2(triangle.b);
            IntVector2 c = new IntVector2(triangle.c);


            DrawLine(a,b,character,colour);
            DrawLine(b,c,character,colour);
            DrawLine(c,a,character,colour);


        }
        // STACKOVERFLOW
        private static void DrawLine(IntVector2 a, IntVector2 b,char character,short colour) 
        {
            int w = b.x - a.x;
            int h = b.y - a.y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;

            if (w<0) dx1 = -1; else if (w>0) dx1 = 1;
            if (h<0) dy1 = -1; else if (h>0) dy1 = 1;
            if (w<0) dx2 = -1; else if (w>0) dx2 = 1;

            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest>shortest)) {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;            
            }
            int numerator = longest >> 1 ;
            if(longest > 1000) return;
            for (int i=0;i<=longest;i++) {
                if (a.y >= 0 && a.y < Settings.SCREEN_SIZE_Y)
                {
                    if (a.x >= 0 && a.x < Settings.SCREEN_SIZE_X)
                    {
                        buffer[a.y*Settings.SCREEN_SIZE_X + a.x].Char.AsciiChar = (byte)character;
                        buffer[a.y*Settings.SCREEN_SIZE_X + a.x].Attributes = colour;
                    }
                }
                
                numerator += shortest ;
                if (!(numerator<longest)) {
                    numerator -= longest ;
                    a.x += dx1;
                    a.y += dy1;
                } else {
                    a.x += dx2;
                    a.y += dy2;
                }
            }
        }

// FILLED
        public static void DrawFilledTriangle(Triangle triangle,char character,short colour)
        {
            IntVector2 a = new IntVector2(triangle.a);
            IntVector2 b = new IntVector2(triangle.b);
            IntVector2 c = new IntVector2(triangle.c);

            // First find the bounds of the triangle.
            IntVector2 pos = new IntVector2();
            pos.x = Math.Min(a.x,Math.Min(b.x,c.x));
            pos.y = Math.Min(a.y,Math.Min(b.y,c.y));


            IntVector2 largestSize = new IntVector2();

            largestSize.x = Math.Max(a.x,Math.Max(b.x,c.x));
            largestSize.y = Math.Max(a.y,Math.Max(b.y,c.y));


            IntVector2 posEnd = new IntVector2();

            pos.x = Math.Max(0,pos.x);
            pos.y = Math.Max(0,pos.y);

            posEnd.x = Math.Min(largestSize.x+pos.x,Settings.SCREEN_SIZE_X);
            posEnd.y = Math.Min(largestSize.y+pos.y,Settings.SCREEN_SIZE_Y);


            // Loop trough all pixels within the bounds,
            // and check if they lie within the triangle.
            for (int y = pos.y; y < posEnd.y; y++)
            {
                for (int x = pos.x; x < posEnd.x; x++)
                {
                    if(LiesPointWithinTriangle(new IntVector2(x,y), a,b,c))
                    {
                        buffer[y*Settings.SCREEN_SIZE_X + x].Char.AsciiChar = (byte)character;
                        buffer[y*Settings.SCREEN_SIZE_X + x].Attributes = colour;
                    }
                }
            }

        }

        private static bool LiesPointWithinTriangle(IntVector2 point, IntVector2 a, IntVector2 b, IntVector2 c)
        {

            int as_x = point.x-a.x;
            int as_y = point.y-a.y;

            bool s_ab = (b.x-a.x)*as_y-(b.y-a.y)*as_x > 0;

            if((c.x-a.x)*as_y-(c.y-a.y)*as_x > 0 == s_ab) return false;

            if((c.x-b.x)*(point.y-b.y)-(c.y-b.y)*(point.x-b.x) > 0 != s_ab) return false;

            return true;

        }
    }
}
