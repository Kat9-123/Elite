using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Elite
{



    public static class InputManager
    {

        private static List<short> keysPressed = new List<short>(16); // If more than 32 keys are being pressed at the same time
                                                     // God help you.

        [DllImport("user32.dll")]
        private static extern int GetAsyncKeyState(int vKeys);

        public static bool IsKeyHeld(short key)
        {
            return (GetAsyncKeyState(key) > 1);
        }

        private static int KeyState(short key)
        {
            return (GetAsyncKeyState(key));
        }


        public static bool IsKeyPressed(short key)
        {
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