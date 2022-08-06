using System;

namespace Elite
{
    public class QuadTest : GameObject
    {
        public override void Start()
        {
            mesh = Models.quad;
           /// is2D = true;
            movesWithCamera = true;
            //forward = new Vector3(0,0,-1);
            filled = true;
            getsClipped = false;
            getsCulled = false;
            scale = new Vector3(1f,5f,1f);
            position = new Vector3(Settings.SCREEN_SIZE_X/2, Settings.SCREEN_SIZE_Y/2,0f);



        }

        public override void Update(float deltaTime)
        {
            int x,y;
            x = 0;
            y = 0;
           // (x,y) = Window.ConvertPxToConsole((int)(Engine.gameManager.mouseController.mousePosition.x+Engine.gameManager.mouseController.centre.x),
            //(int)(Engine.gameManager.mouseController.mousePosition.y+Engine.gameManager.mouseController.centre.y));

            Vector3 pos = new Vector3(0,0,0);
            pos.x = x - Settings.SCREEN_SIZE_X/2;
            pos.y = y;


            up = (pos-new Vector3(Settings.SCREEN_SIZE_X/2, Settings.SCREEN_SIZE_Y/2,0)).Normalise();

            scale.y = (new Vector2(pos.x,pos.y) - new Vector2(Settings.SCREEN_SIZE_X/2, Settings.SCREEN_SIZE_Y/2)).Length();
        }



    }
}
