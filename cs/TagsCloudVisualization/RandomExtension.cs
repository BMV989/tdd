using System.Drawing;

namespace TagsCloudVisualization;

public static class RandomExtension
{
    public static Size RandomSize(this Random random, int minValue, int maxValue)
    {
        if (minValue <= 0)
            throw new ArgumentOutOfRangeException(nameof(minValue), "minValue must be greater than 0");
        if (maxValue < minValue)
            throw new ArgumentOutOfRangeException(nameof(maxValue), "maxValue must be greater than minValue");
        
        return new Size(random.Next(minValue, maxValue), random.Next(minValue, maxValue));
    }
    
    public static Point RandomPoint(this Random random, int minValue, int maxValue) =>
        new (random.Next(minValue, maxValue), random.Next(minValue, maxValue));
}