using Neptune.EFModels.Entities;
using NetTopologySuite.Features;

namespace Neptune.WebMvc.Models
{
    public static class ParcelModelExtensions
    {
        public static FeatureCollection ToGeoJsonFeatureCollectionForTrashMap(this IEnumerable<Parcel> parcels)
        {
            var featureCollection = new FeatureCollection();
            foreach (var parcel in parcels.Where(x => x.ParcelGeometry != null))
            {
                var trashCaptureStatusType = parcel.GetTrashCaptureStatusType();
                var attributesTable = new AttributesTable
                {
                    { "FeatureColor", trashCaptureStatusType.FeatureColorOnTrashModuleMap() },
                    { "TrashCaptureStatusTypeID", trashCaptureStatusType.TrashCaptureStatusTypeID },
                    { "ParcelID", parcel.ParcelID },
                };
                var feature = new Feature(parcel.ParcelGeometry.Geometry4326, attributesTable);
                featureCollection.Add(feature);
            }
            return featureCollection;
        }
    }
}
