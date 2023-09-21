using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vTreatmentBMPDetailed
    {
        public int PrimaryKey { get; set; }
        public int TreatmentBMPID { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string TreatmentBMPName { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string TreatmentBMPTypeName { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string OrganizationName { get; set; }
        public int? RequiredFieldVisitsPerYear { get; set; }
        public int? RequiredPostStormFieldVisitsPerYear { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TreatmentBMPLifespanEndDate { get; set; }
        public int? YearBuilt { get; set; }
        [StringLength(1000)]
        [Unicode(false)]
        public string Notes { get; set; }
        public int OwnerOrganizationID { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string OwnerOrganizationName { get; set; }
        public int? TreatmentBMPLifespanTypeID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TreatmentBMPLifespanTypeDisplayName { get; set; }
        public int TrashCaptureStatusTypeID { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string TrashCaptureStatusTypeDisplayName { get; set; }
        public int SizingBasisTypeID { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string SizingBasisTypeDisplayName { get; set; }
        public int? DelineationTypeID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string DelineationTypeDisplayName { get; set; }
        public long NumberOfAssessments { get; set; }
        public int? LatestTreatmentBMPAssessmentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LatestAssessmentDate { get; set; }
        public double? LatestAssessmentScore { get; set; }
        public long NumberOfMaintenanceRecords { get; set; }
        public int? LatestMaintenanceRecordID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LatestMaintenanceDate { get; set; }
        public int? NumberOfBenchmarkAndThresholds { get; set; }
        public int NumberOfBenchmarkAndThresholdsEntered { get; set; }
    }
}
