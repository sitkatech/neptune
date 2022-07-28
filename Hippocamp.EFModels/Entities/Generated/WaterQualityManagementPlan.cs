using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlan")]
    [Index("WaterQualityManagementPlanName", "StormwaterJurisdictionID", Name = "AK_WaterQualityManagementPlan_WaterQualityManagementPlanName_StormwaterJurisdictionID", IsUnique = true)]
    [Index("WaterQualityManagementPlanBoundary", Name = "SPATIAL_WaterQualityManagementPlan_WaterQualityManagementPlanBoundary")]
    [Index("WaterQualityManagementPlanBoundary4326", Name = "SPATIAL_WaterQualityManagementPlan_WaterQualityManagementPlanBoundary4326")]
    public partial class WaterQualityManagementPlan
    {
        public WaterQualityManagementPlan()
        {
            LoadGeneratingUnits = new HashSet<LoadGeneratingUnit>();
            ProjectLoadGeneratingUnits = new HashSet<ProjectLoadGeneratingUnit>();
            QuickBMPs = new HashSet<QuickBMP>();
            SourceControlBMPs = new HashSet<SourceControlBMP>();
            TreatmentBMPs = new HashSet<TreatmentBMP>();
            WaterQualityManagementPlanDocuments = new HashSet<WaterQualityManagementPlanDocument>();
            WaterQualityManagementPlanParcels = new HashSet<WaterQualityManagementPlanParcel>();
            WaterQualityManagementPlanVerifies = new HashSet<WaterQualityManagementPlanVerify>();
        }

        [Key]
        public int WaterQualityManagementPlanID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int? WaterQualityManagementPlanLandUseID { get; set; }
        public int? WaterQualityManagementPlanPriorityID { get; set; }
        public int? WaterQualityManagementPlanStatusID { get; set; }
        public int? WaterQualityManagementPlanDevelopmentTypeID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string WaterQualityManagementPlanName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ApprovalDate { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string MaintenanceContactName { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string MaintenanceContactOrganization { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string MaintenanceContactPhone { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string MaintenanceContactAddress1 { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string MaintenanceContactAddress2 { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string MaintenanceContactCity { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string MaintenanceContactState { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string MaintenanceContactZip { get; set; }
        public int? WaterQualityManagementPlanPermitTermID { get; set; }
        public int? HydromodificationAppliesTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfContruction { get; set; }
        public int? HydrologicSubareaID { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string RecordNumber { get; set; }
        [Column(TypeName = "decimal(5, 1)")]
        public decimal? RecordedWQMPAreaInAcres { get; set; }
        public int TrashCaptureStatusTypeID { get; set; }
        public int? TrashCaptureEffectiveness { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry WaterQualityManagementPlanBoundary { get; set; }
        public int WaterQualityManagementPlanModelingApproachID { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry WaterQualityManagementPlanBoundary4326 { get; set; }
        public double? WaterQualityManagementPlanAreaInAcres { get; set; }

        [ForeignKey("HydrologicSubareaID")]
        [InverseProperty("WaterQualityManagementPlans")]
        public virtual HydrologicSubarea HydrologicSubarea { get; set; }
        [ForeignKey("HydromodificationAppliesTypeID")]
        [InverseProperty("WaterQualityManagementPlans")]
        public virtual HydromodificationAppliesType HydromodificationAppliesType { get; set; }
        [ForeignKey("StormwaterJurisdictionID")]
        [InverseProperty("WaterQualityManagementPlans")]
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        [ForeignKey("TrashCaptureStatusTypeID")]
        [InverseProperty("WaterQualityManagementPlans")]
        public virtual TrashCaptureStatusType TrashCaptureStatusType { get; set; }
        [ForeignKey("WaterQualityManagementPlanDevelopmentTypeID")]
        [InverseProperty("WaterQualityManagementPlans")]
        public virtual WaterQualityManagementPlanDevelopmentType WaterQualityManagementPlanDevelopmentType { get; set; }
        [ForeignKey("WaterQualityManagementPlanLandUseID")]
        [InverseProperty("WaterQualityManagementPlans")]
        public virtual WaterQualityManagementPlanLandUse WaterQualityManagementPlanLandUse { get; set; }
        [ForeignKey("WaterQualityManagementPlanModelingApproachID")]
        [InverseProperty("WaterQualityManagementPlans")]
        public virtual WaterQualityManagementPlanModelingApproach WaterQualityManagementPlanModelingApproach { get; set; }
        [ForeignKey("WaterQualityManagementPlanPermitTermID")]
        [InverseProperty("WaterQualityManagementPlans")]
        public virtual WaterQualityManagementPlanPermitTerm WaterQualityManagementPlanPermitTerm { get; set; }
        [ForeignKey("WaterQualityManagementPlanPriorityID")]
        [InverseProperty("WaterQualityManagementPlans")]
        public virtual WaterQualityManagementPlanPriority WaterQualityManagementPlanPriority { get; set; }
        [ForeignKey("WaterQualityManagementPlanStatusID")]
        [InverseProperty("WaterQualityManagementPlans")]
        public virtual WaterQualityManagementPlanStatus WaterQualityManagementPlanStatus { get; set; }
        [InverseProperty("WaterQualityManagementPlan")]
        public virtual ICollection<LoadGeneratingUnit> LoadGeneratingUnits { get; set; }
        [InverseProperty("WaterQualityManagementPlan")]
        public virtual ICollection<ProjectLoadGeneratingUnit> ProjectLoadGeneratingUnits { get; set; }
        [InverseProperty("WaterQualityManagementPlan")]
        public virtual ICollection<QuickBMP> QuickBMPs { get; set; }
        [InverseProperty("WaterQualityManagementPlan")]
        public virtual ICollection<SourceControlBMP> SourceControlBMPs { get; set; }
        [InverseProperty("WaterQualityManagementPlan")]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
        [InverseProperty("WaterQualityManagementPlan")]
        public virtual ICollection<WaterQualityManagementPlanDocument> WaterQualityManagementPlanDocuments { get; set; }
        [InverseProperty("WaterQualityManagementPlan")]
        public virtual ICollection<WaterQualityManagementPlanParcel> WaterQualityManagementPlanParcels { get; set; }
        [InverseProperty("WaterQualityManagementPlan")]
        public virtual ICollection<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifies { get; set; }
    }
}
