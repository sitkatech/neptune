using Neptune.Web.Common;
using Neptune.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class EditUpstreamBMPViewModel: IValidatableObject
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Any(x=>x.UpstreamBMPID == UpstreamBMPID))
                yield return new ValidationResult("The BMP is already set as the Upstream BMP for another BMP");
        }
    }
}