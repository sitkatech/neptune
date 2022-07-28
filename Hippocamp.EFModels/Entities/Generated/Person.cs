using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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
            ParcelStagings = new HashSet<ParcelStaging>();
            ProjectCreatePeople = new HashSet<Project>();
            ProjectNetworkSolveHistories = new HashSet<ProjectNetworkSolveHistory>();
            ProjectPrimaryContactPeople = new HashSet<Project>();
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
        [Unicode(false)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string LastName { get; set; }
        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string Email { get; set; }
        [StringLength(30)]
        [Unicode(false)]
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
        [Unicode(false)]
        public string LoginName { get; set; }
        public bool ReceiveRSBRevisionRequestEmails { get; set; }
        public Guid WebServiceAccessToken { get; set; }
        public bool IsOCTAGrantReviewer { get; set; }

        [ForeignKey("OrganizationID")]
        [InverseProperty("People")]
        public virtual Organization Organization { get; set; }
        [ForeignKey("RoleID")]
        [InverseProperty("People")]
        public virtual Role Role { get; set; }
        [InverseProperty("Person")]
        public virtual ICollection<AuditLog> AuditLogs { get; set; }
        [InverseProperty("UploadedByPerson")]
        public virtual ICollection<DelineationStaging> DelineationStagings { get; set; }
        [InverseProperty("VerifiedByPerson")]
        public virtual ICollection<Delineation> Delineations { get; set; }
        [InverseProperty("PerformedByPerson")]
        public virtual ICollection<FieldVisit> FieldVisits { get; set; }
        [InverseProperty("CreatePerson")]
        public virtual ICollection<FileResource> FileResources { get; set; }
        [InverseProperty("UploadedByPerson")]
        public virtual ICollection<LandUseBlockStaging> LandUseBlockStagings { get; set; }
        [InverseProperty("Person")]
        public virtual ICollection<Notification> Notifications { get; set; }
        [InverseProperty("CreatedByPerson")]
        public virtual ICollection<OnlandVisualTrashAssessment> OnlandVisualTrashAssessments { get; set; }
        [InverseProperty("PrimaryContactPerson")]
        public virtual ICollection<Organization> Organizations { get; set; }
        [InverseProperty("UploadedByPerson")]
        public virtual ICollection<ParcelStaging> ParcelStagings { get; set; }
        [InverseProperty("CreatePerson")]
        public virtual ICollection<Project> ProjectCreatePeople { get; set; }
        [InverseProperty("RequestedByPerson")]
        public virtual ICollection<ProjectNetworkSolveHistory> ProjectNetworkSolveHistories { get; set; }
        [InverseProperty("PrimaryContactPerson")]
        public virtual ICollection<Project> ProjectPrimaryContactPeople { get; set; }
        [InverseProperty("ClosedByPerson")]
        public virtual ICollection<RegionalSubbasinRevisionRequest> RegionalSubbasinRevisionRequestClosedByPeople { get; set; }
        [InverseProperty("RequestPerson")]
        public virtual ICollection<RegionalSubbasinRevisionRequest> RegionalSubbasinRevisionRequestRequestPeople { get; set; }
        [InverseProperty("Person")]
        public virtual ICollection<StormwaterJurisdictionPerson> StormwaterJurisdictionPeople { get; set; }
        [InverseProperty("RequestPerson")]
        public virtual ICollection<SupportRequestLog> SupportRequestLogs { get; set; }
        [InverseProperty("AdjustedByPerson")]
        public virtual ICollection<TrashGeneratingUnitAdjustment> TrashGeneratingUnitAdjustments { get; set; }
        [InverseProperty("InventoryVerifiedByPerson")]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
        [InverseProperty("LastEditedByPerson")]
        public virtual ICollection<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifies { get; set; }
    }
}
