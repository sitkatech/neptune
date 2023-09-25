//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptuneArea]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class NeptuneAreaExtensionMethods
    {
        public static NeptuneAreaDto AsDto(this NeptuneArea neptuneArea)
        {
            var neptuneAreaDto = new NeptuneAreaDto()
            {
                NeptuneAreaID = neptuneArea.NeptuneAreaID,
                NeptuneAreaName = neptuneArea.NeptuneAreaName,
                NeptuneAreaDisplayName = neptuneArea.NeptuneAreaDisplayName,
                SortOrder = neptuneArea.SortOrder,
                ShowOnPrimaryNavigation = neptuneArea.ShowOnPrimaryNavigation
            };
            DoCustomMappings(neptuneArea, neptuneAreaDto);
            return neptuneAreaDto;
        }

        static partial void DoCustomMappings(NeptuneArea neptuneArea, NeptuneAreaDto neptuneAreaDto);

        public static NeptuneAreaSimpleDto AsSimpleDto(this NeptuneArea neptuneArea)
        {
            var neptuneAreaSimpleDto = new NeptuneAreaSimpleDto()
            {
                NeptuneAreaID = neptuneArea.NeptuneAreaID,
                NeptuneAreaName = neptuneArea.NeptuneAreaName,
                NeptuneAreaDisplayName = neptuneArea.NeptuneAreaDisplayName,
                SortOrder = neptuneArea.SortOrder,
                ShowOnPrimaryNavigation = neptuneArea.ShowOnPrimaryNavigation
            };
            DoCustomSimpleDtoMappings(neptuneArea, neptuneAreaSimpleDto);
            return neptuneAreaSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(NeptuneArea neptuneArea, NeptuneAreaSimpleDto neptuneAreaSimpleDto);
    }
}