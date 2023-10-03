using System.ComponentModel.DataAnnotations;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Views.RegionalSubbasinRevisionRequest
{
    public class NewViewModel : FormViewModel, IValidatableObject
    {
        public string Notes { get; set; }
        public List<WktAndAnnotation> WktAndAnnotations { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public NewViewModel()
        {

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }

    }
}