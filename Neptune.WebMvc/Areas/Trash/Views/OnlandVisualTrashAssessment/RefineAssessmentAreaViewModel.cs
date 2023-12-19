using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.Common.GeoSpatial;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class RefineAssessmentAreaViewModel : OnlandVisualTrashAssessmentViewModel, IValidatableObject 
    {
        [Required]
        [DisplayName("Assessment Area Geometry")]
        public List<WktAndAnnotation> WktAndAnnotations { get; set; }

        // needed by the ModelBinder
        public RefineAssessmentAreaViewModel()
        {
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (WktAndAnnotations.Select(x => GeometryHelper.FromWKT(x.Wkt, Proj4NetHelper.WEB_MERCATOR))
                .Any(x => !x.IsValid))
            {
                yield return  new ValidationResult("The Assessment Area contained invalid (self-intersecting) shapes. Please try again.");
            }
        }
    }
}
