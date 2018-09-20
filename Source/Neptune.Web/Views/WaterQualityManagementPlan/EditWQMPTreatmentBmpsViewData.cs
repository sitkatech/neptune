using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditWqmpTreatmentBmpsViewData : NeptuneViewData
    {
        public EditWaterQualityManagementPlanTreatmentBmpsViewDataForAngular ViewDataForAngular { get; }

        public EditWqmpTreatmentBmpsViewData(Person currentPerson, Models.WaterQualityManagementPlan waterQualityManagementPlan) : base(currentPerson, StormwaterBreadCrumbEntity.WaterQualityManagementPlan)
        {
            PageTitle = waterQualityManagementPlan.WaterQualityManagementPlanName;
            EntityName = $"{Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Index());


            var treatmentBMPSimples = waterQualityManagementPlan.StormwaterJurisdiction.TreatmentBMPs
                .Where(x => x.WaterQualityManagementPlanID == null ||
                            x.WaterQualityManagementPlanID == waterQualityManagementPlan.WaterQualityManagementPlanID)
                .Select(x => new TreatmentBMPSimple(x))
                .OrderBy(x => x.DisplayName)
                .ToList();
            ViewDataForAngular = new EditWaterQualityManagementPlanTreatmentBmpsViewDataForAngular(treatmentBMPSimples);
        }

        public class EditWaterQualityManagementPlanTreatmentBmpsViewDataForAngular
        {
            public IEnumerable<TreatmentBMPSimple> TreatmentBmps { get; }

            public EditWaterQualityManagementPlanTreatmentBmpsViewDataForAngular(IEnumerable<TreatmentBMPSimple> treatmentBmps)
            {
                TreatmentBmps = treatmentBmps;
            }
        }
    }
}
