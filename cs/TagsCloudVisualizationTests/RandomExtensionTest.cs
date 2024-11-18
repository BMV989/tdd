using FluentAssertions;
using SkiaSharp;
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
    [TestCase(50, 20, TestName = "MinValue is greater than MaxValue")]
    public void NextSkSize_ShouldThrowArgumentOutOfRangeException_WithInvalidParams(int minValue, int maxValue)
    {
        Action act = () => random.NextSkSize(minValue, maxValue);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void NextSkSize_ShouldReturnExpectedNextSize()
    {
        var seed = random.Next();
        var testRandom = new Random(seed);
        var expectedRandom = new Random(seed);

        var actualSize = testRandom.NextSkSize(1, int.MaxValue);
        var expectedSize = new SKSize(
            expectedRandom.Next(1, int.MaxValue),
            expectedRandom.Next(1, int.MaxValue));
        
        actualSize.Should().BeEquivalentTo(expectedSize);
    }

    [Test]
    public void NextSkPoint_ShouldReturnExpectedNextPoint()
    {
        var seed = random.Next();
        var pointRandomizer = new Random(seed);
        var expectedRandomizer = new Random(seed);
        
        var actualPoint = pointRandomizer.NextSkPoint(int.MinValue, int.MaxValue);
        var expectedPoint = new SKPoint(
            expectedRandomizer.Next(int.MinValue, int.MaxValue),
            expectedRandomizer.Next(int.MinValue, int.MaxValue));
        
        actualPoint.Should().BeEquivalentTo(expectedPoint);
    }
}