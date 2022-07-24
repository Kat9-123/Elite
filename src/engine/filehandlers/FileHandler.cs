// Simple filehandler

using System;
using System.IO;
namespace Elite
{   
    public static class FileHandler
    {

        public static string originPath;

        public static void Setup()
        {
            originPath = Directory.GetCurrentDirectory() + "\\assets\\";
        }


        public static string Read(string path)
        {
            return File.ReadAllText(FileHandler.originPath + path);
        }
        public static void Write(string path, string data)
        {
            File.WriteAllText(FileHandler.originPath + path, data);
        }


    }

}