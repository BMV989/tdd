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
        var center = new SKPoint((float)ImageWidth / 2, (float)ImageHeight / 2);
        var cloudLayouter = new CircularCloudLayouter(center);
        var rectangles = Enumerable
            .Range(0, NumberOfRectangles)
            .Select(_ => 
                cloudLayouter.PutNextRectangle(Random.Shared.NextSkSize(MinRectangleSize, MaxRectangleSize)))
            .ToList();
        
        var visualizer = new TagCloudVisualizer(ImageWidth, ImageHeight);
        var bitmap = visualizer.Visualize(rectangles);

        TagCloudSaver.SaveAsPng(bitmap, Path.Combine(ImageDirectory,$"{NumberOfRectangles}_TagCloud"));
    }
}