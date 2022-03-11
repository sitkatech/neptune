//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ModelBasinStaging]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class ModelBasinStagingExtensionMethods
    {
        public static ModelBasinStagingDto AsDto(this ModelBasinStaging modelBasinStaging)
        {
            var modelBasinStagingDto = new ModelBasinStagingDto()
            {
                ModelBasinStagingID = modelBasinStaging.ModelBasinStagingID,
                ModelBasinKey = modelBasinStaging.ModelBasinKey,
                ModelBasinName = modelBasinStaging.ModelBasinName
            };
            DoCustomMappings(modelBasinStaging, modelBasinStagingDto);
            return modelBasinStagingDto;
        }

        static partial void DoCustomMappings(ModelBasinStaging modelBasinStaging, ModelBasinStagingDto modelBasinStagingDto);

        public static ModelBasinStagingSimpleDto AsSimpleDto(this ModelBasinStaging modelBasinStaging)
        {
            var modelBasinStagingSimpleDto = new ModelBasinStagingSimpleDto()
            {
                ModelBasinStagingID = modelBasinStaging.ModelBasinStagingID,
                ModelBasinKey = modelBasinStaging.ModelBasinKey,
                ModelBasinName = modelBasinStaging.ModelBasinName
            };
            DoCustomSimpleDtoMappings(modelBasinStaging, modelBasinStagingSimpleDto);
            return modelBasinStagingSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(ModelBasinStaging modelBasinStaging, ModelBasinStagingSimpleDto modelBasinStagingSimpleDto);
    }
}