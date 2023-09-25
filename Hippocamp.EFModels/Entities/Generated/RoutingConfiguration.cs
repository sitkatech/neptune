using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("RoutingConfiguration")]
    [Index("RoutingConfigurationDisplayName", Name = "AK_RoutingConfiguration_RoutingConfigurationDisplayName", IsUnique = true)]
    [Index("RoutingConfigurationName", Name = "AK_RoutingConfiguration_RoutingConfigurationName", IsUnique = true)]
    public partial class RoutingConfiguration
    {
        public RoutingConfiguration()
        {
            TreatmentBMPModelingAttributes = new HashSet<TreatmentBMPModelingAttribute>();
        }

        [Key]
        public int RoutingConfigurationID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string RoutingConfigurationName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string RoutingConfigurationDisplayName { get; set; }

        [InverseProperty("RoutingConfiguration")]
        public virtual ICollection<TreatmentBMPModelingAttribute> TreatmentBMPModelingAttributes { get; set; }
    }
}
