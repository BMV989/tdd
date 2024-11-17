using System.Drawing;

namespace TagsCloudVisualization;

public class SpiralPointsGenerator : IPointsGenerator
{
    private readonly double angleOffset;
    private readonly double radius;
    private readonly Point start;
    private double angle;

    public SpiralPointsGenerator(Point start, double radius, double angleOffset)
    {
        if (radius <= 0)
            throw new ArgumentException("radius must be greater than 0");
        if (angleOffset <= 0)
            throw new ArgumentException("angleOffset must be greater than 0");
        
        this.angleOffset = angleOffset * Math.PI / 180;
        this.radius = radius;
        this.start = start;
    }

    public Point GetNextPoint()
    {
        var nextPoint = GetPointByPolarCords();
        angle += angleOffset;
        return nextPoint;
    }

    private Point GetPointByPolarCords()
    {
        var offsetPerRadian = radius / (2 * Math.PI);
        var radiusVector =  offsetPerRadian * angle;
        
        var x = (int)Math.Round(radiusVector * Math.Cos(angle) + start.X);
        var y = (int)Math.Round(radiusVector * Math.Sin(angle) + start.Y);

        return new Point(x, y);
    }
}