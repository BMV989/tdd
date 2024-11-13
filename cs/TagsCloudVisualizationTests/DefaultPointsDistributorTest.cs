using System.Drawing;
using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

[TestFixture]
[TestOf(typeof(DefaultPointsDistributor))]
public class DefaultPointsDistributorTest
{
    private const string RadiusErrorMessage = "radius must be greater than 0";
    private const string AngleOffsetErrorMessage = "angleOffset must be greater than 0";

    [TestCase(-1, 10, RadiusErrorMessage, TestName = "radius is negative")]
    [TestCase(0, 3, RadiusErrorMessage, TestName = "radius is zero")]
    [TestCase(5, -11, AngleOffsetErrorMessage, TestName = "angleOffset is negative")]
    [TestCase(100, 0, AngleOffsetErrorMessage, TestName = "angleOffset is zero")]
    public void DefaultPointsDistributor_ShouldThrowArgumentException_WithInvalidParams(double radius,
        double angleOffset, string msg)
    {
        Action act = () => new DefaultPointsDistributor(radius, angleOffset);

        act.Should().Throw<ArgumentException>().WithMessage(msg);
    }

    public static TestCaseData[] DistributePointsTestCases =
    {
        new TestCaseData(1, 125, 0).Returns(new Point(0, 0)),
        new TestCaseData(10, 125, 1).Returns(new Point(-2, 3)),
        new TestCaseData(149, 360, 1).Returns(new Point(149, 0)),
        new TestCaseData(50, 180, 2).Returns(new Point(50, 0)),
        new TestCaseData(4, 90, 3).Returns(new Point(0, -3)),
        new TestCaseData(4, 300, 4).Returns(new Point(-7, 12)),
        new TestCaseData(10, 5, 1).Returns(new Point(0, 0)),
        new TestCaseData(2, 1, 0).Returns(new Point(0, 0)),
        new TestCaseData(10, 5, 3).Returns(new Point(0, 0)),
        new TestCaseData(20, 15, 3).Returns(new Point(2, 2)),
    };

    [TestCaseSource(nameof(DistributePointsTestCases))]
    public Point DistributePoints_ShouldReturnValidPoints_WithValidParams(double radius, double angleOffset,
        int pointsToSkip)
    {
        var PointsDistributor = new DefaultPointsDistributor(radius, angleOffset);
        var startPoint = new Point(0, 0);

        var actualPoint = PointsDistributor
            .DistributePoints(startPoint)
            .Skip(pointsToSkip)
            .First();

        return actualPoint;


    }
    
    
}