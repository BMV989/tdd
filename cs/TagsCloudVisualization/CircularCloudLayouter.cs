using System.Drawing;

namespace TagsCloudVisualization;

public class CircularCloudLayouter : ICloudLayouter
{
    private readonly IPointsGenerator pointsGenerator;
    private readonly List<Rectangle> rectangles = new ();
    
    public List<Rectangle> Rectangles => rectangles;
    
    public CircularCloudLayouter(Point center)
    { 
        pointsGenerator = new SpiralPointsGenerator(center, 1d, 0.5d);
    }
    
    public CircularCloudLayouter(Point center, double radius, double angleOffset)
    { 
        pointsGenerator = new SpiralPointsGenerator(center, radius, angleOffset); 
    }
    
    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        Rectangle rectangle;

        while (true)
        {
            var rectanglePosition = pointsGenerator.GetNextPoint();
            rectangle = CreateRectangle(rectanglePosition, rectangleSize);

            if (!rectangles.Any(rectangle.IntersectsWith)) break;
        }
        
        rectangles.Add(rectangle);

        return rectangle;
    }

    private static Rectangle CreateRectangle(Point center, Size rectangleSize) =>
        new (
            center.X - rectangleSize.Width / 2,
            center.Y - rectangleSize.Height / 2,
            rectangleSize.Width,
            rectangleSize.Height
        );
}

