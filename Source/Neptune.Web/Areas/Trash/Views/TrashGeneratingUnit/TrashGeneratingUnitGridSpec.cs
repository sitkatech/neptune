using System;
using System.Globalization;
using System.Web;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Views;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.TrashGeneratingUnit
{
    public class TrashGeneratingUnitGridSpec : GridSpec<Models.vTrashGeneratingUnitLoadStatistic>
    {
        public TrashGeneratingUnitGridSpec()
        {

            Add("Trash Generating Unit ID", x => x.TrashGeneratingUnitID.ToString(CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.FormattedNumeric);
            Add("Land Use Type", x => x.PriorityLandUseTypeDisplayName ?? "Not Priority Land Use", 140, DhtmlxGridColumnFilterType.Text);
            
            Add("Governing OVTA Area", x => x.OnlandVisualTrashAssessmentArea()?.GetDisplayNameAsDetailUrlNoPermissionCheck() ?? new HtmlString(""), 255, DhtmlxGridColumnFilterType.Html);
            Add("Governing OVTA Area Baseline Score", x => x.OnlandVisualTrashAssessmentArea()?.GetBaselineScoreAsHtmlString() ?? new HtmlString(""), 255, DhtmlxGridColumnFilterType.Html);
            Add("Governing Treatment BMP", x => x.TreatmentBMP()?.GetDisplayNameAsUrl() ?? new HtmlString(""), 190, DhtmlxGridColumnFilterType.Html);
            Add("Governing WQMP", x => x.WaterQualityManagementPlan()?.GetNameAsUrl() ?? new HtmlString(""), 190, DhtmlxGridColumnFilterType.Html);
            Add("Stormwater Jurisdiction", x => x.StormwaterJurisdiction()?.GetDisplayNameAsDetailUrl() ?? new HtmlString(""), 170, DhtmlxGridColumnFilterType.Html);
            Add("Area", x => ((x.TrashGeneratingUnitArea ?? 0) * DbSpatialHelper.SqlGeometryAreaToAcres).ToString("F12", CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.Numeric);
            Add("Baseline Loading Rate", x => x.BaselineLoadingRate, 100, DhtmlxGridColumnFormatType.Decimal);

            Add("Current Loading Rate", x => x.CurrentLoadingRate, 100, DhtmlxGridColumnFormatType.Decimal);
            Add("Trash Capture Status via BMP",
                x => x.TreatmentBMP()?.TrashCaptureStatusType?.TrashCaptureStatusTypeDisplayName ?? "Not Provided", 150,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Trash Capture Status via WQMP",
                x => x.WaterQualityManagementPlan()?.TrashCaptureStatusType?.TrashCaptureStatusTypeDisplayName ??
                     "Not Provided", 150, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Trash Capture Effectiveness via BMP",
                            x => x.TreatmentBMP()?.TrashCaptureEffectiveness?.ToString(CultureInfo.InvariantCulture) ?? "Not Provided", 150,
                            DhtmlxGridColumnFilterType.Numeric);
            Add("Trash Capture Effectiveness via BMP",
                            x => x.WaterQualityManagementPlan()?.TrashCaptureEffectiveness?.ToString(CultureInfo.InvariantCulture) ?? "Not Provided", 150,
                            DhtmlxGridColumnFilterType.Numeric);
            Add("Median Household Income (Residential)", x => x.LandUseBlock()?.MedianHouseholdIncomeResidential, 200);
            Add("Median Household Income (Retail)", x => x.LandUseBlock()?.MedianHouseholdIncomeRetail, 200);
            Add("Permit Class", x => x.LandUseBlock()?.PermitType.PermitTypeDisplayName, 200);
            Add("Land Use For TGR", x => x.LandUseBlock()?.LandUseForTGR, 200);
            Add("Land Use Default TGR", x => x.LandUseBlock()?.TrashGenerationRate, 200);
            Add("Last Updated", x => x.LastUpdateDate, 120, DhtmlxGridColumnFormatType.DateTime);
        }
    }
}
