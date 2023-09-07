﻿using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views.Home
{
    public class LaunchPadViewData
    {
        public Person CurrentPerson { get; }
        public bool IsLoggedIn { get; }
        public bool IsAdmin { get; }
        public bool IsUnassigned { get; }
        public bool IsJurisdictionManager { get; }
        public IEnumerable<StormwaterJurisdiction> Jurisdictions { get; }
        public int NumberOfBmpTypes { get; set; }
        public ViewPageContentViewData LaunchPadViewPageContentViewData { get; }
        public string ManagerDashboardDescription { get;  }

        public string JurisdictionIndexUrl { get; }
        public string RequestSupportUrl { get; set; }
        public string FindABmpUrl { get; set; }
        public string ExploreBmpTypesUrl { get; set; }
        public string AddABmpUrl { get; set; }
        public string InviteANewUserUrl { get; set; }
        public string AddAFundingSourceUrl { get; set; }
        public string FieldFieldVisitsUrl { get; set; }
        public string EditUserRolesUrl { get; set; }
        public string ViewAssessmentAndMaintenanceRecordsUrl { get; }
        public string ViewWaterQualityManagementPlansUrl { get; }
        public string ManagerDashboardUrl { get; }
        public string ViewDelineationReconciliationReportUrl { get; }

        public LaunchPadViewData(LinkGenerator linkGenerator, Person currentPerson, NeptunePage launchPadNeptunePage,
            int numberOfBmpTypes, string managerDashboardDescription)
        {
            CurrentPerson = currentPerson;
            IsLoggedIn = !CurrentPerson.IsAnonymousUser();
            IsAdmin = new NeptuneAdminFeature().HasPermissionByPerson(CurrentPerson);
            IsUnassigned = !CurrentPerson.IsAnonymousUser() && CurrentPerson.RoleID == EFModels.Entities.Role.Unassigned.RoleID;
            IsJurisdictionManager = new JurisdictionManageFeature().HasPermissionByPerson(CurrentPerson);
            Jurisdictions = CurrentPerson.StormwaterJurisdictionPeople.Select(x => x.StormwaterJurisdiction).ToList();
            NumberOfBmpTypes = numberOfBmpTypes;
            ManagerDashboardDescription = managerDashboardDescription;
            LaunchPadViewPageContentViewData = new ViewPageContentViewData(launchPadNeptunePage, CurrentPerson, linkGenerator);
            JurisdictionIndexUrl = ""; //todo SitkaRoute<JurisdictionController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            RequestSupportUrl = SitkaRoute<HelpController>.BuildUrlFromExpression(linkGenerator, x => x.RequestToChangePrivileges());
            FindABmpUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.FindABMP());
            ExploreBmpTypesUrl = SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            AddABmpUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.New());
            ViewAssessmentAndMaintenanceRecordsUrl = ""; //todo SitkaRoute<FieldVisitController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            ViewWaterQualityManagementPlansUrl = ""; //todo SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            FieldFieldVisitsUrl = ""; //todo SitkaRoute<FieldVisitController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            InviteANewUserUrl = ""; //todo SitkaRoute<UserController>.BuildUrlFromExpression(LinkGenerator, x => x.Invite());
            EditUserRolesUrl = ""; //todo SitkaRoute<UserController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            AddAFundingSourceUrl = SitkaRoute<FundingSourceController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            ManagerDashboardUrl = ""; //todo SitkaRoute<ManagerDashboardController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            ViewDelineationReconciliationReportUrl = ""; //todo SitkaRoute<DelineationController>.BuildUrlFromExpression(LinkGenerator, x => x.DelineationReconciliationReport());
        }
    }
}
