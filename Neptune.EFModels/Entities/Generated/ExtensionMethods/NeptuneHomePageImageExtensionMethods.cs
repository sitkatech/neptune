//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptuneHomePageImage]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class NeptuneHomePageImageExtensionMethods
    {
        public static NeptuneHomePageImageSimpleDto AsSimpleDto(this NeptuneHomePageImage neptuneHomePageImage)
        {
            var dto = new NeptuneHomePageImageSimpleDto()
            {
                NeptuneHomePageImageID = neptuneHomePageImage.NeptuneHomePageImageID,
                FileResourceID = neptuneHomePageImage.FileResourceID,
                Caption = neptuneHomePageImage.Caption,
                SortOrder = neptuneHomePageImage.SortOrder
            };
            return dto;
        }
    }
}