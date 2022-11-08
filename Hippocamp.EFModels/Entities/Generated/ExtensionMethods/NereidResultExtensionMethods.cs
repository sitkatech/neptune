//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NereidResult]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class NereidResultExtensionMethods
    {
        public static NereidResultDto AsDto(this NereidResult nereidResult)
        {
            var nereidResultDto = new NereidResultDto()
            {
                NereidResultID = nereidResult.NereidResultID,
                TreatmentBMP = nereidResult.TreatmentBMP?.AsDto(),
                WaterQualityManagementPlan = nereidResult.WaterQualityManagementPlan?.AsDto(),
                RegionalSubbasin = nereidResult.RegionalSubbasin?.AsDto(),
                Delineation = nereidResult.Delineation?.AsDto(),
                NodeID = nereidResult.NodeID,
                FullResponse = nereidResult.FullResponse,
                LastUpdate = nereidResult.LastUpdate,
                IsBaselineCondition = nereidResult.IsBaselineCondition
            };
            DoCustomMappings(nereidResult, nereidResultDto);
            return nereidResultDto;
        }

        static partial void DoCustomMappings(NereidResult nereidResult, NereidResultDto nereidResultDto);

        public static NereidResultSimpleDto AsSimpleDto(this NereidResult nereidResult)
        {
            var nereidResultSimpleDto = new NereidResultSimpleDto()
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
            DoCustomSimpleDtoMappings(nereidResult, nereidResultSimpleDto);
            return nereidResultSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(NereidResult nereidResult, NereidResultSimpleDto nereidResultSimpleDto);
    }
}