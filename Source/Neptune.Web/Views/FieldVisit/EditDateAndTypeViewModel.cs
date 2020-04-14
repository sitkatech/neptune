using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;
using Neptune.Web.Views;
using Neptune.Web.Views.FieldVisit;

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