using System.Collections.Generic;
using System.Globalization;
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
        public IEnumerable<SelectListItem> WaterQualityManagementPlanLandUses { get; }
        public IEnumerable<SelectListItem> WaterQualityManagementPlanPermitTerms { get; }
        public IEnumerable<SelectListItem> HydrologicSubareas { get; }
        public IEnumerable<HydromodificationAppliesType> HydromodificationAppliesTypes { get; }
        public IEnumerable<SelectListItem> TrashCaptureStatusTypes { get; }

        public EditViewData(IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions,
            List<HydrologicSubarea> hydrologicSubareas, IEnumerable<TrashCaptureStatusType> trashCaptureStatusTypes)
        {

            TrashCaptureStatusTypes = trashCaptureStatusTypes.ToSelectListWithDisabledEmptyFirstRow(
                x => x.TrashCaptureStatusTypeID.ToString(CultureInfo.InvariantCulture),
                x => x.TrashCaptureStatusTypeDisplayName.ToString(CultureInfo.InvariantCulture));
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
                .ToSelectListWithEmptyFirstRow(x => x.WaterQualityManagementPlanLandUseID.ToString(), x => x.WaterQualityManagementPlanLandUseDisplayName);
            WaterQualityManagementPlanPermitTerms = WaterQualityManagementPlanPermitTerm.All
                .OrderBy(x => x.WaterQualityManagementPlanPermitTermDisplayName)
                .ToSelectListWithEmptyFirstRow(x => x.WaterQualityManagementPlanPermitTermID.ToString(), x => x.WaterQualityManagementPlanPermitTermDisplayName);
            HydrologicSubareas = hydrologicSubareas.OrderBy(x => x.HydrologicSubareaName).ToSelectListWithEmptyFirstRow(x => x.HydrologicSubareaID.ToString(), x => x.HydrologicSubareaName);
            HydromodificationAppliesTypes = Models.HydromodificationAppliesType.All.OrderBy(x => x.SortOrder).ToList();
        }
    }
}
