using LtInfo.Common.Models;
using Neptune.Web.Views.TreatmentBMPAssessmentObservationType;
using Newtonsoft.Json;

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
        }


        public string GetAuditDescriptionString()
        {
            return QuickBMPName;
        }
    }
}