using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Web.Models
{
    public class TreatmentBMPImageSimple
    {
        [DisplayName("Treatment BMP Image ID")]
        [Required]
        public int? TreatmentBMPImageID { get; set; }

        [DisplayName("Caption")]
        [Required]
        [StringLength(TreatmentBMPImage.FieldLengths.Caption)]
        public string Caption { get; set; }

        [DisplayName("Delete?")]
        [Required]
        public bool ToDelete { get; set; }

        public TreatmentBMPImageSimple()
        {
        }

        public TreatmentBMPImageSimple(TreatmentBMPImage monitoringProgramImage)
        {
            TreatmentBMPImageID = monitoringProgramImage.TreatmentBMPImageID;
            Caption = monitoringProgramImage.Caption;
            ToDelete = false;
        }
    }
}