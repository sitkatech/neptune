using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditWqmpTreatmentBmpsViewData
    {
        public EditWaterQualityManagementPlanTreatmentBmpsViewDataForAngular ViewDataForAngular { get; }

        public EditWqmpTreatmentBmpsViewData(Models.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            var treatmentBMPSimples = waterQualityManagementPlan.StormwaterJurisdiction.TreatmentBMPs
                .Where(x => x.WaterQualityManagementPlanID == null ||
                            x.WaterQualityManagementPlanID == waterQualityManagementPlan.WaterQualityManagementPlanID)
                .Select(x => new TreatmentBMPSimple(x))
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
