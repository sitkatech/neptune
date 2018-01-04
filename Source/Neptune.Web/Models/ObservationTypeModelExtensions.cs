using System.Web;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public static class ObservationTypeModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<ObservationTypeController>.BuildUrlFromExpression(t => t.DeleteObservationType(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this ObservationType observationType)
        {
            return DeleteUrlTemplate.ParameterReplace(observationType.ObservationTypeID);
        }

        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<ObservationTypeController>.BuildUrlFromExpression(t => t.Edit(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this ObservationType observationType)
        {
            return EditUrlTemplate.ParameterReplace(observationType.ObservationTypeID);
        }

        public static HtmlString GetDisplayNameAsUrl(this ObservationType observationType)
        {
            return observationType != null ? UrlTemplate.MakeHrefString(observationType.GetDetailUrl(), observationType.ObservationTypeName) : new HtmlString(null);
        }

        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<ObservationTypeController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this ObservationType observationType)
        {
            return observationType == null ? "" : DetailUrlTemplate.ParameterReplace(observationType.ObservationTypeID);
        }
    }
}