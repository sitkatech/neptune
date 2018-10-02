using LtInfo.Common.Models;
using Neptune.Web.Views.TreatmentBMPAssessmentObservationType;
using Newtonsoft.Json;

namespace Neptune.Web.Models
{
    public partial class WaterQualityManagementPlanVerifyType : IAuditableEntity
    {



        public string GetAuditDescriptionString()
        {
            return WaterQualityManagementPlanVerifyTypeName;
        }
    }
}