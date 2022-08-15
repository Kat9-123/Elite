// Line renderer for debug purposes
namespace Elite
{
    public class Profiler : GameObject
    {



        private Timer intervalTimer = new Timer(1f);

        private const int PROFILE_LINE_COUNT = 8;
        private ProfileLine[] profileLines = new ProfileLine[PROFILE_LINE_COUNT];

        private const float Z = 5;


        private int currentLine = 0;

        private const float DIST_BETWEEN_LINES = 0.5f;
 
        public override void Start()
        {
            visible = false;
            Engine.Instance(new ProfileLine(new Vector3(0,0,0), new Vector3(DIST_BETWEEN_LINES*PROFILE_LINE_COUNT,0,0))).colour = 8;
            for (int i = 0; i < PROFILE_LINE_COUNT; i++)
            {
                profileLines[i] = (ProfileLine) Engine.Instance(new ProfileLine(new Vector3(0+DIST_BETWEEN_LINES*i,0,Z), new Vector3(DIST_BETWEEN_LINES+DIST_BETWEEN_LINES*i,0f,Z)));
            }
        }


        public override void Update(float deltaTime)
        {
            UI.WriteText("60",74,51);

            if(intervalTimer.Accumulate())
            {
                for (int i = 0; i < profileLines.Length; i++)
                {
                    profileLines[i].start.x -= DIST_BETWEEN_LINES;
                    if(profileLines[i].start.x < 0f) 
                    {
                        profileLines[i].start.x = DIST_BETWEEN_LINES*(PROFILE_LINE_COUNT-1);
                        profileLines[i].end.x = DIST_BETWEEN_LINES*PROFILE_LINE_COUNT;
                    }
                    else
                    {
                        profileLines[i].end.x -= DIST_BETWEEN_LINES;
                    }
                    profileLines[i].SetLine();
                    profileLines[i].colour = 10;
                }
                intervalTimer.Reset();
                float fps = 1/deltaTime;
                ProfileLine line = profileLines[currentLine];

                line.end.y = -fps/120f * 4;
                line.colour = 5;
                
                //  Engine.Instance(new ProfileLine(fps));

                if(currentLine != 0) line.start.y = profileLines[currentLine-1].end.y;
                if(currentLine == 0 ) line.start.y = profileLines[PROFILE_LINE_COUNT-1].end.y;
                line.SetLine();
                currentLine++;
                if(currentLine > PROFILE_LINE_COUNT-1) currentLine = 0;
            
                
            }
        }

 


    }
}
