using System;
using System.IO;
using System.Collections.Generic;
namespace Elite
{   
    public static class ModelHandler
    {
        public static Mesh LoadModel(string path)
        {
            string data = FileHandler.Read("models\\" + path);

            string[] splitData = data.Split("\n");

            List<Vector3> verts = new List<Vector3>();
            List<Triangle> tris = new List<Triangle>();

            for (int i = 0; i < splitData.Length; i++)
            {
                if(splitData[i] == "")
                {
                    continue;
                }

                if(splitData[i][0] == 'v')
                {
                    Vector3 vec;
                    string[] c = splitData[i].Split(" ");
                    vec.x = float.Parse(c[1]);
                    vec.y = float.Parse(c[2]);
                    vec.z = float.Parse(c[3]);

                    verts.Add(vec);
                }
                if(splitData[i][0] == 'f')
                {
                    Triangle tri;
                    string[] c = splitData[i].Split(" ");
                    tri.a = verts[Int32.Parse(c[1])-1];
                    tri.b = verts[Int32.Parse(c[2])-1];
                    tri.c = verts[Int32.Parse(c[3])-1];

                    tris.Add(tri);
                }
            }
        
            Mesh mesh = new Mesh(tris.ToArray());
            return mesh;
        }

    }

}