using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vFieldVisitDetailed
    {
        public int PrimaryKey { get; set; }
        public int TreatmentBMPID { get; set; }
        [Required]
        [StringLength(200)]
        public string TreatmentBMPName { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        [Required]
        [StringLength(100)]
        public string TreatmentBMPTypeName { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        [Required]
        [StringLength(200)]
        public string OrganizationName { get; set; }
        public int FieldVisitID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime VisitDate { get; set; }
        public int FieldVisitTypeID { get; set; }
        [Required]
        [StringLength(40)]
        public string FieldVisitTypeDisplayName { get; set; }
        public int PerformedByPersonID { get; set; }
        [Required]
        [StringLength(201)]
        public string PerformedByPersonName { get; set; }
        public int FieldVisitStatusID { get; set; }
        [Required]
        [StringLength(20)]
        public string FieldVisitStatusDisplayName { get; set; }
        public bool IsFieldVisitVerified { get; set; }
        public bool InventoryUpdated { get; set; }
        public int NumberOfRequiredAttributes { get; set; }
        public int NumberRequiredAttributesEntered { get; set; }
        public int? TreatmentBMPAssessmentIDInitial { get; set; }
        public bool IsAssessmentCompleteInitial { get; set; }
        public double? AssessmentScoreInitial { get; set; }
        public int? TreatmentBMPAssessmentIDPM { get; set; }
        public bool IsAssessmentCompletePM { get; set; }
        public double? AssessmentScorePM { get; set; }
        public int? MaintenanceRecordID { get; set; }
    }
}
