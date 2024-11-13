using System.Drawing;
using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

[TestFixture]
[TestOf(typeof(CircularCloudLayouter))]
public class CircularCloudLayouterTest
{
    [Test]
    public void PutNextRectangle_ShouldReturnRectangles_WithoutIntersections()
    {
        var randomizer = new Random();
        var numberOfRectangles = randomizer.Next(100, 300);
        var rectangles = new Queue<Rectangle>(numberOfRectangles);
        var spiralCloudLayouter = new CircularCloudLayouter(new Point(0, 0), 2, 1);

        for (var i = 0; i < numberOfRectangles; i++)
        {
            var size = new Size(randomizer.Next(10, 27), randomizer.Next(10, 27));
            
            rectangles.Enqueue(spiralCloudLayouter.PutNextRectangle(size));
        }

        IsIntersectionBetweenRectangles(rectangles).Should().BeFalse();
    }

    private static bool IsIntersectionBetweenRectangles(Queue<Rectangle> rectangles)
    {
        while (rectangles.Count > 0)
        {
            var rectangle = rectangles.Dequeue();
            
            if (rectangles.Any(rectangle.IntersectsWith)) return true;
        }
        
        return false;
    }
}