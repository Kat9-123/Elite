using System;

namespace Elite
{
    public struct Triangle
    {
        public Vector3 a,b,c;

        public Triangle(Vector3 _a, Vector3 _b, Vector3 _c)
        {
            a = _a;b = _b; c = _c;
        }

        public static Triangle operator +(Triangle tri, Vector3 vec) => new Triangle(tri.a+vec,tri.b+vec,tri.c+vec);

    }
}
