//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NereidResult]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class NereidResultExtensionMethods
    {
        public static NereidResultSimpleDto AsSimpleDto(this NereidResult nereidResult)
        {
            var dto = new NereidResultSimpleDto()
            {
                NereidResultID = nereidResult.NereidResultID,
                TreatmentBMPID = nereidResult.TreatmentBMPID,
                WaterQualityManagementPlanID = nereidResult.WaterQualityManagementPlanID,
                RegionalSubbasinID = nereidResult.RegionalSubbasinID,
                DelineationID = nereidResult.DelineationID,
                NodeID = nereidResult.NodeID,
                FullResponse = nereidResult.FullResponse,
                LastUpdate = nereidResult.LastUpdate,
                IsBaselineCondition = nereidResult.IsBaselineCondition
            };
            return dto;
        }
    }
}