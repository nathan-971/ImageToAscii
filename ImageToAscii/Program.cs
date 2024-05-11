using System.Drawing;
using System.Reflection.PortableExecutable;

namespace ImageToAscii
{
    internal class Program
    {
        private static readonly string folder = "asciiImages";
        private static readonly string imageName = "chill";
        private static readonly string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder);
        static void Main()
        {
            DirectorySetup();
            Bitmap image = new Bitmap(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imageName + ".png"), true);
            string textFilePath = Path.Combine(folderPath, imageName + ".txt");
            string imageSavePath = Path.Combine(folderPath, imageName + ".png");
            int imageTotalX = image.Height;
            int imageTotalY = image.Width;
            image = ScaleImage(image, imageTotalX, imageTotalY/2);
            bool success = false;

            if (!File.Exists(textFilePath))
            {
                File.CreateText(textFilePath);
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(textFilePath, false))
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        for (int x = 0; x < image.Width; x++)
                        {
                            Console.WriteLine($"\n[IMAGE DIMENSIONS: {image.Width}, {image.Height}] Current XY Position: ({x}, {y})");
                            if (image.GetPixel(x, y).GetBrightness() < 0.1)
                            {
                                sw.Write(" ");
                            }
                            else if (image.GetPixel(x, y).GetBrightness() < 0.2)
                            {
                                sw.Write(".");
                            }
                            else if (image.GetPixel(x, y).GetBrightness() < 0.3)
                            {
                                sw.Write(",");
                            }
                            else if (image.GetPixel(x, y).GetBrightness() < 0.4)
                            {
                                sw.Write(":");
                            }
                            else if (image.GetPixel(x, y).GetBrightness() < 0.5)
                            {
                                sw.Write(";");
                            }
                            else if (image.GetPixel(x, y).GetBrightness() < 0.6)
                            {
                                sw.Write("=");
                            }
                            else if (image.GetPixel(x, y).GetBrightness() < 0.7)
                            {
                                sw.Write("!");
                            }
                            else if (image.GetPixel(x, y).GetBrightness() < 0.8)
                            {
                                sw.Write("?");
                            }
                            else if (image.GetPixel(x, y).GetBrightness() < 0.9)
                            {
                                sw.Write("#");
                            }
                            else if (image.GetPixel(x, y).GetBrightness() < 1.0)
                            {
                                sw.Write("@");
                            }

                        }
                        sw.Write("\r\n");
                    }
                }
                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Occurred: {e.Message} \nFull Trace: {e.StackTrace}");
            }

            if(success) 
            {
                using(StreamReader sr = new StreamReader(textFilePath)) 
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
        }
        private static void DirectorySetup()
        {
            if(!Directory.Exists(folderPath)) 
            { 
                try
                {
                    Directory.CreateDirectory(folderPath);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Successfully Made {folderPath}");
                    Console.ResetColor();
                }
                catch (Exception e)
                { 
                    Console.WriteLine($"Error Occurred: {e.Message}");
                }
            }
        }
        private static Bitmap ScaleImage(Bitmap bmp, int maxWidth, int maxHeight) //Requires image to be saved
        {
            var ratioX = (double)maxWidth / bmp.Width;
            var ratioY = (double)maxHeight / bmp.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(bmp.Width * ratio);
            var newHeight = (int)(bmp.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(bmp, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }
    }
}