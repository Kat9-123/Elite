

//https://pastebin.com/TtrCdeER

namespace Elite
{
    public struct Sound
    {   
        public string fileName;
        public string trackName;


        public Sound(string path)
        {
            fileName = FileHandler.originPath + "sounds\\" + path;
            trackName = path;
        }

    }
}
