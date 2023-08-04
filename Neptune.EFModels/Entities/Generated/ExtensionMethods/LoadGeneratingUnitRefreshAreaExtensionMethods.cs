//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LoadGeneratingUnitRefreshArea]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class LoadGeneratingUnitRefreshAreaExtensionMethods
    {
        public static LoadGeneratingUnitRefreshAreaDto AsDto(this LoadGeneratingUnitRefreshArea loadGeneratingUnitRefreshArea)
        {
            var loadGeneratingUnitRefreshAreaDto = new LoadGeneratingUnitRefreshAreaDto()
            {
                LoadGeneratingUnitRefreshAreaID = loadGeneratingUnitRefreshArea.LoadGeneratingUnitRefreshAreaID,
                ProcessDate = loadGeneratingUnitRefreshArea.ProcessDate
            };
            DoCustomMappings(loadGeneratingUnitRefreshArea, loadGeneratingUnitRefreshAreaDto);
            return loadGeneratingUnitRefreshAreaDto;
        }

        static partial void DoCustomMappings(LoadGeneratingUnitRefreshArea loadGeneratingUnitRefreshArea, LoadGeneratingUnitRefreshAreaDto loadGeneratingUnitRefreshAreaDto);

        public static LoadGeneratingUnitRefreshAreaSimpleDto AsSimpleDto(this LoadGeneratingUnitRefreshArea loadGeneratingUnitRefreshArea)
        {
            var loadGeneratingUnitRefreshAreaSimpleDto = new LoadGeneratingUnitRefreshAreaSimpleDto()
            {
                LoadGeneratingUnitRefreshAreaID = loadGeneratingUnitRefreshArea.LoadGeneratingUnitRefreshAreaID,
                ProcessDate = loadGeneratingUnitRefreshArea.ProcessDate
            };
            DoCustomSimpleDtoMappings(loadGeneratingUnitRefreshArea, loadGeneratingUnitRefreshAreaSimpleDto);
            return loadGeneratingUnitRefreshAreaSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(LoadGeneratingUnitRefreshArea loadGeneratingUnitRefreshArea, LoadGeneratingUnitRefreshAreaSimpleDto loadGeneratingUnitRefreshAreaSimpleDto);
    }
}