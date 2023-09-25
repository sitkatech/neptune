using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities
{
    [Keyless]
    public partial class vGeoServerAssessmentAreaExport
    {
        public int OVTAAreaID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string OVTAAreaName { get; set; }
        public int JurisID { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string JurisName { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry OnlandVisualTrashAssessmentAreaGeometry { get; set; }
        [StringLength(1)]
        [Unicode(false)]
        public string Score { get; set; }
        public int? AssessmentID { get; set; }
        [StringLength(12)]
        [Unicode(false)]
        public string CompletedDate { get; set; }
        public bool? IsProgressAssessment { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string Description { get; set; }
    }
}
