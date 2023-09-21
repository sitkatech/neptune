//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[QuickBMP]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class QuickBMPExtensionMethods
    {
        public static QuickBMPDto AsDto(this QuickBMP quickBMP)
        {
            var quickBMPDto = new QuickBMPDto()
            {
                QuickBMPID = quickBMP.QuickBMPID,
                WaterQualityManagementPlan = quickBMP.WaterQualityManagementPlan.AsDto(),
                TreatmentBMPType = quickBMP.TreatmentBMPType.AsDto(),
                QuickBMPName = quickBMP.QuickBMPName,
                QuickBMPNote = quickBMP.QuickBMPNote,
                PercentOfSiteTreated = quickBMP.PercentOfSiteTreated,
                PercentCaptured = quickBMP.PercentCaptured,
                PercentRetained = quickBMP.PercentRetained,
                DryWeatherFlowOverride = quickBMP.DryWeatherFlowOverride?.AsDto()
            };
            DoCustomMappings(quickBMP, quickBMPDto);
            return quickBMPDto;
        }

        static partial void DoCustomMappings(QuickBMP quickBMP, QuickBMPDto quickBMPDto);

        public static QuickBMPSimpleDto AsSimpleDto(this QuickBMP quickBMP)
        {
            var quickBMPSimpleDto = new QuickBMPSimpleDto()
            {
                QuickBMPID = quickBMP.QuickBMPID,
                WaterQualityManagementPlanID = quickBMP.WaterQualityManagementPlanID,
                TreatmentBMPTypeID = quickBMP.TreatmentBMPTypeID,
                QuickBMPName = quickBMP.QuickBMPName,
                QuickBMPNote = quickBMP.QuickBMPNote,
                PercentOfSiteTreated = quickBMP.PercentOfSiteTreated,
                PercentCaptured = quickBMP.PercentCaptured,
                PercentRetained = quickBMP.PercentRetained,
                DryWeatherFlowOverrideID = quickBMP.DryWeatherFlowOverrideID
            };
            DoCustomSimpleDtoMappings(quickBMP, quickBMPSimpleDto);
            return quickBMPSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(QuickBMP quickBMP, QuickBMPSimpleDto quickBMPSimpleDto);
    }
}