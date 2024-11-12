using System.Drawing;
using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class SpiralCloudLayouterTest
{
    [TestCase(0, 0)]
    [TestCase(-5, 2)]
    [TestCase(3, -2)]
    [TestCase(-5, -1)]
    [TestCase(20000, 10000)]
    public void SpiralCloudLayouter_ShouldHaveCenter_AtGivenPoint(int x, int y)
    {
        var layouter = new SpiralCloudLayouter(new Point(x, y));
        
        layouter.Center.Should().BeEquivalentTo(new Point(x, y));
    }
}