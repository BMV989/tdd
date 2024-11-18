using SkiaSharp;

namespace TagsCloudVisualization;

public static class TagCloudSaver
{
    private const int DefaultQuality = 80;
    
    public static void SaveAsPng(SKBitmap bitmap, string filePath)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
        using var file = File.OpenWrite($"{filePath}.png");
        bitmap.Encode(SKEncodedImageFormat.Png, DefaultQuality).SaveTo(file);
    }
}