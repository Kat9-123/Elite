using System;

namespace Elite
{
    public class Planet : GameObject
    {


        private Vector3 pos;
        

        public Planet(Vector3 _offset, short _colour, float _scale, bool _isLit = true)
        {
            pos = _offset;
            colour = _colour;
            scale = new Vector3(_scale,_scale,_scale);
            getsLit = _isLit;


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
            
            position = pos + Engine.cameraPosition;
        }




    }
}
