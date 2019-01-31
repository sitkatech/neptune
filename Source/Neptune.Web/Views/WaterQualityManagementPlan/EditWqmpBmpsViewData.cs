using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditWqmpBmpsViewData : NeptuneViewData
    {
        public EditWaterQualityManagementPlanTreatmentBmpsViewDataForAngular ViewDataForAngular { get; }
        public string WaterQualityManagementPlanDetailUrl { get; }

        public EditWqmpBmpsViewData(Person currentPerson, Models.WaterQualityManagementPlan waterQualityManagementPlan, IEnumerable<TreatmentBMPTypeSimple> treatmentBMPTypes) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            EntityName = $"{Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Index());
            SubEntityName = waterQualityManagementPlan.WaterQualityManagementPlanName;
            SubEntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Detail(waterQualityManagementPlan.WaterQualityManagementPlanID));
            PageTitle = "Edit WQMP BMPs";


            var treatmentBMPSimples = waterQualityManagementPlan.StormwaterJurisdiction.TreatmentBMPs
                .Where(x => x.WaterQualityManagementPlanID == null ||
                            x.WaterQualityManagementPlanID == waterQualityManagementPlan.WaterQualityManagementPlanID)
                .Select(x => new TreatmentBMPSimple(x))
                .OrderBy(x => x.DisplayName)
                .ToList();

            ViewDataForAngular = new EditWaterQualityManagementPlanTreatmentBmpsViewDataForAngular(treatmentBMPSimples, treatmentBMPTypes);

            WaterQualityManagementPlanDetailUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Detail(waterQualityManagementPlan.WaterQualityManagementPlanID));
        }

        public class EditWaterQualityManagementPlanTreatmentBmpsViewDataForAngular
        {
            public IEnumerable<TreatmentBMPSimple> TreatmentBmps { get; }
            public IEnumerable<TreatmentBMPTypeSimple> TreatmentBMPTypes { get; }

            public EditWaterQualityManagementPlanTreatmentBmpsViewDataForAngular(IEnumerable<TreatmentBMPSimple> treatmentBmps, IEnumerable<TreatmentBMPTypeSimple> treatmentBMPTypes)
            {
                TreatmentBmps = treatmentBmps;
                TreatmentBMPTypes = treatmentBMPTypes;
            }
        }
    }
}
