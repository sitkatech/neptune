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

        public EditSimplifiedStructuralBMPsViewData(Person currentPerson, Models.WaterQualityManagementPlan waterQualityManagementPlan, IEnumerable<TreatmentBMPTypeSimple> treatmentBMPTypes) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            EntityName = $"{Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Index());
            SubEntityName = waterQualityManagementPlan.WaterQualityManagementPlanName;
            SubEntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Detail(waterQualityManagementPlan.WaterQualityManagementPlanID));
            PageTitle = "Edit Simplified Structural BMPs";
            FieldDefinitionForPercentOfSiteTreated = Models.FieldDefinition.PercentOfSiteTreated;
            FieldDefinitionForPercentCaptured = Models.FieldDefinition.PercentCaptured;
            FieldDefinitionForPercentRetained = Models.FieldDefinition.PercentRetained;

            ViewDataForAngular = new EditWaterQualityManagementPlanTreatmentBmpsViewDataForAngular(treatmentBMPTypes);

            WaterQualityManagementPlanDetailUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Detail(waterQualityManagementPlan.WaterQualityManagementPlanID));
        }


        public class EditWaterQualityManagementPlanTreatmentBmpsViewDataForAngular
        {
            public IEnumerable<TreatmentBMPTypeSimple> TreatmentBMPTypes { get; }

            public EditWaterQualityManagementPlanTreatmentBmpsViewDataForAngular(IEnumerable<TreatmentBMPTypeSimple> treatmentBMPTypes)
            {
                TreatmentBMPTypes = treatmentBMPTypes;
            }
        }
    }
}
