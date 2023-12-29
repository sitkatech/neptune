using Neptune.Common.GeoSpatial;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

public partial class ParcelStaging : IHasGeometry
{
    public Geometry Geometry
    {
        get => ParcelStagingGeometry;
        set => ParcelStagingGeometry = value;
    }
}