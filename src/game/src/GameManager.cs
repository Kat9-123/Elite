namespace Elite
{   

    public class GameManager : GameObject
    {

        private const string SCORE_NAME = "score";
        public string highscore;
        public bool isSetup = false;

        private bool started = false;
        public Player player;


        // Sounds
        private string music = "";
        private string gameoverTrack = "";
        private Timer musicTimer = new Timer(69f);


        private bool isGameover;
        

        public UIManager uiManager;
        public EnemyManager enemyManager;

        public ExplosionManager explosionManager;

        public MouseController mouseController;

        public Warp warpController;



        public int enemyLayer;


        public void GenerateBodies()
        {
            int desiredPlanetCount = Utils.RandomInt(Settings.MIN_PLANET_COUNT,Settings.MAX_PLANET_COUNT);
            int planetCount = 0;
            while (planetCount < desiredPlanetCount)
            {

                Vector3 pos = Utils.RandomPositionExcludeCentre(3.2f,16f);
                if(pos.Normalise().Dot(new Vector3(0,0,1)) > 0.9f) continue;


                float s = Utils.RandomFloat(1.2f,2.3f);

                Engine.Instance(new Planet(pos,(short)Utils.RandomInt(1,14),s));
                planetCount++;
            }


            Engine.Instance(new Sun());

        }


        public void Setup()
        {
            started = true;
            if(!FileHandler.FileExists(SCORE_NAME))
            {
                FileHandler.Write(SCORE_NAME,"0");
            }

            highscore = FileHandler.Read(SCORE_NAME);

            player = (Player) Engine.Instance(new Player());



            enemyManager = (EnemyManager) Engine.Instance(new EnemyManager());

            player.enemyManager = enemyManager;

            GenerateBodies();
            

            for (int i = 0; i < Settings.DUST_COUNT; i++)
            {
                Engine.Instance(new Dust());
            }



            enemyLayer = Engine.GameObjectCount();  //    <--- ENEMIES

            explosionManager = (ExplosionManager) Engine.Instance(new ExplosionManager());


            warpController = (Warp) Engine.Instance(new Warp());

            player.SetupLasers();


            mouseController = (MouseController) Engine.Instance(new MouseController());

            uiManager = (UIManager) Engine.Instance(new UIManager(player));


            Engine.cameraPosition = new Vector3(0,0,0);


            Engine.cameraUp = new Vector3(0,-1,0);
            Engine.cameraForward = new Vector3(0,0,1);
            Engine.cameraRight = new Vector3(1,0,0);

            isSetup = true;

            musicTimer.Reset();


            if(music != "")
            {
                SoundManager.Stop(music);
            }

            PlayMusic();

            

            if(gameoverTrack != "")
            {
                SoundManager.Stop(gameoverTrack);
                gameoverTrack = "";
            }
            isGameover = false;
            
        }

        private void PlayMusic()
        {
            if(Settings.MUSIC) music = SoundManager.Play(Sounds.music);
            else music = SoundManager.Play(Sounds.bg);  
        }

        public override void Start()
        {
            
            visible = false;

            
            Engine.Instance(new Titlescreen());

        }

        public void Gameover()
        {
            gameoverTrack = SoundManager.Play(Sounds.death);
            if(music != "")
            {
                SoundManager.Stop(music);
            }
            

            isGameover = true;
        }


        public void SaveScore()
        {
            if(int.Parse(uiManager.score) >= int.Parse(highscore))
            {
                FileHandler.Write(SCORE_NAME,uiManager.score);
            }
        }
        public override void Update(float deltaTime)
        {
            if(!started) return;
            if(isGameover) return;

            if(musicTimer.Accumulate())
            {
                PlayMusic();
                musicTimer.Reset();
            }
        }

        public void Exit()
        {
            if(!isSetup) return;


            SaveScore();
        

        }


    }
}