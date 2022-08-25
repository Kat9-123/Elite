using System;

// Your favourite data type, now in integer form.
namespace Elite
{

    public struct IntVector2
    {
        public int x,y;

        public IntVector2(int _x=0, int _y=0)
        {
            x = _x; y = _y;
        }
        public IntVector2(Vector2 vec)
        {
            x = (int) vec.x;
            y = (int) vec.y;
        }

        // The z component gets ignored.
        public IntVector2(Vector3 vec)
        {
            x = (int) vec.x;
            y = (int) vec.y; 
        }

        public float LengthSquared()
        {
            return x*x + y*y;
        }

        public float Length()
        {
            return MathF.Sqrt(LengthSquared());
        }

    }
}
