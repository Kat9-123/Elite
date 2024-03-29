using System;

namespace Elite
{

    public struct Vector3
    {
        public float x,y,z;


        public Vector3(float _x=0f, float _y=0f, float _z=0f)
        {
            x = _x; y = _y; z = _z;
        }

        public Vector3 Normalise()
        {

            Vector3 result = new Vector3(0,0,0);
            float l = Length();

            if(l == 0f) {return result;}
            result = this/l;

            return result;
        }

        public float AngleTo(Vector3 vec)
        {
            Vector3 self = Normalise();
            vec = vec.Normalise();

            float dot = self.Dot(vec);

            if(dot > 1f) dot = 1f;
            if(dot < -1f) dot = -1f; 
            return MathF.Acos(dot);
        }

        public float LengthSquared()
        {
            return x*x + y*y + z*z;
        }

        public float Length()
        {
            return MathF.Sqrt(LengthSquared());
        }

        public Vector3 Clamped(float n)
        {
            Vector3 vec = new Vector3(x,y,z);
            if(LengthSquared() > n*n)
            {
                vec = Normalise() * n;
            }
            return vec;
        }

        public float SquaredDistanceTo(Vector3 vec)
        {
            return ((x - vec.x) * (x - vec.x) + (y - vec.y) * (y - vec.y) + (z - vec.z) * (z - vec.z));
        }


        public float DistanceTo(Vector3 vec)
        {
            return MathF.Sqrt(SquaredDistanceTo(vec));
        }


        public float Dot(Vector3 vec)
        {
            return (x*vec.x + y*vec.y + z*vec.z);
        }


        public override string ToString()
        {
            string result = "(";
            result += x.ToString("0.00") + ", ";
            result += y.ToString("0.00") + ", ";
            result += z.ToString("0.00") + ")";

            return result;
        }


        public static Vector3 operator +(Vector3 vec1, Vector3 vec2) => new Vector3(vec1.x+vec2.x,vec1.y+vec2.y,vec1.z+vec2.z);
        public static Vector3 operator -(Vector3 vec1, Vector3 vec2) => new Vector3(vec1.x-vec2.x,vec1.y-vec2.y,vec1.z-vec2.z);
        public static Vector3 operator *(Vector3 vec1, Vector3 vec2) => new Vector3(vec1.x*vec2.x,vec1.y*vec2.y,vec1.z*vec2.z);

        public static Vector3 operator *(Vector3 vec, float n) => new Vector3(vec.x*n,vec.y*n,vec.z*n);
        public static Vector3 operator /(Vector3 vec, float n) => new Vector3(vec.x/n,vec.y/n,vec.z/n);
        public static Vector3 operator -(Vector3 vec1, float n) => new Vector3(vec1.x-n,vec1.y-n,vec1.z-n);
        public static Vector3 operator +(Vector3 vec, float n) => new Vector3(vec.x+n,vec.y+n,vec.z+n);
    }
}