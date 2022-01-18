using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vGeoServerAssessmentAreaExport
    {
        public int OVTAAreaID { get; set; }
        [Required]
        [StringLength(100)]
        public string OVTAAreaName { get; set; }
        public int JurisID { get; set; }
        [StringLength(200)]
        public string JurisName { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry OnlandVisualTrashAssessmentAreaGeometry { get; set; }
        [StringLength(1)]
        public string Score { get; set; }
        public int? AssessmentID { get; set; }
        [StringLength(12)]
        public string CompletedDate { get; set; }
        public bool? IsProgressAssessment { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
    }
}
