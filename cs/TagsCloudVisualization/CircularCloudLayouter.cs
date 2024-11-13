using System.Drawing;

namespace TagsCloudVisualization;

public class CircularCloudLayouter : ICloudLayouter
{
    private readonly IEnumerator<Point> pointEnumerator;
    private readonly List<Rectangle> rectangles = new List<Rectangle>();
    
    public CircularCloudLayouter(Point center, double radius, double angleOffset)
    {
        pointEnumerator = new SpiralPointsGenerator(radius, angleOffset)
            .GeneratePoints(center)
            .GetEnumerator();
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        Rectangle rectangle;

        while (true)
        {
            pointEnumerator.MoveNext();
            var rectanglePosition = pointEnumerator.Current;
            rectangle = CreateRectangle(rectanglePosition, rectangleSize);

            if (!rectangles.Any(rectangle.IntersectsWith)) break;
        }
        
        rectangles.Add(rectangle);

        return rectangle;
    }

    private static Rectangle CreateRectangle(Point center, Size rectangleSize) =>
        new Rectangle(
            center.X - rectangleSize.Width / 2,
            center.Y - rectangleSize.Height / 2,
            rectangleSize.Width,
            rectangleSize.Height
        );
}

