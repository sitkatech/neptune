using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vPyQgisOnlandVisualTrashAssessmentAreaDated
    {
        public int OVTAID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry OnlandVisualTrashAssessmentAreaGeometry { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AssessDate { get; set; }
    }
}
