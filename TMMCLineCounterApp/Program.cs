using System;

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
            Console.WriteLine($"Argument received: {imgPath}");
            return 0;
        }
    }
}
