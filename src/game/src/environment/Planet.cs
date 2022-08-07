using System;

namespace Elite
{
    public class Planet : GameObject
    {


        private Vector3 pos;


        public Planet(Vector3 _offset, short _colour, float _scale)
        {
            luminances = Settings.PLANET_LUMINANCES;
            pos = _offset;
            colour = _colour;
            scale = new Vector3(_scale,_scale,_scale);
            getsLit = true;

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
            if(Settings.PLANET_SCALE == 0) 
            {
                position = pos + Engine.cameraPosition;
                return;
            }

            position = pos + Engine.cameraPosition - Engine.cameraPosition/Settings.PLANET_SCALE;

            
        }




    }
}
