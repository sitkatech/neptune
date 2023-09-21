//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectLoadGeneratingUnit]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class ProjectLoadGeneratingUnitExtensionMethods
    {
        public static ProjectLoadGeneratingUnitDto AsDto(this ProjectLoadGeneratingUnit projectLoadGeneratingUnit)
        {
            var projectLoadGeneratingUnitDto = new ProjectLoadGeneratingUnitDto()
            {
                ProjectLoadGeneratingUnitID = projectLoadGeneratingUnit.ProjectLoadGeneratingUnitID,
                Project = projectLoadGeneratingUnit.Project.AsDto(),
                ModelBasin = projectLoadGeneratingUnit.ModelBasin?.AsDto(),
                RegionalSubbasin = projectLoadGeneratingUnit.RegionalSubbasin?.AsDto(),
                Delineation = projectLoadGeneratingUnit.Delineation?.AsDto(),
                WaterQualityManagementPlan = projectLoadGeneratingUnit.WaterQualityManagementPlan?.AsDto(),
                IsEmptyResponseFromHRUService = projectLoadGeneratingUnit.IsEmptyResponseFromHRUService
            };
            DoCustomMappings(projectLoadGeneratingUnit, projectLoadGeneratingUnitDto);
            return projectLoadGeneratingUnitDto;
        }

        static partial void DoCustomMappings(ProjectLoadGeneratingUnit projectLoadGeneratingUnit, ProjectLoadGeneratingUnitDto projectLoadGeneratingUnitDto);

        public static ProjectLoadGeneratingUnitSimpleDto AsSimpleDto(this ProjectLoadGeneratingUnit projectLoadGeneratingUnit)
        {
            var projectLoadGeneratingUnitSimpleDto = new ProjectLoadGeneratingUnitSimpleDto()
            {
                ProjectLoadGeneratingUnitID = projectLoadGeneratingUnit.ProjectLoadGeneratingUnitID,
                ProjectID = projectLoadGeneratingUnit.ProjectID,
                ModelBasinID = projectLoadGeneratingUnit.ModelBasinID,
                RegionalSubbasinID = projectLoadGeneratingUnit.RegionalSubbasinID,
                DelineationID = projectLoadGeneratingUnit.DelineationID,
                WaterQualityManagementPlanID = projectLoadGeneratingUnit.WaterQualityManagementPlanID,
                IsEmptyResponseFromHRUService = projectLoadGeneratingUnit.IsEmptyResponseFromHRUService
            };
            DoCustomSimpleDtoMappings(projectLoadGeneratingUnit, projectLoadGeneratingUnitSimpleDto);
            return projectLoadGeneratingUnitSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(ProjectLoadGeneratingUnit projectLoadGeneratingUnit, ProjectLoadGeneratingUnitSimpleDto projectLoadGeneratingUnitSimpleDto);
    }
}