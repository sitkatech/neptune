using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

public partial class vLoadGeneratingUnitUpdateCandidate : ILoadGeneratingUnit
{
    public int PrimaryKey => LoadGeneratingUnitID;
    public Geometry Geometry => LoadGeneratingUnitGeometry;
}