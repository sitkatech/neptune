using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Linq;
using Microsoft.SqlServer.Types;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class RefineAssessmentAreaViewModel : OnlandVisualTrashAssessmentViewModel//, IValidatableObject
    {
        public List<WktAndAnnotation> WktAndAnnotations { get; set; }

        // needed by the ModelBinder
        public RefineAssessmentAreaViewModel()
        {

        }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    foreach (var wkt in WktAndAnnotations.Select(x => x.Wkt))
        //    {
        //        if (!DbGeometry.FromText(wkt).IsValid)
        //        {
        //            yield return new ValidationResult("The drawn area contains self-intersections.");
        //            yield break;
        //        }
        //    }
        //}
    }
}