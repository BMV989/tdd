using SkiaSharp;

namespace TagsCloudVisualization;

public interface ICloudLayouter
{
    public SKRect PutNextRectangle(SKSize rectangleSize);
}