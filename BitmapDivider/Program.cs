using System.Drawing;

namespace BitmapDivider
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            // Specify the path to your large bitmap image
            string largeBitmapPath = "C:\\Users\\tawle\\Desktop\\Large_Image.png";

            // Load the large bitmap image
            Bitmap largeBitmap = new Bitmap(largeBitmapPath);

            // Define the size of the chunks
            int chunkWidth = 512;
            int chunkHeight = 512;

            // Calculate the number of chunks needed
            int numChunksX = (int)Math.Ceiling((double)largeBitmap.Width / chunkWidth);
            int numChunksY = (int)Math.Ceiling((double)largeBitmap.Height / chunkHeight);

            int topLeftX = -16;
            int topLeftZ = -16;

            Directory.CreateDirectory("Output");

            // Loop through each chunk and save it
            for (int chunkY = 0; chunkY < numChunksY; chunkY++)
            {
                for (int chunkX = 0; chunkX < numChunksX; chunkX++)
                {
                    // Calculate the coordinates and size of the current chunk
                    int startX = chunkX * chunkWidth;
                    int startY = chunkY * chunkHeight;
                    int width = Math.Min(chunkWidth, largeBitmap.Width - startX);
                    int height = Math.Min(chunkHeight, largeBitmap.Height - startY);

                    // Create a new bitmap for the current chunk
                    Bitmap chunkBitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                    // Copy the pixels from the large bitmap to the chunk bitmap
                    using (Graphics g = Graphics.FromImage(chunkBitmap))
                    {
                        g.DrawImage(largeBitmap, new Rectangle(0, 0, width, height), new Rectangle(startX, startY, width, height), GraphicsUnit.Pixel);
                    }

                    // Save the chunk bitmap
                    string chunkFileName = $"Output\\r.{topLeftX + chunkX}.{topLeftZ + chunkY}.bmp";
                    chunkBitmap.Save(chunkFileName, System.Drawing.Imaging.ImageFormat.Bmp);
                }
            }

            Console.WriteLine("Chunks saved successfully.");
        }
    }
}