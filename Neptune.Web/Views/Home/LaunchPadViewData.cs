using Neptune.EFModels.Entities;
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

        public LaunchPadViewData(Person currentPerson, EFModels.Entities.NeptunePage launchPadNeptunePage, int numberOfBmpTypes, string managerDashboardDescription, LinkGenerator linkGenerator)
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
            JurisdictionIndexUrl = ""; //todo SitkaRoute<JurisdictionController>.BuildUrlFromExpression(c => c.Index());
            RequestSupportUrl = SitkaRoute<HelpController>.BuildUrlFromExpression(linkGenerator, c => c.RequestToChangePrivileges());
            FindABmpUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, c => c.FindABMP());
            ExploreBmpTypesUrl = SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(linkGenerator, c => c.Index());
            AddABmpUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, c => c.New());
            ViewAssessmentAndMaintenanceRecordsUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, c => c.Index());
            ViewWaterQualityManagementPlansUrl = ""; //todo SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c => c.Index());
            FieldFieldVisitsUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, c => c.Index());
            InviteANewUserUrl = ""; //todo SitkaRoute<UserController>.BuildUrlFromExpression(c => c.Invite());
            EditUserRolesUrl = ""; //todo SitkaRoute<UserController>.BuildUrlFromExpression(c => c.Index());
            AddAFundingSourceUrl = SitkaRoute<FundingSourceController>.BuildUrlFromExpression(linkGenerator, c => c.Index());
            ManagerDashboardUrl = SitkaRoute<ManagerDashboardController>.BuildUrlFromExpression(linkGenerator, c => c.Index()); ;
            ViewDelineationReconciliationReportUrl = ""; //todo SitkaRoute<DelineationController>.BuildUrlFromExpression(c => c.DelineationReconciliationReport());
        }
    }
}
