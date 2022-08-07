// Heavily based on OLC's tutorial
using System;
using System.Collections.Generic;
using System.Globalization;
namespace Elite
{   
    public static class ModelLoader
    {
        // Goddamn this culture bs haunted me for a couple of days
        private static CultureInfo culture = new CultureInfo("en-UK");

        // Obj files are split up in to two parts
        // a list of vertices (prefix: v) and a list of faces (prefix: f)
        // every face uses 3 vertices (thus forming a triangle)

        // Example (Quad)
        /*
            v -1.000000 -1.000000 0.000000
            v 1.000000 -1.000000 0.000000
            v -1.000000 1.000000 -0.000000
            v 1.000000 1.000000 -0.000000

            f 2 3 1
            f 2 4 3
        */
        public static Triangle[] LoadModel(string path)
        {   
            string data = FileHandler.Read("models\\" + path);

            string[] splitData = data.Split("\n");

            List<Vector3> verts = new List<Vector3>();
            List<Triangle> tris = new List<Triangle>();

            for (int i = 0; i < splitData.Length; i++)
            {
                if(splitData[i] == "") continue;


                if(splitData[i][0] == 'v')
                {
                    Vector3 vec = new Vector3();
                    string[] c = splitData[i].Split(" ");
                    vec.x = float.Parse(c[1],culture);
                    vec.y = float.Parse(c[2],culture);
                    vec.z = float.Parse(c[3],culture);

                    verts.Add(vec);
                }
                if(splitData[i][0] == 'f')
                {
                    Triangle tri;
                    string[] c = splitData[i].Split(" ");

                    // A face refrences three vertices
                    // (vertices are indexed starting from 1 for some godforsaken reason)
                    tri.a = verts[Int32.Parse(c[1])-1];
                    tri.b = verts[Int32.Parse(c[2])-1];
                    tri.c = verts[Int32.Parse(c[3])-1];

                    tris.Add(tri);
                }
            }
        
            return tris.ToArray();

        }

    }

}