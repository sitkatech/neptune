//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HydromodificationAppliesType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class HydromodificationAppliesTypeExtensionMethods
    {
        public static HydromodificationAppliesTypeDto AsDto(this HydromodificationAppliesType hydromodificationAppliesType)
        {
            var hydromodificationAppliesTypeDto = new HydromodificationAppliesTypeDto()
            {
                HydromodificationAppliesTypeID = hydromodificationAppliesType.HydromodificationAppliesTypeID,
                HydromodificationAppliesTypeName = hydromodificationAppliesType.HydromodificationAppliesTypeName,
                HydromodificationAppliesTypeDisplayName = hydromodificationAppliesType.HydromodificationAppliesTypeDisplayName
            };
            DoCustomMappings(hydromodificationAppliesType, hydromodificationAppliesTypeDto);
            return hydromodificationAppliesTypeDto;
        }

        static partial void DoCustomMappings(HydromodificationAppliesType hydromodificationAppliesType, HydromodificationAppliesTypeDto hydromodificationAppliesTypeDto);

        public static HydromodificationAppliesTypeSimpleDto AsSimpleDto(this HydromodificationAppliesType hydromodificationAppliesType)
        {
            var hydromodificationAppliesTypeSimpleDto = new HydromodificationAppliesTypeSimpleDto()
            {
                HydromodificationAppliesTypeID = hydromodificationAppliesType.HydromodificationAppliesTypeID,
                HydromodificationAppliesTypeName = hydromodificationAppliesType.HydromodificationAppliesTypeName,
                HydromodificationAppliesTypeDisplayName = hydromodificationAppliesType.HydromodificationAppliesTypeDisplayName
            };
            DoCustomSimpleDtoMappings(hydromodificationAppliesType, hydromodificationAppliesTypeSimpleDto);
            return hydromodificationAppliesTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(HydromodificationAppliesType hydromodificationAppliesType, HydromodificationAppliesTypeSimpleDto hydromodificationAppliesTypeSimpleDto);
    }
}