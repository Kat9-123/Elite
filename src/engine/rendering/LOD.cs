// Simple Level Of Detail system
namespace Elite
{
    public class LOD
    {
        // List of meshes used as LODS
        private Mesh[] lods;

        //. List of distances (squared) from camera required to
        // swap LOD
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

        // The int is a colour for debugging to identify which LOD is currently rendered.
        // Note: This Update function has nothing to do with GameObject.Update!
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