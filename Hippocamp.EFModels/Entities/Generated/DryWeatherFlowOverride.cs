using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("DryWeatherFlowOverride")]
    [Index("DryWeatherFlowOverrideDisplayName", Name = "AK_DryWeatherFlowOverride_DryWeatherFlowOverrideDisplayName", IsUnique = true)]
    [Index("DryWeatherFlowOverrideName", Name = "AK_DryWeatherFlowOverride_DryWeatherFlowOverrideName", IsUnique = true)]
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
        [Unicode(false)]
        public string DryWeatherFlowOverrideName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string DryWeatherFlowOverrideDisplayName { get; set; }

        [InverseProperty("DryWeatherFlowOverride")]
        public virtual ICollection<QuickBMP> QuickBMPs { get; set; }
        [InverseProperty("DryWeatherFlowOverride")]
        public virtual ICollection<TreatmentBMPModelingAttribute> TreatmentBMPModelingAttributes { get; set; }
    }
}
