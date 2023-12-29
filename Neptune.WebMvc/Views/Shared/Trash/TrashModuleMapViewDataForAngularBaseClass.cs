namespace Neptune.WebMvc.Views.Shared.Trash
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