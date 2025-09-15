using System;
using System.IO;

namespace TMMCLineCounterApp
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Invalid input. Please Enter: TMMCLineCounterApp.exe <image-path>");
                return 1;
            }

            string imgPath = args[0];

            if (!File.Exists(imgPath))
            {
                Console.WriteLine($"Image File not found at: {imgPath}");
                return 1;
            }

            Console.WriteLine($"Image File exists at: {imgPath}");
            return 0;
        }
    }
}
