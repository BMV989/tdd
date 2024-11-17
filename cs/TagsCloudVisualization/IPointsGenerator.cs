using System.Drawing;

namespace TagsCloudVisualization;

public interface IPointsGenerator
{
    public Point GetNextPoint();
}