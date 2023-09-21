using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vGeoServerJurisdiction
    {
        public int StormwaterJurisdictionID { get; set; }
        public int OrganizationID { get; set; }
        public int StateProvinceID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry StormwaterJurisdictionGeometry { get; set; }
    }
}
