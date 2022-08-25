// Simple FileHandler
using System.IO;
using System;
using System.Drawing;
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

        public static bool CheckFont()
        {
            //https://stackoverflow.com/questions/113989/test-if-a-font-is-installed
            string fontName = Settings.FONT;
            float fontSize = 12;

            using (Font fontTester = new Font( 
                fontName, 
                fontSize, 
                FontStyle.Regular, 
                GraphicsUnit.Pixel)) 
            {
                if (fontTester.Name != fontName)
                {
                    Console.WriteLine("Font not found");
                    if(fontName == "Square")
                    {
                        Console.WriteLine("Download Square from strlen.com/square/ and install it.");
                    }
                    Console.ReadKey();
                    return false;
                }
            }
            return true;
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