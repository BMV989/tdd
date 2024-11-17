using System.Drawing;
using SkiaSharp;

namespace TagsCloudVisualization;

class Program
{
    private const int ImageWidth = 2560;
    private const int ImageHeight = 1440;

    private const int NumberOfRectangles = 50;
    private const int MinRectangleSize = 10;
    private const int MaxRectangleSize = 50;

    private const string ImageDirectory = "../../../imgs";
    public static void Main(string[] args)
    {
        var center = new Point(ImageWidth / 2, ImageHeight / 2);
        var cloudLayouter = new CircularCloudLayouter(center);
        var randomizer = new Random();
        var rectangles = Enumerable
            .Range(0, NumberOfRectangles)
            .Select(_ => 
                cloudLayouter.PutNextRectangle(randomizer.NextSize(MinRectangleSize, MaxRectangleSize)));
        
        var visualizer = new Visualizer(ImageWidth, ImageHeight);
        var bitmap = visualizer.VisualizeTagCloud(rectangles);
        Directory.CreateDirectory(ImageDirectory);

        using var file = File.OpenWrite(Path.Combine(ImageDirectory, $"{NumberOfRectangles}_TagCloud.png"));
        bitmap.Encode(SKEncodedImageFormat.Png, 80).SaveTo(file);
    }
}