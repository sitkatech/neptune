using LtInfo.Common.Models;

namespace Neptune.Web.Models
{
    public partial class QuickBMP : IAuditableEntity
    {

        public QuickBMP(QuickBMPSimple quickBMPSimple, int waterQualityManagementPlanID)
        {
            QuickBMPID = quickBMPSimple.QuickBMPID ?? ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            WaterQualityManagementPlanID = waterQualityManagementPlanID;
            QuickBMPName = quickBMPSimple.DisplayName;
            TreatmentBMPTypeID = quickBMPSimple.QuickTreatmentBMPTypeID;
            QuickBMPNote = quickBMPSimple.QuickBMPNote;
            DryWeatherFlowOverrideID = quickBMPSimple.DryWeatherFlowOverrideID;
            PercentOfSiteTreated = quickBMPSimple.PercentOfSiteTreated;
            PercentCaptured= quickBMPSimple.PercentCaptured;
            PercentRetained = quickBMPSimple.PercentRetained;
        }


        public string GetAuditDescriptionString()
        {
            return QuickBMPName;
        }
    }
}