using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using NetTopologySuite.Geometries;

namespace Neptune.Jobs.EsriAsynchronousJob;

public static class HruRequestFeatureHelpers
{
    public static IEnumerable<HRURequestFeature> GetHRURequestFeatures(this IEnumerable<LoadGeneratingUnit> loadGeneratingUnits)
    {
        foreach (var loadGeneratingUnit in loadGeneratingUnits)
        {
            var baseAttributes = new HRURequestFeatureAttributes
            {
                ObjectID = loadGeneratingUnit.PrimaryKey,
                Area = loadGeneratingUnit.LoadGeneratingUnitGeometry.Area,
                Length = loadGeneratingUnit.LoadGeneratingUnitGeometry.Length,
                QueryFeatureID = loadGeneratingUnit.LoadGeneratingUnitID
            };
            var catchmentGeometry = loadGeneratingUnit.LoadGeneratingUnitGeometry.ProjectTo2230();
               
            for (var i = 1; i <= catchmentGeometry.NumGeometries; i++)
            {
                if (catchmentGeometry.GetGeometryN(i).GeometryType.ToUpper() == "POLYGON")
                {
                    yield return new HRURequestFeature((Polygon)catchmentGeometry.GetGeometryN(i), baseAttributes,
                        loadGeneratingUnit.LoadGeneratingUnitID);
                }
            }
        }
    }

    public static IEnumerable<HRURequestFeature> GetHRURequestFeatures(this IEnumerable<ProjectLoadGeneratingUnit> loadGeneratingUnits)
    {
        foreach (var loadGeneratingUnit in loadGeneratingUnits)
        {
            var baseAttributes = new HRURequestFeatureAttributes
            {
                ObjectID = loadGeneratingUnit.PrimaryKey,
                Area = 0,
                Length = 0,
                QueryFeatureID = loadGeneratingUnit.ProjectLoadGeneratingUnitID
            };
            var catchmentGeometry = loadGeneratingUnit.ProjectLoadGeneratingUnitGeometry.ProjectTo2230();

            for (var i = 1; i <= catchmentGeometry.NumGeometries; i++)
            {
                if (catchmentGeometry.GetGeometryN(i).GeometryType.ToUpper() == "POLYGON")
                {
                    yield return new HRURequestFeature((Polygon)catchmentGeometry.GetGeometryN(i), baseAttributes,
                        loadGeneratingUnit.ProjectLoadGeneratingUnitID);
                }
            }
        }
    }
}