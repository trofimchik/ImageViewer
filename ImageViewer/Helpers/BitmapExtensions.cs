using Avalonia.Media.Imaging;
using System.IO;

namespace ImageViewer.Helpers
{
    public static class BitmapExtensions
    {
        public static byte[] ToByteArray(this Bitmap bmp)
        {
            MemoryStream memoryStream = new();
            bmp.Save(memoryStream, 100);

            memoryStream.Position = 0;
            byte[] byteBuffer = memoryStream.ToArray();

            return byteBuffer;
        }

        public static Bitmap ToBitmap(this byte[] bytes)
        {
            MemoryStream memoryStream = new(bytes);
            var bitmap = new Bitmap(memoryStream);
            return bitmap;
        }
    }
}
