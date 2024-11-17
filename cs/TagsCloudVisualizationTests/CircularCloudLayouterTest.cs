using System.Drawing;
using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

[TestFixture]
[TestOf(typeof(CircularCloudLayouter))]
public class CircularCloudLayouterTest
{
    private readonly Random randomizer = new();

    [Test]
    public void PutNextRectangle_ShouldReturnRectangle()
    {
        var circularCloudLayouter = CreateCircularCloudLayouterWithRandomParams();
        var rectSize = randomizer.RandomSize(1, int.MaxValue);
        
        var rect = circularCloudLayouter.PutNextRectangle(rectSize);

        rect.Should().BeOfType<Rectangle>();
    }

    [Test]
    public void PutNextRectangle_ShouldReturnRectangleAtCenter_WhenFirstInvoked()
    {
        var circularCloudLayouter = CreateCircularCloudLayouterWithRandomParams();
        var rectSize = randomizer.RandomSize(1, int.MaxValue);
        
        var actualRect = circularCloudLayouter.PutNextRectangle(rectSize);
        var expectedRect = CircularCloudLayouter.CreateRectangle(circularCloudLayouter.Center, rectSize);
        
        actualRect.Should().BeEquivalentTo(expectedRect);
    }

    [Test]
    public void PutNextRectangle_ShouldReturnRectangle_WithCorrectSize()
    {
        var circularCloudLayouter = CreateCircularCloudLayouterWithRandomParams();
        var recSize = randomizer.RandomSize(1, int.MaxValue);
        
        var actualRect = circularCloudLayouter.PutNextRectangle(recSize);
        
        actualRect.Size.Should().Be(recSize);
    }
    
    [Test]
    public void PutNextRectangle_ShouldReturnRectangles_WithoutIntersections()
    {
        var circularCloudLayouter = CreateCircularCloudLayouterWithRandomParams();
        var numberOfRectangles = randomizer.Next(100, 300);
        
        var rectangles = Enumerable
            .Range(0, numberOfRectangles)
            .Select(_ => circularCloudLayouter.PutNextRectangle(randomizer.RandomSize(10, 27)))
            .ToList();
        
        rectangles.Any(fr => rectangles.Any(sr => fr != sr && fr.IntersectsWith(sr)))
            .Should().BeFalse();
    }

    [Test]
    public void GeneratedLayout_ShouldHaveHighTightnessAndShapeOfCircularCloud_WithOptimalParams()
    {
        const double eps = 0.35;
        var circularCloudLayouter = CreateCircularCloudLayouterWithOptimalParams();
        var rectangles = PutRandomRectanglesInLayouter(randomizer.Next(500, 1000), circularCloudLayouter);
        var layoutSize = GetLayoutSize(rectangles);
        
        var diameterOfCircle = Math.Max(layoutSize.Width, layoutSize.Height);
        var areaOfCircle = Math.PI * Math.Pow(diameterOfCircle, 2) / 4;
        var areaOfRectangles = (double)rectangles
            .Select(r => r.Height * r.Width)
            .Sum();
        var areaRatio = areaOfCircle / areaOfRectangles;

        areaRatio.Should().BeApproximately(1, eps);
    }
    
    private CircularCloudLayouter CreateCircularCloudLayouterWithOptimalParams() => new(new Point(0, 0));
    private CircularCloudLayouter CreateCircularCloudLayouterWithRandomParams() => 
        new(randomizer.RandomPoint(-10, 10), randomizer.Next(1, 10), randomizer.Next(1, 10));

    private List<Rectangle> PutRandomRectanglesInLayouter(int numberOfRectangles, 
        CircularCloudLayouter circularCloudLayouter) =>
        Enumerable
            .Range(0, numberOfRectangles)
            .Select(_ => circularCloudLayouter.PutNextRectangle(randomizer.RandomSize(10, 27)))
            .ToList();

    private Size GetLayoutSize(List<Rectangle> rectangles)
    {
        var layoutWidth = rectangles.Max(r => r.Right) - rectangles.Min(r => r.Left);
        var layoutHeight = rectangles.Max(r => r.Top) - rectangles.Min(r => r.Bottom);
        
        return new Size(layoutWidth, layoutHeight);
    }
}