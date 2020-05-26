// Classes for preparing the Esri JSON input to the HRU service.
// Names have to match remote service's expectation, therefore:
// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using LtInfo.Common;
using Neptune.Web.Models;
using Newtonsoft.Json;

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
            var coordinates = new List<double[]>();
            var rings = new List<List<double[]>>();

            for (var j = 1; j <= catchmentGeometry.ExteriorRing.PointCount; j++)
            {
                var point = catchmentGeometry.ExteriorRing.PointAt(j);
                var lon = point.XCoordinate.GetValueOrDefault();
                var lat = point.YCoordinate.GetValueOrDefault();

                coordinates.Add(new[] { lon, lat });
            }

            // need to account for interior rings
            // need to skip geometries with exterior rings I guess

            rings.Add(coordinates);

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
    }

    public static class HruRequestFeatureHelpers
    {
        public static IEnumerable<HRURequestFeature> GetHRURequestFeatures(this IHaveHRUCharacteristics iHaveHRUCharacteristics)
        {
            var baseAttributes = new HRURequestFeatureAttributes
            {
                ObjectID = iHaveHRUCharacteristics.PrimaryKey,
                Area = iHaveHRUCharacteristics.GetCatchmentGeometry().Area.GetValueOrDefault(),
                Length = iHaveHRUCharacteristics.GetCatchmentGeometry().Length.GetValueOrDefault(),
            };

            var catchmentGeometry = iHaveHRUCharacteristics.GetCatchmentGeometry();

            for (var i = 1; i <= catchmentGeometry.ElementCount; i++)
            {
                yield return new HRURequestFeature(catchmentGeometry.ElementAt(i), baseAttributes, i);
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
                DbGeometry catchmentGeometry = null;
                try
                {
                    catchmentGeometry = CoordinateSystemHelper.Project2771To2230(loadGeneratingUnit.LoadGeneratingUnitGeometry);
                }
                catch (Exception ex)
                {
                    var a = ex.Message;
                }

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