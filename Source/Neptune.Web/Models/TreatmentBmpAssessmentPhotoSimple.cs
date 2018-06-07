using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Web.Models
{
    public class TreatmentBmpAssessmentPhotoSimple
    {
        [Required]
        [DisplayName("Treatment BMP Assessment Photo")]
        public int TreatmentBmpAssessmentPhotoID { get; set; }

        [DisplayName("Caption")]
        [MaxLength(TreatmentBMPAssessmentPhoto.FieldLengths.Caption)]
        public string Caption { get; set; }

        [Required]
        [DisplayName("Delete?")]
        public bool FlagForDeletion { get; set; }
    }
}
