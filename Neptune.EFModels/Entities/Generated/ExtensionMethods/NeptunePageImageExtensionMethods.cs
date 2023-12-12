//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePageImage]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class NeptunePageImageExtensionMethods
    {
        public static NeptunePageImageSimpleDto AsSimpleDto(this NeptunePageImage neptunePageImage)
        {
            var dto = new NeptunePageImageSimpleDto()
            {
                NeptunePageImageID = neptunePageImage.NeptunePageImageID,
                NeptunePageID = neptunePageImage.NeptunePageID,
                FileResourceID = neptunePageImage.FileResourceID
            };
            return dto;
        }
    }
}