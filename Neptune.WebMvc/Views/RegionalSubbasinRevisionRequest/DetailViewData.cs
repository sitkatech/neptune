﻿using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;

namespace Neptune.WebMvc.Views.RegionalSubbasinRevisionRequest
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


        public DetailViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.RegionalSubbasinRevisionRequest regionalSubbasinRevisionRequest, RegionalSubbasinRevisionRequestMapInitJson mapInitJson, string geoServerUrl) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
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