using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vGeoServerOCTAPrioritization
    {
        public int OCTAPrioritizationID { get; set; }
        public int OCTAPrioritizationKey { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry OCTAPrioritizationGeometry { get; set; }
        public double TransportationNexusScore { get; set; }
        public double LandUseBasedWaterQualityNeedScore { get; set; }
        public double ReceivingWaterScore { get; set; }
        public double StrategicallyEffectiveAreaScore { get; set; }
    }
}
