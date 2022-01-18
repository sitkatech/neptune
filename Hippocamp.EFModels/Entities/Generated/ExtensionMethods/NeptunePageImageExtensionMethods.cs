//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePageImage]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class NeptunePageImageExtensionMethods
    {
        public static NeptunePageImageDto AsDto(this NeptunePageImage neptunePageImage)
        {
            var neptunePageImageDto = new NeptunePageImageDto()
            {
                NeptunePageImageID = neptunePageImage.NeptunePageImageID,
                NeptunePage = neptunePageImage.NeptunePage.AsDto(),
                FileResource = neptunePageImage.FileResource.AsDto()
            };
            DoCustomMappings(neptunePageImage, neptunePageImageDto);
            return neptunePageImageDto;
        }

        static partial void DoCustomMappings(NeptunePageImage neptunePageImage, NeptunePageImageDto neptunePageImageDto);

        public static NeptunePageImageSimpleDto AsSimpleDto(this NeptunePageImage neptunePageImage)
        {
            var neptunePageImageSimpleDto = new NeptunePageImageSimpleDto()
            {
                NeptunePageImageID = neptunePageImage.NeptunePageImageID,
                NeptunePageID = neptunePageImage.NeptunePageID,
                FileResourceID = neptunePageImage.FileResourceID
            };
            DoCustomSimpleDtoMappings(neptunePageImage, neptunePageImageSimpleDto);
            return neptunePageImageSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(NeptunePageImage neptunePageImage, NeptunePageImageSimpleDto neptunePageImageSimpleDto);
    }
}