using System.Drawing;
using SkiaSharp;
using SkiaSharp.Views.Desktop;


namespace TagsCloudVisualization;

public class Visualizer
{
    private readonly SKBitmap bitmap;
    private readonly SKCanvas canvas;

    public Visualizer(int width, int height)
    {
        bitmap = new SKBitmap(width, height);
        canvas = new SKCanvas(bitmap);
    }

    public SKBitmap VisualizeTagCloud(IEnumerable<Rectangle> rectangles)
    {
        foreach (var rectangle in rectangles)
        {
            var skPaint = new SKPaint
            {
                Color = SKColors.White,
                Style = SKPaintStyle.Stroke
            };
            canvas.DrawRect(rectangle.ToSKRect(), skPaint);
        }
        
        return bitmap;
    }
}