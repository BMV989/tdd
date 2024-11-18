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
        
        var visualizer = new TagCloudVisualizer(ImageWidth, ImageHeight);
        var bitmap = visualizer.Visualize(rectangles);

        var saver = new Saver(ImageDirectory);
        saver.SaveAsPng(bitmap, $"{NumberOfRectangles}_TagCloud.png");
    }
}