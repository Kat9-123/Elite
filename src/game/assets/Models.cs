// I'm no 3d modeller, but I'm pretty happy with the results. 

namespace Elite
{

    public static class Models
    {

        public static Mesh laserLeft = new Mesh(new Triangle[] {
            new Triangle(new Vector3(-1.2f,1f,0),new Vector3(-1.2f,1.03f,0),new Vector3(-0.1f,0.1f,10)),
        });
        public static Mesh laserRight = new Mesh(new Triangle[] {
            new Triangle(new Vector3(1.2f,1f,0),new Vector3(1.2f,1.03f,0),new Vector3(0,0.1f,10)),
        });
        public static Mesh quad = new Mesh(new Triangle[] {

            // SOUTH
            new Triangle(new Vector3(0,0,0),new Vector3(0,1,0),new Vector3(1,1,0)),
            new Triangle(new Vector3(0,0,0),new Vector3(1,1,0),new Vector3(1,0,0)),
        });
        public static Mesh dustMesh = new Mesh(new Triangle[] {


            // NORTH
            new Triangle(new Vector3(1,0,1),new Vector3(1,1,1),new Vector3(0,1,1)),
            new Triangle(new Vector3(1,0,1),new Vector3(0,1,1),new Vector3(0,0,1)),
        });

        public static Mesh cube = new Mesh("Cube.obj");


        public static Mesh pip = new Mesh("PIP.obj");







        public static Mesh enemyLaserMesh = Utils.GenerateRepeatingMesh(new Mesh(new Triangle[]{new Triangle(new Vector3(0,0,0),new Vector3(0,0.0001f,0), new Vector3(0,0,5))}),1000,5);

        public static Mesh bigLaserMesh = Utils.GenerateRepeatingMesh(new Mesh("BigLaser.obj"),1000,20);

        public static Mesh planet = new Mesh("PlanetLow.obj");


        public static Mesh circle = new Mesh("Circle.obj");

        public static Mesh chargerMesh = new Mesh("Ship2.obj");
        public static Mesh stingrayMesh = new Mesh("Ship.obj");
        public static Mesh bossMesh = new Mesh("Boss.obj");

        public static Mesh radarEnemyMesh = new Mesh("RadarEnemy.obj");

        public static Mesh targetMesh = new Mesh("target.obj");


        public static Mesh directionMesh = new Mesh("directionIndicator.obj");
        


        /*
                public static Mesh dustMesh = new Mesh(new Triangle[] {

            new Triangle(new Vector3(0,0,0),new Vector3(0,10f,0),new Vector3(10f,10f,0)),
            new Triangle(new Vector3(0,0,0),new Vector3(10f,10f,0),new Vector3(10f,0,0)),
        });
        */
    }

        
}