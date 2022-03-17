//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PlannedProjectNereidResult]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class PlannedProjectNereidResultExtensionMethods
    {
        public static PlannedProjectNereidResultDto AsDto(this PlannedProjectNereidResult plannedProjectNereidResult)
        {
            var plannedProjectNereidResultDto = new PlannedProjectNereidResultDto()
            {
                PlannedProjectNereidResultID = plannedProjectNereidResult.PlannedProjectNereidResultID,
                Project = plannedProjectNereidResult.Project.AsDto(),
                IsBaselineCondition = plannedProjectNereidResult.IsBaselineCondition,
                TreatmentBMPID = plannedProjectNereidResult.TreatmentBMPID,
                WaterQualityManagementPlanID = plannedProjectNereidResult.WaterQualityManagementPlanID,
                RegionalSubbasinID = plannedProjectNereidResult.RegionalSubbasinID,
                NodeID = plannedProjectNereidResult.NodeID,
                FullResponse = plannedProjectNereidResult.FullResponse,
                LastUpdate = plannedProjectNereidResult.LastUpdate
            };
            DoCustomMappings(plannedProjectNereidResult, plannedProjectNereidResultDto);
            return plannedProjectNereidResultDto;
        }

        static partial void DoCustomMappings(PlannedProjectNereidResult plannedProjectNereidResult, PlannedProjectNereidResultDto plannedProjectNereidResultDto);

        public static PlannedProjectNereidResultSimpleDto AsSimpleDto(this PlannedProjectNereidResult plannedProjectNereidResult)
        {
            var plannedProjectNereidResultSimpleDto = new PlannedProjectNereidResultSimpleDto()
            {
                PlannedProjectNereidResultID = plannedProjectNereidResult.PlannedProjectNereidResultID,
                ProjectID = plannedProjectNereidResult.ProjectID,
                IsBaselineCondition = plannedProjectNereidResult.IsBaselineCondition,
                TreatmentBMPID = plannedProjectNereidResult.TreatmentBMPID,
                WaterQualityManagementPlanID = plannedProjectNereidResult.WaterQualityManagementPlanID,
                RegionalSubbasinID = plannedProjectNereidResult.RegionalSubbasinID,
                NodeID = plannedProjectNereidResult.NodeID,
                FullResponse = plannedProjectNereidResult.FullResponse,
                LastUpdate = plannedProjectNereidResult.LastUpdate
            };
            DoCustomSimpleDtoMappings(plannedProjectNereidResult, plannedProjectNereidResultSimpleDto);
            return plannedProjectNereidResultSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(PlannedProjectNereidResult plannedProjectNereidResult, PlannedProjectNereidResultSimpleDto plannedProjectNereidResultSimpleDto);
    }
}