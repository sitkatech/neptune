using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.DroolTool.Views.Home
{
    public class IndexViewData : DroolToolModuleViewData
    {
        public IndexViewData(Person currentPerson, NeptunePage neptunePage) : base(currentPerson, neptunePage, true)
        {
            EntityName = "Urban Drool Tool";
            PageTitle = "Welcome";

            GeoserverUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;
            StormwaterMapInitJson = new StormwaterMapInitJson("droolToolMap");
        }

        public StormwaterMapInitJson StormwaterMapInitJson { get; }
        public string GeoserverUrl { get; }
    }
}