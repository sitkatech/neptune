using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Table("LoadGeneratingUnitRefreshArea")]
    public partial class LoadGeneratingUnitRefreshArea
    {
        [Key]
        public int LoadGeneratingUnitRefreshAreaID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry LoadGeneratingUnitRefreshAreaGeometry { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ProcessDate { get; set; }
    }
}
