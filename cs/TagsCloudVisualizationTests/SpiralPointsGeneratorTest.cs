using SkiaSharp;
using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

[TestFixture]
[TestOf(typeof(SpiralPointsGenerator))]
public class SpiralPointsGeneratorTest
{
    private const string RadiusErrorMessage = "radius must be greater than 0";
    private const string AngleOffsetErrorMessage = "angleOffset must be greater than 0";

    [TestCase(-1, 10, RadiusErrorMessage, TestName = "radius is negative")]
    [TestCase(0, 3, RadiusErrorMessage, TestName = "radius is zero")]
    [TestCase(5, -11, AngleOffsetErrorMessage, TestName = "angleOffset is negative")]
    [TestCase(100, 0, AngleOffsetErrorMessage, TestName = "angleOffset is zero")]
    public void SpiralPointsGenerator_ShouldThrowArgumentException_WithInvalidParams(double radius,
        double angleOffset, string msg)
    {
        var start = new SKPoint(0, 0);
        Action act = () => new SpiralPointsGenerator(start, radius, angleOffset);

        act.Should().Throw<ArgumentException>().WithMessage(msg);
    }

    [Test]
    public void SpiralPointsGenerator_ShouldReturnExactListOfFiveFirstPoints_WithSpecialParams()
    {
        var spiralPointsGenerator = new SpiralPointsGenerator(new SKPoint(0, 0), 2, 360);
        var expectedListOfPoints = new List<SKPoint>
                {
                    new(0, 0), new(2, 0), new(4, 0), new(6, 0), new(8, 0)
                };
        
        var actualListOfPoints = Enumerable
            .Range(0, 5)
            .Select(_ => spiralPointsGenerator.GetNextPoint())
            .ToList();
        
        actualListOfPoints.Should().BeEquivalentTo(expectedListOfPoints);
    }
}