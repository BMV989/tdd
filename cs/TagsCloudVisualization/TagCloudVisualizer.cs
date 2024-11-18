using SkiaSharp;


namespace TagsCloudVisualization;

public class TagCloudVisualizer(int width, int height)
{
    private readonly SKBitmap bitmap = new(width, height);

    public SKBitmap Visualize(IEnumerable<SKRect> rectangles)
    {
        var canvas = new SKCanvas(bitmap);
        var paint = new SKPaint 
        { 
            Color = SKColors.White,
            Style = SKPaintStyle.Stroke
        };
        
        foreach (var rectangle in rectangles)
            canvas.DrawRect(rectangle, paint);
        
        return bitmap;
    }
}