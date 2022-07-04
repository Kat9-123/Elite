using System;

namespace Elite
{
    public class MouseController : GameObject
    {

        private Vector2 centre = new Vector2(500,500);

        public bool enabled = true;

        private Vector2 virtualMousePosition = new Vector2(0,0);

        public Vector2 spritePos = new Vector2(0,0);
        public override void Start()
        {
            visible = false;
            MouseManager.ShowCursor(false);
        }

        public override void Update(float deltaTime)
        {
            return;
          //  if(!enabled) return;



            Vector2 mouseMovement = MouseManager.GetCursorPosition() - centre;


            Renderer.WriteLine(Utils.FormatVector(mouseMovement,"mouseMovement"));

            virtualMousePosition += mouseMovement;

            if(virtualMousePosition.Length() > 100)
            {
                virtualMousePosition = virtualMousePosition.Normalise() * 100f;
            }

            MouseManager.SetCursorPosition(centre);

            Renderer.WriteLine(Utils.FormatVector(virtualMousePosition,"virtmousePos"));

            spritePos = (virtualMousePosition/5f) + new Vector2(72,80);
            
        }


    }
}
