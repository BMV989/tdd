using System.Drawing;

namespace TagsCloudVisualization;

public class SpiralCloudLayouter : ICloudLayouter
{
    public Point Center { get; private set; }
    
    public SpiralCloudLayouter(Point center)
    {
        Center = center;
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        return Rectangle.Empty;
    }
}

