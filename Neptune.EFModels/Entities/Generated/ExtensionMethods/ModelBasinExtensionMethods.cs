//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ModelBasin]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ModelBasinExtensionMethods
    {
        public static ModelBasinSimpleDto AsSimpleDto(this ModelBasin modelBasin)
        {
            var dto = new ModelBasinSimpleDto()
            {
                ModelBasinID = modelBasin.ModelBasinID,
                ModelBasinKey = modelBasin.ModelBasinKey,
                LastUpdate = modelBasin.LastUpdate,
                ModelBasinState = modelBasin.ModelBasinState,
                ModelBasinRegion = modelBasin.ModelBasinRegion
            };
            return dto;
        }
    }
}