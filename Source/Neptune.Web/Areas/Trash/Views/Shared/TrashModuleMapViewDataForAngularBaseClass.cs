using Neptune.Web.Common;

namespace Neptune.Web.Areas.Trash.Views.Shared
{
    public class TrashModuleMapViewDataForAngularBaseClass
    {
        public TrashModuleMapViewDataForAngularBaseClass()
        {
            GeoServerUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;
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