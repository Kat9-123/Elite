// Gets played by the SoundManager
namespace Elite
{
    public struct Sound
    {   
        public string fileName;

        public Sound(string path)
        {
            fileName = FileHandler.originPath + "sounds\\" + path;   
        }

    }
}
