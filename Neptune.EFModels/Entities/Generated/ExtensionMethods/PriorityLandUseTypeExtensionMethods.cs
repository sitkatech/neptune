//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PriorityLandUseType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class PriorityLandUseTypeExtensionMethods
    {
        public static PriorityLandUseTypeSimpleDto AsSimpleDto(this PriorityLandUseType priorityLandUseType)
        {
            var dto = new PriorityLandUseTypeSimpleDto()
            {
                PriorityLandUseTypeID = priorityLandUseType.PriorityLandUseTypeID,
                PriorityLandUseTypeName = priorityLandUseType.PriorityLandUseTypeName,
                PriorityLandUseTypeDisplayName = priorityLandUseType.PriorityLandUseTypeDisplayName,
                MapColorHexCode = priorityLandUseType.MapColorHexCode
            };
            return dto;
        }
    }
}