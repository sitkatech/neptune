using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vStormwaterJurisdictionOrganizationMapping
    {
        public int StormwaterJurisdictionID { get; set; }
        public int OrganizationID { get; set; }
        [Required]
        [StringLength(200)]
        public string OrganizationName { get; set; }
    }
}
