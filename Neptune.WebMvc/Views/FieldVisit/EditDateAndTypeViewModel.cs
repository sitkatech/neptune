using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.WebMvc.Common.Models;

namespace Neptune.WebMvc.Views.FieldVisit
{
    public class EditDateAndTypeViewModel : FormViewModel
    {
        [Required]
        [DisplayName("Field Visit Type")]
        public int FieldVisitTypeID { get; set; }
        [Required]
        [DisplayName("Field Visit Date")]
        public DateTime FieldVisitDate { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public EditDateAndTypeViewModel()
        {
        }

        public EditDateAndTypeViewModel(EFModels.Entities.FieldVisit inProgressFieldVisit)
        {
            FieldVisitTypeID = inProgressFieldVisit.FieldVisitTypeID;
            FieldVisitDate = inProgressFieldVisit.VisitDate;
        }
    }
}