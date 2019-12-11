﻿// Classes for preparing the Esri JSON input to the HRU service.
// Names have to match remote service's expectation, therefore:
// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using System.Data.Entity.Spatial;
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

        public HRURequestFeature(IHaveHRUCharacteristics iHaveHRUCharacteristics)
        {
            Attributes = new HRURequestFeatureAttributes
            {
                ObjectID = iHaveHRUCharacteristics.PrimaryKey,
                QueryFeatureID = iHaveHRUCharacteristics.PrimaryKey,
                Area = iHaveHRUCharacteristics.GetCatchmentGeometry().Area.GetValueOrDefault(),
                Length = iHaveHRUCharacteristics.GetCatchmentGeometry().Length.GetValueOrDefault(),
            };

            var rings = new List<List<double[]>>();

            // todo: make this iterate through the geometry parts since this might be a multipolygon

            
            // this will break unceremoniously if catchment geometry is multipolygon instead of polygon. 
            // unclear if this is going to break anything in practice.
            // I don't even know if the HRU service will accept a multi-geometry.

            var catchmentGeometry = iHaveHRUCharacteristics.GetCatchmentGeometry();
            
            var coordinates = new List<double[]>();

            for (var i = 1; i <= catchmentGeometry.ExteriorRing.PointCount; i++)
            {
                var point = catchmentGeometry.ExteriorRing.PointAt(i);
                var lon = point.XCoordinate.GetValueOrDefault();
                var lat = point.YCoordinate.GetValueOrDefault();

                coordinates.Add(new[] { lon, lat });
            }

            rings.Add(coordinates);

            Geometry = new EsriPolygonGeometry
            {
                Rings = rings
            };
        }

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
    }
}