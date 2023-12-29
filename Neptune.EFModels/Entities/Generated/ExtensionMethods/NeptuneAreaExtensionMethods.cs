//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptuneArea]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class NeptuneAreaExtensionMethods
    {
        public static NeptuneAreaSimpleDto AsSimpleDto(this NeptuneArea neptuneArea)
        {
            var dto = new NeptuneAreaSimpleDto()
            {
                NeptuneAreaID = neptuneArea.NeptuneAreaID,
                NeptuneAreaName = neptuneArea.NeptuneAreaName,
                NeptuneAreaDisplayName = neptuneArea.NeptuneAreaDisplayName,
                SortOrder = neptuneArea.SortOrder,
                ShowOnPrimaryNavigation = neptuneArea.ShowOnPrimaryNavigation
            };
            return dto;
        }
    }
}