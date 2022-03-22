using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("ModelBasin")]
    [Index(nameof(ModelBasinKey), Name = "AK_ModelBasin_ModelBasinKey", IsUnique = true)]
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
        [StringLength(100)]
        public string ModelBasinName { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry ModelBasinGeometry { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastUpdate { get; set; }

        [InverseProperty(nameof(LoadGeneratingUnit.ModelBasin))]
        public virtual ICollection<LoadGeneratingUnit> LoadGeneratingUnits { get; set; }
        [InverseProperty(nameof(ProjectLoadGeneratingUnit.ModelBasin))]
        public virtual ICollection<ProjectLoadGeneratingUnit> ProjectLoadGeneratingUnits { get; set; }
        [InverseProperty(nameof(RegionalSubbasin.ModelBasin))]
        public virtual ICollection<RegionalSubbasin> RegionalSubbasins { get; set; }
        [InverseProperty(nameof(TreatmentBMP.ModelBasin))]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
    }
}
