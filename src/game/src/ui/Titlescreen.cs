using System;

namespace Elite
{
    public class Titlescreen : GameObject
    {
        private Timer flashTimer = new Timer(0.8f);
        private bool currentFlash = false;

        private float forwardRotation = Utils.RandomFloat(0.08f,0.28f);
        private float upRotation = Utils.RandomFloat(0.08f,0.28f);
        public override void Start()
        {
            Engine.ChanageIndex(this,1);
            position = new Vector3(0,0,40);
            getsLit = true;
            lightingDirection = new Vector3(0,1,0);

            forward = Utils.RotateAroundAxis(forward,up,Utils.RandomFloat(0,2*MathF.PI));
            up = Utils.RotateAroundAxis(up,forward,Utils.RandomFloat(0,2*MathF.PI));

            if(Utils.RandomInt(0,2) == 0)
            {
                mesh = Models.stingrayMesh;
            }
            else
            {
                mesh = Models.chargerMesh;
            }

        }

        public override void Update(float deltaTime)
        {
            if(flashTimer.Accumulate())
            {
                currentFlash = !currentFlash;
                flashTimer.Reset();
            }

            short col = 15;
            if(currentFlash) col = 1;

            UI.WriteText("Elite not very Dangerous",20,20,'#',col);
            UI.WriteText("By: Kat9;123",53,33,'^',8);
            UI.WriteText("Press Enter to start",31,136,'+',11);
            

            forward = Utils.RotateAroundAxis(forward,up,forwardRotation*deltaTime);
            up = Utils.RotateAroundAxis(up,forward,upRotation*deltaTime);
            if(InputManager.IsKeyPressed(InputMap.START_KEY))
            {
                Engine.gameManager.Setup();
                Engine.QueueDestruction(this);
            }
        }


    }
}
