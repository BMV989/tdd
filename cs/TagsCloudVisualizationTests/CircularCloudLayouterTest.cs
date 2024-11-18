using System.Drawing;
using FluentAssertions;
using NUnit.Framework.Interfaces;
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
        circularCloudLayouter = TestContext.CurrentContext.Test.Name.Contains("Optimal") ? 
            CreateCircularCloudLayouterWithOptimalParams() : CreateCircularCloudLayouterWithRandomParams();
    }

    [TearDown]
    public void TearDown()
    {
        var currentContext = TestContext.CurrentContext;
        if (currentContext.Result.Outcome.Status != TestStatus.Failed) return;

        var layoutSize = GetLayoutSize(circularCloudLayouter.Rectangles.ToList());
        var visualizer = new TagCloudVisualizer(layoutSize.Width, layoutSize.Height);
        var bitmap = visualizer.Visualize(circularCloudLayouter.Rectangles);

        var saver = new Saver(ImagesDirectory);
        var filename = $"{currentContext.Test.Name}.png";
        saver.SaveAsPng(bitmap, filename);
        
        TestContext.Out.WriteLine($"Tag cloud visualization saved to file {Path.Combine(ImagesDirectory, filename)}");
    }

    [Test]
    public void PutNextRectangle_ShouldReturnRectangle()
    {
        var rectSize = randomizer.NextSize(1, int.MaxValue);
        
        var rect = circularCloudLayouter.PutNextRectangle(rectSize);

        rect.Should().BeOfType<Rectangle>();
    }

    [Test]
    public void PutNextRectangle_ShouldReturnRectangleAtCenter_WhenFirstInvoked()
    {
        var rectSize = randomizer.NextSize(1, int.MaxValue);
        
        var actualRect = circularCloudLayouter.PutNextRectangle(rectSize);
        var expectedRect = CircularCloudLayouter.CreateRectangle(circularCloudLayouter.Center, rectSize);
        
        actualRect.Should().BeEquivalentTo(expectedRect);
    }

    [Test]
    public void PutNextRectangle_ShouldReturnRectangle_WithCorrectSize()
    {
        var recSize = randomizer.NextSize(1, int.MaxValue);
        
        var actualRect = circularCloudLayouter.PutNextRectangle(recSize);
        
        actualRect.Size.Should().Be(recSize);
    }
    
    [Test]
    public void PutNextRectangle_ShouldReturnRectangles_WithoutIntersections()
    {
        var numberOfRectangles = randomizer.Next(100, 300);
        
        var rectangles = Enumerable
            .Range(0, numberOfRectangles)
            .Select(_ => circularCloudLayouter.PutNextRectangle(randomizer.NextSize(10, 27)))
            .ToList();
        
        rectangles.Any(fr => rectangles.Any(sr => fr != sr && fr.IntersectsWith(sr)))
            .Should().BeFalse();
    }

    [Test]
    public void GeneratedLayout_ShouldHaveHighTightnessAndShapeOfCircularCloud_WithOptimalParams()
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
    
    private CircularCloudLayouter CreateCircularCloudLayouterWithOptimalParams() => new(new Point(0, 0));
    private CircularCloudLayouter CreateCircularCloudLayouterWithRandomParams() => 
        new(randomizer.NextPoint(-10, 10), randomizer.Next(1, 10), randomizer.Next(1, 10));

    private List<Rectangle> PutRandomRectanglesInLayouter(int numberOfRectangles) =>
        Enumerable
            .Range(0, numberOfRectangles)
            .Select(_ => circularCloudLayouter.PutNextRectangle(randomizer.NextSize(10, 27)))
            .ToList();

    private Size GetLayoutSize(List<Rectangle> rectangles)
    {
        var layoutWidth = rectangles.Max(r => r.Right) - rectangles.Min(r => r.Left);
        var layoutHeight = rectangles.Max(r => r.Top) - rectangles.Min(r => r.Bottom);
        
        return new Size(layoutWidth, layoutHeight);
    }
}