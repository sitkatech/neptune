﻿using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GeoJson;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static class ParcelModelExtensions
    {
        public static readonly UrlTemplate<int> TrashMapAssetUrlTemplate = new(SitkaRoute<ParcelController>.BuildUrlFromExpression(t => t.TrashMapAssetPanel(UrlTemplate.Parameter1Int)));

        public static string GetTrashMapAssetUrl(this Parcel parcel)
        {
            return TrashMapAssetUrlTemplate.ParameterReplace(parcel.ParcelID);
        }

        public static FeatureCollection ToGeoJsonFeatureCollectionForTrashMap(this IEnumerable<Parcel> parcels)
        {
            var featureCollection = new FeatureCollection();
            featureCollection.Features.AddRange(parcels.Where(x => x.ParcelGeometry != null).Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithNoReproject(x.ParcelGeometry.Geometry4326);
                var trashCaptureStatusType = x.GetTrashCaptureStatusType();
                feature.Properties.Add("Number", x.ParcelNumber);
                feature.Properties.Add("FeatureColor", trashCaptureStatusType.FeatureColorOnTrashModuleMap());
                feature.Properties.Add("TrashCaptureStatusTypeID", trashCaptureStatusType.TrashCaptureStatusTypeID);
                feature.Properties.Add("ParcelID", x.ParcelID);
                feature.Properties.Add("MapSummaryUrl", x.GetTrashMapAssetUrl());
                return feature;
            }));
            return featureCollection;
        }
    }
}
