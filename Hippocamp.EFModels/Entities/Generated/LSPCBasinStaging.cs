using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("LSPCBasinStaging")]
    [Index(nameof(LSPCBasinKey), Name = "AK_LSPCBasinStaging_LSPCBasinKey", IsUnique = true)]
    public partial class LSPCBasinStaging
    {
        [Key]
        public int LSPCBasinStagingID { get; set; }
        public int LSPCBasinKey { get; set; }
        [Required]
        [StringLength(100)]
        public string LSPCBasinName { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry LSPCBasinGeometry { get; set; }
    }
}
