using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("TimeOfConcentration")]
    [Index("TimeOfConcentrationDisplayName", Name = "AK_TimeOfConcentration_TimeOfConcentrationDisplayName", IsUnique = true)]
    [Index("TimeOfConcentrationName", Name = "AK_TimeOfConcentration_TimeOfConcentrationName", IsUnique = true)]
    public partial class TimeOfConcentration
    {
        public TimeOfConcentration()
        {
            TreatmentBMPModelingAttributes = new HashSet<TreatmentBMPModelingAttribute>();
        }

        [Key]
        public int TimeOfConcentrationID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string TimeOfConcentrationName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string TimeOfConcentrationDisplayName { get; set; }

        [InverseProperty("TimeOfConcentration")]
        public virtual ICollection<TreatmentBMPModelingAttribute> TreatmentBMPModelingAttributes { get; set; }
    }
}
