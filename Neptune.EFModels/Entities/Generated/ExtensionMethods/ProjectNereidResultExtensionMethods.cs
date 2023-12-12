//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectNereidResult]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ProjectNereidResultExtensionMethods
    {
        public static ProjectNereidResultSimpleDto AsSimpleDto(this ProjectNereidResult projectNereidResult)
        {
            var dto = new ProjectNereidResultSimpleDto()
            {
                ProjectNereidResultID = projectNereidResult.ProjectNereidResultID,
                ProjectID = projectNereidResult.ProjectID,
                IsBaselineCondition = projectNereidResult.IsBaselineCondition,
                TreatmentBMPID = projectNereidResult.TreatmentBMPID,
                WaterQualityManagementPlanID = projectNereidResult.WaterQualityManagementPlanID,
                RegionalSubbasinID = projectNereidResult.RegionalSubbasinID,
                DelineationID = projectNereidResult.DelineationID,
                NodeID = projectNereidResult.NodeID,
                FullResponse = projectNereidResult.FullResponse,
                LastUpdate = projectNereidResult.LastUpdate
            };
            return dto;
        }
    }
}