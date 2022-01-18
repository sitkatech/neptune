using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("TimeOfConcentration")]
    [Index(nameof(TimeOfConcentrationDisplayName), Name = "AK_TimeOfConcentration_TimeOfConcentrationDisplayName", IsUnique = true)]
    [Index(nameof(TimeOfConcentrationName), Name = "AK_TimeOfConcentration_TimeOfConcentrationName", IsUnique = true)]
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
        public string TimeOfConcentrationName { get; set; }
        [Required]
        [StringLength(100)]
        public string TimeOfConcentrationDisplayName { get; set; }

        [InverseProperty(nameof(TreatmentBMPModelingAttribute.TimeOfConcentration))]
        public virtual ICollection<TreatmentBMPModelingAttribute> TreatmentBMPModelingAttributes { get; set; }
    }
}
