using System;
using System.Threading;
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
