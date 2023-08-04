//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptuneHomePageImage]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class NeptuneHomePageImageExtensionMethods
    {
        public static NeptuneHomePageImageDto AsDto(this NeptuneHomePageImage neptuneHomePageImage)
        {
            var neptuneHomePageImageDto = new NeptuneHomePageImageDto()
            {
                NeptuneHomePageImageID = neptuneHomePageImage.NeptuneHomePageImageID,
                FileResource = neptuneHomePageImage.FileResource.AsDto(),
                Caption = neptuneHomePageImage.Caption,
                SortOrder = neptuneHomePageImage.SortOrder
            };
            DoCustomMappings(neptuneHomePageImage, neptuneHomePageImageDto);
            return neptuneHomePageImageDto;
        }

        static partial void DoCustomMappings(NeptuneHomePageImage neptuneHomePageImage, NeptuneHomePageImageDto neptuneHomePageImageDto);

        public static NeptuneHomePageImageSimpleDto AsSimpleDto(this NeptuneHomePageImage neptuneHomePageImage)
        {
            var neptuneHomePageImageSimpleDto = new NeptuneHomePageImageSimpleDto()
            {
                NeptuneHomePageImageID = neptuneHomePageImage.NeptuneHomePageImageID,
                FileResourceID = neptuneHomePageImage.FileResourceID,
                Caption = neptuneHomePageImage.Caption,
                SortOrder = neptuneHomePageImage.SortOrder
            };
            DoCustomSimpleDtoMappings(neptuneHomePageImage, neptuneHomePageImageSimpleDto);
            return neptuneHomePageImageSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(NeptuneHomePageImage neptuneHomePageImage, NeptuneHomePageImageSimpleDto neptuneHomePageImageSimpleDto);
    }
}