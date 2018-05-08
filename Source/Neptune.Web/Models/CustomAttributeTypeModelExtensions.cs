using System.Web;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public static class CustomAttributeTypeModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(t => t.DeleteCustomAttributeType(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this CustomAttributeType customAttributeType)
        {
            return DeleteUrlTemplate.ParameterReplace(customAttributeType.CustomAttributeTypeID);
        }

        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(t => t.Edit(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this CustomAttributeType customAttributeType)
        {
            return EditUrlTemplate.ParameterReplace(customAttributeType.CustomAttributeTypeID);
        }

        public static HtmlString GetDisplayNameAsUrl(this CustomAttributeType customAttributeType)
        {
            return customAttributeType != null ? UrlTemplate.MakeHrefString(customAttributeType.GetDetailUrl(), customAttributeType.CustomAttributeTypeName) : new HtmlString(null);
        }

        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this CustomAttributeType customAttributeType)
        {
            return customAttributeType == null ? "" : DetailUrlTemplate.ParameterReplace(customAttributeType.CustomAttributeTypeID);
        }
    }
}