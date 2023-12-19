using Neptune.EFModels.Entities;
using Neptune.WebMvc.Areas.Trash.Controllers;
using Neptune.WebMvc.Common;
using NetTopologySuite.Features;

namespace Neptune.WebMvc.Models
{
    public static class ParcelModelExtensions
    {
        public static FeatureCollection ToGeoJsonFeatureCollectionForTrashMap(this IEnumerable<Parcel> parcels,
            LinkGenerator linkGenerator)
        {
            UrlTemplate<int> trashMapAssetUrlTemplate = new(SitkaRoute<ParcelController>.BuildUrlFromExpression(linkGenerator, x => x.TrashMapAssetPanel(UrlTemplate.Parameter1Int)));
            var featureCollection = new FeatureCollection();
            foreach (var parcel in parcels.Where(x => x.ParcelGeometry != null))
            {
                var trashCaptureStatusType = parcel.GetTrashCaptureStatusType();
                var attributesTable = new AttributesTable
                {
                    { "Number", parcel.ParcelNumber },
                    { "FeatureColor", trashCaptureStatusType.FeatureColorOnTrashModuleMap() },
                    { "TrashCaptureStatusTypeID", trashCaptureStatusType.TrashCaptureStatusTypeID },
                    { "ParcelID", parcel.ParcelID },
                    { "MapSummaryUrl", trashMapAssetUrlTemplate.ParameterReplace(parcel.ParcelID) }
                };
                var feature = new Feature(parcel.ParcelGeometry.Geometry4326, attributesTable);
                featureCollection.Add(feature);
            }
            return featureCollection;
        }
    }
}
