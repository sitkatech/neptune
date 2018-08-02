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
        private static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(
            SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                c.Detail(UrlTemplate.Parameter1Int)));

        public string GetAuditDescriptionString()
        {
            return $"Water Quality Management Plan \"{WaterQualityManagementPlanName}\"";
        }

        public string GetDetailUrl()
        {
            return DetailUrlTemplate.ParameterReplace(WaterQualityManagementPlanID);
        }

        public string GetEditUrl()
        {
            return new UrlTemplate<int>(
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.Edit(UrlTemplate.Parameter1Int))).ParameterReplace(WaterQualityManagementPlanID);
        }

        public string GetDeleteUrl()
        {
            return new UrlTemplate<int>(
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.Delete(UrlTemplate.Parameter1Int))).ParameterReplace(WaterQualityManagementPlanID);
        }

        public HtmlString GetNameAsUrl()
        {
            return UrlTemplate.MakeHrefString(GetDetailUrl(), WaterQualityManagementPlanName);
        }

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

        public bool HasAllRequiredDocuments()
        {
            foreach (var documentType in WaterQualityManagementPlanDocumentType.All.Where(x => x.IsRequired))
            {
                if (WaterQualityManagementPlanDocuments.All(x => x.WaterQualityManagementPlanDocumentType != documentType))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
