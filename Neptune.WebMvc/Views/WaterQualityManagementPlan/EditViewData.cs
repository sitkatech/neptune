using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class EditViewData
    {
        public IEnumerable<WaterQualityManagementPlanPriority> WaterQualityManagementPlanPriorities { get; }
        public IEnumerable<WaterQualityManagementPlanStatus> WaterQualityManagementPlanStatuses { get; }
        public IEnumerable<WaterQualityManagementPlanDevelopmentType> WaterQualityManagementPlanDevelopmentTypes { get; }
        public IEnumerable<SelectListItem> WaterQualityManagementPlanLandUses { get; }
        public IEnumerable<SelectListItem> WaterQualityManagementPlanPermitTerms { get; }
        public IEnumerable<SelectListItem> HydrologicSubareas { get; }
        public IEnumerable<HydromodificationAppliesType> HydromodificationAppliesTypes { get; }
        public IEnumerable<SelectListItem> TrashCaptureStatusTypes { get; }

        public EditViewData(List<HydrologicSubarea> hydrologicSubareas, IEnumerable<TrashCaptureStatusType> trashCaptureStatusTypes)
        {

            TrashCaptureStatusTypes = trashCaptureStatusTypes.ToSelectListWithDisabledEmptyFirstRow(
                x => x.TrashCaptureStatusTypeID.ToString(),
                x => x.TrashCaptureStatusTypeDisplayName.ToString());
            WaterQualityManagementPlanPriorities = WaterQualityManagementPlanPriority.All
                .OrderBy(x => x.WaterQualityManagementPlanPriorityDisplayName)
                .ToList();
            WaterQualityManagementPlanStatuses = WaterQualityManagementPlanStatus.All
                .OrderBy(x => x.WaterQualityManagementPlanStatusDisplayName)
                .ToList();
            WaterQualityManagementPlanDevelopmentTypes = WaterQualityManagementPlanDevelopmentType.All
                .OrderBy(x => x.WaterQualityManagementPlanDevelopmentTypeDisplayName)
                .ToList();
            WaterQualityManagementPlanLandUses = WaterQualityManagementPlanLandUse.All
                .OrderBy(x => x.SortOrder)
                .ToSelectListWithEmptyFirstRow(x => x.WaterQualityManagementPlanLandUseID.ToString(), x => x.WaterQualityManagementPlanLandUseDisplayName);
            WaterQualityManagementPlanPermitTerms = WaterQualityManagementPlanPermitTerm.All
                .OrderBy(x => x.WaterQualityManagementPlanPermitTermDisplayName)
                .ToSelectListWithEmptyFirstRow(x => x.WaterQualityManagementPlanPermitTermID.ToString(), x => x.WaterQualityManagementPlanPermitTermDisplayName);
            HydrologicSubareas = hydrologicSubareas.OrderBy(x => x.HydrologicSubareaName).ToSelectListWithEmptyFirstRow(x => x.HydrologicSubareaID.ToString(), x => x.HydrologicSubareaName);
            HydromodificationAppliesTypes = HydromodificationAppliesType.All.OrderBy(x => x.HydromodificationAppliesTypeDisplayName).ToList();
        }
    }
}
