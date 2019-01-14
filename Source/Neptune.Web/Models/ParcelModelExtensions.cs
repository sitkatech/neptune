using System;
using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using LtInfo.Common.GeoJson;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static class ParcelModelExtensions
    {
        public const string ParcelColor = "#fb00be";

        public static string GetTrashMapAssetUrl(this Parcel parcel)
        {
            return SitkaRoute<ParcelController>.BuildUrlFromExpression(x=>x.TrashMapAssetPanel(parcel));
        }

        public static LayerGeoJson GetParcelWmsLayerGeoJson(string layerColor, decimal layerOpacity, LayerInitialVisibility layerInitialVisibility, SystemAttribute systemAttribute)
        {
            return new LayerGeoJson(FieldDefinition.Parcel.GetFieldDefinitionLabelPluralized(),
                NeptuneWebConfiguration.ParcelMapServiceUrl, systemAttribute.ParcelLayerName, "#", layerColor,
                layerOpacity, layerInitialVisibility);
        }

        public static FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<Parcel> parcels) =>
            new FeatureCollection(parcels.Select(MakeFeatureWithRelevantProperties).ToList());

        public static Feature MakeFeatureWithRelevantProperties(this Parcel parcel) =>
            DbGeometryToGeoJsonHelper.FromDbGeometry(parcel.ParcelGeometry);

        public static FeatureCollection ToGeoJsonFeatureCollectionForTrashMap(this IEnumerable<Parcel> parcels)
        {
            var featureCollection = new FeatureCollection();
            featureCollection.Features.AddRange(parcels.Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(x.ParcelGeometry);
                var trashCaptureStatusType = x.GetTrashCaptureStatusType();
                feature.Properties.Add("Number", x.ParcelNumber);
                feature.Properties.Add("FeatureColor", trashCaptureStatusType.FeatureColorOnTrashModuleMap());
                feature.Properties.Add("TrashCaptureStatusTypeID", trashCaptureStatusType.TrashCaptureStatusTypeID);
                feature.Properties.Add("ParcelID", x.ParcelID);
                feature.Properties.Add("MapSummaryUrl", x.GetTrashMapAssetUrl());return feature;
            }));
            return featureCollection;
        }
    }
}
