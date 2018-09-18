using System.Linq;
using System.Web;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public static class FieldVisitModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate =
            new UrlTemplate<int>(
                SitkaRoute<FieldVisitController>.BuildUrlFromExpression(t => t.Delete(UrlTemplate.Parameter1Int)));

        public static string GetDeleteUrl(this FieldVisit fieldVisit)
        {
            return DeleteUrlTemplate.ParameterReplace(fieldVisit.FieldVisitID);
        }

        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(
            SitkaRoute<FieldVisitController>.BuildUrlFromExpression(t => t.Inventory(UrlTemplate.Parameter1Int)));

        public static HtmlString GetStatusAsWorkflowUrl(this FieldVisit fieldVisit)
        {
            var fieldVisitStatus = fieldVisit.FieldVisitStatus;
            return fieldVisitStatus != FieldVisitStatus.InProgress
                ? new HtmlString(fieldVisitStatus.FieldVisitStatusDisplayName)
                : UrlTemplate.MakeHrefString(DetailUrlTemplate.ParameterReplace(fieldVisit.FieldVisitID),
                    fieldVisitStatus.FieldVisitStatusDisplayName);
        }

        public static TreatmentBMPAssessment GetInitialAssessment(this FieldVisit fieldVisit)
        {
            return fieldVisit.TreatmentBMPAssessments.SingleOrDefault(x => x.TreatmentBMPAssessmentType == TreatmentBMPAssessmentType.Initial);
        }

        public static TreatmentBMPAssessment GetPostMaintenanceAssessment(this FieldVisit fieldVisit)
        {
            return fieldVisit.TreatmentBMPAssessments.SingleOrDefault(x => x.TreatmentBMPAssessmentType == TreatmentBMPAssessmentType.PostMaintenance);
        }

        public static MaintenanceRecord GetMaintenanceRecord(this FieldVisit fieldVisit)
        {
            return fieldVisit.MaintenanceRecords.SingleOrDefault();
        }
    }
}