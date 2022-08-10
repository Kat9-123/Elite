using System;

namespace Elite
{
    public class MouseController : GameObject
    {

        private bool mouseFreed = false;

        // If the window gets refocused we want the mousecursor to return to the centre.
        private bool reFocused = false;



        // Max mouse distance from centre expressed as a percentage
        // of the screen size.
        private const float MAX_MOUSE_DIST_PERCENTAGE = 0.75f;

        // "Safe" radius expressed as a percentage of the screen size
        // if the mouse is within this region, no rotation happens.
        private const float MIN_MOUSE_DIST_PERCENTAGE = 0.1f;



        public Vector2 mousePosition;


        public override void Start()
        {
            visible = false;

            if(!Settings.DO_MOUSE_CONTROLS) return;

            CentreMousePosition();
        }


        private void ResetRotationDirection()
        {
            Engine.gameManager.player.rotationDirection.x = 0f;
            Engine.gameManager.player.rotationDirection.y = 0f;
        }
        private void CentreMousePosition()
        {
            Vector2 pos,size;
            (pos,size) = Window.WindowRect();


            Vector2 centre = size / 2f + pos;

            InputManager.SetCursorPosition(centre);
        }



        public override void Update(float deltaTime)
        {
            

            if(!Settings.DO_MOUSE_CONTROLS) return;


            if(Engine.gameManager.player.isDead) return;

            if(InputManager.IsKeyPressed(InputMap.FREE_MOUSE)) 
            {
                mouseFreed = !mouseFreed;


                if(!mouseFreed) CentreMousePosition();
            }

            if(mouseFreed) 
            {
                ResetRotationDirection();
                return;
            }

            if(!Window.IsFocused()) 
            {
                ResetRotationDirection();
                reFocused = false; 
                return;
            }

            if(!reFocused)
            {
                reFocused = true;
                CentreMousePosition();
            }

            Vector2 pos,size;
            (pos,size) = Window.WindowRect();


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
                ResetRotationDirection();  
            }


            InputManager.SetCursorPosition(mousePosition + centre);


        }


    }
}
