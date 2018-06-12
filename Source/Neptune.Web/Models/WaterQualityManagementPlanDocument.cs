using System;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class WaterQualityManagementPlanDocument : IAuditableEntity
    {
        public string AuditDescriptionString =>
            $"Water Quality Management Plan \"{WaterQualityManagementPlan?.WaterQualityManagementPlanName ?? "<Plan Not Found>"}\" Document \"{DisplayName}\"";

        public string GetEditWaterQualityManagementPlanDocumentUrl() =>
            SitkaRoute<WaterQualityManagementPlanDocumentController>.BuildUrlFromExpression(c =>
                c.Edit(WaterQualityManagementPlanDocumentID));

        public string GetDeleteWaterQualityManagementPlanDocumentUrl() =>
            SitkaRoute<WaterQualityManagementPlanDocumentController>.BuildUrlFromExpression(c =>
                c.Delete(WaterQualityManagementPlanDocumentID));
    }
}
