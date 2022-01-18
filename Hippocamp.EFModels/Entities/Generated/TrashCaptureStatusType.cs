using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("TrashCaptureStatusType")]
    [Index(nameof(TrashCaptureStatusTypeDisplayName), Name = "AK_TrashCaptureStatusType_TrashCaptureStatusTypeDisplayName", IsUnique = true)]
    [Index(nameof(TrashCaptureStatusTypeName), Name = "AK_TrashCaptureStatusType_TrashCaptureStatusTypeName", IsUnique = true)]
    public partial class TrashCaptureStatusType
    {
        public TrashCaptureStatusType()
        {
            TreatmentBMPs = new HashSet<TreatmentBMP>();
            WaterQualityManagementPlans = new HashSet<WaterQualityManagementPlan>();
        }

        [Key]
        public int TrashCaptureStatusTypeID { get; set; }
        [Required]
        [StringLength(50)]
        public string TrashCaptureStatusTypeName { get; set; }
        [Required]
        [StringLength(50)]
        public string TrashCaptureStatusTypeDisplayName { get; set; }
        public int TrashCaptureStatusTypeSortOrder { get; set; }
        public int TrashCaptureStatusTypePriority { get; set; }
        [Required]
        [StringLength(6)]
        public string TrashCaptureStatusTypeColorCode { get; set; }

        [InverseProperty(nameof(TreatmentBMP.TrashCaptureStatusType))]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
        [InverseProperty(nameof(WaterQualityManagementPlan.TrashCaptureStatusType))]
        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
    }
}
