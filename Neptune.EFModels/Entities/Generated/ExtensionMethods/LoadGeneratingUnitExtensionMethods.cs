//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LoadGeneratingUnit]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class LoadGeneratingUnitExtensionMethods
    {
        public static LoadGeneratingUnitSimpleDto AsSimpleDto(this LoadGeneratingUnit loadGeneratingUnit)
        {
            var dto = new LoadGeneratingUnitSimpleDto()
            {
                LoadGeneratingUnitID = loadGeneratingUnit.LoadGeneratingUnitID,
                ModelBasinID = loadGeneratingUnit.ModelBasinID,
                RegionalSubbasinID = loadGeneratingUnit.RegionalSubbasinID,
                DelineationID = loadGeneratingUnit.DelineationID,
                WaterQualityManagementPlanID = loadGeneratingUnit.WaterQualityManagementPlanID,
                IsEmptyResponseFromHRUService = loadGeneratingUnit.IsEmptyResponseFromHRUService
            };
            return dto;
        }
    }
}