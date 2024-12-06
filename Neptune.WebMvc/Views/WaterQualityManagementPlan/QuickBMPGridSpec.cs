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
            Add("Type", x => x.TreatmentBMPType.TreatmentBMPTypeName, 174, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Notes", x => x.QuickBMPNote, 224, DhtmlxGridColumnFilterType.Text);
            Add(FieldDefinitionType.NumberOfIndividualBMPs.ToGridHeaderString(), x => x.NumberOfIndividualBMPs, 100);
            Add(FieldDefinitionType.PercentOfSiteTreated.ToGridHeaderString(), x => x.PercentOfSiteTreated, 100, DhtmlxGridColumnFormatType.PercentNoDecimalPlace, DhtmlxGridColumnAggregationType.Total);
            Add(FieldDefinitionType.PercentCaptured.ToGridHeaderString(), x => x.PercentCaptured, 100, DhtmlxGridColumnFormatType.PercentNoDecimalPlace);
            Add(FieldDefinitionType.PercentRetained.ToGridHeaderString(), x => x.PercentRetained, 100, DhtmlxGridColumnFormatType.PercentNoDecimalPlace);
            Add(FieldDefinitionType.DryWeatherFlowOverrideID.ToGridHeaderString(),
                x => x.DryWeatherFlowOverride != null ? x.DryWeatherFlowOverride?.DryWeatherFlowOverrideDisplayName : DryWeatherFlowOverride.No.DryWeatherFlowOverrideDisplayName,
                140, DhtmlxGridColumnFilterType.SelectFilterStrict);


        }
    }
}
 