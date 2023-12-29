//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StateProvince]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class StateProvinceExtensionMethods
    {
        public static StateProvinceSimpleDto AsSimpleDto(this StateProvince stateProvince)
        {
            var dto = new StateProvinceSimpleDto()
            {
                StateProvinceID = stateProvince.StateProvinceID,
                StateProvinceName = stateProvince.StateProvinceName,
                StateProvinceAbbreviation = stateProvince.StateProvinceAbbreviation
            };
            return dto;
        }
    }
}