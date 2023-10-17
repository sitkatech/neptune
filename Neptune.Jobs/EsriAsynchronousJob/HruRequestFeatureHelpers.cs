using Neptune.Common.GeoSpatial;
using NetTopologySuite.Geometries;

namespace Neptune.Jobs.EsriAsynchronousJob;

public static class HruRequestFeatureHelpers
{
    public static IEnumerable<HRURequestFeature> GetHRURequestFeatures(Dictionary<int, Geometry> loadGeneratingUnits, bool isProject)
    {
        foreach (var loadGeneratingUnit in loadGeneratingUnits)
        {
            var baseAttributes = new HRURequestFeatureAttributes
            {
                ObjectID = loadGeneratingUnit.Key,
                Area = isProject ? 0 : loadGeneratingUnit.Value.Area,
                Length = isProject ? 0 : loadGeneratingUnit.Value.Length,
                QueryFeatureID = loadGeneratingUnit.Key
            };
            var catchmentGeometry = loadGeneratingUnit.Value.ProjectTo2230();
               
            for (var i = 1; i <= catchmentGeometry.NumGeometries; i++)
            {
                if (catchmentGeometry.GetGeometryN(i).GeometryType.ToUpper() == "POLYGON")
                {
                    yield return new HRURequestFeature((Polygon)catchmentGeometry.GetGeometryN(i), baseAttributes,
                        loadGeneratingUnit.Key);
                }
            }
        }
    }
}