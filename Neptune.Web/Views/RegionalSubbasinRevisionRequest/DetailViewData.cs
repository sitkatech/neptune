using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Security;

namespace Neptune.Web.Views.RegionalSubbasinRevisionRequest
{
    public class DetailViewData : NeptuneViewData
    {
        public EFModels.Entities.RegionalSubbasinRevisionRequest RegionalSubbasinRevisionRequest { get; }
        public string SubmitUrl { get; }
        public RegionalSubbasinRevisionRequestMapInitJson MapInitJson { get; }
        public string GeoServerUrl { get; }
        public string MapFormID { get; }
        public string CloseUrl { get; }
        public bool CurrentPersonCanClose { get; }
        public bool HasAdminPermissions { get;  }
        public string DownloadUrl { get; }
        public string TreatmentBMPDetailUrl { get; }


        public DetailViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.RegionalSubbasinRevisionRequest regionalSubbasinRevisionRequest, RegionalSubbasinRevisionRequestMapInitJson mapInitJson, string geoServerUrl) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools)
        {
            RegionalSubbasinRevisionRequest = regionalSubbasinRevisionRequest;
            MapInitJson = mapInitJson;
            GeoServerUrl = geoServerUrl;
            MapFormID = "revisionRequestHiddenInputContainer";
            SubmitUrl =
                SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(regionalSubbasinRevisionRequest));
            EntityName = "Regional Subbasin";
            PageTitle = "Revision";
            EntityUrl = SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            CloseUrl = SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(linkGenerator, x =>
                x.Close(regionalSubbasinRevisionRequest));
            DownloadUrl = SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(linkGenerator, x =>
                x.Download(regionalSubbasinRevisionRequest));
            CurrentPersonCanClose =
                new RegionalSubbasinRevisionRequestCloseFeature().HasPermission(currentPerson,
                    regionalSubbasinRevisionRequest).HasPermission;
            HasAdminPermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            TreatmentBMPDetailUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(regionalSubbasinRevisionRequest.TreatmentBMPID));
        }
    }
}