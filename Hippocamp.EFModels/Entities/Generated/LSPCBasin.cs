using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("LSPCBasin")]
    [Index(nameof(LSPCBasinKey), Name = "AK_LSPCBasin_LSPCBasinKey", IsUnique = true)]
    public partial class LSPCBasin
    {
        public LSPCBasin()
        {
            LoadGeneratingUnits = new HashSet<LoadGeneratingUnit>();
            RegionalSubbasins = new HashSet<RegionalSubbasin>();
            TreatmentBMPs = new HashSet<TreatmentBMP>();
        }

        [Key]
        public int LSPCBasinID { get; set; }
        public int LSPCBasinKey { get; set; }
        [Required]
        [StringLength(100)]
        public string LSPCBasinName { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry LSPCBasinGeometry { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastUpdate { get; set; }

        [InverseProperty(nameof(LoadGeneratingUnit.LSPCBasin))]
        public virtual ICollection<LoadGeneratingUnit> LoadGeneratingUnits { get; set; }
        [InverseProperty(nameof(RegionalSubbasin.LSPCBasin))]
        public virtual ICollection<RegionalSubbasin> RegionalSubbasins { get; set; }
        [InverseProperty(nameof(TreatmentBMP.LSPCBasin))]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
    }
}
