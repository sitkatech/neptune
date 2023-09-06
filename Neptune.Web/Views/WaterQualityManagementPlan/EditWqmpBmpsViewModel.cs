using System.ComponentModel.DataAnnotations;
using Neptune.EFModels.Entities;
using Neptune.Web.Common.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditWqmpBmpsViewModel : FormViewModel, IValidatableObject
    {
        public IEnumerable<int> TreatmentBmpIDs { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditWqmpBmpsViewModel()
        {
        }

        public EditWqmpBmpsViewModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            TreatmentBmpIDs = waterQualityManagementPlan.TreatmentBMPs.Select(x => x.TreatmentBMPID).ToList();
        }

        public void UpdateModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan, NeptuneDbContext dbContext)
        {
            waterQualityManagementPlan.TreatmentBMPs.ToList().ForEach(x => { x.WaterQualityManagementPlan = null; });
            TreatmentBmpIDs = TreatmentBmpIDs ?? new List<int>();


            dbContext.TreatmentBMPs.Where(x => TreatmentBmpIDs.Contains(x.TreatmentBMPID))
                .ToList()
                .ForEach(x => { x.WaterQualityManagementPlan = waterQualityManagementPlan; });
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
