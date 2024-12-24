//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LoadGeneratingUnit4326]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class LoadGeneratingUnit4326ExtensionMethods
    {
        public static LoadGeneratingUnit4326SimpleDto AsSimpleDto(this LoadGeneratingUnit4326 loadGeneratingUnit4326)
        {
            var dto = new LoadGeneratingUnit4326SimpleDto()
            {
                LoadGeneratingUnit4326ID = loadGeneratingUnit4326.LoadGeneratingUnit4326ID,
                ModelBasinID = loadGeneratingUnit4326.ModelBasinID,
                RegionalSubbasinID = loadGeneratingUnit4326.RegionalSubbasinID,
                DelineationID = loadGeneratingUnit4326.DelineationID,
                WaterQualityManagementPlanID = loadGeneratingUnit4326.WaterQualityManagementPlanID,
                IsEmptyResponseFromHRUService = loadGeneratingUnit4326.IsEmptyResponseFromHRUService,
                DateHRURequested = loadGeneratingUnit4326.DateHRURequested
            };
            return dto;
        }
    }
}