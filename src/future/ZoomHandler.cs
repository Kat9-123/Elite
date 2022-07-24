using System;

namespace Elite
{
    public class ZoomHandler : GameObject
    {
        private bool isZoomed = false;
        public override void Start()
        {
            visible = false;
        }

        public override void Update(float deltaTime)
        {
            if(InputManager.IsKeyPressed(InputMap.ZOOM))
            {
                isZoomed = !isZoomed;
                
            }

            if(isZoomed)
            {
                Renderer.SetProjectionMatrix(40f);
                Engine.main.player.zoomMultiplier = 0.6f;
            }
            else
            {
                Renderer.SetProjectionMatrix(Settings.FOV);
                Engine.main.player.zoomMultiplier = 1f;
            }
        }


    }
}
