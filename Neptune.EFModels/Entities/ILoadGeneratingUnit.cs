using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

public interface ILoadGeneratingUnit
{
    int PrimaryKey { get; }
    Geometry Geometry { get; }
}