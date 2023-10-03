using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class EditSimplifiedStructuralBMPsViewData : NeptuneViewData
    {
        public EditWaterQualityManagementPlanTreatmentBmpsViewDataForAngular ViewDataForAngular { get; }
        public string WaterQualityManagementPlanDetailUrl { get; }
        public FieldDefinitionType FieldDefinitionForPercentOfSiteTreated { get; }
        public FieldDefinitionType FieldDefinitionForPercentCaptured { get; }
        public FieldDefinitionType FieldDefinitionForPercentRetained { get; }
        public FieldDefinitionType FieldDefinitionForDryWeatherFlowOverride { get; }

        public EditSimplifiedStructuralBMPsViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan,
            IEnumerable<TreatmentBMPTypeSimpleDto> treatmentBMPTypes, List<DryWeatherFlowOverride> dryWeatherFlowOverrides,
            int dryWeatherFlowOverrideDefaultID,
            int dryWeatherFlowOverrideYesID) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            EntityName = $"{FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            SubEntityName = waterQualityManagementPlan.WaterQualityManagementPlanName;
            SubEntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(waterQualityManagementPlan.WaterQualityManagementPlanID));
            PageTitle = "Edit Simplified Structural BMPs";
            FieldDefinitionForPercentOfSiteTreated = FieldDefinitionType.PercentOfSiteTreated;
            FieldDefinitionForPercentCaptured = FieldDefinitionType.PercentCaptured;
            FieldDefinitionForPercentRetained = FieldDefinitionType.PercentRetained;
            FieldDefinitionForDryWeatherFlowOverride = FieldDefinitionType.DryWeatherFlowOverride;

            ViewDataForAngular = new EditWaterQualityManagementPlanTreatmentBmpsViewDataForAngular(treatmentBMPTypes, dryWeatherFlowOverrides, dryWeatherFlowOverrideDefaultID, dryWeatherFlowOverrideYesID);

            WaterQualityManagementPlanDetailUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(waterQualityManagementPlan.WaterQualityManagementPlanID));
        }


        public class EditWaterQualityManagementPlanTreatmentBmpsViewDataForAngular
        {
            public IEnumerable<TreatmentBMPTypeSimpleDto> TreatmentBMPTypes { get; }
            public IEnumerable<DryWeatherFlowOverride> DryWeatherFlowOverrides { get; }
            public int DryWeatherFlowOverrideDefaultID { get; }
            public int DryWeatherFlowOverrideYesID { get; set; }

            public EditWaterQualityManagementPlanTreatmentBmpsViewDataForAngular(
                IEnumerable<TreatmentBMPTypeSimpleDto> treatmentBMPTypes,
                IEnumerable<DryWeatherFlowOverride> dryWeatherFlowOverrides, int dryWeatherFlowOverrideDefaultID,
                int dryWeatherFlowOverrideYesID)
            {
                TreatmentBMPTypes = treatmentBMPTypes;
                DryWeatherFlowOverrides = dryWeatherFlowOverrides;
                DryWeatherFlowOverrideDefaultID = dryWeatherFlowOverrideDefaultID;
                DryWeatherFlowOverrideYesID = dryWeatherFlowOverrideYesID;
            }
        }
    }
}
