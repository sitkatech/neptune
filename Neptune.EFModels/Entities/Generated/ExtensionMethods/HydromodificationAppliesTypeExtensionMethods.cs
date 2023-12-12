//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HydromodificationAppliesType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class HydromodificationAppliesTypeExtensionMethods
    {

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