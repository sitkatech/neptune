﻿using System.Collections.Generic;
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

        public LaunchPadViewData(Person currentPerson, Models.NeptunePage launchPadNeptunePage, int numberOfBmpTypes, string managerDashboardDescription)
        {
            CurrentPerson = currentPerson;
            IsLoggedIn = !CurrentPerson.IsAnonymousUser();
            IsAdmin = new NeptuneAdminFeature().HasPermissionByPerson(CurrentPerson);
            IsUnassigned = !CurrentPerson.IsAnonymousUser() && CurrentPerson.Role == Models.Role.Unassigned;
            IsJurisdictionManager = new JurisdictionManageFeature().HasPermissionByPerson(CurrentPerson);
            Jurisdictions = CurrentPerson.StormwaterJurisdictionPeople.Select(x => x.StormwaterJurisdiction).ToList();
            NumberOfBmpTypes = numberOfBmpTypes;
            ManagerDashboardDescription = managerDashboardDescription;
            LaunchPadViewPageContentViewData =
                new ViewPageContentViewData(launchPadNeptunePage, CurrentPerson);
            JurisdictionIndexUrl = SitkaRoute<JurisdictionController>.BuildUrlFromExpression(c => c.Index());
            RequestSupportUrl = SitkaRoute<HelpController>.BuildUrlFromExpression(c => c.RequestToChangePrivileges());
            FindABmpUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(c => c.FindABMP());
            ExploreBmpTypesUrl = SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(c => c.Index());
            AddABmpUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(c => c.New());
            ViewAssessmentAndMaintenanceRecordsUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(c => c.Index());
            ViewWaterQualityManagementPlansUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c => c.Index());
            FieldFieldVisitsUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(c => c.Index());
            InviteANewUserUrl = SitkaRoute<UserController>.BuildUrlFromExpression(c => c.Invite());
            EditUserRolesUrl = SitkaRoute<UserController>.BuildUrlFromExpression(c => c.Index());
            AddAFundingSourceUrl = SitkaRoute<FundingSourceController>.BuildUrlFromExpression(c => c.Index());
            ManagerDashboardUrl = SitkaRoute<ManagerDashboardController>.BuildUrlFromExpression(c => c.Index());
            ViewDelineationReconciliationReportUrl = SitkaRoute<DelineationController>.BuildUrlFromExpression(c => c.DelineationReconciliationReport());
        }
    }
}
