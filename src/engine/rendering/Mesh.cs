// A mesh is basically just a list of triangles.
namespace Elite
{
    public struct Mesh
    {
        public Triangle[] tris;

        public Mesh(Triangle[] _tris)
        {
            tris = _tris;
        }
        public Mesh(string path="")
        {
            tris = ModelLoader.LoadModel(path);
        }


    }
}
