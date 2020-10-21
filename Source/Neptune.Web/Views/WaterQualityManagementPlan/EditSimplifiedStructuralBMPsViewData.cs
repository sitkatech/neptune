using System.Collections.Generic;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditSimplifiedStructuralBMPsViewData : NeptuneViewData
    {
        public EditWaterQualityManagementPlanTreatmentBmpsViewDataForAngular ViewDataForAngular { get; }
        public string WaterQualityManagementPlanDetailUrl { get; }
        public Models.FieldDefinition FieldDefinitionForPercentOfSiteTreated { get; }
        public Models.FieldDefinition FieldDefinitionForPercentCaptured { get; }
        public Models.FieldDefinition FieldDefinitionForPercentRetained { get; }
        public Models.FieldDefinition FieldDefinitionForDryWeatherFlowOverride { get; }

        public EditSimplifiedStructuralBMPsViewData(Person currentPerson,
            Models.WaterQualityManagementPlan waterQualityManagementPlan,
            IEnumerable<TreatmentBMPTypeSimple> treatmentBMPTypes, List<DryWeatherFlowOverride> dryWeatherFlowOverrides,
            int dryWeatherFlowOverrideDefaultID,
            int dryWeatherFlowOverrideYesID) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            EntityName = $"{Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Index());
            SubEntityName = waterQualityManagementPlan.WaterQualityManagementPlanName;
            SubEntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Detail(waterQualityManagementPlan.WaterQualityManagementPlanID));
            PageTitle = "Edit Simplified Structural BMPs";
            FieldDefinitionForPercentOfSiteTreated = Models.FieldDefinition.PercentOfSiteTreated;
            FieldDefinitionForPercentCaptured = Models.FieldDefinition.PercentCaptured;
            FieldDefinitionForPercentRetained = Models.FieldDefinition.PercentRetained;
            FieldDefinitionForDryWeatherFlowOverride = Models.FieldDefinition.DryWeatherFlowOverride;

            ViewDataForAngular = new EditWaterQualityManagementPlanTreatmentBmpsViewDataForAngular(treatmentBMPTypes, dryWeatherFlowOverrides, dryWeatherFlowOverrideDefaultID, dryWeatherFlowOverrideYesID);

            WaterQualityManagementPlanDetailUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Detail(waterQualityManagementPlan.WaterQualityManagementPlanID));
        }


        public class EditWaterQualityManagementPlanTreatmentBmpsViewDataForAngular
        {
            public IEnumerable<TreatmentBMPTypeSimple> TreatmentBMPTypes { get; }
            public IEnumerable<DryWeatherFlowOverride> DryWeatherFlowOverrides { get; }
            public int DryWeatherFlowOverrideDefaultID { get; }
            public int DryWeatherFlowOverrideYesID { get; set; }

            public EditWaterQualityManagementPlanTreatmentBmpsViewDataForAngular(
                IEnumerable<TreatmentBMPTypeSimple> treatmentBMPTypes,
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
