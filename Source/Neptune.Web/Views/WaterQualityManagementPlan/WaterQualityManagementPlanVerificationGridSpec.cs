using System.Collections.Generic;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Views;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class WaterQualityManagementPlanVerificationGridSpec : GridSpec<Models.WaterQualityManagementPlanVerify>
    {
        public WaterQualityManagementPlanVerificationGridSpec(Person currentPerson)
        {
            // Reusing permissions checks from WQMPs for WQMP Verifications
            var waterQualityManagementPlanManageFeature = new WaterQualityManagementPlanManageFeature();
            var waterQualityManagementPlanDeleteFeature = new WaterQualityManagementPlanDeleteFeature();

            var currentUserCanManage = waterQualityManagementPlanManageFeature.HasPermissionByPerson(currentPerson);

            ObjectNameSingular = "Water Quality Management Plan O&M Verification";
            ObjectNamePlural = "Water Quality Management Plan O&M Verifications";
            SaveFiltersInCookie = true;
            var isAnonymousOrUnassigned = currentPerson.IsAnonymousOrUnassigned();
            if (currentUserCanManage)
            {
                Add(string.Empty, x =>
                    {
                        var userHasDeletePermission = waterQualityManagementPlanDeleteFeature
                            .HasPermission(currentPerson, x.WaterQualityManagementPlan).HasPermission;
                        return DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(),
                            userHasDeletePermission);
                    }, 26,
                    DhtmlxGridColumnFilterType.None);
            }

            if (!isAnonymousOrUnassigned)
            {
                Add(string.Empty,
                    x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), "View",
                        new Dictionary<string, string> {{"class", "gridButton"}}), 60,
                    DhtmlxGridColumnFilterType.None);
            }
            Add("WQMP Name", x => isAnonymousOrUnassigned ? new HtmlString(x.WaterQualityManagementPlan.WaterQualityManagementPlanName) : x.WaterQualityManagementPlan.GetDisplayNameAsUrl(), 300, DhtmlxGridColumnFilterType.Text);
            Add("Jurisdiction", x => isAnonymousOrUnassigned ? new HtmlString(x.WaterQualityManagementPlan.StormwaterJurisdiction.GetOrganizationDisplayName()) : x.WaterQualityManagementPlan.StormwaterJurisdiction.GetDisplayNameAsDetailUrl(), 150);
            Add("Verification Date", x => x.VerificationDate, 150);
            Add("Last Edited Date", x => x.LastEditedDate, 150);
            Add("Last Edited By", x => x.LastEditedByPerson.GetFullNameFirstLast(), 150, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Type of Verification",
                x => x.WaterQualityManagementPlanVerifyType.WaterQualityManagementPlanVerifyTypeName, 150, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Visit Status", x => x.WaterQualityManagementPlanVisitStatus.WaterQualityManagementPlanVisitStatusName,
                150, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Verification Status",
                x => x.WaterQualityManagementPlanVerifyStatus?.WaterQualityManagementPlanVerifyStatusName, 200, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Source Control Condition", x => x.SourceControlCondition, 150);
            Add("Enforcement of Follow-up Actions", x => x.EnforcementOrFollowupActions, 150);
            Add("Draft or Finalized",
                x => x.IsDraft ? "Draft" : "Finalized", 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
        }
    }
}

 