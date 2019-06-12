using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.GeoJson;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public static class LandUseBlockModelExtensions
    {
        public static string GetLandUseBlockAreaString(this LandUseBlock landUseBlock)
        {
            //todo: move the sqm - ac conversion factor to a const
            return (landUseBlock?.LandUseBlockGeometry.Area * 2471050)?.ToString("0.00") ?? "-";
        }

        //public static readonly UrlTemplate<int> DeleteUrlTemplate =
        //    new UrlTemplate<int>(
        //        SitkaRoute<LandUseBlockController>.BuildUrlFromExpression(t => t.Delete(UrlTemplate.Parameter1Int)));

        //public static string GetDeleteUrl(this LandUseBlock landUseBlock)
        //{
        //    return DeleteUrlTemplate.ParameterReplace(landUseBlock.LandUseBlockID);
        //}

        //public static string GetDetailUrl(this LandUseBlock landUseBlock)
        //{
        //    return landUseBlock.TreatmentBMP.GetLandUseBlockMapUrl();
        //}

        //public static HtmlString GetDetailUrlForGrid(this LandUseBlock landUseBlock)
        //{
        //    return new HtmlString($"<a href={GetDetailUrl(landUseBlock)} class='gridButton'>View</a>");
        //}

        public static GeoJSON.Net.Feature.FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<LandUseBlock> landUseBlockGeometryStagings)
        {
            var featureCollection = new GeoJSON.Net.Feature.FeatureCollection();
            featureCollection.Features.AddRange(landUseBlockGeometryStagings.Where(x => x.LandUseBlockGeometry != null).Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(x.LandUseBlockGeometry);
                feature.Properties.Add("LandUseBlockID", x.LandUseBlockID);
                feature.Properties.Add("Name", x.LandUseBlockID);
                feature.Properties.Add("FeatureWeight", 1);
                feature.Properties.Add("FillPolygon", true);
                feature.Properties.Add("FeatureColor", "#405d74");
                feature.Properties.Add("FillOpacity", "0.2");
                return feature;
            }));
            return featureCollection;
        }

        public static GeoJSON.Net.Feature.FeatureCollection ToGeoJsonFeatureCollectionGeneric(this IEnumerable<LandUseBlock> landUseBlockGeometryStagings)
        {
            var featureCollection = new GeoJSON.Net.Feature.FeatureCollection();
            featureCollection.Features.AddRange(landUseBlockGeometryStagings.Where(x => x.LandUseBlockGeometry != null).Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(x.LandUseBlockGeometry);
                feature.Properties.Add("LandUseBlockID", x.LandUseBlockID);
                feature.Properties.Add("Name", x.LandUseBlockID);
                feature.Properties.Add("FeatureWeight", 1);
                feature.Properties.Add("FillPolygon", true);
                feature.Properties.Add("FillOpacity", "0.2");
                feature.Properties.Add("FeatureColor", "#405d74");
                return feature;
            }));
            return featureCollection;
        }
    }
}