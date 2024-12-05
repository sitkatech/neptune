using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Common.HtmlHelperExtensions;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class QuickBMPGridSpec : GridSpec<QuickBMP>
    {
        public QuickBMPGridSpec()
        {
            ObjectNameSingular = "Simplified Structural BMP";
            ObjectNamePlural = "Simplified Structural BMP";
            GridInstructionsWhenEmpty = "No Other Structural BMPs have been selected.";
            SaveFiltersInCookie = true;

            Add("Name", x => x.QuickBMPName, 150, DhtmlxGridColumnFilterType.Text);
            Add("Type", x => x.TreatmentBMPType.TreatmentBMPTypeName, 175, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Notes", x => x.QuickBMPNote, 225, DhtmlxGridColumnFilterType.Text);
            Add(FieldDefinitionType.PercentOfSiteTreated.ToGridHeaderString(), x => x.PercentOfSiteTreated, 100, DhtmlxGridColumnFormatType.Percent, DhtmlxGridColumnAggregationType.Total);
            Add(FieldDefinitionType.PercentCaptured.ToGridHeaderString(), x => x.PercentCaptured, 100, DhtmlxGridColumnFormatType.Percent);
            Add(FieldDefinitionType.PercentRetained.ToGridHeaderString(), x => x.PercentRetained, 100, DhtmlxGridColumnFormatType.Percent);
            Add(FieldDefinitionType.DryWeatherFlowOverrideID.ToGridHeaderString(),
                x => x.DryWeatherFlowOverride != null ? x.DryWeatherFlowOverride?.DryWeatherFlowOverrideDisplayName : DryWeatherFlowOverride.No.DryWeatherFlowOverrideDisplayName,
                150, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.NumberOfIndividualBMPs.ToGridHeaderString(), x => x.NumberOfIndividualBMPs, 100);

        }
    }
}
 