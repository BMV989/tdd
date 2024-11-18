using SkiaSharp;

namespace TagsCloudVisualization;

public class CircularCloudLayouter : ICloudLayouter
{
    private const double OptimalRadius = 1d;
    private const double OptimalAngleOffset = 0.5;
    
    private readonly List<SKRect> rectangles = new();
    private readonly SpiralPointsGenerator pointsGenerator;
    private readonly SKPoint center;
    
    public SKPoint Center => center;
    public IEnumerable<SKRect> Rectangles => rectangles;
    
    public CircularCloudLayouter(SKPoint center)
    {
       this.center = center;
       pointsGenerator = new SpiralPointsGenerator(center, OptimalRadius, OptimalAngleOffset);
    }
    
    public SKRect PutNextRectangle(SKSize rectangleSize)
    {
        while (true)
        {
            var rectanglePosition = pointsGenerator.GetNextPoint();
            var rectangle = CreateRectangleWithCenter(rectanglePosition, rectangleSize);

            if (rectangles.Any(rectangle.IntersectsWith)) continue;
            
            rectangles.Add(rectangle);
            
            return rectangle;
        }
    }

    public static SKRect CreateRectangleWithCenter(SKPoint center, SKSize rectangleSize)
    {
        var left = center.X - rectangleSize.Width / 2;
        var top = center.Y - rectangleSize.Height / 2;
        
        return new SKRect(left, top, left + rectangleSize.Width, top + rectangleSize.Height);
        
    }
}

