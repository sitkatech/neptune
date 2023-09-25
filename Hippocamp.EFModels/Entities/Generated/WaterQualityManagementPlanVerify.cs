using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
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
        [Unicode(false)]
        public string SourceControlCondition { get; set; }
        [StringLength(1000)]
        [Unicode(false)]
        public string EnforcementOrFollowupActions { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastEditedDate { get; set; }
        public bool IsDraft { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime VerificationDate { get; set; }

        [ForeignKey("FileResourceID")]
        [InverseProperty("WaterQualityManagementPlanVerifies")]
        public virtual FileResource FileResource { get; set; }
        [ForeignKey("LastEditedByPersonID")]
        [InverseProperty("WaterQualityManagementPlanVerifies")]
        public virtual Person LastEditedByPerson { get; set; }
        [ForeignKey("WaterQualityManagementPlanID")]
        [InverseProperty("WaterQualityManagementPlanVerifies")]
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
        [ForeignKey("WaterQualityManagementPlanVerifyStatusID")]
        [InverseProperty("WaterQualityManagementPlanVerifies")]
        public virtual WaterQualityManagementPlanVerifyStatus WaterQualityManagementPlanVerifyStatus { get; set; }
        [ForeignKey("WaterQualityManagementPlanVerifyTypeID")]
        [InverseProperty("WaterQualityManagementPlanVerifies")]
        public virtual WaterQualityManagementPlanVerifyType WaterQualityManagementPlanVerifyType { get; set; }
        [ForeignKey("WaterQualityManagementPlanVisitStatusID")]
        [InverseProperty("WaterQualityManagementPlanVerifies")]
        public virtual WaterQualityManagementPlanVisitStatus WaterQualityManagementPlanVisitStatus { get; set; }
        [InverseProperty("WaterQualityManagementPlanVerify")]
        public virtual ICollection<WaterQualityManagementPlanVerifyPhoto> WaterQualityManagementPlanVerifyPhotos { get; set; }
        [InverseProperty("WaterQualityManagementPlanVerify")]
        public virtual ICollection<WaterQualityManagementPlanVerifyQuickBMP> WaterQualityManagementPlanVerifyQuickBMPs { get; set; }
        [InverseProperty("WaterQualityManagementPlanVerify")]
        public virtual ICollection<WaterQualityManagementPlanVerifySourceControlBMP> WaterQualityManagementPlanVerifySourceControlBMPs { get; set; }
        [InverseProperty("WaterQualityManagementPlanVerify")]
        public virtual ICollection<WaterQualityManagementPlanVerifyTreatmentBMP> WaterQualityManagementPlanVerifyTreatmentBMPs { get; set; }
    }
}
