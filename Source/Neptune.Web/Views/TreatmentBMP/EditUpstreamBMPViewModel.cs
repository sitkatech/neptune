using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class EditUpstreamBMPViewModel
    {
        [Required]
        [DisplayName("Upstream BMP")]
        public int? UpstreamBMPID { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public EditUpstreamBMPViewModel()
        {

        }

        public EditUpstreamBMPViewModel(Models.TreatmentBMP treatmentBMP)
        {
            UpstreamBMPID = treatmentBMP.UpstreamBMPID;
        }

        public void UpdateModel(Models.TreatmentBMP treatmentBMP)
        {
            treatmentBMP.UpstreamBMPID = UpstreamBMPID;
        }
    }
}