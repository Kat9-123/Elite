/*
This engine lacks some very important features, true frustum culling,
occlusion, object ordering based on distance, actual lighting, etc. I didn't implement these features
because they really aren't necessary for this project.

For more information, visit: https://github.com/Kat9-123/Elite#engine

=== High level engine overview

                     (UIManager) --------------------> [  UI  ]
                          |                                |
                          |                               \./
         AssetData -> GAMEOBJECTS    [RENDERER] --> [RASTERISER] --> [WINDOW]
                          /.\            /.\
                        {Update}          |
                           |    {Sends all GameObjects}
[ENTRY] -{SETUP}--+--> [ENGINE] ---+------+
                  |                |
                  +---{GAMELOOP}---+

[] = Core engine component
() = GameObject that functions as a manager.
{} = What happens


*/
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace Elite
{
    static class Engine
    {


        // The camera is an essential part of the engine. There is only one camera and it's not handled
        // like a gameobject.
        public static Vector3 cameraPosition = new Vector3(0,0,0);

        public static Vector3 cameraUp = new Vector3(0,-1,0);
        public static Vector3 cameraForward = new Vector3(0,0,1);

        // Tracking cameraright is not necessary, but it gives me peace of mind
        public static Vector3 cameraRight = new Vector3(1,0,0);


        // GameManager. First object to be instanced. Treated as somewhat of a god-object
        public static GameManager gameManager;

        public static List<GameObject> gameObjects = new List<GameObject>(128);


        private static List<GameObject> queuedObjectsForDestruction = new List<GameObject>();

        // We all know what deltaTime is right?
        public static float deltaTime = 0f;

        // Used in deltaTime calculations
        private static double previousTime = 0;


        private const string TITLE = "Elite not very Dangerous | By Kat9_123";



        public static int GameObjectCount()
        {
            return gameObjects.Count;
        }

        // Change the index of a gameObject in the gameObjects list.
        // Objects with a higher index render in front of Objects with a lower index.
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

       
        // When a mommy object and a daddy object love eachother very much
        // they can instance a gameobject (well you only need one object
        // to instance another but whatever)
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


        public static bool Setup()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine("This project only supports Windows. Sorry!");
                Console.ReadKey();
                return false;
            }
 

            
            FileHandler.Setup();

            Console.Title = TITLE;
            Window.SetConsoleIcon("assets\\icon.ico");
            Console.CursorVisible = false;
            
            // Initialise file stuff
            FontHandler.LoadFont();
            SettingHandler.Initialise();

            // Check if the user has the correct font installed
            if(!FileHandler.CheckFont()) return false;
            
            if(Settings.SHOW_COLOURS_ON_STARTUP) Utils.ShowColours();


            // Windows is a bit strange so we first need to set the
            // font to the minimum size before changing the windowsize.
            Window.SetCurrentFont(Settings.FONT, 1);

            Console.SetWindowSize(Settings.SCREEN_SIZE_X,Settings.SCREEN_SIZE_Y);
            Console.SetBufferSize(Settings.SCREEN_SIZE_X,Settings.SCREEN_SIZE_Y); 
       
            Window.SetCurrentFont(Settings.FONT, Settings.FONT_SCALE);


    

            // Graphics
            Renderer.Initialise();
            Window.Initialise();

            SoundManager.Setup();


            return true;


        }

        public static void Restart()
        {    
            // This doesn't work but ah well....
            cameraPosition = new Vector3(0,0,0);
            cameraForward = new Vector3(0,0,1);
            cameraUp = new Vector3(0,-1,0);
            cameraRight = new Vector3(1,0,0);

            gameManager.SaveScore();
    
            // Destroy all objects except for the gamemanager.
            for (int i = 1; i < gameObjects.Count; i++)
            {
                QueueDestruction(gameObjects[i]);      
            }
            DestroyQueuedObjects();

            gameManager.Setup(); 
        }

        private static void Exit()
        {
            gameManager.Exit();
            Environment.Exit(0);
        }

        public static void Run()
        {

            // Instance the gamemanager
            gameManager = (GameManager) Instance(new GameManager());       

            // Main gameloop
            while (true)
            {  
                DestroyQueuedObjects();

                // Stop checking for input if the window is not in focus                
                InputManager.TestFocus(); // Might cause performance problems?

                SoundManager.SoundUpdate();

                
                // Exit if ESC was pressed
                if(InputManager.IsKeyPressed(InputMap.EXIT)) Exit();


                if(gameManager.isSetup && InputManager.IsKeyPressed(InputMap.RESTART)) Restart();


                deltaTime = ((float) Utils.CalculateDeltaTime(ref previousTime));
                
                if(Settings.SHOW_FPS)
                {
                    string fps = "";
                    if (deltaTime != 0f) fps = (1f/deltaTime).ToString();

                    if(fps.Length > 5) fps = fps.Substring(0,5);
                    UI.WriteFPS(fps);

                }

  
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
