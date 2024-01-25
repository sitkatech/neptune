using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

public partial class vProjectLoadGeneratingUnitUpdateCandidate : ILoadGeneratingUnit
{
    public int PrimaryKey => ProjectLoadGeneratingUnitID;
    public Geometry Geometry => ProjectLoadGeneratingUnitGeometry;
}