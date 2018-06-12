using System.Collections.Generic;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditViewData
    {
        public IEnumerable<SelectListItem> StormwaterJurisdictionSelectListItems { get; }
        public IEnumerable<SelectListItem> WaterQualityManagementPlanPrioritySelectListItems { get; }
        public IEnumerable<SelectListItem> WaterQualityManagementPlanStatusSelectListItems { get; }
        public IEnumerable<SelectListItem> WaterQualityManagementPlanDevelopmentTypeSelectListItems { get; }
        public IEnumerable<SelectListItem> WaterQualityManagementPlanLandUseSelectListItems { get; }

        public EditViewData(IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions, IEnumerable<Person> people,
            IEnumerable<Models.Organization> organizations)
        {
            StormwaterJurisdictionSelectListItems =
                stormwaterJurisdictions.ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(),
                    x => x.OrganizationDisplayName);
            WaterQualityManagementPlanPrioritySelectListItems = WaterQualityManagementPlanPriority.All.ToSelectListWithEmptyFirstRow(
                x => x.WaterQualityManagementPlanPriorityID.ToString(),
                x => x.WaterQualityManagementPlanPriorityDisplayName);
            WaterQualityManagementPlanStatusSelectListItems = WaterQualityManagementPlanStatus.All.ToSelectListWithEmptyFirstRow(
                x => x.WaterQualityManagementPlanStatusID.ToString(),
                x => x.WaterQualityManagementPlanStatusDisplayName);
            WaterQualityManagementPlanDevelopmentTypeSelectListItems =
                WaterQualityManagementPlanDevelopmentType.All.ToSelectListWithEmptyFirstRow(
                    x => x.WaterQualityManagementPlanDevelopmentTypeID.ToString(),
                    x => x.WaterQualityManagementPlanDevelopmentTypeDisplayName);
            WaterQualityManagementPlanLandUseSelectListItems = WaterQualityManagementPlanLandUse.All.ToSelectListWithEmptyFirstRow(
                x => x.WaterQualityManagementPlanLandUseID.ToString(),
                x => x.WaterQualityManagementPlanLandUseDisplayName);
        }
    }
}
