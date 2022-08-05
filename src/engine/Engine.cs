/*
This engine lacks some very important features, like audio, true frustum culling,
occlusion, object ordering based on distance, etc. I didn't implement these features
because they really aren't necessary for this project.



=== High level engine overview

                     (UIManager) --------------------> [  UI  ]
                          |                                |
                          |                               \./
                     [GAMEOBJECTS] --> [RENDERER] --> [RASTERISER] --> [CONSOLEINTERFACE]
                       /.\    |
                     {Update} |
                        |    \./
[ENTRY] -{SETUP}--+--> [ENGINE] ---+
                  |                |
                  +---{GAMELOOP}---+

[] = Core engine part
() = GameObject that functions as a manager.
{} = What happens


*/
using System;
using System.Collections.Generic;


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


        // GameManager. First object to be instanced.
        public static GameManager gameManager;

        public static List<GameObject> gameObjects = new List<GameObject>(128);


        private static List<GameObject> queuedObjectsForDestruction = new List<GameObject>();

        
        private const string TITLE = "Elite not very Dangerous | By Kat9_123";

        public static float deltaTime = 0f;

        // Used in deltaTime calculations
        private static double previousTime = 0;




        public static int GameObjectCount()
        {
            return gameObjects.Count;
        }

        // Change the index of a gameObject in the gameObjects list.
        // Objects with a higher index render in front of
        // Objects with a lower index.
        public static void ChanageIndex(GameObject gameObject, int newIndex)
        {

            int oldIndex = gameObjects.IndexOf(gameObject);

            gameObjects.RemoveAt(oldIndex);

            if (newIndex > oldIndex) newIndex--;

            gameObjects.Insert(newIndex, gameObject);
        
            

        }
        private static void DestroyQueuedObjects()
        {
            // Here the objects get deleted back to front because.. ummm...
            // otherwise it doesn't work...
            for (int i = queuedObjectsForDestruction.Count-1; i >= 0; i--)
            {
                gameObjects.Remove(queuedObjectsForDestruction[i]);
                queuedObjectsForDestruction.Remove(queuedObjectsForDestruction[i]);
            }

        }

       

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
            
            // Initialise file stuff
            FileHandler.Setup();
            FontHandler.LoadFont();
            SettingHandler.Initialise();
            
            if(Settings.SHOW_COLOURS_ON_STARTUP) Utils.ShowColours();


            // Windows is a bit strange so we first need to set the
            // font to the minimum size before changing the windowsize.
            ConsoleInterface.SetCurrentFont(Settings.FONT, 1);

            Console.SetWindowSize(Settings.SCREEN_SIZE_X,Settings.SCREEN_SIZE_Y);
            Console.SetBufferSize(Settings.SCREEN_SIZE_X,Settings.SCREEN_SIZE_Y); 
       
            ConsoleInterface.SetCurrentFont(Settings.FONT, Settings.FONT_SCALE);




            // Graphics
            Renderer.Initialise();

            Console.CursorVisible = false;
            Console.Title = TITLE;


        }

        public static void Restart()
        {    

            // Destroy all objects except for the gamemanager.
            for (int i = 1; i < gameObjects.Count; i++)
            {
                QueueDestruction(gameObjects[i]);      
            }
            DestroyQueuedObjects();

            gameManager.Setup();

 
        }

        public static void Run()
        {

            // Instance the gamemanager
            gameManager = (GameManager) Instance(new GameManager());       


            while (true)
            {  

                // Exit if ESC was pressed
                if(InputManager.IsKeyPressed(InputMap.PAUSE)) Environment.Exit(1);

                if(InputManager.IsKeyPressed(InputMap.RESTART)) Restart();


                DestroyQueuedObjects();

                deltaTime = ((float) Utils.CalculateDeltaTime(ref previousTime));

                
                
                string fps = "";
                if (deltaTime != 0f) fps = (1f/deltaTime).ToString();

                if(fps.Length > 5) fps = fps.Substring(0,5);
                UI.WriteLine("FPS: " + fps + "\n");
                //Console.Title = title + " | " + fps;
                
    

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
