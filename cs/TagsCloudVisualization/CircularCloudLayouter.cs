using System.Drawing;

namespace TagsCloudVisualization;

public class CircularCloudLayouter : ICloudLayouter
{
    private const double OptimalRadius = 1d;
    private const double OptimalAngleOffset = 0.5;
    
    private readonly List<Rectangle> rectangles = new();
    private readonly SpiralPointsGenerator pointsGenerator;
    private readonly Point center;
    
    public Point Center => center;
    public IEnumerable<Rectangle> Rectangles => rectangles;
    
    public CircularCloudLayouter(Point center)
    {
       this.center = center;
       pointsGenerator = new SpiralPointsGenerator(center, OptimalRadius, OptimalAngleOffset);
    }
    
    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        while (true)
        {
            var rectanglePosition = pointsGenerator.GetNextPoint();
            var rectangle = CreateRectangle(rectanglePosition, rectangleSize);

            if (rectangles.Any(rectangle.IntersectsWith)) continue;
            
            rectangles.Add(rectangle);
            
            return rectangle;
        }
    }

    public static Rectangle CreateRectangle(Point center, Size rectangleSize) =>
        new(
            center.X - rectangleSize.Width / 2,
            center.Y - rectangleSize.Height / 2,
            rectangleSize.Width,
            rectangleSize.Height
        );
}

