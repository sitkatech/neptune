using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using LtInfo.Common.GeoJson;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static class ParcelModelExtensions
    {
        public const string ParcelColor = "#fb00be";

        public static LayerGeoJson GetParcelWmsLayerGeoJson(string layerColor, decimal layerOpacity, LayerInitialVisibility layerInitialVisibility, TenantAttribute tenantAttribute)
        {
            return new LayerGeoJson(FieldDefinition.Parcel.GetFieldDefinitionLabelPluralized(),
                NeptuneWebConfiguration.ParcelMapServiceUrl, tenantAttribute.ParcelLayerName, "#", layerColor,
                layerOpacity, layerInitialVisibility);
        }

        public static FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<Parcel> parcels) =>
            new FeatureCollection(parcels.Select(MakeFeatureWithRelevantProperties).ToList());

        public static Feature MakeFeatureWithRelevantProperties(this Parcel parcel) =>
            DbGeometryToGeoJsonHelper.FromDbGeometry(parcel.ParcelGeometry);
    }
}
