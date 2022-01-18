using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("Person")]
    public partial class Person
    {
        public Person()
        {
            AuditLogs = new HashSet<AuditLog>();
            DelineationStagings = new HashSet<DelineationStaging>();
            Delineations = new HashSet<Delineation>();
            FieldVisits = new HashSet<FieldVisit>();
            FileResources = new HashSet<FileResource>();
            LandUseBlockStagings = new HashSet<LandUseBlockStaging>();
            Notifications = new HashSet<Notification>();
            OnlandVisualTrashAssessments = new HashSet<OnlandVisualTrashAssessment>();
            Organizations = new HashSet<Organization>();
            RegionalSubbasinRevisionRequestClosedByPeople = new HashSet<RegionalSubbasinRevisionRequest>();
            RegionalSubbasinRevisionRequestRequestPeople = new HashSet<RegionalSubbasinRevisionRequest>();
            StormwaterJurisdictionPeople = new HashSet<StormwaterJurisdictionPerson>();
            SupportRequestLogs = new HashSet<SupportRequestLog>();
            TrashGeneratingUnitAdjustments = new HashSet<TrashGeneratingUnitAdjustment>();
            TreatmentBMPs = new HashSet<TreatmentBMP>();
            WaterQualityManagementPlanVerifies = new HashSet<WaterQualityManagementPlanVerify>();
        }

        [Key]
        public int PersonID { get; set; }
        public Guid PersonGuid { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(30)]
        public string Phone { get; set; }
        public int RoleID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastActivityDate { get; set; }
        public bool IsActive { get; set; }
        public int OrganizationID { get; set; }
        public bool ReceiveSupportEmails { get; set; }
        [Required]
        [StringLength(128)]
        public string LoginName { get; set; }
        public bool ReceiveRSBRevisionRequestEmails { get; set; }
        public Guid WebServiceAccessToken { get; set; }

        [ForeignKey(nameof(OrganizationID))]
        [InverseProperty("People")]
        public virtual Organization Organization { get; set; }
        [ForeignKey(nameof(RoleID))]
        [InverseProperty("People")]
        public virtual Role Role { get; set; }
        [InverseProperty(nameof(AuditLog.Person))]
        public virtual ICollection<AuditLog> AuditLogs { get; set; }
        [InverseProperty(nameof(DelineationStaging.UploadedByPerson))]
        public virtual ICollection<DelineationStaging> DelineationStagings { get; set; }
        [InverseProperty(nameof(Delineation.VerifiedByPerson))]
        public virtual ICollection<Delineation> Delineations { get; set; }
        [InverseProperty(nameof(FieldVisit.PerformedByPerson))]
        public virtual ICollection<FieldVisit> FieldVisits { get; set; }
        [InverseProperty(nameof(FileResource.CreatePerson))]
        public virtual ICollection<FileResource> FileResources { get; set; }
        [InverseProperty(nameof(LandUseBlockStaging.UploadedByPerson))]
        public virtual ICollection<LandUseBlockStaging> LandUseBlockStagings { get; set; }
        [InverseProperty(nameof(Notification.Person))]
        public virtual ICollection<Notification> Notifications { get; set; }
        [InverseProperty(nameof(OnlandVisualTrashAssessment.CreatedByPerson))]
        public virtual ICollection<OnlandVisualTrashAssessment> OnlandVisualTrashAssessments { get; set; }
        [InverseProperty("PrimaryContactPerson")]
        public virtual ICollection<Organization> Organizations { get; set; }
        [InverseProperty(nameof(RegionalSubbasinRevisionRequest.ClosedByPerson))]
        public virtual ICollection<RegionalSubbasinRevisionRequest> RegionalSubbasinRevisionRequestClosedByPeople { get; set; }
        [InverseProperty(nameof(RegionalSubbasinRevisionRequest.RequestPerson))]
        public virtual ICollection<RegionalSubbasinRevisionRequest> RegionalSubbasinRevisionRequestRequestPeople { get; set; }
        [InverseProperty(nameof(StormwaterJurisdictionPerson.Person))]
        public virtual ICollection<StormwaterJurisdictionPerson> StormwaterJurisdictionPeople { get; set; }
        [InverseProperty(nameof(SupportRequestLog.RequestPerson))]
        public virtual ICollection<SupportRequestLog> SupportRequestLogs { get; set; }
        [InverseProperty(nameof(TrashGeneratingUnitAdjustment.AdjustedByPerson))]
        public virtual ICollection<TrashGeneratingUnitAdjustment> TrashGeneratingUnitAdjustments { get; set; }
        [InverseProperty(nameof(TreatmentBMP.InventoryVerifiedByPerson))]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
        [InverseProperty(nameof(WaterQualityManagementPlanVerify.LastEditedByPerson))]
        public virtual ICollection<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifies { get; set; }
    }
}
