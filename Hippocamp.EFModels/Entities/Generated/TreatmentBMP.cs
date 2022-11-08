using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Table("TreatmentBMP")]
    [Index("StormwaterJurisdictionID", "TreatmentBMPName", Name = "AK_TreatmentBMP_StormwaterJurisdictionID_TreatmentBMPName", IsUnique = true)]
    [Index("TreatmentBMPID", "TreatmentBMPTypeID", Name = "AK_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID", IsUnique = true)]
    [Index("LocationPoint", Name = "SPATIAL_TreatmentBMP_LocationPoint")]
    public partial class TreatmentBMP
    {
        public TreatmentBMP()
        {
            CustomAttributeTreatmentBMPNavigations = new HashSet<CustomAttribute>();
            CustomAttributeTreatmentBMPs = new HashSet<CustomAttribute>();
            DirtyModelNodes = new HashSet<DirtyModelNode>();
            FundingEvents = new HashSet<FundingEvent>();
            InverseUpstreamBMP = new HashSet<TreatmentBMP>();
            MaintenanceRecordTreatmentBMPNavigations = new HashSet<MaintenanceRecord>();
            MaintenanceRecordTreatmentBMPs = new HashSet<MaintenanceRecord>();
            NereidResults = new HashSet<NereidResult>();
            ProjectNereidResults = new HashSet<ProjectNereidResult>();
            RegionalSubbasinRevisionRequests = new HashSet<RegionalSubbasinRevisionRequest>();
            TreatmentBMPAssessmentTreatmentBMPNavigations = new HashSet<TreatmentBMPAssessment>();
            TreatmentBMPAssessmentTreatmentBMPs = new HashSet<TreatmentBMPAssessment>();
            TreatmentBMPBenchmarkAndThresholdTreatmentBMPNavigations = new HashSet<TreatmentBMPBenchmarkAndThreshold>();
            TreatmentBMPBenchmarkAndThresholdTreatmentBMPs = new HashSet<TreatmentBMPBenchmarkAndThreshold>();
            TreatmentBMPDocuments = new HashSet<TreatmentBMPDocument>();
            TreatmentBMPImages = new HashSet<TreatmentBMPImage>();
            TreatmentBMPModelingAttributeUpstreamTreatmentBMPs = new HashSet<TreatmentBMPModelingAttribute>();
            WaterQualityManagementPlanVerifyTreatmentBMPs = new HashSet<WaterQualityManagementPlanVerifyTreatmentBMP>();
        }

        [Key]
        public int TreatmentBMPID { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string TreatmentBMPName { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry LocationPoint { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        [StringLength(1000)]
        [Unicode(false)]
        public string Notes { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string SystemOfRecordID { get; set; }
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
        public Geometry LocationPoint4326 { get; set; }
        public int? WatershedID { get; set; }
        public int? ModelBasinID { get; set; }
        public int? PrecipitationZoneID { get; set; }
        public int? UpstreamBMPID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? ProjectID { get; set; }

        [ForeignKey("InventoryVerifiedByPersonID")]
        [InverseProperty("TreatmentBMPs")]
        public virtual Person InventoryVerifiedByPerson { get; set; }
        [ForeignKey("ModelBasinID")]
        [InverseProperty("TreatmentBMPs")]
        public virtual ModelBasin ModelBasin { get; set; }
        [ForeignKey("OwnerOrganizationID")]
        [InverseProperty("TreatmentBMPs")]
        public virtual Organization OwnerOrganization { get; set; }
        [ForeignKey("PrecipitationZoneID")]
        [InverseProperty("TreatmentBMPs")]
        public virtual PrecipitationZone PrecipitationZone { get; set; }
        [ForeignKey("ProjectID")]
        [InverseProperty("TreatmentBMPs")]
        public virtual Project Project { get; set; }
        [ForeignKey("SizingBasisTypeID")]
        [InverseProperty("TreatmentBMPs")]
        public virtual SizingBasisType SizingBasisType { get; set; }
        [ForeignKey("StormwaterJurisdictionID")]
        [InverseProperty("TreatmentBMPs")]
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        [ForeignKey("TrashCaptureStatusTypeID")]
        [InverseProperty("TreatmentBMPs")]
        public virtual TrashCaptureStatusType TrashCaptureStatusType { get; set; }
        [ForeignKey("TreatmentBMPLifespanTypeID")]
        [InverseProperty("TreatmentBMPs")]
        public virtual TreatmentBMPLifespanType TreatmentBMPLifespanType { get; set; }
        [ForeignKey("TreatmentBMPTypeID")]
        [InverseProperty("TreatmentBMPs")]
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        [ForeignKey("UpstreamBMPID")]
        [InverseProperty("InverseUpstreamBMP")]
        public virtual TreatmentBMP UpstreamBMP { get; set; }
        [ForeignKey("WaterQualityManagementPlanID")]
        [InverseProperty("TreatmentBMPs")]
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
        [ForeignKey("WatershedID")]
        [InverseProperty("TreatmentBMPs")]
        public virtual Watershed Watershed { get; set; }
        [InverseProperty("TreatmentBMP")]
        public virtual Delineation Delineation { get; set; }
        [InverseProperty("TreatmentBMP")]
        public virtual FieldVisit FieldVisit { get; set; }
        [InverseProperty("TreatmentBMP")]
        public virtual TreatmentBMPModelingAttribute TreatmentBMPModelingAttributeTreatmentBMP { get; set; }
        public virtual ICollection<CustomAttribute> CustomAttributeTreatmentBMPNavigations { get; set; }
        [InverseProperty("TreatmentBMP")]
        public virtual ICollection<CustomAttribute> CustomAttributeTreatmentBMPs { get; set; }
        [InverseProperty("TreatmentBMP")]
        public virtual ICollection<DirtyModelNode> DirtyModelNodes { get; set; }
        [InverseProperty("TreatmentBMP")]
        public virtual ICollection<FundingEvent> FundingEvents { get; set; }
        [InverseProperty("UpstreamBMP")]
        public virtual ICollection<TreatmentBMP> InverseUpstreamBMP { get; set; }
        public virtual ICollection<MaintenanceRecord> MaintenanceRecordTreatmentBMPNavigations { get; set; }
        [InverseProperty("TreatmentBMP")]
        public virtual ICollection<MaintenanceRecord> MaintenanceRecordTreatmentBMPs { get; set; }
        [InverseProperty("TreatmentBMP")]
        public virtual ICollection<NereidResult> NereidResults { get; set; }
        [InverseProperty("TreatmentBMP")]
        public virtual ICollection<ProjectNereidResult> ProjectNereidResults { get; set; }
        [InverseProperty("TreatmentBMP")]
        public virtual ICollection<RegionalSubbasinRevisionRequest> RegionalSubbasinRevisionRequests { get; set; }
        public virtual ICollection<TreatmentBMPAssessment> TreatmentBMPAssessmentTreatmentBMPNavigations { get; set; }
        [InverseProperty("TreatmentBMP")]
        public virtual ICollection<TreatmentBMPAssessment> TreatmentBMPAssessmentTreatmentBMPs { get; set; }
        public virtual ICollection<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholdTreatmentBMPNavigations { get; set; }
        [InverseProperty("TreatmentBMP")]
        public virtual ICollection<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholdTreatmentBMPs { get; set; }
        [InverseProperty("TreatmentBMP")]
        public virtual ICollection<TreatmentBMPDocument> TreatmentBMPDocuments { get; set; }
        [InverseProperty("TreatmentBMP")]
        public virtual ICollection<TreatmentBMPImage> TreatmentBMPImages { get; set; }
        [InverseProperty("UpstreamTreatmentBMP")]
        public virtual ICollection<TreatmentBMPModelingAttribute> TreatmentBMPModelingAttributeUpstreamTreatmentBMPs { get; set; }
        [InverseProperty("TreatmentBMP")]
        public virtual ICollection<WaterQualityManagementPlanVerifyTreatmentBMP> WaterQualityManagementPlanVerifyTreatmentBMPs { get; set; }
    }
}
