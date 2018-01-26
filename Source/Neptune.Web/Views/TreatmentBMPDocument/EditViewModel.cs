using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LtInfo.Common.Models;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPDocument
{
    public class EditViewModel : FormViewModel
    {
        [StringLength(Models.TreatmentBMPDocument.FieldLengths.DisplayName)]
        [DisplayName("Document Display Name")]
        [Required]
        public string DisplayName { get; set; }

        [StringLength(Models.TreatmentBMPDocument.FieldLengths.DocumentDescription)]
        [DisplayName("Document Description")]
        [Required]
        public string DocumentDescription { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(Models.TreatmentBMPDocument treatmentBMPDocument)
        {
            DisplayName = treatmentBMPDocument.DisplayName;
            DocumentDescription = treatmentBMPDocument.DocumentDescription;
        }

        public void UpdateModel(Models.TreatmentBMPDocument treatmentBMPDocument, Person currentPerson)
        {
            treatmentBMPDocument.DisplayName = DisplayName;
            treatmentBMPDocument.DocumentDescription = DocumentDescription;
        }
    }
}