using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common.Models;
using Neptune.Web.Common;

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

        public EditWqmpBmpsViewModel(Models.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            TreatmentBmpIDs = waterQualityManagementPlan.TreatmentBMPs.Select(x => x.TreatmentBMPID).ToList();
        }

        public void UpdateModels(Models.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            waterQualityManagementPlan.TreatmentBMPs.ToList().ForEach(x => { x.WaterQualityManagementPlan = null; });
            TreatmentBmpIDs = TreatmentBmpIDs ?? new List<int>();


            HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => TreatmentBmpIDs.Contains(x.TreatmentBMPID))
                .ToList()
                .ForEach(x => { x.WaterQualityManagementPlan = waterQualityManagementPlan; });
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
