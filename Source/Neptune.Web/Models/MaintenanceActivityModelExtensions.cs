using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public static class MaintenanceActivityModelExtensions
    {
        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<MaintenanceActivityController>.BuildUrlFromExpression(t => t.Edit(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this MaintenanceActivity maintenanceActivity)
        {
            return EditUrlTemplate.ParameterReplace(maintenanceActivity.MaintenanceActivityID);
        }

        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<MaintenanceActivityController>.BuildUrlFromExpression(t => t.Delete(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this MaintenanceActivity maintenanceActivity)
        {
            return DeleteUrlTemplate.ParameterReplace(maintenanceActivity.MaintenanceActivityID);
        }
    }
}