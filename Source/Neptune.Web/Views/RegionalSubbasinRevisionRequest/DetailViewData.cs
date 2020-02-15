using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.RegionalSubbasinRevisionRequest
{
    public class DetailViewData : NeptuneViewData
    {
        public Models.RegionalSubbasinRevisionRequest RegionalSubbasinRevisionRequest { get; }
        public string SubmitUrl { get; }
        public RegionalSubbasinRevisionRequestMapInitJson MapInitJson { get; }
        public string GeoServerUrl { get; }
        public string MapFormID { get; }

        public DetailViewData(Person currentPerson, Models.RegionalSubbasinRevisionRequest regionalSubbasinRevisionRequest, RegionalSubbasinRevisionRequestMapInitJson mapInitJson) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            RegionalSubbasinRevisionRequest = regionalSubbasinRevisionRequest;
            MapInitJson = mapInitJson;
            GeoServerUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;
            MapFormID = "revisionRequestHiddenInputContainer";
            SubmitUrl =
                SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(x => x.Detail(regionalSubbasinRevisionRequest));
            EntityName = "Regional Subbasin";
            PageTitle = "Revision";
        }
    }
}