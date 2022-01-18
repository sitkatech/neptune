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
    public partial class vGeoServerObservationPointExport
    {
        [Required]
        [StringLength(100)]
        public string OVTAAreaName { get; set; }
        public int AssessmentID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry LocationPoint { get; set; }
        [StringLength(500)]
        public string Note { get; set; }
        [StringLength(12)]
        public string CompletedDate { get; set; }
        [StringLength(1)]
        public string Score { get; set; }
        public int JurisID { get; set; }
        [StringLength(200)]
        public string JurisName { get; set; }
        [StringLength(70)]
        public string PhotoUrl { get; set; }
    }
}
