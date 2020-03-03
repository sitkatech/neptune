using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class EditUpstreamBMPViewModel
    {
        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.UpstreamBMP)]
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