//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ModelBasin]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class ModelBasinExtensionMethods
    {
        public static ModelBasinDto AsDto(this ModelBasin modelBasin)
        {
            var modelBasinDto = new ModelBasinDto()
            {
                ModelBasinID = modelBasin.ModelBasinID,
                ModelBasinKey = modelBasin.ModelBasinKey,
                LastUpdate = modelBasin.LastUpdate,
                ModelBasinState = modelBasin.ModelBasinState,
                ModelBasinRegion = modelBasin.ModelBasinRegion
            };
            DoCustomMappings(modelBasin, modelBasinDto);
            return modelBasinDto;
        }

        static partial void DoCustomMappings(ModelBasin modelBasin, ModelBasinDto modelBasinDto);

        public static ModelBasinSimpleDto AsSimpleDto(this ModelBasin modelBasin)
        {
            var modelBasinSimpleDto = new ModelBasinSimpleDto()
            {
                ModelBasinID = modelBasin.ModelBasinID,
                ModelBasinKey = modelBasin.ModelBasinKey,
                LastUpdate = modelBasin.LastUpdate,
                ModelBasinState = modelBasin.ModelBasinState,
                ModelBasinRegion = modelBasin.ModelBasinRegion
            };
            DoCustomSimpleDtoMappings(modelBasin, modelBasinSimpleDto);
            return modelBasinSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(ModelBasin modelBasin, ModelBasinSimpleDto modelBasinSimpleDto);
    }
}