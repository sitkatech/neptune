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
            var hydrologicSubareaSimpleDto = new HydrologicSubareaSimpleDto()
            {
                HydrologicSubareaID = hydrologicSubarea.HydrologicSubareaID,
                HydrologicSubareaName = hydrologicSubarea.HydrologicSubareaName
            };
            DoCustomSimpleDtoMappings(hydrologicSubarea, hydrologicSubareaSimpleDto);
            return hydrologicSubareaSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(HydrologicSubarea hydrologicSubarea, HydrologicSubareaSimpleDto hydrologicSubareaSimpleDto);
    }
}