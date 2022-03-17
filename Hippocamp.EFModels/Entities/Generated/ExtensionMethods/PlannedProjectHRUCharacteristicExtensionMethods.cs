//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PlannedProjectHRUCharacteristic]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class PlannedProjectHRUCharacteristicExtensionMethods
    {
        public static PlannedProjectHRUCharacteristicDto AsDto(this PlannedProjectHRUCharacteristic plannedProjectHRUCharacteristic)
        {
            var plannedProjectHRUCharacteristicDto = new PlannedProjectHRUCharacteristicDto()
            {
                PlannedProjectHRUCharacteristicID = plannedProjectHRUCharacteristic.PlannedProjectHRUCharacteristicID,
                Project = plannedProjectHRUCharacteristic.Project.AsDto(),
                HydrologicSoilGroup = plannedProjectHRUCharacteristic.HydrologicSoilGroup,
                SlopePercentage = plannedProjectHRUCharacteristic.SlopePercentage,
                ImperviousAcres = plannedProjectHRUCharacteristic.ImperviousAcres,
                LastUpdated = plannedProjectHRUCharacteristic.LastUpdated,
                Area = plannedProjectHRUCharacteristic.Area,
                HRUCharacteristicLandUseCode = plannedProjectHRUCharacteristic.HRUCharacteristicLandUseCode.AsDto(),
                PlannedProjectLoadGeneratingUnit = plannedProjectHRUCharacteristic.PlannedProjectLoadGeneratingUnit.AsDto(),
                BaselineImperviousAcres = plannedProjectHRUCharacteristic.BaselineImperviousAcres,
                BaselineHRUCharacteristicLandUseCode = plannedProjectHRUCharacteristic.BaselineHRUCharacteristicLandUseCode.AsDto()
            };
            DoCustomMappings(plannedProjectHRUCharacteristic, plannedProjectHRUCharacteristicDto);
            return plannedProjectHRUCharacteristicDto;
        }

        static partial void DoCustomMappings(PlannedProjectHRUCharacteristic plannedProjectHRUCharacteristic, PlannedProjectHRUCharacteristicDto plannedProjectHRUCharacteristicDto);

        public static PlannedProjectHRUCharacteristicSimpleDto AsSimpleDto(this PlannedProjectHRUCharacteristic plannedProjectHRUCharacteristic)
        {
            var plannedProjectHRUCharacteristicSimpleDto = new PlannedProjectHRUCharacteristicSimpleDto()
            {
                PlannedProjectHRUCharacteristicID = plannedProjectHRUCharacteristic.PlannedProjectHRUCharacteristicID,
                ProjectID = plannedProjectHRUCharacteristic.ProjectID,
                HydrologicSoilGroup = plannedProjectHRUCharacteristic.HydrologicSoilGroup,
                SlopePercentage = plannedProjectHRUCharacteristic.SlopePercentage,
                ImperviousAcres = plannedProjectHRUCharacteristic.ImperviousAcres,
                LastUpdated = plannedProjectHRUCharacteristic.LastUpdated,
                Area = plannedProjectHRUCharacteristic.Area,
                HRUCharacteristicLandUseCodeID = plannedProjectHRUCharacteristic.HRUCharacteristicLandUseCodeID,
                PlannedProjectLoadGeneratingUnitID = plannedProjectHRUCharacteristic.PlannedProjectLoadGeneratingUnitID,
                BaselineImperviousAcres = plannedProjectHRUCharacteristic.BaselineImperviousAcres,
                BaselineHRUCharacteristicLandUseCodeID = plannedProjectHRUCharacteristic.BaselineHRUCharacteristicLandUseCodeID
            };
            DoCustomSimpleDtoMappings(plannedProjectHRUCharacteristic, plannedProjectHRUCharacteristicSimpleDto);
            return plannedProjectHRUCharacteristicSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(PlannedProjectHRUCharacteristic plannedProjectHRUCharacteristic, PlannedProjectHRUCharacteristicSimpleDto plannedProjectHRUCharacteristicSimpleDto);
    }
}