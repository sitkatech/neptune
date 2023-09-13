using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.EFModels.Entities;
using Neptune.Web.Common.Models;

namespace Neptune.Web.Views.TreatmentBMPDocument
{
    public class EditViewModel : FormViewModel
    {
        [StringLength(EFModels.Entities.TreatmentBMPDocument.FieldLengths.DisplayName)]
        [DisplayName("Document Display Name")]
        [Required]
        public string DisplayName { get; set; }

        [StringLength(EFModels.Entities.TreatmentBMPDocument.FieldLengths.DocumentDescription)]
        [DisplayName("Document Description")]
        [Required]
        public string DocumentDescription { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(EFModels.Entities.TreatmentBMPDocument treatmentBMPDocument)
        {
            DisplayName = treatmentBMPDocument.DisplayName;
            DocumentDescription = treatmentBMPDocument.DocumentDescription;
        }

        public void UpdateModel(EFModels.Entities.TreatmentBMPDocument treatmentBMPDocument, Person currentPerson)
        {
            treatmentBMPDocument.DisplayName = DisplayName;
            treatmentBMPDocument.DocumentDescription = DocumentDescription;
        }
    }
}