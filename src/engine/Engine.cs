using System;
using System.Collections.Generic;

using System.Threading;
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
        public static Vector3 cameraRight = new Vector3(1,0,0);

    
        public static Main main;

        public volatile static List<GameObject> gameObjects = new List<GameObject>(256);


        private static List<GameObject> queuedObjectsForDestruction = new List<GameObject>();

        

        public static float deltaTime = 0f;

        // Used in deltaTime calculations
        private static double previousTime = 0;


        
        public static void MoveLayer(GameObject item, int newIndex)
        {

            int oldIndex = gameObjects.IndexOf(item);

            gameObjects.RemoveAt(oldIndex);

            if (newIndex > oldIndex) newIndex--;
            // the actual index could have shifted due to the removal

            gameObjects.Insert(newIndex, item);
        
            

        }

       
        // Instance the given object.
        public static GameObject Instance(GameObject obj)
        {
            gameObjects.Add(obj);
            obj.Start();
            return obj;
        }

        public static void QueueDestruction(GameObject obj)
        {
            queuedObjectsForDestruction.Add(obj);
            obj.isDestroyed = true;
        }

        public static void Setup()
        {
            ConsoleInterface.SetCurrentFont("Square", 1);
            Console.SetWindowSize(Settings.SCREEN_SIZE_X,Settings.SCREEN_SIZE_Y);
            Console.SetBufferSize(Settings.SCREEN_SIZE_X,Settings.SCREEN_SIZE_Y);    
            ConsoleInterface.SetCurrentFont("Square", 3);  







  
  
            Renderer.Initialise();
            FileHandler.Setup();


            Console.CursorVisible = false;
            Console.Title = "ELITE";


            /*
            Thread gameThread = new Thread(Run);



            gameThread.Start();

            Renderer.RenderLoop();
            //Run();
            */
        }

        public static void Run()
        {
            main = (Main) Instance(new Main());       

            // Instance the gamemanager



            while (true)
            {  
                if(InputManager.IsKeyPressed(InputMap.PAUSE))
                {
                    Environment.Exit(1);
                }

                for (int i = 0; i < queuedObjectsForDestruction.Count; i++)
                {
                    gameObjects.Remove(queuedObjectsForDestruction[i]);
                    queuedObjectsForDestruction.Remove(queuedObjectsForDestruction[i]);
                }

                deltaTime = (float) Utils.CalculateDeltaTime(ref previousTime);

                
                
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

        






    }
}
