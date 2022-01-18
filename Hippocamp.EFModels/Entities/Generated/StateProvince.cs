using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("StateProvince")]
    [Index(nameof(StateProvinceFeature), Name = "SPATIAL_StateProvince_StateProvinceFeature")]
    [Index(nameof(StateProvinceFeatureForAnalysis), Name = "SPATIAL_StateProvince_StateProvinceFeatureForAnalysis")]
    public partial class StateProvince
    {
        public StateProvince()
        {
            Counties = new HashSet<County>();
            StormwaterJurisdictions = new HashSet<StormwaterJurisdiction>();
        }

        [Key]
        public int StateProvinceID { get; set; }
        [Required]
        [StringLength(100)]
        public string StateProvinceName { get; set; }
        [Required]
        [StringLength(2)]
        public string StateProvinceAbbreviation { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry StateProvinceFeature { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry StateProvinceFeatureForAnalysis { get; set; }

        [InverseProperty(nameof(County.StateProvince))]
        public virtual ICollection<County> Counties { get; set; }
        [InverseProperty(nameof(StormwaterJurisdiction.StateProvince))]
        public virtual ICollection<StormwaterJurisdiction> StormwaterJurisdictions { get; set; }
    }
}
