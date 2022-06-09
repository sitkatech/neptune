using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Table("ModelBasinStaging")]
    [Index("ModelBasinKey", Name = "AK_ModelBasinStaging_ModelBasinKey", IsUnique = true)]
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
        [Unicode(false)]
        public string ModelBasinState { get; set; }
        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string ModelBasinRegion { get; set; }
    }
}
