using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Neptune.EFModels.Entities
{
    [Table("PlannedProjectHRUCharacteristic")]
    public partial class PlannedProjectHRUCharacteristic
    {
        [Key]
        public int PlannedProjectHRUCharacteristicID { get; set; }
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
        public int PlannedProjectLoadGeneratingUnitID { get; set; }
        public double BaselineImperviousAcres { get; set; }
        public int BaselineHRUCharacteristicLandUseCodeID { get; set; }

        [ForeignKey(nameof(BaselineHRUCharacteristicLandUseCodeID))]
        [InverseProperty("PlannedProjectHRUCharacteristicBaselineHRUCharacteristicLandUseCodes")]
        public virtual HRUCharacteristicLandUseCode BaselineHRUCharacteristicLandUseCode { get; set; }
        [ForeignKey(nameof(HRUCharacteristicLandUseCodeID))]
        [InverseProperty("PlannedProjectHRUCharacteristicHRUCharacteristicLandUseCodes")]
        public virtual HRUCharacteristicLandUseCode HRUCharacteristicLandUseCode { get; set; }
        [ForeignKey(nameof(PlannedProjectLoadGeneratingUnitID))]
        [InverseProperty("PlannedProjectHRUCharacteristics")]
        public virtual PlannedProjectLoadGeneratingUnit PlannedProjectLoadGeneratingUnit { get; set; }
        [ForeignKey(nameof(ProjectID))]
        [InverseProperty("PlannedProjectHRUCharacteristics")]
        public virtual Project Project { get; set; }
    }
}
