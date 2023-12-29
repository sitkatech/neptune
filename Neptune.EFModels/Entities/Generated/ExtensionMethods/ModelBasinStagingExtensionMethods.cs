//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ModelBasinStaging]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ModelBasinStagingExtensionMethods
    {
        public static ModelBasinStagingSimpleDto AsSimpleDto(this ModelBasinStaging modelBasinStaging)
        {
            var dto = new ModelBasinStagingSimpleDto()
            {
                ModelBasinStagingID = modelBasinStaging.ModelBasinStagingID,
                ModelBasinKey = modelBasinStaging.ModelBasinKey,
                ModelBasinState = modelBasinStaging.ModelBasinState,
                ModelBasinRegion = modelBasinStaging.ModelBasinRegion
            };
            return dto;
        }
    }
}