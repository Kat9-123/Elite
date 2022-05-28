namespace Elite
{

    public static class Models
    {
        public static Mesh cubeMesh = new Mesh(new Triangle[] {
            // SOUTH
            new Triangle(new Vector3(0,0,0),new Vector3(0,1,0),new Vector3(1,1,0)),
            
            new Triangle(new Vector3(0,0,0),new Vector3(1,1,0),new Vector3(1,0,0)),

            // EAST
            new Triangle(new Vector3(1,0,0),new Vector3(1,1,0),new Vector3(1,1,1)),
            new Triangle(new Vector3(1,0,0),new Vector3(1,1,1),new Vector3(1,0,1)),

            // NORTH
            new Triangle(new Vector3(1,0,1),new Vector3(1,1,1),new Vector3(0,1,1)),
            new Triangle(new Vector3(1,0,1),new Vector3(0,1,1),new Vector3(0,0,1)),

            // WEST
            new Triangle(new Vector3(0,0,1),new Vector3(0,1,1),new Vector3(0,1,0)),
            new Triangle(new Vector3(0,0,1),new Vector3(0,1,0),new Vector3(0,0,0)),

            // TOP
            new Triangle(new Vector3(0,1,0),new Vector3(0,1,1),new Vector3(1,1,1)),
            new Triangle(new Vector3(0,1,0),new Vector3(1,1,1),new Vector3(1,1,0)),

            // BOTTOM
            new Triangle(new Vector3(1,0,1),new Vector3(0,0,1),new Vector3(0,0,0)),
            new Triangle(new Vector3(1,0,1),new Vector3(0,0,0),new Vector3(1,0,0)),
            

        });

        public static Mesh laserLeft = new Mesh(new Triangle[] {
            new Triangle(new Vector3(-1.2f,1f,0),new Vector3(-1.2f,1.03f,0),new Vector3(-0.1f,0.1f,10)),
        });
        public static Mesh laserRight = new Mesh(new Triangle[] {
            new Triangle(new Vector3(1.2f,1f,0),new Vector3(1.2f,1.03f,0),new Vector3(0,0.1f,10)),
        });


        public static Mesh laser = new Mesh(new Triangle[] {
            new Triangle(new Vector3(0f,0f,0),new Vector3(0,0.03f,0),new Vector3(0,0,100)),
        });

        public static Mesh quad = new Mesh(new Triangle[] {

            // SOUTH
            new Triangle(new Vector3(0,0,0),new Vector3(0,1,0),new Vector3(1,1,0)),
            new Triangle(new Vector3(0,0,0),new Vector3(1,1,0),new Vector3(1,0,0)),
        });
        public static Mesh dustMesh = new Mesh(new Triangle[] {

            // SOUTH
            new Triangle(new Vector3(0,0,0),new Vector3(0,1,0),new Vector3(1,1,0)),
            new Triangle(new Vector3(0,0,0),new Vector3(1,1,0),new Vector3(1,0,0)),
            // NORTH
            new Triangle(new Vector3(1,0,1),new Vector3(1,1,1),new Vector3(0,1,1)),
            new Triangle(new Vector3(1,0,1),new Vector3(0,1,1),new Vector3(0,0,1)),
        });

        public static Mesh planetMesh = new Mesh(new Triangle[] {

            // SOUTH


        });

        /*
                public static Mesh dustMesh = new Mesh(new Triangle[] {

            new Triangle(new Vector3(0,0,0),new Vector3(0,10f,0),new Vector3(10f,10f,0)),
            new Triangle(new Vector3(0,0,0),new Vector3(10f,10f,0),new Vector3(10f,0,0)),
        });
        */
    }

        
}