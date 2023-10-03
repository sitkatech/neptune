using Neptune.EFModels.Entities;
using Neptune.Web.Common.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditTreatmentBMPsViewModel : FormViewModel
    {
        public IEnumerable<int> TreatmentBmpIDs { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditTreatmentBMPsViewModel()
        {
        }

        public EditTreatmentBMPsViewModel(List<int> treatmentBmpIDs)
        {
            TreatmentBmpIDs = treatmentBmpIDs;
        }

        public void UpdateModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan, NeptuneDbContext dbContext, List<EFModels.Entities.TreatmentBMP> existingTreatmentBMPs)
        {
            existingTreatmentBMPs.ForEach(x => { x.WaterQualityManagementPlan = null; });

            dbContext.TreatmentBMPs.Where(x => TreatmentBmpIDs.Contains(x.TreatmentBMPID))
                .ToList()
                .ForEach(x => { x.WaterQualityManagementPlan = waterQualityManagementPlan; });
        }
    }
}
