using SkiaSharp;

namespace TagsCloudVisualization;

public class Saver(string imageDirectory)
{
    public void SaveAsPng(SKBitmap bitmap, string filename)
    {
        Directory.CreateDirectory(imageDirectory);
        using var file = File.OpenWrite(Path.Combine(imageDirectory, filename));
        bitmap.Encode(SKEncodedImageFormat.Png, 80).SaveTo(file);
    }
}