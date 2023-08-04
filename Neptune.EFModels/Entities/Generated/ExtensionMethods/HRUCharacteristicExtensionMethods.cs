//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HRUCharacteristic]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class HRUCharacteristicExtensionMethods
    {
        public static HRUCharacteristicDto AsDto(this HRUCharacteristic hRUCharacteristic)
        {
            var hRUCharacteristicDto = new HRUCharacteristicDto()
            {
                HRUCharacteristicID = hRUCharacteristic.HRUCharacteristicID,
                HydrologicSoilGroup = hRUCharacteristic.HydrologicSoilGroup,
                SlopePercentage = hRUCharacteristic.SlopePercentage,
                ImperviousAcres = hRUCharacteristic.ImperviousAcres,
                LastUpdated = hRUCharacteristic.LastUpdated,
                Area = hRUCharacteristic.Area,
                HRUCharacteristicLandUseCode = hRUCharacteristic.HRUCharacteristicLandUseCode.AsDto(),
                LoadGeneratingUnit = hRUCharacteristic.LoadGeneratingUnit.AsDto(),
                BaselineImperviousAcres = hRUCharacteristic.BaselineImperviousAcres,
                BaselineHRUCharacteristicLandUseCode = hRUCharacteristic.BaselineHRUCharacteristicLandUseCode.AsDto()
            };
            DoCustomMappings(hRUCharacteristic, hRUCharacteristicDto);
            return hRUCharacteristicDto;
        }

        static partial void DoCustomMappings(HRUCharacteristic hRUCharacteristic, HRUCharacteristicDto hRUCharacteristicDto);

        public static HRUCharacteristicSimpleDto AsSimpleDto(this HRUCharacteristic hRUCharacteristic)
        {
            var hRUCharacteristicSimpleDto = new HRUCharacteristicSimpleDto()
            {
                HRUCharacteristicID = hRUCharacteristic.HRUCharacteristicID,
                HydrologicSoilGroup = hRUCharacteristic.HydrologicSoilGroup,
                SlopePercentage = hRUCharacteristic.SlopePercentage,
                ImperviousAcres = hRUCharacteristic.ImperviousAcres,
                LastUpdated = hRUCharacteristic.LastUpdated,
                Area = hRUCharacteristic.Area,
                HRUCharacteristicLandUseCodeID = hRUCharacteristic.HRUCharacteristicLandUseCodeID,
                LoadGeneratingUnitID = hRUCharacteristic.LoadGeneratingUnitID,
                BaselineImperviousAcres = hRUCharacteristic.BaselineImperviousAcres,
                BaselineHRUCharacteristicLandUseCodeID = hRUCharacteristic.BaselineHRUCharacteristicLandUseCodeID
            };
            DoCustomSimpleDtoMappings(hRUCharacteristic, hRUCharacteristicSimpleDto);
            return hRUCharacteristicSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(HRUCharacteristic hRUCharacteristic, HRUCharacteristicSimpleDto hRUCharacteristicSimpleDto);
    }
}