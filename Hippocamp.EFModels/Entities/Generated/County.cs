using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("County")]
    [Index(nameof(CountyName), nameof(StateProvinceID), Name = "AK_County_CountyName_StateProvinceID", IsUnique = true)]
    public partial class County
    {
        [Key]
        public int CountyID { get; set; }
        [Required]
        [StringLength(100)]
        public string CountyName { get; set; }
        public int StateProvinceID { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry CountyFeature { get; set; }

        [ForeignKey(nameof(StateProvinceID))]
        [InverseProperty("Counties")]
        public virtual StateProvince StateProvince { get; set; }
    }
}
