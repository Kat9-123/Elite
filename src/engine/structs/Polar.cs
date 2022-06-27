using System;

namespace Elite
{

    public struct Polar
    {
        public float length, angle;

        public Polar(float _length, float _angle)
        {
            length = _length; angle = _angle;


        }
        /*
        public Vector2 ToCartesian()
        {
            Vector vec = new Vector();
            
            vec.x = length * MathF.Cos(angle);
            vec.y = length * MathF.Sin(angle);

            return vec;

        }
        */





    }
}
