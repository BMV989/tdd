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
        var rectangles = new List<Rectangle>(numberOfRectangles);
        var circularCloudLayouter = new CircularCloudLayouter(new Point(0, 0), 2, 1);

        for (var i = 0; i < numberOfRectangles; i++)
        {
            var size = new Size(randomizer.Next(10, 27), randomizer.Next(10, 27));
            
            rectangles.Add(circularCloudLayouter.PutNextRectangle(size));
        }

        rectangles.Any(fr => rectangles.Any(sr => fr != sr && fr.IntersectsWith(sr)))
            .Should().BeFalse();
    }
}