using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("TreatmentBMP")]
[Index("TreatmentBMPID", "TreatmentBMPTypeID", Name = "AK_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID", IsUnique = true)]
[Index("LocationPoint", Name = "SPATIAL_TreatmentBMP_LocationPoint")]
[Index("LocationPoint4326", Name = "SPATIAL_TreatmentBMP_LocationPoint4326")]
public partial class TreatmentBMP
{
    [Key]
    public int TreatmentBMPID { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? TreatmentBMPName { get; set; }

    public int TreatmentBMPTypeID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry? LocationPoint { get; set; }

    public int StormwaterJurisdictionID { get; set; }

    [StringLength(2000)]
    [Unicode(false)]
    public string? Notes { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? SystemOfRecordID { get; set; }

    public int? YearBuilt { get; set; }

    public int OwnerOrganizationID { get; set; }

    public int? WaterQualityManagementPlanID { get; set; }

    public int? TreatmentBMPLifespanTypeID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TreatmentBMPLifespanEndDate { get; set; }

    public int? RequiredFieldVisitsPerYear { get; set; }

    public int? RequiredPostStormFieldVisitsPerYear { get; set; }

    public bool InventoryIsVerified { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateOfLastInventoryVerification { get; set; }

    public int? InventoryVerifiedByPersonID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? InventoryLastChangedDate { get; set; }

    public int TrashCaptureStatusTypeID { get; set; }

    public int SizingBasisTypeID { get; set; }

    public int? TrashCaptureEffectiveness { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry? LocationPoint4326 { get; set; }

    public int? WatershedID { get; set; }

    public int? ModelBasinID { get; set; }

    public int? PrecipitationZoneID { get; set; }

    public int? UpstreamBMPID { get; set; }

    public int? RegionalSubbasinID { get; set; }

    public int? ProjectID { get; set; }

    [InverseProperty("TreatmentBMP")]
    public virtual ICollection<CustomAttribute> CustomAttributes { get; set; } = new List<CustomAttribute>();

    [InverseProperty("TreatmentBMP")]
    public virtual Delineation? Delineation { get; set; }

    [InverseProperty("TreatmentBMP")]
    public virtual ICollection<DirtyModelNode> DirtyModelNodes { get; set; } = new List<DirtyModelNode>();

    [InverseProperty("TreatmentBMP")]
    public virtual FieldVisit? FieldVisit { get; set; }

    [InverseProperty("TreatmentBMP")]
    public virtual ICollection<FundingEvent> FundingEvents { get; set; } = new List<FundingEvent>();

    [ForeignKey("InventoryVerifiedByPersonID")]
    [InverseProperty("TreatmentBMPs")]
    public virtual Person? InventoryVerifiedByPerson { get; set; }

    [InverseProperty("UpstreamBMP")]
    public virtual ICollection<TreatmentBMP> InverseUpstreamBMP { get; set; } = new List<TreatmentBMP>();

    [InverseProperty("TreatmentBMP")]
    public virtual ICollection<MaintenanceRecord> MaintenanceRecords { get; set; } = new List<MaintenanceRecord>();

    [ForeignKey("ModelBasinID")]
    [InverseProperty("TreatmentBMPs")]
    public virtual ModelBasin? ModelBasin { get; set; }

    [InverseProperty("TreatmentBMP")]
    public virtual ICollection<NereidResult> NereidResults { get; set; } = new List<NereidResult>();

    [ForeignKey("OwnerOrganizationID")]
    [InverseProperty("TreatmentBMPs")]
    public virtual Organization OwnerOrganization { get; set; } = null!;

    [ForeignKey("PrecipitationZoneID")]
    [InverseProperty("TreatmentBMPs")]
    public virtual PrecipitationZone? PrecipitationZone { get; set; }

    [ForeignKey("ProjectID")]
    [InverseProperty("TreatmentBMPs")]
    public virtual Project? Project { get; set; }

    [InverseProperty("TreatmentBMP")]
    public virtual ICollection<ProjectNereidResult> ProjectNereidResults { get; set; } = new List<ProjectNereidResult>();

    [InverseProperty("TreatmentBMP")]
    public virtual ICollection<RegionalSubbasinRevisionRequest> RegionalSubbasinRevisionRequests { get; set; } = new List<RegionalSubbasinRevisionRequest>();

    [ForeignKey("StormwaterJurisdictionID")]
    [InverseProperty("TreatmentBMPs")]
    public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; } = null!;

    [InverseProperty("TreatmentBMP")]
    public virtual ICollection<TreatmentBMPAssessment> TreatmentBMPAssessments { get; set; } = new List<TreatmentBMPAssessment>();

    [InverseProperty("TreatmentBMP")]
    public virtual ICollection<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get; set; } = new List<TreatmentBMPBenchmarkAndThreshold>();

    [InverseProperty("TreatmentBMP")]
    public virtual ICollection<TreatmentBMPDocument> TreatmentBMPDocuments { get; set; } = new List<TreatmentBMPDocument>();

    [InverseProperty("TreatmentBMP")]
    public virtual ICollection<TreatmentBMPImage> TreatmentBMPImages { get; set; } = new List<TreatmentBMPImage>();

    [InverseProperty("TreatmentBMP")]
    public virtual TreatmentBMPModelingAttribute? TreatmentBMPModelingAttributeTreatmentBMP { get; set; }

    [InverseProperty("UpstreamTreatmentBMP")]
    public virtual ICollection<TreatmentBMPModelingAttribute> TreatmentBMPModelingAttributeUpstreamTreatmentBMPs { get; set; } = new List<TreatmentBMPModelingAttribute>();

    [ForeignKey("TreatmentBMPTypeID")]
    [InverseProperty("TreatmentBMPs")]
    public virtual TreatmentBMPType TreatmentBMPType { get; set; } = null!;

    [ForeignKey("UpstreamBMPID")]
    [InverseProperty("InverseUpstreamBMP")]
    public virtual TreatmentBMP? UpstreamBMP { get; set; }

    [ForeignKey("WaterQualityManagementPlanID")]
    [InverseProperty("TreatmentBMPs")]
    public virtual WaterQualityManagementPlan? WaterQualityManagementPlan { get; set; }

    [InverseProperty("TreatmentBMP")]
    public virtual ICollection<WaterQualityManagementPlanVerifyTreatmentBMP> WaterQualityManagementPlanVerifyTreatmentBMPs { get; set; } = new List<WaterQualityManagementPlanVerifyTreatmentBMP>();

    [ForeignKey("WatershedID")]
    [InverseProperty("TreatmentBMPs")]
    public virtual Watershed? Watershed { get; set; }
}
