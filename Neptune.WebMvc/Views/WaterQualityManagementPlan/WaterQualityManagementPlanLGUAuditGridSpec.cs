using Neptune.WebMvc.Common.Mvc;
using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{

    public abstract class LGUAudit : TypedWebViewPage<LGUAuditViewData>
    {

    }

    public class LGUAuditViewData : NeptuneViewData
    {
        public LGUAuditViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, WaterQualityManagementPlanLGUAuditGridSpec wqmpGridSpec) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            var waterQualityManagementPlanPluralized = FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized();
            PageTitle = $"Water Quality Management Plan LGU Audit";
            EntityName = $"{waterQualityManagementPlanPluralized}";
            
            AuditGridSpec = wqmpGridSpec;
            GridName = "waterQualityManagementPlanLGUAuditGrid";
            GridDataUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, c =>
                    c.WaterQualityManagementPlanLGUAuditGridData());
        }

        public string GridDataUrl { get; set; }

        public string GridName { get; set; }

        public WaterQualityManagementPlanLGUAuditGridSpec AuditGridSpec { get; set; }
    }

    public class WaterQualityManagementPlanLGUAuditGridSpec : GridSpec<vWaterQualityManagementPlanLGUAudit>
    {
        public WaterQualityManagementPlanLGUAuditGridSpec(LinkGenerator linkGenerator)
        {
            var detailUrlTemplate = new UrlTemplate<int>(
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, c =>
                    c.Detail(UrlTemplate.Parameter1Int)));

            Add("Water Quality Management Plan",
                x => new HtmlString(
                    $"<a href='{detailUrlTemplate.ParameterReplace(x.WaterQualityManagementPlanID)}'>{x.WaterQualityManagementPlanName}</a>"),
                300, DhtmlxGridColumnFilterType.Html);
            Add("Are LGUs Populated?", x => x.LoadGeneratingUnitsPopulated == true ? "Yes" : "No", 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Is Boundary Defined?", x => x.BoundaryIsDefined == true ? "Yes" : "No", 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Intersects Model Basin(s)?", x => x.CountOfIntersectingModelBasins != 0 ? "Yes" : "No", 100,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
        }
    }
}