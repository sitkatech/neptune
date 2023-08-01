using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LtInfo.Common.Models;

namespace Neptune.Web.Views
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

        public EditDateAndTypeViewModel(Models.FieldVisit inProgressFieldVisit)
        {
            FieldVisitTypeID = inProgressFieldVisit.FieldVisitTypeID;
            FieldVisitDate = inProgressFieldVisit.VisitDate;
        }
    }
}