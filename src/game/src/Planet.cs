using System;

namespace Elite
{
    public class Planet : GameObject
    {


        private Vector3 offset;
        

        public Planet(Vector3 _offset, short _colour, float _scale, bool _isLit = true)
        {
            offset = _offset;
            colour = _colour;
            scale = new Vector3(_scale,_scale,_scale);
            getsLit = _isLit;


           

        }

        public override void Start()
        {

            mesh = Models.planet;
            

        }

        public override void Update(float deltaTime)
        {
            
            position = offset + Engine.cameraPosition;
        }




    }
}
