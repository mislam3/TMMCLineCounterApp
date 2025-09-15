using System;
using System.IO;
using System.Drawing;

namespace TMMCLineCounterApp
{
    class Program
    {
        static bool DetectBlack(Color clr, int threshold = 32)
        {
            return clr.R < threshold && clr.G < threshold && clr.B < threshold;
        }


        static int Main(string[] args)
        {
            try
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

                using (Bitmap bmp = new Bitmap(imgPath))
                {
                    Console.WriteLine($"Loaded image fmt: {bmp.Width} x {bmp.Height}");
                }
                return 0;
            }

            catch (Exception e) 
            {
                Console.WriteLine($"Exception: {e.Message}");
                return 2;
            }

        }
    }
}
