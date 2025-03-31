using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Keyless]
public partial class vWaterQualityManagementPlanAnnualReport
{
    [StringLength(38)]
    [Unicode(false)]
    public string PrimaryKey { get; set; } = null!;

    public int WaterQualityManagementPlanID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? WaterQualityManagementPlanName { get; set; }

    public int? WaterQualityManagementPlanStatusID { get; set; }

    public int StormwaterJurisdictionID { get; set; }

    public int StormwaterJurisdictionPublicWQMPVisibilityTypeID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ApprovalDate { get; set; }

    public int? TreatmentBMPCount { get; set; }

    public int? QuickBMPCount { get; set; }

    public int? QuickBMPNumberOfIndividualBMPs { get; set; }

    public int? WaterQualityManagementPlanVerifyID { get; set; }

    public bool? WaterQualityManagementPlanVerifyIsDraft { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? WaterQualityManagementPlanVerifyVerificationDate { get; set; }

    [StringLength(8000)]
    [Unicode(false)]
    public string? WaterQualityManagementPlanVerifyTreatmentBMPNotes { get; set; }

    [StringLength(8000)]
    [Unicode(false)]
    public string? WaterQualityManagementPlanVerifyQuickBMPNotes { get; set; }

    public int? FieldVisitID { get; set; }

    public int? FieldVisitStatusID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FieldVisitDate { get; set; }
}
