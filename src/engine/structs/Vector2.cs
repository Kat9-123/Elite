using System;

namespace Elite
{

    public struct Vector2
    {
        public float x,y;

        public Vector2(float _x, float _y)
        {
            x = _x; y = _y;
        }

        public float LengthSquared()
        {
            return x*x + y*y;
        }

        public float Length()
        {
            return MathF.Sqrt(LengthSquared());
        }

        public Vector2 Normalise()
        {

            Vector2 result = new Vector2(0,0);
            float l = Length();

            if(l == 0f) {result.x = 0f; result.y = 0f; return result;}
            result.x = x/l; result.y = y/l;

            return result;
        }


        public static Vector2 operator -(Vector2 vec1, Vector2 vec2) => new Vector2(vec1.x-vec2.x,vec1.y-vec2.y);
        public static Vector2 operator +(Vector2 vec1, Vector2 vec2) => new Vector2(vec1.x+vec2.x,vec1.y+vec2.y);
        public static Vector2 operator *(Vector2 vec, float val) => new Vector2(vec.x*val,vec.y*val);
        public static Vector2 operator /(Vector2 vec, float n) => new Vector2(vec.x/n,vec.y/n);
    }
}
