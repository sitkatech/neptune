using System.Web;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public static class TreatmentBMPTypeModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(t => t.DeleteTreatmentBMPType(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this TreatmentBMPType treatmentBMPType)
        {
            return DeleteUrlTemplate.ParameterReplace(treatmentBMPType.TreatmentBMPTypeID);
        }

        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(t => t.Edit(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this TreatmentBMPType treatmentBMPType)
        {
            return EditUrlTemplate.ParameterReplace(treatmentBMPType.TreatmentBMPTypeID);
        }

        public static HtmlString GetDisplayNameAsUrl(this TreatmentBMPType treatmentBMPType)
        {
            return treatmentBMPType != null ? UrlTemplate.MakeHrefString(treatmentBMPType.GetDetailUrl(), treatmentBMPType.TreatmentBMPTypeName) : new HtmlString(null);
        }

        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this TreatmentBMPType treatmentBMPType)
        {
            return treatmentBMPType == null ? "" : DetailUrlTemplate.ParameterReplace(treatmentBMPType.TreatmentBMPTypeID);
        }
    }
}