﻿using Speckle.Converters.Common.Objects;

namespace Speckle.Converters.Rhino7.ToHost.Raw;

public class PlaneToHostConverter : IRawConversion<SOG.Plane, RG.Plane>
{
  private readonly IRawConversion<SOG.Point, RG.Point3d> _pointConverter;
  private readonly IRawConversion<SOG.Vector, RG.Vector3d> _vectorConverter;

  public PlaneToHostConverter(
    IRawConversion<SOG.Point, RG.Point3d> pointConverter,
    IRawConversion<SOG.Vector, RG.Vector3d> vectorConverter
  )
  {
    _pointConverter = pointConverter;
    _vectorConverter = vectorConverter;
  }

  /// <summary>
  /// Converts a Speckle Plane object to a Rhino Plane object.
  /// </summary>
  /// <param name="target">The Speckle Plane object to be converted.</param>
  /// <returns>The converted Rhino Plane object.</returns>
  /// <remarks>⚠️ This conversion does NOT perform scaling.</remarks>
  public RG.Plane RawConvert(SOG.Plane target) =>
    new(
      _pointConverter.RawConvert(target.origin),
      _vectorConverter.RawConvert(target.xdir),
      _vectorConverter.RawConvert(target.ydir)
    );
}
