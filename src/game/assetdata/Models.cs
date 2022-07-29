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


        // Isn't this just a quad? yes.
        public static Mesh dustMesh = new Mesh(new Triangle[] {


            // NORTH
            new Triangle(new Vector3(1,0,1),new Vector3(1,1,1),new Vector3(0,1,1)),
            new Triangle(new Vector3(1,0,1),new Vector3(0,1,1),new Vector3(0,0,1)),
        });


        public static Mesh line = Utils.GenerateRepeatingMesh(new Mesh(new Triangle[] {
            new Triangle(new Vector3(0,0,0),new Vector3(0,0,1),new Vector3(0,0.0001f,1)),
        }),10,1);



        public static Mesh cube = new Mesh("Cube.obj");






        public static Mesh enemyLaserMesh = Utils.GenerateRepeatingMesh(new Mesh(new Triangle[]{new Triangle(new Vector3(0,0,0),new Vector3(0,0.0001f,0), new Vector3(0,0,5))}),1000,5);

        public static Mesh bigLaserMesh = Utils.GenerateRepeatingMesh(new Mesh("BigLaser.obj"),1000,20);


        public static Mesh planetHigh = new Mesh("PlanetHigh.obj");
        public static Mesh planetMedium = new Mesh("PlanetMedium.obj");
        public static Mesh planetLow = new Mesh("PlanetLow.obj");


        // Enemies
        public static Mesh chargerMesh = new Mesh("Charger.obj");
        public static Mesh stingrayMesh = new Mesh("Stingray.obj");
        public static Mesh bossMesh = new Mesh("Boss.obj");

        // UI
        public static Mesh radarEnemyMesh = new Mesh("RadarEnemy.obj");
        public static Mesh targetMesh = new Mesh("Target.obj");
        public static Mesh circle = new Mesh("Circle.obj");
        public static Mesh directionMesh = new Mesh("DirectionIndicator.obj");
        



    }
    
}