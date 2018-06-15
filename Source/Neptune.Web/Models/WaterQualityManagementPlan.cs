using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class WaterQualityManagementPlan : IAuditableEntity
    {
        public string AuditDescriptionString => $"Water Quality Management Plan \"{WaterQualityManagementPlanName}\"";

        private static UrlTemplate<int> DetailUrlTemplate => new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c => c.Detail(UrlTemplate.Parameter1Int)));
        public string GetDetailUrl() => DetailUrlTemplate.ParameterReplace(WaterQualityManagementPlanID);

        private static UrlTemplate<int> EditUrlTemplate => new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c => c.Edit(UrlTemplate.Parameter1Int)));
        public string GetEditUrl() => EditUrlTemplate.ParameterReplace(WaterQualityManagementPlanID);

        private static UrlTemplate<int> DeleteUrlTemplate => new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c => c.Delete(UrlTemplate.Parameter1Int)));
        public string GetDeleteUrl() => DeleteUrlTemplate.ParameterReplace(WaterQualityManagementPlanID);

        public HtmlString GetNameAsUrl() => UrlTemplate.MakeHrefString(GetDetailUrl(), WaterQualityManagementPlanName);

        public string MaintenanceContactAddressToString()
        {
            return string.Join(" ",
                new List<string>
                {
                    MaintenanceContactAddress1,
                    MaintenanceContactAddress2,
                    MaintenanceContactCity,
                    MaintenanceContactState,
                    MaintenanceContactZip
                }.Where(x => !string.IsNullOrWhiteSpace(x)));
        }
    }
}
