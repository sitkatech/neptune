using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("OCTAPrioritizationStaging")]
    [Index(nameof(OCTAPrioritizationKey), Name = "AK_OCTAPrioritizationStaging_OCTAPrioritizationKey", IsUnique = true)]
    public partial class OCTAPrioritizationStaging
    {
        [Key]
        public int OCTAPrioritizationStagingID { get; set; }
        public int OCTAPrioritizationKey { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry OCTAPrioritizationGeometry { get; set; }
        [Required]
        [StringLength(80)]
        public string Watershed { get; set; }
        [Required]
        [StringLength(80)]
        public string CatchIDN { get; set; }
        public double TPI { get; set; }
        public double WQNLU { get; set; }
        public double WQNMON { get; set; }
        public double IMPAIR { get; set; }
        public double MON { get; set; }
        public double SEA { get; set; }
        [Required]
        [StringLength(80)]
        public string SEA_PCTL { get; set; }
        public double PC_VOL_PCT { get; set; }
        public double PC_NUT_PCT { get; set; }
        public double PC_BAC_PCT { get; set; }
        public double PC_MET_PCT { get; set; }
        public double PC_TSS_PCT { get; set; }
    }
}
