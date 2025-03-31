using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class AnnualReportApprovalSummaryGridSpec : GridSpec<vWaterQualityManagementPlanDetailed>
    {
        public AnnualReportApprovalSummaryGridSpec(LinkGenerator linkGenerator)
        {
            var detailUrlTemplate = new UrlTemplate<int>(
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, c =>
                    c.Detail(UrlTemplate.Parameter1Int)));

            Add("Water Quality Management Plan",
                x => new HtmlString(
                    $"<a href='{detailUrlTemplate.ParameterReplace(x.WaterQualityManagementPlanID)}'>{x.WaterQualityManagementPlanName}</a>"),
                450, DhtmlxGridColumnFilterType.Html);
            Add("Priority", x => x.WaterQualityManagementPlanPriorityDisplayName, 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Land Use", x => x.WaterQualityManagementPlanLandUseDisplayName, 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Hydrologic Subarea", x => x.HydrologicSubareaName, 150, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Acres (user-entered)", x => x.RecordedWQMPAreaInAcres, 130, DhtmlxGridColumnFormatType.Decimal);
            Add("Date Approved", x => x.ApprovalDate, 140, DhtmlxGridColumnFormatType.Date);
        }
    }

    public class AnnualReportPostConstructionInspectionAndVerificationGridSpec : GridSpec<PostConstructionInspectionAndVerificationGridSimple>
    {
        public AnnualReportPostConstructionInspectionAndVerificationGridSpec(LinkGenerator linkGenerator)
        {
            var detailUrlTemplate = new UrlTemplate<int>(
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, c =>
                    c.Detail(UrlTemplate.Parameter1Int)));

            Add("Water Quality Management Plan",
                x => new HtmlString(
                    $"<a href='{detailUrlTemplate.ParameterReplace(x.WaterQualityManagementPlanID)}'>{x.WaterQualityManagementPlanName}</a>"),
                450, DhtmlxGridColumnFilterType.Html);
            Add("# of BMPs", x => x.NumberOfBMPs, 100, DhtmlxGridColumnFormatType.Integer);
            Add("# of Field Visits", x => x.NumberOfFieldVisits, 100, DhtmlxGridColumnFormatType.Integer);
            Add("# of WQMP O&M Verifications", x => x.NumberOfWQMPVerifications, 100, DhtmlxGridColumnFormatType.Integer);
            Add("WQMP O&M Verification Comments", x => x.WQMPVerificationComments, 320, DhtmlxGridColumnFilterType.Text);

        }
    }

    public class PostConstructionInspectionAndVerificationGridSimple
    {
        public int WaterQualityManagementPlanID { get; set; }
        public string? WaterQualityManagementPlanName { get; set; }
        public int? NumberOfBMPs { get; set; }
        public int? NumberOfFieldVisits { get; set; }
        public int? NumberOfWQMPVerifications { get; set; }
        public string? WQMPVerificationComments { get; set; }
    }
}