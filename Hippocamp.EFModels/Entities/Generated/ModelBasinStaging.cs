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
        [StringLength(100)]
        public string ModelBasinName { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry ModelBasinGeometry { get; set; }
    }
}
