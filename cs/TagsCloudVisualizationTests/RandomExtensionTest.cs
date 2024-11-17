using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

[TestFixture]
[TestOf(typeof(RandomExtension))]
public class RandomExtensionTest
{
    private readonly Random random = new();
    
    [TestCase(0, 5, TestName = "MinValue is zero")] 
    [TestCase(-1, 10, TestName = "MinValue is negative")]
    [TestCase(50, 20, Description = "MinValue is greater than MaxValue")]
    public void NextSize_ShouldThrowArgumentOutOfRangeException_WithInvalidParams(int minValue, int maxValue)
    {
        Action act = () => random.NextSize(minValue, maxValue);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}