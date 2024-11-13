using System.Drawing;

namespace TagsCloudVisualization;

public class DefaultPointsDistributor : IPointsDistributor
{
    private readonly double angleOffset;
    private readonly double radius;

    public DefaultPointsDistributor(double radius, double angleOffset)
    {
        if (radius <= 0)
            throw new ArgumentException("radius must be greater than 0");
        if (angleOffset <= 0)
            throw new ArgumentException("angleOffset must be greater than 0");
        
        this.angleOffset = angleOffset * Math.PI / 180;
        this.radius = radius;
    }

    public IEnumerable<Point> DistributePoints(Point start)
    {
        var angle = 0d;

        while (true)
        {
            yield return GetPointByPolarCords(start, angle);
            angle += angleOffset;
        }
    }

    private Point GetPointByPolarCords(Point point, double angle)
    {
        var offsetPerRadian = radius / (2 * Math.PI);
        var radiusVector =  offsetPerRadian * angle;
        
        var x = (int)Math.Round(radiusVector * Math.Cos(angle) + point.X);
        var y = (int)Math.Round(radiusVector * Math.Sin(angle) + point.Y);

        return new Point(x, y);
    }
}