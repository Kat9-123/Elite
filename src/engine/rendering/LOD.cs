using System;

namespace Elite
{
    public class LOD
    {
        private Mesh[] lods;
        private float[] squaredDistances;
        public LOD(string[] lodPaths, float[] distances)
        {
            lods = new Mesh[lodPaths.Length];
            squaredDistances = new float[distances.Length];
            for (int i = 0; i < lodPaths.Length; i++)
            {
                lods[i] = new Mesh(lodPaths[i]);


                squaredDistances[i] = distances[i]*distances[i];
            }
            
        }

        public (Mesh, int) Update(Vector3 position)
        {
            for (int i = squaredDistances.Length-1; i >= 0; i--)
            {
                if(position.SquaredDistanceTo(Engine.cameraPosition) > squaredDistances[i])
                {
                    return (lods[i],i+3);
                }
            }   
            return (lods[0],1);
        }
    }
}