using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditViewData
    {
        public IEnumerable<SelectListItem> StormwaterJurisdictionSelectListItems { get; }
        public IEnumerable<WaterQualityManagementPlanPriority> WaterQualityManagementPlanPriorities { get; }
        public IEnumerable<WaterQualityManagementPlanStatus> WaterQualityManagementPlanStatuses { get; }
        public IEnumerable<WaterQualityManagementPlanDevelopmentType> WaterQualityManagementPlanDevelopmentTypes { get; }
        public IEnumerable<WaterQualityManagementPlanLandUse> WaterQualityManagementPlanLandUses { get; }

        public EditViewData(IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions)
        {
            StormwaterJurisdictionSelectListItems = stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
                .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(), x => x.GetOrganizationDisplayName());
            WaterQualityManagementPlanPriorities = WaterQualityManagementPlanPriority.All
                .OrderBy(x => x.SortOrder)
                .ToList();
            WaterQualityManagementPlanStatuses = WaterQualityManagementPlanStatus.All
                .OrderBy(x => x.SortOrder)
                .ToList();
            WaterQualityManagementPlanDevelopmentTypes = WaterQualityManagementPlanDevelopmentType.All
                .OrderBy(x => x.SortOrder)
                .ToList();
            WaterQualityManagementPlanLandUses = WaterQualityManagementPlanLandUse.All
                .OrderBy(x => x.SortOrder)
                .ToList();
        }
    }
}
