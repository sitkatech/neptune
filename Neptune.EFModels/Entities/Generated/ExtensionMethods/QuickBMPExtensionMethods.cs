//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[QuickBMP]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class QuickBMPExtensionMethods
    {
        public static QuickBMPSimpleDto AsSimpleDto(this QuickBMP quickBMP)
        {
            var dto = new QuickBMPSimpleDto()
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
            return dto;
        }
    }
}