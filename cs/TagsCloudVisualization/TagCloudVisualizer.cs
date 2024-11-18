using SkiaSharp;


namespace TagsCloudVisualization;

public class TagCloudVisualizer(int width, int height)
{
    public SKBitmap Visualize(List<SKRect> rectangles)
    {
        var bitmap = new SKBitmap(width, height);
        var canvas = new SKCanvas(bitmap);
        var paint = new SKPaint 
        { 
            Color = SKColors.Black,
            Style = SKPaintStyle.Stroke
        };
        
        canvas.Clear(SKColors.White);
        
        foreach (var rectangle in rectangles)
            canvas.DrawRect(rectangle, paint);
        
        return bitmap;
    }
}