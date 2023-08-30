using NetTopologySuite.Geometries;

namespace Neptune.EFModels;

public interface IHaveHRUCharacteristics : IHavePrimaryKey
{
    Geometry GetCatchmentGeometry();
}