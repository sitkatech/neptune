//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectHRUCharacteristic]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class ProjectHRUCharacteristicExtensionMethods
    {
        public static ProjectHRUCharacteristicDto AsDto(this ProjectHRUCharacteristic projectHRUCharacteristic)
        {
            var projectHRUCharacteristicDto = new ProjectHRUCharacteristicDto()
            {
                ProjectHRUCharacteristicID = projectHRUCharacteristic.ProjectHRUCharacteristicID,
                Project = projectHRUCharacteristic.Project.AsDto(),
                HydrologicSoilGroup = projectHRUCharacteristic.HydrologicSoilGroup,
                SlopePercentage = projectHRUCharacteristic.SlopePercentage,
                ImperviousAcres = projectHRUCharacteristic.ImperviousAcres,
                LastUpdated = projectHRUCharacteristic.LastUpdated,
                Area = projectHRUCharacteristic.Area,
                HRUCharacteristicLandUseCode = projectHRUCharacteristic.HRUCharacteristicLandUseCode.AsDto(),
                ProjectLoadGeneratingUnit = projectHRUCharacteristic.ProjectLoadGeneratingUnit.AsDto(),
                BaselineImperviousAcres = projectHRUCharacteristic.BaselineImperviousAcres,
                BaselineHRUCharacteristicLandUseCode = projectHRUCharacteristic.BaselineHRUCharacteristicLandUseCode.AsDto()
            };
            DoCustomMappings(projectHRUCharacteristic, projectHRUCharacteristicDto);
            return projectHRUCharacteristicDto;
        }

        static partial void DoCustomMappings(ProjectHRUCharacteristic projectHRUCharacteristic, ProjectHRUCharacteristicDto projectHRUCharacteristicDto);

        public static ProjectHRUCharacteristicSimpleDto AsSimpleDto(this ProjectHRUCharacteristic projectHRUCharacteristic)
        {
            var projectHRUCharacteristicSimpleDto = new ProjectHRUCharacteristicSimpleDto()
            {
                ProjectHRUCharacteristicID = projectHRUCharacteristic.ProjectHRUCharacteristicID,
                ProjectID = projectHRUCharacteristic.ProjectID,
                HydrologicSoilGroup = projectHRUCharacteristic.HydrologicSoilGroup,
                SlopePercentage = projectHRUCharacteristic.SlopePercentage,
                ImperviousAcres = projectHRUCharacteristic.ImperviousAcres,
                LastUpdated = projectHRUCharacteristic.LastUpdated,
                Area = projectHRUCharacteristic.Area,
                HRUCharacteristicLandUseCodeID = projectHRUCharacteristic.HRUCharacteristicLandUseCodeID,
                ProjectLoadGeneratingUnitID = projectHRUCharacteristic.ProjectLoadGeneratingUnitID,
                BaselineImperviousAcres = projectHRUCharacteristic.BaselineImperviousAcres,
                BaselineHRUCharacteristicLandUseCodeID = projectHRUCharacteristic.BaselineHRUCharacteristicLandUseCodeID
            };
            DoCustomSimpleDtoMappings(projectHRUCharacteristic, projectHRUCharacteristicSimpleDto);
            return projectHRUCharacteristicSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(ProjectHRUCharacteristic projectHRUCharacteristic, ProjectHRUCharacteristicSimpleDto projectHRUCharacteristicSimpleDto);
    }
}