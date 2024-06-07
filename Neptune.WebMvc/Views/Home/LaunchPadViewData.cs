using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.Shared;

namespace Neptune.WebMvc.Views.Home
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
        public string AddAFundingSourceUrl { get; set; }
        public string FieldVisitsUrl { get; set; }
        public string EditUserRolesUrl { get; set; }
        public string ViewWaterQualityManagementPlansUrl { get; }
        public string ManagerDashboardUrl { get; }
        public string ViewDelineationReconciliationReportUrl { get; }
        public UrlTemplate<int> StormwaterJurisdictionUrlTemplate { get; }

        public LaunchPadViewData(LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.NeptunePage launchPadNeptunePage,
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
            LaunchPadViewPageContentViewData = new ViewPageContentViewData(linkGenerator, launchPadNeptunePage, CurrentPerson);
            JurisdictionIndexUrl = SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            RequestSupportUrl = SitkaRoute<HelpController>.BuildUrlFromExpression(linkGenerator, x => x.RequestToChangePrivileges());
            FindABmpUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.FindABMP());
            ExploreBmpTypesUrl = SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            AddABmpUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.New());
            var fieldVisitsUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            ViewWaterQualityManagementPlansUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            FieldVisitsUrl = fieldVisitsUrl;
            EditUserRolesUrl = SitkaRoute<UserController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            AddAFundingSourceUrl = SitkaRoute<FundingSourceController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            ManagerDashboardUrl = SitkaRoute<ManagerDashboardController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            ViewDelineationReconciliationReportUrl = SitkaRoute<DelineationController>.BuildUrlFromExpression(linkGenerator, x => x.DelineationReconciliationReport());
            StormwaterJurisdictionUrlTemplate = new UrlTemplate<int>(
                SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator,
                    x => x.Detail(UrlTemplate.Parameter1Int)));
        }
    }
}
