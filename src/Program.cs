// https://github.com/Kat9-123/Elite
namespace Elite
{
    static class Program
    {        
        private static void Main(string[] args)
        {
            if(!Engine.Setup()) return;

            Engine.Run();
        }    
    }
}
