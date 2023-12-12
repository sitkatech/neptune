//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectHRUCharacteristic]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ProjectHRUCharacteristicExtensionMethods
    {

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