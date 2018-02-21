using System;
using System.ComponentModel.DataAnnotations;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.MaintenanceActivity
{
    public class EditMaintenanceActivityViewModel : FormViewModel
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public EditMaintenanceActivityViewModel()
        {
        }

        public EditMaintenanceActivityViewModel(Models.MaintenanceActivity maintenanceActivity)
        {
            MaintenanceActivityDate = maintenanceActivity.MaintenanceActivityDate;
            PerformedByPersonID = maintenanceActivity.PerformedByPersonID;
            MaintenanceActivityTypeID = maintenanceActivity.MaintenanceActivityTypeID;
            MaintenanceActivityDescription = maintenanceActivity.MaintenanceActivityDescription;
        }

        [Required]
        [MaxLength(Models.MaintenanceActivity.FieldLengths.MaintenanceActivityDescription)]
        [Display(Name = "Description")]
        public string MaintenanceActivityDescription { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.MaintenanceActivityType)]
        public int? MaintenanceActivityTypeID { get; set; }

        [Required]
        [Display(Name = "Performed By")]
        public int? PerformedByPersonID { get; set; }

        [Display(Name = "Date")]
        [Required]
        public DateTime? MaintenanceActivityDate { get; set; }

        public void UpdateModel(Models.MaintenanceActivity maintenanceActivity)
        {
            maintenanceActivity.MaintenanceActivityDate = MaintenanceActivityDate.Value;
            maintenanceActivity.PerformedByPersonID = PerformedByPersonID.Value;
            maintenanceActivity.MaintenanceActivityTypeID = MaintenanceActivityTypeID.Value;
            maintenanceActivity.MaintenanceActivityDescription = MaintenanceActivityDescription;
        }
    }
}