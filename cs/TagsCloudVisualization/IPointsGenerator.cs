using SkiaSharp;

namespace TagsCloudVisualization;

public interface IPointsGenerator
{
    public SKPoint GetNextPoint();
}