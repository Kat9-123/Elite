// Simple FileHandler
using System.IO;
using System;
namespace Elite
{   
    public static class FileHandler
    {

        public static string originPath;

        public static void Setup()
        {
            
            originPath = Directory.GetCurrentDirectory() + "\\assets\\";
            if(!Directory.Exists(originPath))
            {
                Console.WriteLine("Assets folder not found.");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        public static bool FileExists(string path)
        {
            return File.Exists(originPath + path);
        }


        public static string Read(string path)
        {
            return File.ReadAllText(originPath + path);
        }
        public static void Write(string path, string data)
        {
            File.WriteAllText(originPath + path, data);
        }


    }

}