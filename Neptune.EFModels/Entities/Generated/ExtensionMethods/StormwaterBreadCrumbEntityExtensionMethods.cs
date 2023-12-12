//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterBreadCrumbEntity]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class StormwaterBreadCrumbEntityExtensionMethods
    {

        public static StormwaterBreadCrumbEntitySimpleDto AsSimpleDto(this StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity)
        {
            var stormwaterBreadCrumbEntitySimpleDto = new StormwaterBreadCrumbEntitySimpleDto()
            {
                StormwaterBreadCrumbEntityID = stormwaterBreadCrumbEntity.StormwaterBreadCrumbEntityID,
                StormwaterBreadCrumbEntityName = stormwaterBreadCrumbEntity.StormwaterBreadCrumbEntityName,
                StormwaterBreadCrumbEntityDisplayName = stormwaterBreadCrumbEntity.StormwaterBreadCrumbEntityDisplayName,
                GlyphIconClass = stormwaterBreadCrumbEntity.GlyphIconClass,
                ColorClass = stormwaterBreadCrumbEntity.ColorClass
            };
            DoCustomSimpleDtoMappings(stormwaterBreadCrumbEntity, stormwaterBreadCrumbEntitySimpleDto);
            return stormwaterBreadCrumbEntitySimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity, StormwaterBreadCrumbEntitySimpleDto stormwaterBreadCrumbEntitySimpleDto);
    }
}