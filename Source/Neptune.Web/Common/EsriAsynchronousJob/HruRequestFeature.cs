// Classes for preparing the Esri JSON input to the HRU service.
// Names have to match remote service's expectation, therefore:
// ReSharper disable InconsistentNaming

using LtInfo.Common;
using Neptune.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Entity.Spatial;

namespace Neptune.Web.Common.EsriAsynchronousJob
{
    public class HRURequestFeature
    {
        [JsonProperty("attributes")]
        public HRURequestFeatureAttributes Attributes { get; set; }
        [JsonProperty("geometry")]
        public EsriPolygonGeometry Geometry { get; set; }

        public HRURequestFeature(DbGeometry catchmentGeometry, HRURequestFeatureAttributes baseAttributes, int i)
        {
            var rings = new List<List<double[]>>();
            var catchmentGeometryExteriorRing = catchmentGeometry.ExteriorRing;

            var exteriorRingCoordinates = GetRingCoordinates(catchmentGeometryExteriorRing);

            // need to account for interior rings
            // need to skip geometries with exterior rings I guess

            rings.Add(exteriorRingCoordinates);

            for (var j = 1; j <= catchmentGeometry.InteriorRingCount; j++)
            {
                var interiorRing = catchmentGeometry.InteriorRingAt(j);
                var interiorRingCoordinates = GetRingCoordinates(interiorRing);
                rings.Add(interiorRingCoordinates);
            }

            Geometry = new EsriPolygonGeometry
            {
                Rings = rings
            };

            Attributes = new HRURequestFeatureAttributes
            {
                ObjectID = baseAttributes.ObjectID,
                QueryFeatureID = i,
                Area = baseAttributes.Area,
                Length = baseAttributes.Length
            };
        }

        private static List<double[]> GetRingCoordinates(DbGeometry catchmentGeometryExteriorRing)
        {
            var coordinates = new List<double[]>();
            for (var j = 1; j <= catchmentGeometryExteriorRing.PointCount; j++)
            {
                var point = catchmentGeometryExteriorRing.PointAt(j);
                var lon = point.XCoordinate.GetValueOrDefault();
                var lat = point.YCoordinate.GetValueOrDefault();

                coordinates.Add(new[] {lon, lat});
            }

            return coordinates;
        }
    }

    public static class HruRequestFeatureHelpers
    {
        // ReSharper disable once UnusedMember.Global
        public static IEnumerable<HRURequestFeature> GetHRURequestFeatures(this IHaveHRUCharacteristics iHaveHRUCharacteristics)
        {
            var baseAttributes = new HRURequestFeatureAttributes
            {
                ObjectID = iHaveHRUCharacteristics.PrimaryKey,
                Area = iHaveHRUCharacteristics.GetCatchmentGeometry().Area.GetValueOrDefault(),
                Length = iHaveHRUCharacteristics.GetCatchmentGeometry().Length.GetValueOrDefault(),
            };

            var catchmentGeometry =
                CoordinateSystemHelper.Project2771To2230(iHaveHRUCharacteristics.GetCatchmentGeometry());

            for (var i = 1; i <= catchmentGeometry.ElementCount; i++)
            {
                if (catchmentGeometry.ElementAt(i).SpatialTypeName.ToUpper() == "POLYGON")
                {
                    yield return new HRURequestFeature(catchmentGeometry.ElementAt(i), baseAttributes, i);
                }
            }
        }
        
        public static IEnumerable<HRURequestFeature> GetHRURequestFeatures(this IEnumerable<LoadGeneratingUnit> loadGeneratingUnits)
        {
            foreach (var loadGeneratingUnit in loadGeneratingUnits)
            {
                var baseAttributes = new HRURequestFeatureAttributes
                {
                    ObjectID = loadGeneratingUnit.PrimaryKey,
                    Area = loadGeneratingUnit.LoadGeneratingUnitGeometry.Area.GetValueOrDefault(),
                    Length = loadGeneratingUnit.LoadGeneratingUnitGeometry.Length.GetValueOrDefault(),
                    QueryFeatureID = loadGeneratingUnit.LoadGeneratingUnitID
                };
                var catchmentGeometry =
                    CoordinateSystemHelper.Project2771To2230(loadGeneratingUnit.LoadGeneratingUnitGeometry);
               
                for (var i = 1; i <= catchmentGeometry.ElementCount; i++)
                {
                    if (catchmentGeometry.ElementAt(i).SpatialTypeName.ToUpper() == "POLYGON")
                    {
                        yield return new HRURequestFeature(catchmentGeometry.ElementAt(i), baseAttributes,
                            loadGeneratingUnit.LoadGeneratingUnitID);
                    }
                }
            }
        }
    }
}