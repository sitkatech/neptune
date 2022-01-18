using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vMostRecentTreatmentBMPAssessment
    {
        public int PrimaryKey { get; set; }
        public int TreatmentBMPID { get; set; }
        [Required]
        [StringLength(200)]
        public string TreatmentBMPName { get; set; }
        [Required]
        [StringLength(200)]
        public string StormwaterJurisdictionName { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        [Required]
        [StringLength(200)]
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
        public string FieldVisitType { get; set; }
    }
}
