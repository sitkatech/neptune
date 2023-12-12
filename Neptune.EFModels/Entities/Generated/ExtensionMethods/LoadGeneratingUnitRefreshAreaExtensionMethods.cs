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
            var dto = new LoadGeneratingUnitRefreshAreaSimpleDto()
            {
                LoadGeneratingUnitRefreshAreaID = loadGeneratingUnitRefreshArea.LoadGeneratingUnitRefreshAreaID,
                ProcessDate = loadGeneratingUnitRefreshArea.ProcessDate
            };
            return dto;
        }
    }
}