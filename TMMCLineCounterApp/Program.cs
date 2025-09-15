using System;
using System.IO;
using System.Drawing;

namespace TMMCLineCounterApp
{
    class Program
    {
        // Black pixel detection method
        // Pixel is black if all channels fall below the threshold
        static bool DetectBlack(Color clr, int threshold = 32)
        {
            return clr.R < threshold && clr.G < threshold && clr.B < threshold;
        }

        // Scanning image columns and detecting adjacent vertical black lines
        static int CountBlackLines(string imgPath)
        {
            // Loading image from disk into bitmap
            using (Bitmap bmp = new Bitmap(imgPath))
            {
                int height = bmp.Height;
                int width = bmp.Width;

                // Determining if column is part of a line
                // Column : atleast 2% black pixels ; Black threshold - pixel is black if all channels < 32 ; ignores outlier dots
                const double minBlackRatio = 0.02;
                const int threshold = 32;
                bool[] adjColumn = new bool[width];

                // looping through pixels x,y 
                // Counting black pixels separately in top and bottom halves; tracking blackTotal for ratio check
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

                // Counting contiguous lines
                // Contiguous runs of Adjacent columns -> one line
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
            // Argument validation and help message
            try
            {
                string imgPath = args[0];
                if (!File.Exists(imgPath))
                {
                    Console.WriteLine("\n" + $"Image File not found at: {imgPath}" + "\n");
                    return 1; // Exit code 1 : File not found
                }

                if (args.Length != 1)
                {
                    Console.WriteLine("\n" + "Invalid input. Please Enter: TMMCLineCounterApp.exe <image-path>" + "\n");
                    return 1; // Exit code 1 : Invalid arguments
                }

                // Line counting
                int verticalLinesCount = CountBlackLines(imgPath);

                // Printing result
                Console.WriteLine(verticalLinesCount);

                return 0; // Exit code 0 : Success
            }

            // Exceptions handling
            catch (Exception e)
            {
                Console.WriteLine("\n" + $"Exception: {e.Message}");
                return 2; // Exit code 2 : Exception
            }

        }
    }
}
