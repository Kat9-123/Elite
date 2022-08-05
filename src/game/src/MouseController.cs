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

            if(!Settings.DO_MOUSE_CONTROLS) return;

            Vector2 pos,size;
            (pos,size) = ConsoleInterface.WindowRect();


            Vector2 centre = size / 2f + pos;

            InputManager.SetCursorPosition(centre);
        }

        public override void Update(float deltaTime)
        {
            

            if(!Settings.DO_MOUSE_CONTROLS) return;

            if(InputManager.IsKeyPressed(InputMap.FREE_MOUSE)) 
            {
                mouseFreed = !mouseFreed;

                Vector2 apos,asize;
                (apos,asize) = ConsoleInterface.WindowRect();

                Vector2 acentre = asize / 2f + apos;

                if(!mouseFreed) InputManager.SetCursorPosition(acentre);
            }

            if(mouseFreed) 
            {
                Engine.gameManager.player.rotationDirection.x = 0f;
                Engine.gameManager.player.rotationDirection.y = 0f;
                return;
            }

            Vector2 pos,size;
            (pos,size) = ConsoleInterface.WindowRect();


            Vector2 centre = size / 2f + pos;

            
            float maxMouseDist = size.x * MAX_MOUSE_DIST_PERCENTAGE / 2f;
            float minMouseDist = size.x * MIN_MOUSE_DIST_PERCENTAGE / 2f;


            mousePosition = InputManager.GetCursorPosition() - centre;

    

            mousePosition = mousePosition.Clamp(maxMouseDist);


            Vector2 direction = mousePosition.Normalise();
            float length = mousePosition.Length() - minMouseDist;
            if(length >= 0)
            {
                Vector2 adjusted = direction * length;


                Engine.gameManager.player.rotationDirection.x = -(adjusted.y / maxMouseDist) * 1.5f;
                Engine.gameManager.player.rotationDirection.y = (adjusted.x / maxMouseDist) * 1.5f;
                
            }
            else
            {
                
                Engine.gameManager.player.rotationDirection.x = 0f;
                Engine.gameManager.player.rotationDirection.y = 0f;
               
            }


            InputManager.SetCursorPosition(mousePosition + centre);


        }


    }
}
