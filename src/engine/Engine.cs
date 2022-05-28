using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Elite
{
    static class Engine
    {


        // The camera is an essential part of the engine. There is only one camera and it's not handled
        // like a gameobject. 
        public static Vector3 cameraPosition = new Vector3(0,0,0);


        public static Vector3 cameraUp = new Vector3(0,1,0);
        public static Vector3 cameraForward = new Vector3(0,0,1);

    
        public static Main main;

        private static List<GameObject> gameObjects = new List<GameObject>(256);



        // Used in deltaTime calculations
        private static double previousTime = 0;

       
        // Instance the given object.
        public static GameObject Instance(GameObject obj)
        {
            gameObjects.Add(obj);
            obj.Start();
            return obj;
        }

        public static void Destroy(GameObject obj)
        {
            gameObjects.Remove(obj);
        }

        public static void Setup()
        {
            ConsoleInterface.SetCurrentFont("Square", 1);
            Console.SetWindowSize(Settings.SCREEN_SIZE_X,Settings.SCREEN_SIZE_Y);
            Console.SetBufferSize(Settings.SCREEN_SIZE_X,Settings.SCREEN_SIZE_Y);    
            ConsoleInterface.SetCurrentFont("Square", 3);    
  
            Renderer.Initialise();

            FileHandler.Setup();



        }

        public static void Run()
        {
            
            

            // Instance the gamemanager
            main = (Main) Instance(new Main());

            Console.Title = "ELITE";


            while (true)
            {  

                float deltaTime = (float) CalculateDeltaTime();
                
                
                string fps = "";
                if (deltaTime != 0f) fps = (1f/deltaTime).ToString();

                if(fps.Length > 5) fps = fps.Substring(0,5);
                Renderer.WriteLine("FPS: " + fps + "\n");
                //Console.Title = fps;
                
    

                UI.ResetUI();                
                // Update all gameobjects
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    gameObjects[i].Update(deltaTime);
                }

                Renderer.Render(gameObjects);

            }
            
        }



        private static double CalculateDeltaTime()
        {
            
            double timestamp = Stopwatch.GetTimestamp();
            double now = timestamp / Stopwatch.Frequency;
            if (previousTime == 0) previousTime = now;
            double deltaTime = (now - previousTime);
            previousTime = now;
            return deltaTime;
        
        }
    }
}
