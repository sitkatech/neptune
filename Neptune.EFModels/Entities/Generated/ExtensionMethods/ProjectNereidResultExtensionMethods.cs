//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectNereidResult]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ProjectNereidResultExtensionMethods
    {
        public static ProjectNereidResultDto AsDto(this ProjectNereidResult projectNereidResult)
        {
            var projectNereidResultDto = new ProjectNereidResultDto()
            {
                ProjectNereidResultID = projectNereidResult.ProjectNereidResultID,
                Project = projectNereidResult.Project.AsDto(),
                IsBaselineCondition = projectNereidResult.IsBaselineCondition,
                TreatmentBMP = projectNereidResult.TreatmentBMP?.AsDto(),
                WaterQualityManagementPlan = projectNereidResult.WaterQualityManagementPlan?.AsDto(),
                RegionalSubbasin = projectNereidResult.RegionalSubbasin?.AsDto(),
                Delineation = projectNereidResult.Delineation?.AsDto(),
                NodeID = projectNereidResult.NodeID,
                FullResponse = projectNereidResult.FullResponse,
                LastUpdate = projectNereidResult.LastUpdate
            };
            DoCustomMappings(projectNereidResult, projectNereidResultDto);
            return projectNereidResultDto;
        }

        static partial void DoCustomMappings(ProjectNereidResult projectNereidResult, ProjectNereidResultDto projectNereidResultDto);

        public static ProjectNereidResultSimpleDto AsSimpleDto(this ProjectNereidResult projectNereidResult)
        {
            var projectNereidResultSimpleDto = new ProjectNereidResultSimpleDto()
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
            DoCustomSimpleDtoMappings(projectNereidResult, projectNereidResultSimpleDto);
            return projectNereidResultSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(ProjectNereidResult projectNereidResult, ProjectNereidResultSimpleDto projectNereidResultSimpleDto);
    }
}