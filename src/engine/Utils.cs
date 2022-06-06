using System;

namespace Elite
{

    public static class Utils
    {
        private static Random rng = new Random();

        public static float RandomFloat(float min, float max)
        {
            return ((float) rng.NextDouble()) * (max - min) + min;
            
        }
        public static int RandomInt(int min, int max)
        {
            return rng.Next(min,max);
        }

        public static int RandomSign()
        {
            int n = rng.Next(0,2);


            if (n == 1) return 1;
            
            return -1;
        }


        public static Mesh GenerateRepeatingMesh(Mesh _mesh, int count, int seperation)
        {
            //scale = new Vector3(20,20,1);


        //    Triangle baseTri = new Triangle();

            Triangle[] triangles = new Triangle[count*_mesh.tris.Length];

            for (int segment = 0; segment < count; segment++)
            {
                for (int tri = 0; tri < _mesh.tris.Length; tri++)
                {
                    triangles[segment*_mesh.tris.Length + tri] = _mesh.tris[tri];

                    triangles[segment*_mesh.tris.Length + tri].a.z += seperation*segment;
                    triangles[segment*_mesh.tris.Length + tri].b.z += seperation*segment;
                    triangles[segment*_mesh.tris.Length + tri].c.z += seperation*segment;
                }

            }
            return new Mesh(triangles);
        }

        public static string FormatVector(Vector3 vec, string name)
        {
            string result = name + ": (";
            result += vec.x.ToString() + ", ";
            result += vec.y.ToString() + ", ";
            result += vec.z.ToString() + ")";

            return result;
        }

        public static string FormatBool(bool b, string name)
        {
            string result = name + ": ";
            result += b.ToString();

            return result;
        }




        public static Vector3 RotateAroundAxis(Vector3 vec, Vector3 axis, float theta)
        {
            Vector3 result;

            result = vec * MathF.Cos(theta) + (Cross(axis,vec))*MathF.Sin(theta) + axis*(axis.Dot(vec)) * (1-MathF.Cos(theta));
            return result;
        }






        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            Vector3 result = new Vector3();

            result.x = a.y * b.z - b.y * a.z;
            result.y = (a.x * b.z - b.x * a.z) * -1;
            result.z = a.x * b.y - b.x * a.y;

            return result;
        }

        public static Vector3 RelativeToRotation(Vector3 pos, Vector3 forward, Vector3 up)
        {
            Vector3 right = Cross(up,forward);


            Vector3 result;
            result = right*pos.x + up*pos.y + forward*pos.z;

            return result;


        }


        public static float Lerp(float start, float end, float t)
        {
            return (1 - t) * start + t * end;
        }

    }
}