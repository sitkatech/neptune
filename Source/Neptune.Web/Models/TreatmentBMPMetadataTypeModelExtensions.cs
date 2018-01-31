using System.Web;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public static class TreatmentBMPAttributeTypeModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPAttributeTypeController>.BuildUrlFromExpression(t => t.DeleteTreatmentBMPAttributeType(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this TreatmentBMPAttributeType treatmentBMPAttributeType)
        {
            return DeleteUrlTemplate.ParameterReplace(treatmentBMPAttributeType.TreatmentBMPAttributeTypeID);
        }

        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPAttributeTypeController>.BuildUrlFromExpression(t => t.Edit(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this TreatmentBMPAttributeType treatmentBMPAttributeType)
        {
            return EditUrlTemplate.ParameterReplace(treatmentBMPAttributeType.TreatmentBMPAttributeTypeID);
        }

        public static HtmlString GetDisplayNameAsUrl(this TreatmentBMPAttributeType treatmentBMPAttributeType)
        {
            return treatmentBMPAttributeType != null ? UrlTemplate.MakeHrefString(treatmentBMPAttributeType.GetDetailUrl(), treatmentBMPAttributeType.TreatmentBMPAttributeTypeName) : new HtmlString(null);
        }

        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPAttributeTypeController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this TreatmentBMPAttributeType treatmentBMPAttributeType)
        {
            return treatmentBMPAttributeType == null ? "" : DetailUrlTemplate.ParameterReplace(treatmentBMPAttributeType.TreatmentBMPAttributeTypeID);
        }
    }
}