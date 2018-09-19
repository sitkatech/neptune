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

        public static readonly UrlTemplate<int> WorkflowUrlTemplate = new UrlTemplate<int>(
            SitkaRoute<FieldVisitController>.BuildUrlFromExpression(t => t.Inventory(UrlTemplate.Parameter1Int)));

        public static HtmlString GetStatusAsWorkflowUrl(this FieldVisit fieldVisit)
        {
            var fieldVisitStatus = fieldVisit.FieldVisitStatus;
            return fieldVisitStatus != FieldVisitStatus.InProgress
                ? new HtmlString(fieldVisitStatus.FieldVisitStatusDisplayName)
                : UrlTemplate.MakeHrefString(WorkflowUrlTemplate.ParameterReplace(fieldVisit.FieldVisitID),
                    fieldVisitStatus.FieldVisitStatusDisplayName);
        }

        public static string GetEditUrl(this FieldVisit fieldVisit)
        {
            return WorkflowUrlTemplate.ParameterReplace(fieldVisit.FieldVisitID);
        }

        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(
            SitkaRoute<FieldVisitController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));

        public static string GetDetailUrl(this FieldVisit fieldVisit)
        {
            return DetailUrlTemplate.ParameterReplace(fieldVisit.FieldVisitID);
        }
    }
}