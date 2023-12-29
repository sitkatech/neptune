using System.Globalization;
using Microsoft.AspNetCore.Html;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.TrashGeneratingUnit
{
    public class TrashGeneratingUnitGridSpec : GridSpec<vTrashGeneratingUnitLoadStatistic>
    {
        public TrashGeneratingUnitGridSpec(LinkGenerator linkGenerator)
        {
            var detailUrlTemplate =
                new UrlTemplate<int>(
                    SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var treatmentBMPDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<WebMvc.Controllers.TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, t => t.Detail(UrlTemplate.Parameter1Int)));
            var wqmpDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var stormwaterJurisdictionDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, t => t.Detail(UrlTemplate.Parameter1Int)));

            Add("Trash Generating Unit ID", x => x.TrashGeneratingUnitID.ToString(CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.FormattedNumeric);
            Add("Land Use Type", x => x.PriorityLandUseTypeDisplayName ?? "Not Priority Land Use", 140, DhtmlxGridColumnFilterType.Text);
            
            Add("Governing OVTA Area", x => x.OnlandVisualTrashAssessmentAreaID != null ? UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(x.OnlandVisualTrashAssessmentAreaID.Value), x.OnlandVisualTrashAssessmentAreaName, x.OnlandVisualTrashAssessmentAreaName) : new HtmlString(""), 255, DhtmlxGridColumnFilterType.Html);
            Add("Governing OVTA Area Baseline Score", x => !string.IsNullOrWhiteSpace(x.OnlandVisualTrashAssessmentAreaBaselineScore) ?
                new HtmlString(x.OnlandVisualTrashAssessmentAreaBaselineScore)
                    : new HtmlString("<p class='systemText'>No completed assessments</p>"), 255, DhtmlxGridColumnFilterType.Html);
            Add("Governing Treatment BMP", x => x.TreatmentBMPID != null ?
                UrlTemplate.MakeHrefString(treatmentBMPDetailUrlTemplate.ParameterReplace(x.TreatmentBMPID.Value), x.TreatmentBMPName) : new HtmlString(""), 190, DhtmlxGridColumnFilterType.Html);
            Add("Governing WQMP", x => x.WaterQualityManagementPlanID != null ?
                UrlTemplate.MakeHrefString(wqmpDetailUrlTemplate.ParameterReplace(x.WaterQualityManagementPlanID.Value), x.WaterQualityManagementPlanName) : new HtmlString(""), 190, DhtmlxGridColumnFilterType.Html);
            Add("Stormwater Jurisdiction", x => UrlTemplate.MakeHrefString(stormwaterJurisdictionDetailUrlTemplate.ParameterReplace(x.StormwaterJurisdictionID), x.OrganizationName), 170, DhtmlxGridColumnFilterType.Html);
            Add("Area", x => ((x.TrashGeneratingUnitArea ?? 0) * Constants.SquareMetersToAcres).ToString("F12", CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.Numeric);
            Add("Baseline Loading Rate", x => x.BaselineLoadingRate, 100, DhtmlxGridColumnFormatType.Decimal);

            Add("Current Loading Rate", x => x.CurrentLoadingRate, 100, DhtmlxGridColumnFormatType.Decimal);
            Add("Trash Capture Status via BMP",
                x => x.TrashCaptureStatusBMP ?? "Not Provided", 150,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Trash Capture Status via WQMP",
                x => x.TrashCaptureStatusWQMP ??
                     "Not Provided", 150, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Trash Capture Effectiveness via BMP",
                            x => x.TrashCaptureEffectivenessBMP?.ToString(CultureInfo.InvariantCulture) ?? "Not Provided", 150,
                            DhtmlxGridColumnFilterType.Numeric);
            Add("Trash Capture Effectiveness via WQMP",
                            x => x.TrashCaptureEffectivenessWQMP?.ToString(CultureInfo.InvariantCulture) ?? "Not Provided", 150,
                            DhtmlxGridColumnFilterType.Numeric);
            Add("Median Household Income (Residential)", x => x.MedianHouseholdIncomeResidential, 200);
            Add("Median Household Income (Retail)", x => x.MedianHouseholdIncomeRetail, 200);
            Add("Permit Class", x => x.PermitClass, 200);
            Add("Land Use For TGR", x => x.LandUseForTGR, 200);
            Add("Land Use Default TGR", x => x.TrashGenerationRate, 200);
            Add("Last Updated", x => x.LastUpdateDate, 120, DhtmlxGridColumnFormatType.DateTime);
        }
    }
}
