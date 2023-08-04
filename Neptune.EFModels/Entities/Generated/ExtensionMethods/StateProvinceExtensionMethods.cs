//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StateProvince]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class StateProvinceExtensionMethods
    {
        public static StateProvinceDto AsDto(this StateProvince stateProvince)
        {
            var stateProvinceDto = new StateProvinceDto()
            {
                StateProvinceID = stateProvince.StateProvinceID,
                StateProvinceName = stateProvince.StateProvinceName,
                StateProvinceAbbreviation = stateProvince.StateProvinceAbbreviation
            };
            DoCustomMappings(stateProvince, stateProvinceDto);
            return stateProvinceDto;
        }

        static partial void DoCustomMappings(StateProvince stateProvince, StateProvinceDto stateProvinceDto);

        public static StateProvinceSimpleDto AsSimpleDto(this StateProvince stateProvince)
        {
            var stateProvinceSimpleDto = new StateProvinceSimpleDto()
            {
                StateProvinceID = stateProvince.StateProvinceID,
                StateProvinceName = stateProvince.StateProvinceName,
                StateProvinceAbbreviation = stateProvince.StateProvinceAbbreviation
            };
            DoCustomSimpleDtoMappings(stateProvince, stateProvinceSimpleDto);
            return stateProvinceSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(StateProvince stateProvince, StateProvinceSimpleDto stateProvinceSimpleDto);
    }
}