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
        public Models.FieldDefinitionType FieldDefinitionForPercentOfSiteTreated { get; }
        public Models.FieldDefinitionType FieldDefinitionForPercentCaptured { get; }
        public Models.FieldDefinitionType FieldDefinitionForPercentRetained { get; }
        public Models.FieldDefinitionType FieldDefinitionForDryWeatherFlowOverride { get; }

        public EditSimplifiedStructuralBMPsViewData(Person currentPerson,
            Models.WaterQualityManagementPlan waterQualityManagementPlan,
            IEnumerable<TreatmentBMPTypeSimple> treatmentBMPTypes, List<DryWeatherFlowOverride> dryWeatherFlowOverrides,
            int dryWeatherFlowOverrideDefaultID,
            int dryWeatherFlowOverrideYesID) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            EntityName = $"{FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Index());
            SubEntityName = waterQualityManagementPlan.WaterQualityManagementPlanName;
            SubEntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Detail(waterQualityManagementPlan.WaterQualityManagementPlanID));
            PageTitle = "Edit Simplified Structural BMPs";
            FieldDefinitionForPercentOfSiteTreated = FieldDefinitionType.PercentOfSiteTreated;
            FieldDefinitionForPercentCaptured = FieldDefinitionType.PercentCaptured;
            FieldDefinitionForPercentRetained = FieldDefinitionType.PercentRetained;
            FieldDefinitionForDryWeatherFlowOverride = FieldDefinitionType.DryWeatherFlowOverride;

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
