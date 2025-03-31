using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Views.Shared;
using static Neptune.Common.DateUtilities;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{

    public class AnnualReportViewData : NeptuneViewData
    {
        public ViewPageContentViewData ApprovalSummaryPage { get; }
        public string ApprovalSummaryGridName { get; set; }
        public AnnualReportApprovalSummaryGridSpec ApprovalSummaryGridSpec { get; set; }
        public ViewPageContentViewData PostConstructionInspectionAndVerificationPage { get; }
        public string PostConstructionInspectionAndVerificationGridName { get; set; }
        public AnnualReportPostConstructionInspectionAndVerificationGridSpec PostConstructionInspectionAndVerificationGridSpec { get; set; }
        public ViewDataForAngularClass ViewDataForAngular { get; set; }

        public AnnualReportViewData(HttpContext httpContext, LinkGenerator linkGenerator,
            WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.NeptunePage approvalSummaryPage, EFModels.Entities.NeptunePage postConstructionInspectionAndVerificationPage,
            AnnualReportApprovalSummaryGridSpec approvalSummaryGridSpec, AnnualReportPostConstructionInspectionAndVerificationGridSpec postConstructionInspectionAndVerificationGridSpec,
            List<StormwaterJurisdiction> stormwaterJurisdictions) : base(httpContext, linkGenerator,
            currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            var waterQualityManagementPlanPluralized = FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized();
            PageTitle = $"Water Quality Management Plan Annual Report";
            EntityName = $"{waterQualityManagementPlanPluralized}";

            ApprovalSummaryPage = new ViewPageContentViewData(linkGenerator, approvalSummaryPage, currentPerson);
            ApprovalSummaryGridSpec = approvalSummaryGridSpec;
            ApprovalSummaryGridName = "wqmpApprovalSummaryGrid";
           
            PostConstructionInspectionAndVerificationPage = new ViewPageContentViewData(linkGenerator, postConstructionInspectionAndVerificationPage, currentPerson);
            PostConstructionInspectionAndVerificationGridSpec = postConstructionInspectionAndVerificationGridSpec;
            PostConstructionInspectionAndVerificationGridName = "wqmpPostConstructionInspectionAndVerificationGrid";

            var approvalSummaryGridUrlTemplate =
                new UrlTemplate<int, int>(
                    SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x =>
                        x.AnnualReportApprovalSummaryGridData(UrlTemplate.Parameter1Int, UrlTemplate.Parameter2Int)));
            var postConstructionInspectionAndVerificationGridUrlTemplate =
                new UrlTemplate<int, int>(
                    SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x =>
                        x.AnnualReportPostConstructionInspectionAndVerificationGridData(UrlTemplate.Parameter1Int, UrlTemplate.Parameter2Int)));
            ViewDataForAngular = new ViewDataForAngularClass(stormwaterJurisdictions,
                approvalSummaryGridUrlTemplate.UrlTemplateString, postConstructionInspectionAndVerificationGridUrlTemplate.UrlTemplateString);
        }


    }

    public class ViewDataForAngularClass
    {
        public const int MinimumReportingYear = 2022;
        public List<ReportingYearSimple> ReportingYearSimples { get; }
        public List<StormwaterJurisdictionDisplayDto> StormwaterJurisdictions { get; }
        public string ApprovalSummaryGridUrlTemplateString { get; }
        public string PostConstructionInspectionAndVerificationGridUrlTemplateString { get; }

        public ViewDataForAngularClass(List<StormwaterJurisdiction> stormwaterJurisdictions, string approvalSummaryGridUrlTemplateString, string postConstructionInspectionAndVerificationGridUrlTemplateString)
        {
            var currentReportingYear = GetReportingYear();
            var range = DateUtilities.GetRangeOfYears(MinimumReportingYear, currentReportingYear);

            ReportingYearSimples = range.Select(x => new ReportingYearSimple(x)).OrderByDescending(x => x.ReportingYear).ToList();
            StormwaterJurisdictions = stormwaterJurisdictions.OrderBy(x => x.Organization.GetDisplayName())
                .Select(x => x.AsDisplayDto()).ToList();
            StormwaterJurisdictions.Insert(0, new StormwaterJurisdictionDisplayDto() {StormwaterJurisdictionID = -1, StormwaterJurisdictionDisplayName = "All"});

            ApprovalSummaryGridUrlTemplateString = approvalSummaryGridUrlTemplateString;
            PostConstructionInspectionAndVerificationGridUrlTemplateString = postConstructionInspectionAndVerificationGridUrlTemplateString;
        }

        private static int GetReportingYear()
        {
            var dateToCheck = DateTime.UtcNow;
            var reportingQuarter = GetReportingQuarter((Month)dateToCheck.Month);
            if (reportingQuarter == FiscalQuarter.First || reportingQuarter == FiscalQuarter.Second)
                return dateToCheck.Year + 1;
            return dateToCheck.Year;
        }

        private static FiscalQuarter GetReportingQuarter(Month month)
        {
            if (month >= Month.July && month <= Month.September)
            {
                return FiscalQuarter.First; // 1st Fiscal FiscalQuarter = July 1 to September 30
            }
            if (month >= Month.October)
            {
                return FiscalQuarter.Second; // 2nd Fiscal FiscalQuarter = October 1 to December 31
            }
            if (month <= Month.March)
            {
                return FiscalQuarter.Third; // 3rd Fiscal FiscalQuarter = January 1 to  March 31
            }

            return FiscalQuarter.Fourth; // 4th Fiscal FiscalQuarter = April 1 to June 30
        }

    }

    public class ReportingYearSimple
    {
        public int ReportingYear { get; }
        public string ReportingYearDisplay { get; }

        public ReportingYearSimple()
        {
        }

        public ReportingYearSimple(int reportingYear)
        {
            ReportingYear = reportingYear;
            ReportingYearDisplay = $"FY {reportingYear - 1}-{reportingYear}";
        }
    }

}