﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("PrecipitationZone")]
    [Index(nameof(PrecipitationZoneKey), Name = "AK_PrecipitationZone_PrecipitationZoneKey", IsUnique = true)]
    public partial class PrecipitationZone
    {
        public PrecipitationZone()
        {
            TreatmentBMPs = new HashSet<TreatmentBMP>();
        }

        [Key]
        public int PrecipitationZoneID { get; set; }
        public int PrecipitationZoneKey { get; set; }
        public double DesignStormwaterDepthInInches { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry PrecipitationZoneGeometry { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastUpdate { get; set; }

        [InverseProperty(nameof(TreatmentBMP.PrecipitationZone))]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
    }
}
