namespace Elite
{
    public class GameObject
    {
        public bool getsLit = false;
        public bool visible = true;

        // If a triangle isn't seen by the camera, it doesn't get rendered
        public bool getsClipped = true;

        // If a triangles normal is pointing away from the camera, it doesn't get rendered
        public bool getsCulled = true;

        public bool filled = false;

        // Hacky simple option that will stop the renderer from applying
        // the camera matrices. It's simple & performant
        public bool movesWithCamera = false;

        // Instead of euler angles I decided to define rotation with
        // a forward vector and an up vector, because it is (in my opinion)
        // far easier to reason with. There is probably some horrible
        // issue with doing rotation like this, but it is what it is.
        public Vector3 up = new Vector3(0,1,0);
        public Vector3 forward = new Vector3(0,0,1);


        public Vector3 scale = new Vector3(1,1,1);
        public Vector3 position = new Vector3(0,0,0);

        // Kinda legacy, only used for hard-coded models
        public Vector3 offset;
        
        
        public short colour = 15;


        // From what direction the object gets lit.
        //
        // 2D example:
        // lightingDir = Vector2(1,0)
        //  
        //              | < y-axis
        //              |  light >  # < dark
        //              |
        //--------------+==>-------   < x-axis
        //              | (1,0)
        //              |
        // Objects get lit from the base of the arrow, in the direction of the arrow. 
        // The lighting source is uniform and infinitely far away.
        public Vector3 lightingDirection = new Vector3(0,0,-1);



        public Mesh mesh;




        public char character;

        // Characters that are used for lighting.
        public string luminances = "#0OC*+/^,.  "; //"$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\\|()1{}[]?-_+~<>i!lI;:,\"^`'. ";

 
        public bool isDestroyed = false;


        public virtual void Start() {}
        public virtual void Update(float deltaTime) {}



        // Legacy
        public void SetMesh(Mesh _mesh)
        {
            mesh = _mesh;
            offset = (FindLargestSize() / 2f) * -1f;


        }

        public void LookAt(Vector3 lookAtPos)
        {
            forward = (lookAtPos - position).Normalise();  
        }

        // Legacy
        public Vector3 FindLargestSize()
        {
            Vector3 result = new Vector3(0,0,0);


            Vector3 topLeft = new Vector3(0,0,0);

            for (int i = 0; i < mesh.tris.Length; i++)
            {
                if(mesh.tris[i].a.x > result.x) result.x = mesh.tris[i].a.x;
                if(mesh.tris[i].a.y > result.y) result.y = mesh.tris[i].a.y;
                if(mesh.tris[i].a.z > result.z) result.z = mesh.tris[i].a.z;

                if(mesh.tris[i].b.x > result.x) result.x = mesh.tris[i].b.x;
                if(mesh.tris[i].b.y > result.y) result.y = mesh.tris[i].b.y;
                if(mesh.tris[i].b.z > result.z) result.z = mesh.tris[i].b.z;

                if(mesh.tris[i].c.x > result.x) result.x = mesh.tris[i].c.x;
                if(mesh.tris[i].c.y > result.y) result.y = mesh.tris[i].c.y;
                if(mesh.tris[i].c.z > result.z) result.z = mesh.tris[i].c.z;
            }

            return result;
        }


    }
}
