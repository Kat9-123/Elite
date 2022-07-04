using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Elite
{



    public static class InputManager
    {

        private static List<short> keysPressed = new List<short>(16); 


        [DllImport("user32.dll")]
        private static extern int GetAsyncKeyState(int vKeys);

        public static bool IsKeyHeld(InputMap key)
        {
            return (GetAsyncKeyState((short)key) > 1);
        }


        public static bool IsKeyPressed(InputMap _key)
        {
            short key = (short) _key;
            bool state = (GetAsyncKeyState(key) > 1);

            if(state)
            {
                if(keysPressed.Contains(key))
                {
                    return false;
                }

                keysPressed.Add(key);
                return true;
            }


            if(keysPressed.Contains(key))
            {
                keysPressed.Remove(key);
            }



            return false;
        
        }
    }
}