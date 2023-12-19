using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Areas.Trash.Views.Shared
{
    public class TrashModuleMapViewDataForAngularBaseClass
    {
        public TrashModuleMapViewDataForAngularBaseClass(string geoServerUrl)
        {
            GeoServerUrl = geoServerUrl;
            ParcelMapServiceLayerName = "Parcels";
            LandUseBlockMapServiceLayerName = "LandUseBlocks";
            ParcelFieldDefinitionLabel = "Parcels";
        }

        public string GeoServerUrl { get; }
        public string ParcelMapServiceLayerName { get; }
        public string LandUseBlockMapServiceLayerName { get; }
        public string ParcelFieldDefinitionLabel { get; }
    }
}