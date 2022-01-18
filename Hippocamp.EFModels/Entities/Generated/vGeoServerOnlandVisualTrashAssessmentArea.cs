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
    public partial class vGeoServerOnlandVisualTrashAssessmentArea
    {
        public int OnlandVisualTrashAssessmentAreaID { get; set; }
        [Required]
        [StringLength(100)]
        public string OnlandVisualTrashAssessmentAreaName { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry OnlandVisualTrashAssessmentAreaGeometry { get; set; }
        [StringLength(1)]
        public string Score { get; set; }
        public int? OnlandVisualTrashAssessmentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CompletedDate { get; set; }
        public bool? IsProgressAssessment { get; set; }
    }
}
