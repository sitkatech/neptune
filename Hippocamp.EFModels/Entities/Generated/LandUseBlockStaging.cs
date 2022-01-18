using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("LandUseBlockStaging")]
    public partial class LandUseBlockStaging
    {
        [Key]
        public int LandUseBlockStagingID { get; set; }
        [StringLength(255)]
        public string PriorityLandUseType { get; set; }
        [StringLength(500)]
        public string LandUseDescription { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry LandUseBlockStagingGeometry { get; set; }
        [Column(TypeName = "decimal(4, 1)")]
        public decimal? TrashGenerationRate { get; set; }
        [StringLength(80)]
        public string LandUseForTGR { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? MedianHouseholdIncome { get; set; }
        [Required]
        [StringLength(255)]
        public string StormwaterJurisdiction { get; set; }
        [StringLength(255)]
        public string PermitType { get; set; }
        public int UploadedByPersonID { get; set; }

        [ForeignKey(nameof(UploadedByPersonID))]
        [InverseProperty(nameof(Person.LandUseBlockStagings))]
        public virtual Person UploadedByPerson { get; set; }
    }
}
