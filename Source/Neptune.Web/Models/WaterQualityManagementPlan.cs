using System.Web;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class WaterQualityManagementPlan : IAuditableEntity
    {
        public string AuditDescriptionString => $"Water Quality Management Plan \"{WaterQualityManagementPlanName}\"";

        public string GetDetailUrl() => SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
            c.Detail(WaterQualityManagementPlanID));

        public string GetEditUrl() =>
            SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c => c.Edit(this));

        public string GetDeleteUrl() =>
            SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c => c.Delete(this));

        public HtmlString GetNameAsUrl() => UrlTemplate.MakeHrefString(GetDetailUrl(), WaterQualityManagementPlanName);
    }
}
