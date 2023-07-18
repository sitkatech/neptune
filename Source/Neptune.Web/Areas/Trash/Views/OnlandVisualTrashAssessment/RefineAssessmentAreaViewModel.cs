using Neptune.Web.Views.Shared;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Linq;
using LtInfo.Common;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
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
            if (WktAndAnnotations.Select(x => DbGeometry.FromText(x.Wkt, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID))
                .Any(x => !x.IsValid))
            {
                yield return  new ValidationResult("The Assessment Area contained invalid (self-intersecting) shapes. Please try again.");
            }
        }
    }
}
