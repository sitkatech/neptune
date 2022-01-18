using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("RoutingConfiguration")]
    [Index(nameof(RoutingConfigurationDisplayName), Name = "AK_RoutingConfiguration_RoutingConfigurationDisplayName", IsUnique = true)]
    [Index(nameof(RoutingConfigurationName), Name = "AK_RoutingConfiguration_RoutingConfigurationName", IsUnique = true)]
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
        public string RoutingConfigurationName { get; set; }
        [Required]
        [StringLength(100)]
        public string RoutingConfigurationDisplayName { get; set; }

        [InverseProperty(nameof(TreatmentBMPModelingAttribute.RoutingConfiguration))]
        public virtual ICollection<TreatmentBMPModelingAttribute> TreatmentBMPModelingAttributes { get; set; }
    }
}
