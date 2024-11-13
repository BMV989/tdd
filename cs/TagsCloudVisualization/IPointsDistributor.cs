using System.Drawing;

namespace TagsCloudVisualization;

public interface IPointsDistributor
{
    public IEnumerable<Point> DistributePoints(Point start);
}