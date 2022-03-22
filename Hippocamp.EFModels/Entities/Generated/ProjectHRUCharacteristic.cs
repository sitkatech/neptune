using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("ProjectHRUCharacteristic")]
    public partial class ProjectHRUCharacteristic
    {
        [Key]
        public int ProjectHRUCharacteristicID { get; set; }
        public int ProjectID { get; set; }
        [Required]
        [StringLength(5)]
        public string HydrologicSoilGroup { get; set; }
        public int SlopePercentage { get; set; }
        public double ImperviousAcres { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastUpdated { get; set; }
        public double Area { get; set; }
        public int HRUCharacteristicLandUseCodeID { get; set; }
        public int ProjectLoadGeneratingUnitID { get; set; }
        public double BaselineImperviousAcres { get; set; }
        public int BaselineHRUCharacteristicLandUseCodeID { get; set; }

        [ForeignKey(nameof(BaselineHRUCharacteristicLandUseCodeID))]
        [InverseProperty("ProjectHRUCharacteristicBaselineHRUCharacteristicLandUseCodes")]
        public virtual HRUCharacteristicLandUseCode BaselineHRUCharacteristicLandUseCode { get; set; }
        [ForeignKey(nameof(HRUCharacteristicLandUseCodeID))]
        [InverseProperty("ProjectHRUCharacteristicHRUCharacteristicLandUseCodes")]
        public virtual HRUCharacteristicLandUseCode HRUCharacteristicLandUseCode { get; set; }
        [ForeignKey(nameof(ProjectID))]
        [InverseProperty("ProjectHRUCharacteristics")]
        public virtual Project Project { get; set; }
        [ForeignKey(nameof(ProjectLoadGeneratingUnitID))]
        [InverseProperty("ProjectHRUCharacteristics")]
        public virtual ProjectLoadGeneratingUnit ProjectLoadGeneratingUnit { get; set; }
    }
}
