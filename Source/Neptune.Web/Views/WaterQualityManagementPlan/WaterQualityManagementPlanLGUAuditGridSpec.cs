using System.Web;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Mvc;
using LtInfo.Common.Views;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{

    public abstract class LGUAudit : TypedWebViewPage<LGUAuditViewData>
    {

    }

    public class LGUAuditViewData : NeptuneViewData
    {
        public LGUAuditViewData(Person currentPerson, WaterQualityManagementPlanLGUAuditGridSpec wqmpGridSpec) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            var waterQualityManagementPlanPluralized = FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized();
            PageTitle = $"Water Quality Management Plan LGU Audit";
            EntityName = $"{waterQualityManagementPlanPluralized}";
            
            AuditGridSpec = wqmpGridSpec;
            GridName = "waterQualityManagementPlanLGUAuditGrid";
            GridDataUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.WaterQualityManagementPlanLGUAuditGridData());
        }

        public string GridDataUrl { get; set; }

        public string GridName { get; set; }

        public WaterQualityManagementPlanLGUAuditGridSpec AuditGridSpec { get; set; }
    }

    public class WaterQualityManagementPlanLGUAuditGridSpec : GridSpec<Models.vWaterQualityManagementPlanLGUAudit>
    {
        public WaterQualityManagementPlanLGUAuditGridSpec()
        {
            Add("Water Quality Management Plan",
                x => new HtmlString(
                    $"<a href='{GetDetailUrl(x.WaterQualityManagementPlanID)}'>{x.WaterQualityManagementPlanName}</a>"),
                300, DhtmlxGridColumnFilterType.Html);
            Add("Are LGUs Populated?", x => x.LoadGeneratingUnitsPopulated == true ? "Yes" : "No", 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Is Boundary Defined?", x => x.BoundaryIsDefined == true ? "Yes" : "No", 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Intersects LSPC Basin(s)?", x => x.CountOfIntersectingLSPCBasins != 0 ? "Yes" : "No", 100,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
        }

        private static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(
            SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                c.Detail(UrlTemplate.Parameter1Int)));

        private static string GetDetailUrl(int waterQualityManagemetPlanID)
        {
            return DetailUrlTemplate.ParameterReplace(waterQualityManagemetPlanID);
        }
    }
}