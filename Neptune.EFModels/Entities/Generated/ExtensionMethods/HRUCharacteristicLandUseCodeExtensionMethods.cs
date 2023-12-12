//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HRUCharacteristicLandUseCode]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class HRUCharacteristicLandUseCodeExtensionMethods
    {

        public static HRUCharacteristicLandUseCodeSimpleDto AsSimpleDto(this HRUCharacteristicLandUseCode hRUCharacteristicLandUseCode)
        {
            var hRUCharacteristicLandUseCodeSimpleDto = new HRUCharacteristicLandUseCodeSimpleDto()
            {
                HRUCharacteristicLandUseCodeID = hRUCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID,
                HRUCharacteristicLandUseCodeName = hRUCharacteristicLandUseCode.HRUCharacteristicLandUseCodeName,
                HRUCharacteristicLandUseCodeDisplayName = hRUCharacteristicLandUseCode.HRUCharacteristicLandUseCodeDisplayName
            };
            DoCustomSimpleDtoMappings(hRUCharacteristicLandUseCode, hRUCharacteristicLandUseCodeSimpleDto);
            return hRUCharacteristicLandUseCodeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(HRUCharacteristicLandUseCode hRUCharacteristicLandUseCode, HRUCharacteristicLandUseCodeSimpleDto hRUCharacteristicLandUseCodeSimpleDto);
    }
}