using FluentAssertions;
using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

[TestFixture]
[TestOf(typeof(RandomExtension))]
public class RandomExtensionTest
{
    private const int Seed = 123456789;
    private readonly Random random = new(Seed);
    
    [TestCase(0, 5, TestName = "MinValue is zero")] 
    [TestCase(-1, 10, TestName = "MinValue is negative")]
    [TestCase(50, 20, Description = "MinValue is greater than MaxValue")]
    public void NextSize_ShouldThrowArgumentOutOfRangeException_WithInvalidParams(int minValue, int maxValue)
    {
        Action act = () => random.NextSize(minValue, maxValue);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void NextSize_ShouldReturnExpectedNextSize()
    {
        var seed = random.Next();
        var testRandom = new Random(seed);
        var expectedRandom = new Random(seed);

        var actualSize = testRandom.NextSize(1, int.MaxValue);
        var expectedSize = new Size(
            expectedRandom.Next(1, int.MaxValue),
            expectedRandom.Next(1, int.MaxValue));
        
        actualSize.Should().BeEquivalentTo(expectedSize);
    }

    [Test]
    public void NextPoint_ShouldReturnPoint()
    {
        random.NextPoint(int.MinValue, int.MaxValue).Should().BeOfType<Point>();
    }

    [Test]
    public void NextPoint_ShouldReturnExpectedNextPoint()
    {
        var seed = random.Next();
        var pointRandomizer = new Random(seed);
        var expectedRandomizer = new Random(seed);
        
        var actualPoint = pointRandomizer.NextPoint(int.MinValue, int.MaxValue);
        var expectedPoint = new Point(
            expectedRandomizer.Next(int.MinValue, int.MaxValue),
            expectedRandomizer.Next(int.MinValue, int.MaxValue));
        
        actualPoint.Should().BeEquivalentTo(expectedPoint);
    }
}