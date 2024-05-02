using System.Drawing;

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
            int imageTotalX = image.Height;
            int imageTotalY = image.Width;

            if (!File.Exists(textFilePath))
            {
                File.CreateText(textFilePath);
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(textFilePath, false))
                {
                    for (int y = 0; y < imageTotalY; y++)
                    {
                        for (int x = 0; x < imageTotalX; x++)
                        {
                            if(image.GetPixel(x, y).GetBrightness() < 0.25)
                            {
                                sw.Write(" ");
                            }
                            else if (image.GetPixel(x, y).GetBrightness() < 0.5)
                            {
                                sw.Write("%");
                            }
                            else if (image.GetPixel(x, y).GetBrightness() < 0.75)
                            {
                                sw.Write("#");
                            }
                            else if (image.GetPixel(x, y).GetBrightness() < 1)
                            {
                                sw.Write("?");
                            }
                        }
                        sw.Write("\r\n");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Occurred: {e.Message} \nFull Trace: {e.StackTrace}");
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
    }
}