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
    public class TrashGeneratingUnitGridSpec : GridSpec<Models.TrashGeneratingUnit>
    {
        public TrashGeneratingUnitGridSpec()
        {

            Add("Trash Generating Unit ID", x => x.TrashGeneratingUnitID.ToString(CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.FormattedNumeric);
            Add("Land Use Type", x =>
            {
                if (x.LandUseBlock == null)
                {
                    return "No data provided";
                }

                return x.LandUseBlock?.PriorityLandUseType?.PriorityLandUseTypeDisplayName ?? "Not Priority Land Use";
            }, 140, DhtmlxGridColumnFilterType.Text);
            
            Add("Governing OVTA Area", x => x.OnlandVisualTrashAssessmentArea?.GetDisplayNameAsDetailUrlNoPermissionCheck() ?? new HtmlString(""), 255, DhtmlxGridColumnFilterType.Html);
            Add("Governing OVTA Area Baseline Score", x => x.OnlandVisualTrashAssessmentArea?.GetBaselineScoreAsHtmlString() ?? new HtmlString(""), 255, DhtmlxGridColumnFilterType.Html);
            Add("Governing Treatment BMP", x => x.TreatmentBMP?.GetDisplayNameAsUrl() ?? new HtmlString(""), 190, DhtmlxGridColumnFilterType.Html);
            Add("Stormwater Jurisdiction", x => x.StormwaterJurisdiction?.GetDisplayNameAsDetailUrl() ?? new HtmlString(""), 170, DhtmlxGridColumnFilterType.Html);
            Add("Area", x => ((x.TrashGeneratingUnitGeometry.Area ?? 0) * DbSpatialHelper.SqlGeometryAreaToAcres).ToString("F2", CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.Numeric);
            Add("Baseline Loading Rate", x =>
            {
                if (x.LandUseBlock == null)
                {
                    return "N/A";
                }

                return (x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentBaselineScore != null ? x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore.TrashGenerationRate : x.LandUseBlock.TrashGenerationRate).GetValueOrDefault().ToString(CultureInfo.InvariantCulture);
            }, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);

            Add("Current Loading Rate", x => CurrentLoadingRate(x), 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Trash Capture Status",
                x => x.TreatmentBMP?.TrashCaptureStatusType?.TrashCaptureStatusTypeDisplayName ?? "Not Provided", 150,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Trash Capture Effectiveness",
                            x => x.TreatmentBMP?.TrashCaptureEffectiveness?.ToString(CultureInfo.InvariantCulture) ?? "Not Provided", 150,
                            DhtmlxGridColumnFilterType.Numeric);
            Add("Median Household Income (Residential)", x => x.LandUseBlock?.MedianHouseholdIncomeResidential, 200);
            Add("Median Household Income (Retail)", x => x.LandUseBlock?.MedianHouseholdIncomeRetail, 200);
            Add("Permit Class", x => x.LandUseBlock?.PermitType.PermitTypeDisplayName, 200);
            Add("Land Use For TGR", x => x.LandUseBlock?.LandUseForTGR, 200);
            Add("Land Use Default TGR", x => x.LandUseBlock?.TrashGenerationRate, 200);
            Add("Last Updated", x => x.LastUpdateDate, 120, DhtmlxGridColumnFormatType.DateTime);
        }

        public static string CurrentLoadingRate(Models.TrashGeneratingUnit x)
        {
            if (x.LandUseBlock?.TrashGenerationRate == null)
            {
                return "Not Set";
            }

            // fully-captured automatically means that current loading rate is 2.5
            if (x.TreatmentBMP?.TrashCaptureStatusTypeID == TrashCaptureStatusType.Full.TrashCaptureStatusTypeID)
            {
                return "2.5";
            }

            var baselineGenerationRate = x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentBaselineScore != null
                ? x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentBaselineScore.TrashGenerationRate
                : x.LandUseBlock.TrashGenerationRate;
            var assessmentScoreTrashGenerationRate = x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentProgressScore
                ?.TrashGenerationRate;

            var preCaptureLoadingRate = assessmentScoreTrashGenerationRate != null
                ? Math.Min(assessmentScoreTrashGenerationRate.Value, baselineGenerationRate.Value)
                : baselineGenerationRate.Value;

            var treatmentBMPTrashCaptureEffectiveness = x.TreatmentBMP?.TrashCaptureEffectiveness;
            if (x.TreatmentBMP?.TrashCaptureStatusTypeID ==
                TrashCaptureStatusType.Partial.TrashCaptureStatusTypeID &&
                treatmentBMPTrashCaptureEffectiveness != null)
            {
                var postCaptureLoadingRate = (1.0 - (treatmentBMPTrashCaptureEffectiveness.Value / 100.0)) * (double)preCaptureLoadingRate;
                return Math.Max(2.5, postCaptureLoadingRate).ToString(CultureInfo.InvariantCulture);
            }

            return preCaptureLoadingRate.ToString(CultureInfo.InvariantCulture);
        }
    }
}
