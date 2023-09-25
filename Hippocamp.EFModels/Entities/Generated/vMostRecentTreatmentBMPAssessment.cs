using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Keyless]
    public partial class vMostRecentTreatmentBMPAssessment
    {
        public int PrimaryKey { get; set; }
        public int TreatmentBMPID { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string TreatmentBMPName { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string StormwaterJurisdictionName { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string OwnerOrganizationName { get; set; }
        public int OwnerOrganizationID { get; set; }
        public int? RequiredFieldVisitsPerYear { get; set; }
        public int? NumberOfAssessments { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastAssessmentDate { get; set; }
        public int LastAssessmentID { get; set; }
        public double? AssessmentScore { get; set; }
        [Required]
        [StringLength(40)]
        [Unicode(false)]
        public string FieldVisitType { get; set; }
    }
}
