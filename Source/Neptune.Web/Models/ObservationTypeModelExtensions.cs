using System.Web;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public static class ObservationTypeModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<ObservationTypeController>.BuildUrlFromExpression(t => t.DeleteObservationType(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            return DeleteUrlTemplate.ParameterReplace(TreatmentBMPAssessmentObservationType.ObservationTypeID);
        }

        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<ObservationTypeController>.BuildUrlFromExpression(t => t.Edit(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            return EditUrlTemplate.ParameterReplace(TreatmentBMPAssessmentObservationType.ObservationTypeID);
        }

        public static HtmlString GetDisplayNameAsUrl(this TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            return TreatmentBMPAssessmentObservationType != null ? UrlTemplate.MakeHrefString(TreatmentBMPAssessmentObservationType.GetDetailUrl(), TreatmentBMPAssessmentObservationType.ObservationTypeName) : new HtmlString(null);
        }

        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<ObservationTypeController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            return TreatmentBMPAssessmentObservationType == null ? "" : DetailUrlTemplate.ParameterReplace(TreatmentBMPAssessmentObservationType.ObservationTypeID);
        }
    }
}