using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Neptune.Common;
using Neptune.WebMvc.Common.Models;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class EditNotesViewModel : FormViewModel
    {
        [Required]
        [DisplayName("Water Quality Management Plan")]
        public int? WaterQualityManagementPlanID { get; set; }

        [DisplayName("WQMP Notes")]
        [StringLength(500)]
        public string WaterQualityManagementPlanBoundaryNotes { get; set; }


        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditNotesViewModel()
        {
        }

        public EditNotesViewModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID;
            WaterQualityManagementPlanBoundaryNotes = waterQualityManagementPlan.WaterQualityManagementPlanBoundaryNotes;
        }

        public virtual void UpdateModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            waterQualityManagementPlan.WaterQualityManagementPlanBoundaryNotes =
                WaterQualityManagementPlanBoundaryNotes;
        }
    }
}
