using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("TrashCaptureStatusType")]
    [Index("TrashCaptureStatusTypeDisplayName", Name = "AK_TrashCaptureStatusType_TrashCaptureStatusTypeDisplayName", IsUnique = true)]
    [Index("TrashCaptureStatusTypeName", Name = "AK_TrashCaptureStatusType_TrashCaptureStatusTypeName", IsUnique = true)]
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
        [Unicode(false)]
        public string TrashCaptureStatusTypeName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string TrashCaptureStatusTypeDisplayName { get; set; }
        public int TrashCaptureStatusTypeSortOrder { get; set; }
        public int TrashCaptureStatusTypePriority { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string TrashCaptureStatusTypeColorCode { get; set; }

        [InverseProperty("TrashCaptureStatusType")]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
        [InverseProperty("TrashCaptureStatusType")]
        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
    }
}
