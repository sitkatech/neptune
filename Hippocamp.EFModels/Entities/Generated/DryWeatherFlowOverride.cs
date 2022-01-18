using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("DryWeatherFlowOverride")]
    [Index(nameof(DryWeatherFlowOverrideDisplayName), Name = "AK_DryWeatherFlowOverride_DryWeatherFlowOverrideDisplayName", IsUnique = true)]
    [Index(nameof(DryWeatherFlowOverrideName), Name = "AK_DryWeatherFlowOverride_DryWeatherFlowOverrideName", IsUnique = true)]
    public partial class DryWeatherFlowOverride
    {
        public DryWeatherFlowOverride()
        {
            QuickBMPs = new HashSet<QuickBMP>();
            TreatmentBMPModelingAttributes = new HashSet<TreatmentBMPModelingAttribute>();
        }

        [Key]
        public int DryWeatherFlowOverrideID { get; set; }
        [Required]
        [StringLength(50)]
        public string DryWeatherFlowOverrideName { get; set; }
        [Required]
        [StringLength(50)]
        public string DryWeatherFlowOverrideDisplayName { get; set; }

        [InverseProperty(nameof(QuickBMP.DryWeatherFlowOverride))]
        public virtual ICollection<QuickBMP> QuickBMPs { get; set; }
        [InverseProperty(nameof(TreatmentBMPModelingAttribute.DryWeatherFlowOverride))]
        public virtual ICollection<TreatmentBMPModelingAttribute> TreatmentBMPModelingAttributes { get; set; }
    }
}
