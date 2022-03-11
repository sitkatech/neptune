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
                ModelBasinName = modelBasin.ModelBasinName,
                LastUpdate = modelBasin.LastUpdate
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
                ModelBasinName = modelBasin.ModelBasinName,
                LastUpdate = modelBasin.LastUpdate
            };
            DoCustomSimpleDtoMappings(modelBasin, modelBasinSimpleDto);
            return modelBasinSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(ModelBasin modelBasin, ModelBasinSimpleDto modelBasinSimpleDto);
    }
}