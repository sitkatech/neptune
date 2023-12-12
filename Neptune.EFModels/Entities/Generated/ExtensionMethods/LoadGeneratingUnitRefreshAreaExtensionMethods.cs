//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LoadGeneratingUnitRefreshArea]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class LoadGeneratingUnitRefreshAreaExtensionMethods
    {

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