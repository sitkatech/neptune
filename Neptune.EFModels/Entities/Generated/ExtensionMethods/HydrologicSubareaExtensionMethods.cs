//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HydrologicSubarea]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class HydrologicSubareaExtensionMethods
    {
        public static HydrologicSubareaSimpleDto AsSimpleDto(this HydrologicSubarea hydrologicSubarea)
        {
            var dto = new HydrologicSubareaSimpleDto()
            {
                HydrologicSubareaID = hydrologicSubarea.HydrologicSubareaID,
                HydrologicSubareaName = hydrologicSubarea.HydrologicSubareaName
            };
            return dto;
        }
    }
}