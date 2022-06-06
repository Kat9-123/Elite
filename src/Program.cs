using System;

namespace Elite
{
    static class Program
    {



        private static void Main(string[] args)
        {


            FileHandler.Setup();
            FontHandler.LoadFont();
           // Console.ReadLine();
            
            for (int y = 0; y < 50; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    Console.BackgroundColor = (System.ConsoleColor) ((x+y)%16);
                    Console.Write(' ');            
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine();

            //Console.ReadLine();
  

            Engine.Setup();

            Engine.Run();

            Console.ReadKey();
        }
    }
}
