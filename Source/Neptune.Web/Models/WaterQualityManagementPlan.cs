using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class WaterQualityManagementPlan : IAuditableEntity, IHaveHRUCharacteristics
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

        public HtmlString GetDisplayNameAsUrl()
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

        public double CalculateParcelAcreageTotal()
        {
            return
                (this
                    .WaterQualityManagementPlanBoundary?.Area.GetValueOrDefault() ?? 0) * DbSpatialHelper.SquareMetersToAcres;
        }

        public string GetLatestOandMVerificationDate()
        {
            return this.WaterQualityManagementPlanVerifies.Count > 0 ? this.WaterQualityManagementPlanVerifies.Select(x => x.LastEditedDate).Max().ToString("MM/dd/yyyy") : "";
        }

        public string GetLatestOandMVerificationUrl()
        {
            return WaterQualityManagementPlanVerifies.Count > 0 ? WaterQualityManagementPlanVerifies.Single(x => x.LastEditedDate == this.WaterQualityManagementPlanVerifies.Select(y => y.LastEditedDate).Max()).GetDetailUrl() : string.Empty;
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

        public DbGeometry GetCatchmentGeometry()
        {
            return WaterQualityManagementPlanBoundary;
        }
    }
}
