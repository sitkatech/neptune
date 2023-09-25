using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities
{
    [Keyless]
    public partial class vOnlandVisualTrashAssessmentAreaDated
    {
        public int OnlandVisualTrashAssessmentAreaID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry OnlandVisualTrashAssessmentAreaGeometry { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MostRecentAssessmentDate { get; set; }
        [StringLength(1)]
        [Unicode(false)]
        public string MostRecentAssessmentScore { get; set; }
    }
}
