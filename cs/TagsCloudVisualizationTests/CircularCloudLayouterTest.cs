using FluentAssertions;
using NUnit.Framework.Interfaces;
using SkiaSharp;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

[TestFixture]
[TestOf(typeof(CircularCloudLayouter))]
public class CircularCloudLayouterTest
{
    private const string ImagesDirectory = "../../../failedTests";
    private readonly Random randomizer = new();
    private CircularCloudLayouter circularCloudLayouter;

    [SetUp]
    public void Setup()
    {
        circularCloudLayouter = new CircularCloudLayouter(randomizer.NextSkPoint(-10, 10));
    }

    [TearDown]
    public void TearDown()
    {
        var currentContext = TestContext.CurrentContext;
        if (currentContext.Result.Outcome.Status != TestStatus.Failed) return;

        var layoutSize = GetLayoutSize(circularCloudLayouter.Rectangles.ToList());
        var visualizer = new TagCloudVisualizer((int)layoutSize.Width, (int)layoutSize.Height);
        var bitmap = visualizer.Visualize(circularCloudLayouter.Rectangles);

        var saver = new Saver(ImagesDirectory);
        var filename = $"{currentContext.Test.Name}.png";
        saver.SaveAsPng(bitmap, filename);
        
        TestContext.Out.WriteLine($"Tag cloud visualization saved to file {Path.Combine(ImagesDirectory, filename)}");
    }

    [Test]
    public void PutNextRectangle_ShouldReturnRectangleAtCenter_WhenFirstInvoked()
    {
        var rectSize = randomizer.NextSkSize(1, int.MaxValue);
        
        var actualRect = circularCloudLayouter.PutNextRectangle(rectSize);
        var expectedRect = CircularCloudLayouter.CreateRectangleWithCenter(circularCloudLayouter.Center, rectSize);
        
        actualRect.Should().BeEquivalentTo(expectedRect);
    }

    [Test]
    public void PutNextRectangle_ShouldReturnRectangle_WithCorrectSize()
    {
        var rectangleSize = randomizer.NextSkSize(1, int.MaxValue);
        
        var actualRectangle = circularCloudLayouter.PutNextRectangle(rectangleSize);
        
        actualRectangle.Size.Should().Be(rectangleSize);
    }
    
    [Test]
    public void PutNextRectangle_ShouldReturnRectangles_WithoutIntersections()
    {
        var numberOfRectangles = randomizer.Next(100, 300);
        
        var rectangles = Enumerable
            .Range(0, numberOfRectangles)
            .Select(_ => circularCloudLayouter.PutNextRectangle(randomizer.NextSkSize(10, 27)))
            .ToList();

        bool IsIntersectionBetweenRectangles(SKRect rect) => 
                    rectangles.Any(otherRect => rect != otherRect && rect.IntersectsWith(otherRect));

        rectangles.Any(IsIntersectionBetweenRectangles)
            .Should().BeFalse();
    }

    [Test]
    public void GeneratedLayout_ShouldHaveHighTightnessAndShapeOfCircularCloud()
    {
        const double eps = 0.35;
        var rectangles = PutRandomRectanglesInLayouter(randomizer.Next(500, 1000));
        var layoutSize = GetLayoutSize(rectangles);
        
        var diameterOfCircle = Math.Max(layoutSize.Width, layoutSize.Height);
        var areaOfCircle = Math.PI * Math.Pow(diameterOfCircle, 2) / 4;
        var areaOfRectangles = (double)rectangles
            .Select(r => r.Height * r.Width)
            .Sum();
        var areaRatio = areaOfCircle / areaOfRectangles;

        areaRatio.Should().BeApproximately(1, eps);
    }
    
    private List<SKRect> PutRandomRectanglesInLayouter(int numberOfRectangles) =>
        Enumerable
            .Range(0, numberOfRectangles)
            .Select(_ => circularCloudLayouter.PutNextRectangle(randomizer.NextSkSize(10, 27)))
            .ToList();

    private SKSize GetLayoutSize(List<SKRect> rectangles)
    {
        var layoutWidth = rectangles.Max(r => r.Right) - rectangles.Min(r => r.Left);
        var layoutHeight = rectangles.Max(r => r.Top) - rectangles.Min(r => r.Bottom);
        
        return new SKSize(layoutWidth, layoutHeight);
    }
}