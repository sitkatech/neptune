using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities
{
    [Table("ModelBasin")]
    [Index("ModelBasinKey", Name = "AK_ModelBasin_ModelBasinKey", IsUnique = true)]
    public partial class ModelBasin
    {
        public ModelBasin()
        {
            LoadGeneratingUnits = new HashSet<LoadGeneratingUnit>();
            ProjectLoadGeneratingUnits = new HashSet<ProjectLoadGeneratingUnit>();
            RegionalSubbasins = new HashSet<RegionalSubbasin>();
            TreatmentBMPs = new HashSet<TreatmentBMP>();
        }

        [Key]
        public int ModelBasinID { get; set; }
        public int ModelBasinKey { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry ModelBasinGeometry { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastUpdate { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string ModelBasinState { get; set; }
        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string ModelBasinRegion { get; set; }

        [InverseProperty("ModelBasin")]
        public virtual ICollection<LoadGeneratingUnit> LoadGeneratingUnits { get; set; }
        [InverseProperty("ModelBasin")]
        public virtual ICollection<ProjectLoadGeneratingUnit> ProjectLoadGeneratingUnits { get; set; }
        [InverseProperty("ModelBasin")]
        public virtual ICollection<RegionalSubbasin> RegionalSubbasins { get; set; }
        [InverseProperty("ModelBasin")]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
    }
}
