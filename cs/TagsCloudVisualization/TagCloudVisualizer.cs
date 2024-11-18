using System.Drawing;
using SkiaSharp;
using SkiaSharp.Views.Desktop;


namespace TagsCloudVisualization;

public class TagCloudVisualizer(int width, int height)
{
    private readonly SKBitmap bitmap = new(width, height);

    public SKBitmap Visualize(IEnumerable<Rectangle> rectangles)
    {
        var canvas = new SKCanvas(bitmap);
        var paint = new SKPaint 
        { 
            Color = SKColors.White,
            Style = SKPaintStyle.Stroke
        };
        
        foreach (var rectangle in rectangles)
            canvas.DrawRect(rectangle.ToSKRect(), paint);
        
        return bitmap;
    }
}