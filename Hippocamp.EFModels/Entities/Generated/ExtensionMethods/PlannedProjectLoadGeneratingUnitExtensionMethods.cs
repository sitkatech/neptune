//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PlannedProjectLoadGeneratingUnit]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class PlannedProjectLoadGeneratingUnitExtensionMethods
    {
        public static PlannedProjectLoadGeneratingUnitDto AsDto(this PlannedProjectLoadGeneratingUnit plannedProjectLoadGeneratingUnit)
        {
            var plannedProjectLoadGeneratingUnitDto = new PlannedProjectLoadGeneratingUnitDto()
            {
                PlannedProjectLoadGeneratingUnitID = plannedProjectLoadGeneratingUnit.PlannedProjectLoadGeneratingUnitID,
                Project = plannedProjectLoadGeneratingUnit.Project.AsDto(),
                ModelBasin = plannedProjectLoadGeneratingUnit.ModelBasin?.AsDto(),
                RegionalSubbasin = plannedProjectLoadGeneratingUnit.RegionalSubbasin?.AsDto(),
                Delineation = plannedProjectLoadGeneratingUnit.Delineation?.AsDto(),
                WaterQualityManagementPlan = plannedProjectLoadGeneratingUnit.WaterQualityManagementPlan?.AsDto(),
                IsEmptyResponseFromHRUService = plannedProjectLoadGeneratingUnit.IsEmptyResponseFromHRUService
            };
            DoCustomMappings(plannedProjectLoadGeneratingUnit, plannedProjectLoadGeneratingUnitDto);
            return plannedProjectLoadGeneratingUnitDto;
        }

        static partial void DoCustomMappings(PlannedProjectLoadGeneratingUnit plannedProjectLoadGeneratingUnit, PlannedProjectLoadGeneratingUnitDto plannedProjectLoadGeneratingUnitDto);

        public static PlannedProjectLoadGeneratingUnitSimpleDto AsSimpleDto(this PlannedProjectLoadGeneratingUnit plannedProjectLoadGeneratingUnit)
        {
            var plannedProjectLoadGeneratingUnitSimpleDto = new PlannedProjectLoadGeneratingUnitSimpleDto()
            {
                PlannedProjectLoadGeneratingUnitID = plannedProjectLoadGeneratingUnit.PlannedProjectLoadGeneratingUnitID,
                ProjectID = plannedProjectLoadGeneratingUnit.ProjectID,
                ModelBasinID = plannedProjectLoadGeneratingUnit.ModelBasinID,
                RegionalSubbasinID = plannedProjectLoadGeneratingUnit.RegionalSubbasinID,
                DelineationID = plannedProjectLoadGeneratingUnit.DelineationID,
                WaterQualityManagementPlanID = plannedProjectLoadGeneratingUnit.WaterQualityManagementPlanID,
                IsEmptyResponseFromHRUService = plannedProjectLoadGeneratingUnit.IsEmptyResponseFromHRUService
            };
            DoCustomSimpleDtoMappings(plannedProjectLoadGeneratingUnit, plannedProjectLoadGeneratingUnitSimpleDto);
            return plannedProjectLoadGeneratingUnitSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(PlannedProjectLoadGeneratingUnit plannedProjectLoadGeneratingUnit, PlannedProjectLoadGeneratingUnitSimpleDto plannedProjectLoadGeneratingUnitSimpleDto);
    }
}