using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanVerify")]
    public partial class WaterQualityManagementPlanVerify
    {
        public WaterQualityManagementPlanVerify()
        {
            WaterQualityManagementPlanVerifyPhotos = new HashSet<WaterQualityManagementPlanVerifyPhoto>();
            WaterQualityManagementPlanVerifyQuickBMPs = new HashSet<WaterQualityManagementPlanVerifyQuickBMP>();
            WaterQualityManagementPlanVerifySourceControlBMPs = new HashSet<WaterQualityManagementPlanVerifySourceControlBMP>();
            WaterQualityManagementPlanVerifyTreatmentBMPs = new HashSet<WaterQualityManagementPlanVerifyTreatmentBMP>();
        }

        [Key]
        public int WaterQualityManagementPlanVerifyID { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        public int WaterQualityManagementPlanVerifyTypeID { get; set; }
        public int WaterQualityManagementPlanVisitStatusID { get; set; }
        public int? FileResourceID { get; set; }
        public int? WaterQualityManagementPlanVerifyStatusID { get; set; }
        public int LastEditedByPersonID { get; set; }
        [StringLength(1000)]
        public string SourceControlCondition { get; set; }
        [StringLength(1000)]
        public string EnforcementOrFollowupActions { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastEditedDate { get; set; }
        public bool IsDraft { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime VerificationDate { get; set; }

        [ForeignKey(nameof(FileResourceID))]
        [InverseProperty("WaterQualityManagementPlanVerifies")]
        public virtual FileResource FileResource { get; set; }
        [ForeignKey(nameof(LastEditedByPersonID))]
        [InverseProperty(nameof(Person.WaterQualityManagementPlanVerifies))]
        public virtual Person LastEditedByPerson { get; set; }
        [ForeignKey(nameof(WaterQualityManagementPlanID))]
        [InverseProperty("WaterQualityManagementPlanVerifies")]
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
        [ForeignKey(nameof(WaterQualityManagementPlanVerifyStatusID))]
        [InverseProperty("WaterQualityManagementPlanVerifies")]
        public virtual WaterQualityManagementPlanVerifyStatus WaterQualityManagementPlanVerifyStatus { get; set; }
        [ForeignKey(nameof(WaterQualityManagementPlanVerifyTypeID))]
        [InverseProperty("WaterQualityManagementPlanVerifies")]
        public virtual WaterQualityManagementPlanVerifyType WaterQualityManagementPlanVerifyType { get; set; }
        [ForeignKey(nameof(WaterQualityManagementPlanVisitStatusID))]
        [InverseProperty("WaterQualityManagementPlanVerifies")]
        public virtual WaterQualityManagementPlanVisitStatus WaterQualityManagementPlanVisitStatus { get; set; }
        [InverseProperty(nameof(WaterQualityManagementPlanVerifyPhoto.WaterQualityManagementPlanVerify))]
        public virtual ICollection<WaterQualityManagementPlanVerifyPhoto> WaterQualityManagementPlanVerifyPhotos { get; set; }
        [InverseProperty(nameof(WaterQualityManagementPlanVerifyQuickBMP.WaterQualityManagementPlanVerify))]
        public virtual ICollection<WaterQualityManagementPlanVerifyQuickBMP> WaterQualityManagementPlanVerifyQuickBMPs { get; set; }
        [InverseProperty(nameof(WaterQualityManagementPlanVerifySourceControlBMP.WaterQualityManagementPlanVerify))]
        public virtual ICollection<WaterQualityManagementPlanVerifySourceControlBMP> WaterQualityManagementPlanVerifySourceControlBMPs { get; set; }
        [InverseProperty(nameof(WaterQualityManagementPlanVerifyTreatmentBMP.WaterQualityManagementPlanVerify))]
        public virtual ICollection<WaterQualityManagementPlanVerifyTreatmentBMP> WaterQualityManagementPlanVerifyTreatmentBMPs { get; set; }
    }
}
