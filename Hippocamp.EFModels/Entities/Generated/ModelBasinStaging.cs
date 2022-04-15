using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("ModelBasinStaging")]
    [Index(nameof(ModelBasinKey), Name = "AK_ModelBasinStaging_ModelBasinKey", IsUnique = true)]
    public partial class ModelBasinStaging
    {
        [Key]
        public int ModelBasinStagingID { get; set; }
        public int ModelBasinKey { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry ModelBasinGeometry { get; set; }
        [Required]
        [StringLength(5)]
        public string ModelBasinState { get; set; }
        [Required]
        [StringLength(10)]
        public string ModelBasinRegion { get; set; }
    }
}
