using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("StormwaterJurisdictionPerson")]
    public partial class StormwaterJurisdictionPerson
    {
        [Key]
        public int StormwaterJurisdictionPersonID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int PersonID { get; set; }

        [ForeignKey(nameof(PersonID))]
        [InverseProperty("StormwaterJurisdictionPeople")]
        public virtual Person Person { get; set; }
        [ForeignKey(nameof(StormwaterJurisdictionID))]
        [InverseProperty("StormwaterJurisdictionPeople")]
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
    }
}
