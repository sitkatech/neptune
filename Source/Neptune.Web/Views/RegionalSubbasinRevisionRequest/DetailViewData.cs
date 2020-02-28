using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.RegionalSubbasinRevisionRequest
{
    public class DetailViewData : NeptuneViewData
    {
        public Models.RegionalSubbasinRevisionRequest RegionalSubbasinRevisionRequest { get; }
        public string SubmitUrl { get; }
        public RegionalSubbasinRevisionRequestMapInitJson MapInitJson { get; }
        public string GeoServerUrl { get; }
        public string MapFormID { get; }
        public string CloseUrl { get; }
        public bool CurrentPersonCanClose { get; }
        public bool HasAdminPermissions { get;  }
        public string DownloadUrl { get; }


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
            EntityUrl = SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(x => x.Index());
            CloseUrl = SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(x =>
                x.Close(regionalSubbasinRevisionRequest));
            DownloadUrl = SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(x =>
                x.Download(regionalSubbasinRevisionRequest));
            CurrentPersonCanClose =
                new RegionalSubbasinRevisionRequestCloseFeature().HasPermission(currentPerson,
                    regionalSubbasinRevisionRequest).HasPermission;
            HasAdminPermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
        }

    }
}
