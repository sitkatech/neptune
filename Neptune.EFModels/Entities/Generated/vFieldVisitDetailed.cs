using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Keyless]
public partial class vFieldVisitDetailed
{
    public int PrimaryKey { get; set; }

    public int TreatmentBMPID { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? TreatmentBMPName { get; set; }

    public int TreatmentBMPTypeID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? TreatmentBMPTypeName { get; set; }

    public int StormwaterJurisdictionID { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? OrganizationName { get; set; }

    public int FieldVisitID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime VisitDate { get; set; }

    public int FieldVisitTypeID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FieldVisitTypeDisplayName { get; set; }

    public int PerformedByPersonID { get; set; }

    [StringLength(201)]
    [Unicode(false)]
    public string? PerformedByPersonName { get; set; }

    public int FieldVisitStatusID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FieldVisitStatusDisplayName { get; set; }

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

    public int WaterQualityManagementPlanID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string WaterQualityManagementPlanName { get; set; } = null!;
}
