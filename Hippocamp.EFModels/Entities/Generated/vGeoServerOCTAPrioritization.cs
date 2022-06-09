using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vGeoServerOCTAPrioritization
    {
        public int OCTAPrioritizationID { get; set; }
        public int OCTAPrioritizationKey { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry OCTAPrioritizationGeometry { get; set; }
        [Required]
        [StringLength(80)]
        [Unicode(false)]
        public string CatchIDN { get; set; }
        [Required]
        [StringLength(80)]
        [Unicode(false)]
        public string Watershed { get; set; }
        public double TransportationNexusScore { get; set; }
        public double LandUseBasedWaterQualityNeedScore { get; set; }
        public double ReceivingWaterScore { get; set; }
        public double StrategicallyEffectiveAreaScore { get; set; }
        public double PC_BAC_PCT { get; set; }
        public double PC_MET_PCT { get; set; }
        public double PC_NUT_PCT { get; set; }
        public double PC_TSS_PCT { get; set; }
        public double PC_VOL_PCT { get; set; }
    }
}
