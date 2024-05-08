using Speckle.Converters.Common;
using Speckle.Converters.Common.Objects;
using Speckle.Core.Models;

namespace Speckle.Converters.ArcGIS3.Geometry.ISpeckleObjectToHost;

[NameAndRankValue(nameof(SOG.Ellipse), NameAndRankValueAttribute.SPECKLE_DEFAULT_RANK)]
public class EllipseToHostConverter : ISpeckleObjectToHostConversion, IRawConversion<SOG.Ellipse, ACG.Polyline>
{
  private readonly IRawConversion<SOG.Point, ACG.MapPoint> _pointConverter;

  public EllipseToHostConverter(IRawConversion<SOG.Point, ACG.MapPoint> pointConverter)
  {
    _pointConverter = pointConverter;
  }

  public object Convert(Base target) => RawConvert((SOG.Ellipse)target);

  public ACG.Polyline RawConvert(SOG.Ellipse target)
  {
    // Determine the number of vertices to create along the Ellipse
    int numVertices = Math.Max((int)target.length, 3); // Determine based on desired segment length or other criteria
    List<SOG.Point> pointsOriginal = new();

    if (target.firstRadius == null || target.secondRadius == null)
    {
      throw new SpeckleConversionException("Conversion failed: Ellipse doesn't have 1st and 2nd radius");
    }
    double maxRadius = Math.Max((double)target.firstRadius, (double)target.secondRadius);
    double minRadius = Math.Min((double)target.firstRadius, (double)target.secondRadius);

    // Calculate the vertices along the arc
    for (int i = 0; i <= numVertices; i++)
    {
      // Calculate the point along the arc
      double angle = 2 * Math.PI * (i / (double)numVertices);
      SOG.Point pointOnEllipse =
        new(
          target.plane.origin.x + maxRadius * Math.Cos(angle),
          target.plane.origin.y + minRadius * Math.Sin(angle),
          target.plane.origin.z
        );

      pointsOriginal.Add(pointOnEllipse);
    }
    pointsOriginal.Add(pointsOriginal[0]);

    var points = pointsOriginal.Select(x => _pointConverter.RawConvert(x));
    return new ACG.PolylineBuilderEx(points, ACG.AttributeFlags.HasZ).ToGeometry();
  }
}