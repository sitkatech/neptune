using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views.Home
{
    public class LaunchPadViewData
    {
        public Person CurrentPerson { get; }
        public bool IsLoggedIn { get; }
        public bool IsAdmin { get; }
        public bool IsJurisdictionManager { get; }
        public IEnumerable<StormwaterJurisdiction> Jurisdictions { get; }
        public int NumberOfBmpTypes { get; set; }
        public ViewPageContentViewData LaunchPadViewPageContentViewData { get; }
        public string JurisdictionIndexUrl { get; }
        public string FindABmpUrl { get; set; }
        public string ExploreBmpTypesUrl { get; set; }
        public string AddABmpUrl { get; set; }
        public string InviteANewUserUrl { get; set; }
        public string AddAFundingSourceUrl { get; set; }
        public string ViewAssessmentAndMaintenanceRecordsUrl { get; set; }
        public string EditUserRolesUrl { get; set; }

        public LaunchPadViewData(Person currentPerson, Models.NeptunePage launchPadNeptunePage, int numberOfBmpTypes)
        {
            CurrentPerson = currentPerson;
            IsLoggedIn = !CurrentPerson.IsAnonymousUser;
            IsAdmin = new NeptuneAdminFeature().HasPermissionByPerson(CurrentPerson);
            IsJurisdictionManager = new JurisdictionManageFeature().HasPermissionByPerson(CurrentPerson);
            Jurisdictions = CurrentPerson.StormwaterJurisdictionPeople.Select(x => x.StormwaterJurisdiction).ToList();
            NumberOfBmpTypes = numberOfBmpTypes;
            LaunchPadViewPageContentViewData =
                new ViewPageContentViewData(launchPadNeptunePage, CurrentPerson);

            JurisdictionIndexUrl = SitkaRoute<JurisdictionController>.BuildUrlFromExpression(c => c.Index());
            FindABmpUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(c => c.FindABMP());
            ExploreBmpTypesUrl = SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(c => c.Index());
            AddABmpUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(c => c.New());
            ViewAssessmentAndMaintenanceRecordsUrl = SitkaRoute<AssessmentController>.BuildUrlFromExpression(c => c.Index());
            InviteANewUserUrl = "#"; // TODO add invite user url
            EditUserRolesUrl = SitkaRoute<UserController>.BuildUrlFromExpression(c => c.Index());
            AddAFundingSourceUrl = SitkaRoute<FundingSourceController>.BuildUrlFromExpression(c => c.Index());
        }
    }
}
