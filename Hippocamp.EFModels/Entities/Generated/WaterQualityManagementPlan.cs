using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlan")]
    [Index(nameof(WaterQualityManagementPlanName), nameof(StormwaterJurisdictionID), Name = "AK_WaterQualityManagementPlan_WaterQualityManagementPlanName_StormwaterJurisdictionID", IsUnique = true)]
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
        public string WaterQualityManagementPlanName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ApprovalDate { get; set; }
        [StringLength(100)]
        public string MaintenanceContactName { get; set; }
        [StringLength(100)]
        public string MaintenanceContactOrganization { get; set; }
        [StringLength(100)]
        public string MaintenanceContactPhone { get; set; }
        [StringLength(100)]
        public string MaintenanceContactAddress1 { get; set; }
        [StringLength(100)]
        public string MaintenanceContactAddress2 { get; set; }
        [StringLength(100)]
        public string MaintenanceContactCity { get; set; }
        [StringLength(100)]
        public string MaintenanceContactState { get; set; }
        [StringLength(100)]
        public string MaintenanceContactZip { get; set; }
        public int? WaterQualityManagementPlanPermitTermID { get; set; }
        public int? HydromodificationAppliesTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfContruction { get; set; }
        public int? HydrologicSubareaID { get; set; }
        [StringLength(500)]
        public string RecordNumber { get; set; }
        [Column(TypeName = "decimal(5, 1)")]
        public decimal? RecordedWQMPAreaInAcres { get; set; }
        public int TrashCaptureStatusTypeID { get; set; }
        public int? TrashCaptureEffectiveness { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry WaterQualityManagementPlanBoundary { get; set; }
        public int WaterQualityManagementPlanModelingApproachID { get; set; }

        [ForeignKey(nameof(HydrologicSubareaID))]
        [InverseProperty("WaterQualityManagementPlans")]
        public virtual HydrologicSubarea HydrologicSubarea { get; set; }
        [ForeignKey(nameof(HydromodificationAppliesTypeID))]
        [InverseProperty("WaterQualityManagementPlans")]
        public virtual HydromodificationAppliesType HydromodificationAppliesType { get; set; }
        [ForeignKey(nameof(StormwaterJurisdictionID))]
        [InverseProperty("WaterQualityManagementPlans")]
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        [ForeignKey(nameof(TrashCaptureStatusTypeID))]
        [InverseProperty("WaterQualityManagementPlans")]
        public virtual TrashCaptureStatusType TrashCaptureStatusType { get; set; }
        [ForeignKey(nameof(WaterQualityManagementPlanDevelopmentTypeID))]
        [InverseProperty("WaterQualityManagementPlans")]
        public virtual WaterQualityManagementPlanDevelopmentType WaterQualityManagementPlanDevelopmentType { get; set; }
        [ForeignKey(nameof(WaterQualityManagementPlanLandUseID))]
        [InverseProperty("WaterQualityManagementPlans")]
        public virtual WaterQualityManagementPlanLandUse WaterQualityManagementPlanLandUse { get; set; }
        [ForeignKey(nameof(WaterQualityManagementPlanModelingApproachID))]
        [InverseProperty("WaterQualityManagementPlans")]
        public virtual WaterQualityManagementPlanModelingApproach WaterQualityManagementPlanModelingApproach { get; set; }
        [ForeignKey(nameof(WaterQualityManagementPlanPermitTermID))]
        [InverseProperty("WaterQualityManagementPlans")]
        public virtual WaterQualityManagementPlanPermitTerm WaterQualityManagementPlanPermitTerm { get; set; }
        [ForeignKey(nameof(WaterQualityManagementPlanPriorityID))]
        [InverseProperty("WaterQualityManagementPlans")]
        public virtual WaterQualityManagementPlanPriority WaterQualityManagementPlanPriority { get; set; }
        [ForeignKey(nameof(WaterQualityManagementPlanStatusID))]
        [InverseProperty("WaterQualityManagementPlans")]
        public virtual WaterQualityManagementPlanStatus WaterQualityManagementPlanStatus { get; set; }
        [InverseProperty(nameof(LoadGeneratingUnit.WaterQualityManagementPlan))]
        public virtual ICollection<LoadGeneratingUnit> LoadGeneratingUnits { get; set; }
        [InverseProperty(nameof(ProjectLoadGeneratingUnit.WaterQualityManagementPlan))]
        public virtual ICollection<ProjectLoadGeneratingUnit> ProjectLoadGeneratingUnits { get; set; }
        [InverseProperty(nameof(QuickBMP.WaterQualityManagementPlan))]
        public virtual ICollection<QuickBMP> QuickBMPs { get; set; }
        [InverseProperty(nameof(SourceControlBMP.WaterQualityManagementPlan))]
        public virtual ICollection<SourceControlBMP> SourceControlBMPs { get; set; }
        [InverseProperty(nameof(TreatmentBMP.WaterQualityManagementPlan))]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
        [InverseProperty(nameof(WaterQualityManagementPlanDocument.WaterQualityManagementPlan))]
        public virtual ICollection<WaterQualityManagementPlanDocument> WaterQualityManagementPlanDocuments { get; set; }
        [InverseProperty(nameof(WaterQualityManagementPlanParcel.WaterQualityManagementPlan))]
        public virtual ICollection<WaterQualityManagementPlanParcel> WaterQualityManagementPlanParcels { get; set; }
        [InverseProperty(nameof(WaterQualityManagementPlanVerify.WaterQualityManagementPlan))]
        public virtual ICollection<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifies { get; set; }
    }
}
