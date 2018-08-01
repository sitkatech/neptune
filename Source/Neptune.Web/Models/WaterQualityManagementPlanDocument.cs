using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class WaterQualityManagementPlanDocument : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return
                $"Water Quality Management Plan \"{WaterQualityManagementPlan?.WaterQualityManagementPlanName ?? "<Plan Not Found>"}\" Document \"{DisplayName}\"";
        }

        private static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanDocumentController>.BuildUrlFromExpression(c => c.Edit(UrlTemplate.Parameter1Int)));
        public string GetEditUrl()
        {
            return EditUrlTemplate.ParameterReplace(WaterQualityManagementPlanDocumentID);
        }

        private static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanDocumentController>.BuildUrlFromExpression(c => c.Delete(UrlTemplate.Parameter1Int)));
        public string GetDeleteUrl()
        {
            return DeleteUrlTemplate.ParameterReplace(WaterQualityManagementPlanDocumentID);
        }
    }
}
