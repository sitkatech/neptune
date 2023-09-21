using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("HRUCharacteristic")]
    public partial class HRUCharacteristic
    {
        [Key]
        public int HRUCharacteristicID { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string HydrologicSoilGroup { get; set; }
        public int SlopePercentage { get; set; }
        public double ImperviousAcres { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastUpdated { get; set; }
        public double Area { get; set; }
        public int HRUCharacteristicLandUseCodeID { get; set; }
        public int LoadGeneratingUnitID { get; set; }
        public double BaselineImperviousAcres { get; set; }
        public int BaselineHRUCharacteristicLandUseCodeID { get; set; }

        [ForeignKey("BaselineHRUCharacteristicLandUseCodeID")]
        [InverseProperty("HRUCharacteristicBaselineHRUCharacteristicLandUseCodes")]
        public virtual HRUCharacteristicLandUseCode BaselineHRUCharacteristicLandUseCode { get; set; }
        [ForeignKey("HRUCharacteristicLandUseCodeID")]
        [InverseProperty("HRUCharacteristicHRUCharacteristicLandUseCodes")]
        public virtual HRUCharacteristicLandUseCode HRUCharacteristicLandUseCode { get; set; }
        [ForeignKey("LoadGeneratingUnitID")]
        [InverseProperty("HRUCharacteristics")]
        public virtual LoadGeneratingUnit LoadGeneratingUnit { get; set; }
    }
}
