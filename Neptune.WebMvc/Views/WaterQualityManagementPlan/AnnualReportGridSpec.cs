﻿using Microsoft.AspNetCore.Html;
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
            Add("Hydrologic Subarea", x => x.HydrologicSubareaName, 200, DhtmlxGridColumnFilterType.SelectFilterStrict);
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
                340, DhtmlxGridColumnFilterType.Html);
            Add("WQMP Status at End of Period", x => x.WaterQualityManagementPlanVerifyStatusName, 260, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("# of BMPs", x => x.NumberOfBMPs, 100, DhtmlxGridColumnFormatType.Integer);
            Add("BMPs Adequate", x => x.NumberOfBMPsAdequate, 100, DhtmlxGridColumnFormatType.Integer);
            Add("BMPs Deficient", x => x.NumberOfBMPsDeficient, 100, DhtmlxGridColumnFormatType.Integer);
            Add("WQMP O&M Verification Comments", x => x.WQMPVerificationComments, 320, DhtmlxGridColumnFilterType.Text);

        }
    }

    public class PostConstructionInspectionAndVerificationGridSimple
    {
        public int WaterQualityManagementPlanID { get; set; }
        public string WaterQualityManagementPlanName { get; set; }
        public string WaterQualityManagementPlanVerifyStatusName { get; set; }
        public int? NumberOfBMPs { get; set; }
        public int? NumberOfBMPsAdequate { get; set; }
        public int? NumberOfBMPsDeficient{ get; set; }
        public string WQMPVerificationComments { get; set; }
    }
}