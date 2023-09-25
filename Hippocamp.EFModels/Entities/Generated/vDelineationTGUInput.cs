using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities
{
    [Keyless]
    public partial class vDelineationTGUInput
    {
        public int DelineationID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry DelineationGeometry { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int TrashCaptureEffectiveness { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string TrashCaptureStatusTypeDisplayName { get; set; }
    }
}
