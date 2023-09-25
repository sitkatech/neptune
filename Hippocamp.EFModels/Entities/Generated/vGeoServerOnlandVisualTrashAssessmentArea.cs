using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities
{
    [Keyless]
    public partial class vGeoServerOnlandVisualTrashAssessmentArea
    {
        public int OnlandVisualTrashAssessmentAreaID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string OnlandVisualTrashAssessmentAreaName { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry OnlandVisualTrashAssessmentAreaGeometry { get; set; }
        [StringLength(1)]
        [Unicode(false)]
        public string Score { get; set; }
        public int? OnlandVisualTrashAssessmentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CompletedDate { get; set; }
        public bool? IsProgressAssessment { get; set; }
    }
}
