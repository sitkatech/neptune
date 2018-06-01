using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Web.Views.FieldVisit
{
    public class MaintainViewModel : FieldVisitSectionViewModel
    {
        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public MaintainViewModel()
        {

        }

        public MaintainViewModel(Models.FieldVisit fieldVisit)
        {
            MaintenanceRecordTypeID = fieldVisit.MaintenanceRecord?.MaintenanceRecordTypeID;
        }

        [Required(ErrorMessage = "You must select a Maintenance Record Type.")]
        [DisplayName("Maintenance Record Type")]
        public int? MaintenanceRecordTypeID { get; set; }
    }
}