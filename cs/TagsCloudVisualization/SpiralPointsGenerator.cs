using SkiaSharp;

namespace TagsCloudVisualization;

public class SpiralPointsGenerator : IPointsGenerator
{
    private readonly double angleOffset;
    private readonly double radius;
    private readonly SKPoint start;
    private double angle;

    public SpiralPointsGenerator(SKPoint start, double radius, double angleOffset)
    {
        if (radius <= 0)
            throw new ArgumentException("radius must be greater than 0");
        if (angleOffset <= 0)
            throw new ArgumentException("angleOffset must be greater than 0");
        
        this.angleOffset = angleOffset * Math.PI / 180;
        this.radius = radius;
        this.start = start;
    }

    public SKPoint GetNextPoint()
    {
        var nextPoint = GetPointByPolarCords();
        angle += angleOffset;
        return nextPoint;
    }

    private SKPoint GetPointByPolarCords()
    {
        var offsetPerRadian = radius / (2 * Math.PI);
        var radiusVector =  offsetPerRadian * angle;
        
        var x = (int)Math.Round(radiusVector * Math.Cos(angle) + start.X);
        var y = (int)Math.Round(radiusVector * Math.Sin(angle) + start.Y);

        return new SKPoint(x, y);
    }
}