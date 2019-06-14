using Neptune.Web.Views.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class RefineAssessmentAreaViewModel : OnlandVisualTrashAssessmentViewModel, IValidatableObject 
    {
        public List<WktAndAnnotation> WktAndAnnotations { get; set; }

        // needed by the ModelBinder
        public RefineAssessmentAreaViewModel()
        {

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (WktAndAnnotations.Select(x => DbGeometry.FromText(x.Wkt, MapInitJson.CoordinateSystemId))
                .Any(x => !x.IsValid))
            {
                yield return  new ValidationResult("The Assessment Area contained invalid (self-intersecting) shapes. Please try again.");
            }
        }
    }
}
