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
    public partial class vGeoServerRegionalSubbasin
    {
        public int RegionalSubbasinID { get; set; }
        public int OCSurveyCatchmentID { get; set; }
        public int? OCSurveyDownstreamCatchmentID { get; set; }
        [StringLength(10)]
        public string DrainID { get; set; }
        [StringLength(100)]
        public string Watershed { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry CatchmentGeometry { get; set; }
        public double? Area { get; set; }
    }
}
