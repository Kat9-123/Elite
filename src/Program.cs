using System;

namespace Elite
{
    static class Program
    {

        private static void Main(string[] args)
        {

            // Debug colours
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < 16; i++)
            {
                Console.BackgroundColor = 0;
                
                Console.Write((i.ToString() + ": ").PadRight(4));
                Console.BackgroundColor = (System.ConsoleColor) ((i) %16);
                Console.WriteLine(" ");
            }
            Console.BackgroundColor = ConsoleColor.Black;



            Console.ReadLine();
  

            Engine.Setup();

            Engine.Run();


            Console.ReadKey();
        }
    }
}
