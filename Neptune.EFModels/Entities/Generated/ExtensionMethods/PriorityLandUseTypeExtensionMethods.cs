//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PriorityLandUseType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class PriorityLandUseTypeExtensionMethods
    {
        public static PriorityLandUseTypeDto AsDto(this PriorityLandUseType priorityLandUseType)
        {
            var priorityLandUseTypeDto = new PriorityLandUseTypeDto()
            {
                PriorityLandUseTypeID = priorityLandUseType.PriorityLandUseTypeID,
                PriorityLandUseTypeName = priorityLandUseType.PriorityLandUseTypeName,
                PriorityLandUseTypeDisplayName = priorityLandUseType.PriorityLandUseTypeDisplayName,
                MapColorHexCode = priorityLandUseType.MapColorHexCode
            };
            DoCustomMappings(priorityLandUseType, priorityLandUseTypeDto);
            return priorityLandUseTypeDto;
        }

        static partial void DoCustomMappings(PriorityLandUseType priorityLandUseType, PriorityLandUseTypeDto priorityLandUseTypeDto);

        public static PriorityLandUseTypeSimpleDto AsSimpleDto(this PriorityLandUseType priorityLandUseType)
        {
            var priorityLandUseTypeSimpleDto = new PriorityLandUseTypeSimpleDto()
            {
                PriorityLandUseTypeID = priorityLandUseType.PriorityLandUseTypeID,
                PriorityLandUseTypeName = priorityLandUseType.PriorityLandUseTypeName,
                PriorityLandUseTypeDisplayName = priorityLandUseType.PriorityLandUseTypeDisplayName,
                MapColorHexCode = priorityLandUseType.MapColorHexCode
            };
            DoCustomSimpleDtoMappings(priorityLandUseType, priorityLandUseTypeSimpleDto);
            return priorityLandUseTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(PriorityLandUseType priorityLandUseType, PriorityLandUseTypeSimpleDto priorityLandUseTypeSimpleDto);
    }
}