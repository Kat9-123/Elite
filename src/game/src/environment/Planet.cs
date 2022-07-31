using System;

namespace Elite
{
    public class Planet : GameObject
    {


        private Vector3 pos;

        private bool isSun = false;
        

        public Planet(Vector3 _offset, short _colour, float _scale, bool _isLit = true)
        {
            luminances = "#0OC*+/^,.        ";
            pos = _offset;
            colour = _colour;
            scale = new Vector3(_scale,_scale,_scale);
            getsLit = _isLit;
            isSun = !getsLit;

            position = pos;


            switch(Settings.PLANET_QUALITY)
            {
                case 0:
                    mesh = Models.planetLow;
                    break;
                case 1:
                    mesh = Models.planetMedium;
                    break;
                case 2:
                    mesh = Models.planetHigh;
                    break;
            }
            

           

        }


        public override void Update(float deltaTime)
        {
            if(isSun || Settings.PLANET_SCALE == 0) 
            {
                position = pos + Engine.cameraPosition;
                return;
            }


            position = pos + Engine.cameraPosition - Engine.cameraPosition/Settings.PLANET_SCALE;

            
        }




    }
}
