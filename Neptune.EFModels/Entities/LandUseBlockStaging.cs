using Neptune.Common.GeoSpatial;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

public partial class LandUseBlockStaging : IHasGeometry
{
    public Geometry Geometry
    {
        get => LandUseBlockStagingGeometry;
        set => LandUseBlockStagingGeometry = value;
    }
}