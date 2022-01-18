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
    public partial class vLandUseBlockTGUInput
    {
        public int LandUseBlockID { get; set; }
        public int? PriorityLandUseTypeID { get; set; }
        [StringLength(500)]
        public string LandUseDescription { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry LandUseBlockGeometry { get; set; }
        [Column(TypeName = "decimal(4, 1)")]
        public decimal? TrashGenerationRate { get; set; }
        [StringLength(80)]
        public string LandUseForTGR { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? MedianHouseholdIncomeResidential { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? MedianHouseholdIncomeRetail { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int PermitTypeID { get; set; }
    }
}
