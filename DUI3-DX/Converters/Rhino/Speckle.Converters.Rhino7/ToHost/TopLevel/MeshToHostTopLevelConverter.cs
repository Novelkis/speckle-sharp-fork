﻿using Rhino;
using Speckle.Converters.Common;
using Speckle.Converters.Common.Objects;

namespace Speckle.Converters.Rhino7.ToHost.TopLevel;

[NameAndRankValue(nameof(SOG.Mesh), NameAndRankValueAttribute.SPECKLE_DEFAULT_RANK)]
public class MeshToHostTopLevelConverter : SpeckleToHostGeometryBaseConversion<SOG.Mesh, RG.Mesh>
{
  public MeshToHostTopLevelConverter(
    IConversionContextStack<RhinoDoc, UnitSystem> contextStack,
    IRawConversion<SOG.Mesh, RG.Mesh> geometryBaseConverter
  )
    : base(contextStack, geometryBaseConverter) { }
}