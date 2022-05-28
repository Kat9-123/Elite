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


        public static Vector3 RotationToFowardVector(Vector3 rotation)
        {
            Vector3 forward = new Vector3(0,0,0);
            forward.x = (float) (Math.Cos(rotation.x) * Math.Sin(rotation.y));
            forward.y = -MathF.Sin(rotation.x);
            forward.z = (float)( Math.Cos(rotation.x) * Math.Cos(rotation.y));
            return forward;
        }

        public static Vector3 RotateAroundAxis(Vector3 vec, Vector3 axis, float theta)
        {
            Vector3 result;

            result = vec * MathF.Cos(theta) + (Cross(axis,vec))*MathF.Sin(theta) + axis*(axis.Dot(vec)) * (1-MathF.Cos(theta));
            return result;
        }




        public static Mesh GenerateCircle(int pointCount)
        {
            Mesh result = new Mesh();

            Vector3[] vectors = new Vector3[pointCount];
            float fullCircle = 2 * MathF.PI;
            float pointRads = fullCircle / (float) pointCount;

            
            float currentRads = 0f;
            for (int i = 0; i < pointCount; i++)
            {

            
                
                
                vectors[i].x = MathF.Cos(currentRads);
                vectors[i].y = MathF.Sin(currentRads);

                 

                currentRads += pointRads;
            }

            Triangle[] tris = new Triangle[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                if(i == pointCount-1)
                {
                    tris[i].a = vectors[i];
                    tris[i].b = vectors[0];
                    tris[i].c = new Vector3(0,0,0);
                    continue;
                }



                tris[i].a = new Vector3(0,0,0);
                tris[i].b = vectors[i];
                tris[i].c = vectors[i+1];
            





            }
            result.tris = tris;

            return result;
        }


        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            Vector3 result;

            result.x = a.y * b.z - b.y * a.z;
            result.y = (a.x * b.z - b.x * a.z) * -1;
            result.z = a.x * b.y - b.x * a.y;

            return result;
        }


        public static float Lerp(float start, float end, float t)
        {
            return (1 - t) * start + t * end;
        }

    }
}