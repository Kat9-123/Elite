using System;
using System.Runtime.InteropServices;


namespace Elite
{
    static class Program
    {        
        private static void Main(string[] args)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine("This project only supports windows.");
                Console.ReadKey();
                return;
            }

            Engine.Setup();

            Engine.Run();
        }
    }
}
