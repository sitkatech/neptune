﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class NewViewData
    {
        public IEnumerable<SelectListItem> StormwaterJurisdictionSelectListItems { get; }
        public IEnumerable<WaterQualityManagementPlanPriority> WaterQualityManagementPlanPriorities { get; }
        public IEnumerable<WaterQualityManagementPlanStatus> WaterQualityManagementPlanStatuses { get; }
        public IEnumerable<WaterQualityManagementPlanDevelopmentType> WaterQualityManagementPlanDevelopmentTypes { get; }
        public IEnumerable<WaterQualityManagementPlanLandUse> WaterQualityManagementPlanLandUses { get; }

        public NewViewData(IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions)
        {
            StormwaterJurisdictionSelectListItems = stormwaterJurisdictions.OrderBy(x => x.OrganizationDisplayName)
                .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(), x => x.OrganizationDisplayName);
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
