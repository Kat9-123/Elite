// Very simple inputmanager 
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Drawing;



namespace Elite
{



    public static class InputManager
    {
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);


        [DllImport("user32.dll")]
        private static extern int GetAsyncKeyState(int vKeys);

        // Keys that are currently pressed
        private static List<short> keysPressed = new List<short>(16);



        public static bool IsKeyHeld(InputMap key)
        {
            return (GetAsyncKeyState((short)key) > 1);
        }


        public static bool IsKeyPressed(InputMap _key)
        {
            short key = (short) _key;
            bool state = (GetAsyncKeyState(key) > 1);


            // If the key is pressed, we check if it's in the keypressed list.
            // if it is, the key is held, so we return false. Otherwise
            // we add it to the keyspressed list and return true
            if(state)
            {
                if(keysPressed.Contains(key)) return false;

                keysPressed.Add(key);

                return true;
            }

            // If the key isn't pressed, we check if it's still in the pressed list,
            // remove it and of course return false.
            if(keysPressed.Contains(key)) keysPressed.Remove(key);
            return false;
        
        }





        public static void SetCursorPosition(Vector2 pos)
        {
            SetCursorPos((int) pos.x, (int) pos.y);
        }

        public static Vector2 GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);

            Vector2 vec = new Vector2((float) lpPoint.X, (float) lpPoint.Y);
            return vec;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

    }
}