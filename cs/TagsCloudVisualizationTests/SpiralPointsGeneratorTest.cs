using System.Drawing;
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
        var start = new Point(0, 0);
        Action act = () => new SpiralPointsGenerator(start, radius, angleOffset);

        act.Should().Throw<ArgumentException>().WithMessage(msg);
    }
}