using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vPowerBIWaterQualityManagementPlanOAndMVerification
    {
        public int PrimaryKey { get; set; }
        [Required]
        [StringLength(100)]
        public string WQMPName { get; set; }
        [Required]
        [StringLength(200)]
        public string Jurisdiction { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime VerificationDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastEditedDate { get; set; }
        [StringLength(201)]
        public string LastEditedBy { get; set; }
        [Required]
        [StringLength(100)]
        public string TypeOfVerification { get; set; }
        [Required]
        [StringLength(100)]
        public string VisitStatus { get; set; }
        [StringLength(100)]
        public string VerificationStatus { get; set; }
        [StringLength(1000)]
        public string SourceControlCondition { get; set; }
        [StringLength(1000)]
        public string EnforcementOrFollowupActions { get; set; }
        [Required]
        [StringLength(9)]
        public string DraftOrFinalized { get; set; }
    }
}
