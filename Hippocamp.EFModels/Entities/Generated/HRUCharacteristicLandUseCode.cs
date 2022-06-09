using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("HRUCharacteristicLandUseCode")]
    [Index("HRUCharacteristicLandUseCodeDisplayName", Name = "AK_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeDisplayName", IsUnique = true)]
    [Index("HRUCharacteristicLandUseCodeName", Name = "AK_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeName", IsUnique = true)]
    public partial class HRUCharacteristicLandUseCode
    {
        public HRUCharacteristicLandUseCode()
        {
            HRUCharacteristicBaselineHRUCharacteristicLandUseCodes = new HashSet<HRUCharacteristic>();
            HRUCharacteristicHRUCharacteristicLandUseCodes = new HashSet<HRUCharacteristic>();
            ProjectHRUCharacteristicBaselineHRUCharacteristicLandUseCodes = new HashSet<ProjectHRUCharacteristic>();
            ProjectHRUCharacteristicHRUCharacteristicLandUseCodes = new HashSet<ProjectHRUCharacteristic>();
        }

        [Key]
        public int HRUCharacteristicLandUseCodeID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string HRUCharacteristicLandUseCodeName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string HRUCharacteristicLandUseCodeDisplayName { get; set; }

        [InverseProperty("BaselineHRUCharacteristicLandUseCode")]
        public virtual ICollection<HRUCharacteristic> HRUCharacteristicBaselineHRUCharacteristicLandUseCodes { get; set; }
        [InverseProperty("HRUCharacteristicLandUseCode")]
        public virtual ICollection<HRUCharacteristic> HRUCharacteristicHRUCharacteristicLandUseCodes { get; set; }
        [InverseProperty("BaselineHRUCharacteristicLandUseCode")]
        public virtual ICollection<ProjectHRUCharacteristic> ProjectHRUCharacteristicBaselineHRUCharacteristicLandUseCodes { get; set; }
        [InverseProperty("HRUCharacteristicLandUseCode")]
        public virtual ICollection<ProjectHRUCharacteristic> ProjectHRUCharacteristicHRUCharacteristicLandUseCodes { get; set; }
    }
}
