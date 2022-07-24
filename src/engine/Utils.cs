using System;

using System.Diagnostics;
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

        // Returns -1 or 1
        public static int RandomSign()
        {
            int n = rng.Next(0,2);


            if (n == 1) return 1;
            
            return -1;
        }

        // Takes the given mesh and extends it by the given amount. The distance between itterations
        // is defined by the seperation. This function is mainly used for lasers.
        public static Mesh GenerateRepeatingMesh(Mesh _mesh, int count, int seperation)
        {
 

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


        // Format a vector3/2 so that it can be printed
        public static string FormatVector(Vector3 vec, string name)
        {
            string result = name + ": (";
            result += vec.x.ToString() + ", ";
            result += vec.y.ToString() + ", ";
            result += vec.z.ToString() + ")";

            return result;
        }
        public static string FormatVector(Vector2 vec, string name)
        {
            string result = name + ": (";
            result += vec.x.ToString() + ", ";
            result += vec.y.ToString() + ")";

            return result;
        }

        // Format bool so that it can be printed
        public static string FormatBool(bool b, string name)
        {
            string result = name + ": ";
            result += b.ToString();

            return result;
        }



        // https://en.wikipedia.org/wiki/Rodrigues%27_rotation_formula
        // Should I use matrices for this? yes. Do I want to? no.
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


        // Takes the given position and rotates it so that it's located relative to the given forward and up direction.
        // examples:

        // Forward = (0,0,1), Up = (0,1,0)
        // pos = (0,0,10)
        // returns (0,0,10)

        // Forward = (0,0,-1), Up = (0,1,0)
        // pos = (0,0,10)
        // returns (0,0,-10)

        // Forward = (1,0,0), Up = (0,1,0)
        // pos = (0,0,10)
        // returns (10,0,0)
        // etc.
        public static Vector3 RelativeToRotation(Vector3 pos, Vector3 forward, Vector3 up)
        {
            Vector3 right = Cross(up,forward);


            Vector3 result;
            result = right*pos.x + up*pos.y + forward*pos.z;

            return result;


        }




        public static double CalculateDeltaTime(ref double previousTime)
        {
            
            double timestamp = Stopwatch.GetTimestamp();
            double now = timestamp / Stopwatch.Frequency;
            if (previousTime == 0) previousTime = now;
            double deltaTime = (now - previousTime);
            previousTime = now;
            return deltaTime;
        
        }

        // Generates a random point within the given square
        // but it rejects points that are too close to the centre 
        public static Vector3 RandomPositionExcludeCentre(float minDist, float maxDist)
        {
            Vector3 pos = new Vector3(0,0,0);
            
            pos.x = RandomFloat(minDist,maxDist) * RandomSign();
            pos.y = RandomFloat(minDist,maxDist) * RandomSign();
            pos.z = RandomFloat(minDist,maxDist) * RandomSign();

            
            return pos;
        }

    }
}