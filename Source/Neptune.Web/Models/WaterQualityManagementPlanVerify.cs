using LtInfo.Common.Models;
using Neptune.Web.Views.TreatmentBMPAssessmentObservationType;
using Newtonsoft.Json;

namespace Neptune.Web.Models
{
    public partial class WaterQualityManagementPlanVerify : IAuditableEntity
    {



        public string GetAuditDescriptionString()
        {
            return LastEditedDate.ToLongDateString();
        }
    }
}