using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("HRUCharacteristicLandUseCode")]
    [Index(nameof(HRUCharacteristicLandUseCodeDisplayName), Name = "AK_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeDisplayName", IsUnique = true)]
    [Index(nameof(HRUCharacteristicLandUseCodeName), Name = "AK_HRUCharacteristicLandUseCode_HRUCharacteristicLandUseCodeName", IsUnique = true)]
    public partial class HRUCharacteristicLandUseCode
    {
        public HRUCharacteristicLandUseCode()
        {
            HRUCharacteristicBaselineHRUCharacteristicLandUseCodes = new HashSet<HRUCharacteristic>();
            HRUCharacteristicHRUCharacteristicLandUseCodes = new HashSet<HRUCharacteristic>();
        }

        [Key]
        public int HRUCharacteristicLandUseCodeID { get; set; }
        [Required]
        [StringLength(100)]
        public string HRUCharacteristicLandUseCodeName { get; set; }
        [Required]
        [StringLength(100)]
        public string HRUCharacteristicLandUseCodeDisplayName { get; set; }

        [InverseProperty(nameof(HRUCharacteristic.BaselineHRUCharacteristicLandUseCode))]
        public virtual ICollection<HRUCharacteristic> HRUCharacteristicBaselineHRUCharacteristicLandUseCodes { get; set; }
        [InverseProperty(nameof(HRUCharacteristic.HRUCharacteristicLandUseCode))]
        public virtual ICollection<HRUCharacteristic> HRUCharacteristicHRUCharacteristicLandUseCodes { get; set; }
    }
}
