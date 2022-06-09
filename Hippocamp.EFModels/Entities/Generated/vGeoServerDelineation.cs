using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vGeoServerDelineation
    {
        public int? PrimaryKey { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry DelineationGeometry { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string DelineationType { get; set; }
        public int? TreatmentBMPID { get; set; }
        public int? StormwaterJurisdictionID { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string TreatmentBMPName { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string OrganizationName { get; set; }
        [Required]
        [StringLength(11)]
        [Unicode(false)]
        public string DelineationStatus { get; set; }
    }
}
