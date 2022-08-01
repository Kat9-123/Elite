using System;

namespace Elite
{
    public class MouseController : GameObject
    {

        private bool mouseFreed = false;



        // Max mouse distance from centre expressed as a percentage
        // of the screen size.
        private const float MAX_MOUSE_DIST_PERCENTAGE = 0.7f;

        private const float MIN_MOUSE_DIST_PERCENTAGE = 0.1f;



        public Vector2 mousePosition;


        public override void Start()
        {
            visible = false;

            DisableConsoleQuickEdit.Go();
            if(!Settings.DO_MOUSE_CONTROLS) return;
            MouseManager.ShowCursor(false);

            Vector2 pos,size;
            (pos,size) = Window.WinRect();


            Vector2 centre = size / 2f + pos;

            MouseManager.SetCursorPosition(centre);
        }

        public override void Update(float deltaTime)
        {
            

            if(!Settings.DO_MOUSE_CONTROLS) return;

            if(InputManager.IsKeyPressed(InputMap.FREE_MOUSE)) 
            {
                mouseFreed = !mouseFreed;

                Vector2 apos,asize;
                (apos,asize) = Window.WinRect();


                Vector2 acentre = asize / 2f + apos;

                if(!mouseFreed) MouseManager.SetCursorPosition(acentre);
            }

            if(mouseFreed) 
            {
                Engine.main.player.rotationDirection.x = 0f;
                Engine.main.player.rotationDirection.y = 0f;
                return;
            }

            Vector2 pos,size;
            (pos,size) = Window.WinRect();


            Vector2 centre = size / 2f + pos;

            
            float maxMouseDist = size.x * MAX_MOUSE_DIST_PERCENTAGE / 2f;
            float minMouseDist = size.x * MIN_MOUSE_DIST_PERCENTAGE / 2f;


            mousePosition = MouseManager.GetCursorPosition() - centre;

    

            mousePosition = mousePosition.Clamp(maxMouseDist);


            Vector2 direction = mousePosition.Normalise();
            float length = mousePosition.Length() - minMouseDist;
            if(length >= 0)
            {
                Vector2 adjusted = direction * length;


                Engine.main.player.rotationDirection.x = -(adjusted.y / maxMouseDist) * 1.5f;
                Engine.main.player.rotationDirection.y = (adjusted.x / maxMouseDist) * 1.5f;
                
            }
            else
            {
                
                Engine.main.player.rotationDirection.x = 0f;
                Engine.main.player.rotationDirection.y = 0f;
               
            }


            MouseManager.SetCursorPosition(mousePosition + centre);


        }


    }
}
