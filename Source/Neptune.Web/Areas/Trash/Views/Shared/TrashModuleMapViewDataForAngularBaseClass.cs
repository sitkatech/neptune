using Neptune.Web.Common;

namespace Neptune.Web.Areas.Trash.Views.Shared
{
    public class TrashModuleMapViewDataForAngularBaseClass
    {
        public TrashModuleMapViewDataForAngularBaseClass()
        {
            GeoServerUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;
        }

        public string GeoServerUrl { get; }
    }
}