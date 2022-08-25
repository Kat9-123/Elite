// I'm no 3d modeller, but I'm pretty happy with the results. 

namespace Elite
{

    public static class Models
    {
        // Debug
        public static readonly Mesh line = new Mesh(new Triangle[] {
            new Triangle(new Vector3(0,0,0),new Vector3(0,0,1),new Vector3(0,0.0001f,1)),
        });
        public static readonly Mesh cube = new Mesh("basic\\Cube.obj");



        public static readonly Mesh laserLeft = new Mesh(new Triangle[] {
            new Triangle(new Vector3(-1.2f,1f,0),new Vector3(-1.2f,1.03f,0),new Vector3(-0.1f,0.1f,10)),
        });
        public static readonly Mesh laserRight = new Mesh(new Triangle[] {
            new Triangle(new Vector3(1.2f,1f,0),new Vector3(1.2f,1.03f,0),new Vector3(0,0.1f,10)),
        });
        public static readonly Mesh quad = new Mesh(new Triangle[] {

            // SOUTH
            new Triangle(new Vector3(0,0,0),new Vector3(0,1,0),new Vector3(1,1,0)),
            new Triangle(new Vector3(0,0,0),new Vector3(1,1,0),new Vector3(1,0,0)),
        });


        // Isn't this just a quad? yes.
        public static readonly Mesh dustMesh = new Mesh(new Triangle[] {


            // NORTH
            new Triangle(new Vector3(1,0,1),new Vector3(1,1,1),new Vector3(0,1,1)),
            new Triangle(new Vector3(1,0,1),new Vector3(0,1,1),new Vector3(0,0,1)),
        });





        public static readonly Mesh enemyLaserMesh = Utils.GenerateRepeatingMesh(new Mesh(new Triangle[]{new Triangle(new Vector3(0,0,0),new Vector3(0,0.0001f,0), new Vector3(0,0,5))}),1000,5);

        public static readonly Mesh bigLaserMesh = Utils.GenerateRepeatingMesh(new Mesh("basic\\Cylinder.obj"),100,20);


        public static readonly Mesh warp = new Mesh("basic\\Cylinder.obj");

        // Planets
        public static readonly Mesh planetHigh = new Mesh("planets\\PlanetHigh.obj");
        public static readonly Mesh planetMedium = new Mesh("planets\\PlanetMedium.obj");
        public static readonly Mesh planetLow = new Mesh("planets\\PlanetLow.obj");


        // Enemies
        public static readonly Mesh stingrayMesh = new Mesh("enemies\\stingray\\Stingray-LOD0.obj");
        public static readonly LOD stringrayLOD = new LOD(new string[] 
            {"enemies\\stingray\\Stingray-LOD0.obj", "enemies\\stingray\\Stingray-LOD1.obj", "enemies\\stingray\\Stingray-LOD2.obj"},
            new float[] {0f,150f,230f}
        );

        public static readonly Mesh chargerMesh = new Mesh("enemies\\charger\\Charger-LOD0.obj");
        public static readonly LOD chargerLOD = new LOD(new string[] 
            {"enemies\\charger\\Charger-LOD0.obj", "enemies\\charger\\Charger-LOD1.obj", "enemies\\charger\\Charger-LOD2.obj"},
            new float[] {0f,150f,230f}
        );
        public static readonly Mesh bossMesh = new Mesh("enemies\\boss\\Boss-LOD1.obj");

        public static readonly LOD bossLOD = new LOD(new string[] 
            {"enemies\\boss\\Boss-LOD0.obj", "enemies\\boss\\Boss-LOD1.obj", "enemies\\boss\\Boss-LOD2.obj"},
            new float[] {0f,200f,500f}
        );

        // UI
        public static readonly Mesh radarEnemyMesh = new Mesh("ui\\RadarEnemy.obj");
        public static readonly Mesh targetMesh = new Mesh("ui\\Target.obj");
        public static readonly Mesh circle = new Mesh("basic\\Circle.obj");
        public static readonly Mesh directionMesh = new Mesh("ui\\DirectionIndicator.obj");

    }
}