//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectLoadGeneratingUnit]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ProjectLoadGeneratingUnitExtensionMethods
    {
        public static ProjectLoadGeneratingUnitSimpleDto AsSimpleDto(this ProjectLoadGeneratingUnit projectLoadGeneratingUnit)
        {
            var dto = new ProjectLoadGeneratingUnitSimpleDto()
            {
                ProjectLoadGeneratingUnitID = projectLoadGeneratingUnit.ProjectLoadGeneratingUnitID,
                ProjectID = projectLoadGeneratingUnit.ProjectID,
                ModelBasinID = projectLoadGeneratingUnit.ModelBasinID,
                RegionalSubbasinID = projectLoadGeneratingUnit.RegionalSubbasinID,
                DelineationID = projectLoadGeneratingUnit.DelineationID,
                WaterQualityManagementPlanID = projectLoadGeneratingUnit.WaterQualityManagementPlanID,
                IsEmptyResponseFromHRUService = projectLoadGeneratingUnit.IsEmptyResponseFromHRUService,
                DateHRURequested = projectLoadGeneratingUnit.DateHRURequested
            };
            return dto;
        }
    }
}