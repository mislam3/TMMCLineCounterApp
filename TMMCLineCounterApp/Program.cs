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

        static int CountLines(string imgPath)
        {
            using (Bitmap bmp = new Bitmap(imgPath))
            {
                int height = bmp.Height;
                int width = bmp.Width;

                const double minBlackRatio = 0.02;
                const int threshold = 32;
                bool[] adjColumn = new bool[width];              

                for (int x = 0; x < width; x++)
                {
                    int topHalfBlack = 0; 
                    int bottomHalfBlack = 0;
                    int blackTotal = 0;

                    for (int y = 0; y < height; y++)
                    {
                        Color clr = bmp.GetPixel(x, y);
                        if (DetectBlack(clr, threshold))
                        {
                            blackTotal++;

                            if (y < height / 2)
                            {
                                topHalfBlack++;
                            }
                            else 
                            {
                                bottomHalfBlack++;
                            }
                        }
                    }

                    double blackRatio = (double)blackTotal / height;
                    adjColumn[x] = (blackRatio >= minBlackRatio && topHalfBlack > 0 && bottomHalfBlack > 0);
                }

                int verticalLines = 0;
                for (int x = 0; x < width; x++)
                {
                    if ((x == 0 || !adjColumn[x - 1]) && adjColumn[x])
                    {
                        verticalLines++;
                    }
                }
                return verticalLines;
            }
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
