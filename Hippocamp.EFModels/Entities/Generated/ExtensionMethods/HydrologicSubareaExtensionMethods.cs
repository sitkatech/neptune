//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HydrologicSubarea]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class HydrologicSubareaExtensionMethods
    {
        public static HydrologicSubareaDto AsDto(this HydrologicSubarea hydrologicSubarea)
        {
            var hydrologicSubareaDto = new HydrologicSubareaDto()
            {
                HydrologicSubareaID = hydrologicSubarea.HydrologicSubareaID,
                HydrologicSubareaName = hydrologicSubarea.HydrologicSubareaName
            };
            DoCustomMappings(hydrologicSubarea, hydrologicSubareaDto);
            return hydrologicSubareaDto;
        }

        static partial void DoCustomMappings(HydrologicSubarea hydrologicSubarea, HydrologicSubareaDto hydrologicSubareaDto);

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