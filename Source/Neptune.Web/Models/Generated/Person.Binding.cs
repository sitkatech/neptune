//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Person]
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    // Table [dbo].[Person] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[Person]")]
    public partial class Person : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected Person()
        {
            this.AuditLogs = new HashSet<AuditLog>();
            this.DelineationsWhereYouAreTheVerifiedByPerson = new HashSet<Delineation>();
            this.DelineationStagingsWhereYouAreTheUploadedByPerson = new HashSet<DelineationStaging>();
            this.FieldVisitsWhereYouAreThePerformedByPerson = new HashSet<FieldVisit>();
            this.FileResourcesWhereYouAreTheCreatePerson = new HashSet<FileResource>();
            this.LandUseBlockStagingsWhereYouAreTheUploadedByPerson = new HashSet<LandUseBlockStaging>();
            this.Notifications = new HashSet<Notification>();
            this.OnlandVisualTrashAssessmentsWhereYouAreTheCreatedByPerson = new HashSet<OnlandVisualTrashAssessment>();
            this.OrganizationsWhereYouAreThePrimaryContactPerson = new HashSet<Organization>();
            this.ProjectsWhereYouAreTheCreatePerson = new HashSet<Project>();
            this.ProjectsWhereYouAreThePrimaryContactPerson = new HashSet<Project>();
            this.ProjectNetworkSolveHistoriesWhereYouAreTheRequestedByPerson = new HashSet<ProjectNetworkSolveHistory>();
            this.RegionalSubbasinRevisionRequestsWhereYouAreTheClosedByPerson = new HashSet<RegionalSubbasinRevisionRequest>();
            this.RegionalSubbasinRevisionRequestsWhereYouAreTheRequestPerson = new HashSet<RegionalSubbasinRevisionRequest>();
            this.StormwaterJurisdictionPeople = new HashSet<StormwaterJurisdictionPerson>();
            this.SupportRequestLogsWhereYouAreTheRequestPerson = new HashSet<SupportRequestLog>();
            this.TrashGeneratingUnitAdjustmentsWhereYouAreTheAdjustedByPerson = new HashSet<TrashGeneratingUnitAdjustment>();
            this.TreatmentBMPsWhereYouAreTheInventoryVerifiedByPerson = new HashSet<TreatmentBMP>();
            this.WaterQualityManagementPlanVerifiesWhereYouAreTheLastEditedByPerson = new HashSet<WaterQualityManagementPlanVerify>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Person(int personID, Guid personGuid, string firstName, string lastName, string email, string phone, int roleID, DateTime createDate, DateTime? updateDate, DateTime? lastActivityDate, bool isActive, int organizationID, bool receiveSupportEmails, string loginName, bool receiveRSBRevisionRequestEmails, Guid webServiceAccessToken) : this()
        {
            this.PersonID = personID;
            this.PersonGuid = personGuid;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;
            this.RoleID = roleID;
            this.CreateDate = createDate;
            this.UpdateDate = updateDate;
            this.LastActivityDate = lastActivityDate;
            this.IsActive = isActive;
            this.OrganizationID = organizationID;
            this.ReceiveSupportEmails = receiveSupportEmails;
            this.LoginName = loginName;
            this.ReceiveRSBRevisionRequestEmails = receiveRSBRevisionRequestEmails;
            this.WebServiceAccessToken = webServiceAccessToken;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Person(Guid personGuid, string firstName, string lastName, string email, int roleID, DateTime createDate, bool isActive, int organizationID, bool receiveSupportEmails, string loginName, bool receiveRSBRevisionRequestEmails, Guid webServiceAccessToken) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.PersonID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.PersonGuid = personGuid;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.RoleID = roleID;
            this.CreateDate = createDate;
            this.IsActive = isActive;
            this.OrganizationID = organizationID;
            this.ReceiveSupportEmails = receiveSupportEmails;
            this.LoginName = loginName;
            this.ReceiveRSBRevisionRequestEmails = receiveRSBRevisionRequestEmails;
            this.WebServiceAccessToken = webServiceAccessToken;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public Person(Guid personGuid, string firstName, string lastName, string email, Role role, DateTime createDate, bool isActive, Organization organization, bool receiveSupportEmails, string loginName, bool receiveRSBRevisionRequestEmails, Guid webServiceAccessToken) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.PersonID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.PersonGuid = personGuid;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.RoleID = role.RoleID;
            this.CreateDate = createDate;
            this.IsActive = isActive;
            this.OrganizationID = organization.OrganizationID;
            this.Organization = organization;
            organization.People.Add(this);
            this.ReceiveSupportEmails = receiveSupportEmails;
            this.LoginName = loginName;
            this.ReceiveRSBRevisionRequestEmails = receiveRSBRevisionRequestEmails;
            this.WebServiceAccessToken = webServiceAccessToken;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Person CreateNewBlank(Role role, Organization organization)
        {
            return new Person(default(Guid), default(string), default(string), default(string), role, default(DateTime), default(bool), organization, default(bool), default(string), default(bool), default(Guid));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return AuditLogs.Any() || DelineationsWhereYouAreTheVerifiedByPerson.Any() || DelineationStagingsWhereYouAreTheUploadedByPerson.Any() || FieldVisitsWhereYouAreThePerformedByPerson.Any() || FileResourcesWhereYouAreTheCreatePerson.Any() || LandUseBlockStagingsWhereYouAreTheUploadedByPerson.Any() || Notifications.Any() || OnlandVisualTrashAssessmentsWhereYouAreTheCreatedByPerson.Any() || OrganizationsWhereYouAreThePrimaryContactPerson.Any() || ProjectsWhereYouAreTheCreatePerson.Any() || ProjectsWhereYouAreThePrimaryContactPerson.Any() || ProjectNetworkSolveHistoriesWhereYouAreTheRequestedByPerson.Any() || RegionalSubbasinRevisionRequestsWhereYouAreTheClosedByPerson.Any() || RegionalSubbasinRevisionRequestsWhereYouAreTheRequestPerson.Any() || StormwaterJurisdictionPeople.Any() || SupportRequestLogsWhereYouAreTheRequestPerson.Any() || TrashGeneratingUnitAdjustmentsWhereYouAreTheAdjustedByPerson.Any() || TreatmentBMPsWhereYouAreTheInventoryVerifiedByPerson.Any() || WaterQualityManagementPlanVerifiesWhereYouAreTheLastEditedByPerson.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(AuditLogs.Any())
            {
                dependentObjects.Add(typeof(AuditLog).Name);
            }

            if(DelineationsWhereYouAreTheVerifiedByPerson.Any())
            {
                dependentObjects.Add(typeof(Delineation).Name);
            }

            if(DelineationStagingsWhereYouAreTheUploadedByPerson.Any())
            {
                dependentObjects.Add(typeof(DelineationStaging).Name);
            }

            if(FieldVisitsWhereYouAreThePerformedByPerson.Any())
            {
                dependentObjects.Add(typeof(FieldVisit).Name);
            }

            if(FileResourcesWhereYouAreTheCreatePerson.Any())
            {
                dependentObjects.Add(typeof(FileResource).Name);
            }

            if(LandUseBlockStagingsWhereYouAreTheUploadedByPerson.Any())
            {
                dependentObjects.Add(typeof(LandUseBlockStaging).Name);
            }

            if(Notifications.Any())
            {
                dependentObjects.Add(typeof(Notification).Name);
            }

            if(OnlandVisualTrashAssessmentsWhereYouAreTheCreatedByPerson.Any())
            {
                dependentObjects.Add(typeof(OnlandVisualTrashAssessment).Name);
            }

            if(OrganizationsWhereYouAreThePrimaryContactPerson.Any())
            {
                dependentObjects.Add(typeof(Organization).Name);
            }

            if(ProjectsWhereYouAreTheCreatePerson.Any())
            {
                dependentObjects.Add(typeof(Project).Name);
            }

            if(ProjectsWhereYouAreThePrimaryContactPerson.Any())
            {
                dependentObjects.Add(typeof(Project).Name);
            }

            if(ProjectNetworkSolveHistoriesWhereYouAreTheRequestedByPerson.Any())
            {
                dependentObjects.Add(typeof(ProjectNetworkSolveHistory).Name);
            }

            if(RegionalSubbasinRevisionRequestsWhereYouAreTheClosedByPerson.Any())
            {
                dependentObjects.Add(typeof(RegionalSubbasinRevisionRequest).Name);
            }

            if(RegionalSubbasinRevisionRequestsWhereYouAreTheRequestPerson.Any())
            {
                dependentObjects.Add(typeof(RegionalSubbasinRevisionRequest).Name);
            }

            if(StormwaterJurisdictionPeople.Any())
            {
                dependentObjects.Add(typeof(StormwaterJurisdictionPerson).Name);
            }

            if(SupportRequestLogsWhereYouAreTheRequestPerson.Any())
            {
                dependentObjects.Add(typeof(SupportRequestLog).Name);
            }

            if(TrashGeneratingUnitAdjustmentsWhereYouAreTheAdjustedByPerson.Any())
            {
                dependentObjects.Add(typeof(TrashGeneratingUnitAdjustment).Name);
            }

            if(TreatmentBMPsWhereYouAreTheInventoryVerifiedByPerson.Any())
            {
                dependentObjects.Add(typeof(TreatmentBMP).Name);
            }

            if(WaterQualityManagementPlanVerifiesWhereYouAreTheLastEditedByPerson.Any())
            {
                dependentObjects.Add(typeof(WaterQualityManagementPlanVerify).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Person).Name, typeof(AuditLog).Name, typeof(Delineation).Name, typeof(DelineationStaging).Name, typeof(FieldVisit).Name, typeof(FileResource).Name, typeof(LandUseBlockStaging).Name, typeof(Notification).Name, typeof(OnlandVisualTrashAssessment).Name, typeof(Organization).Name, typeof(Project).Name, typeof(ProjectNetworkSolveHistory).Name, typeof(RegionalSubbasinRevisionRequest).Name, typeof(StormwaterJurisdictionPerson).Name, typeof(SupportRequestLog).Name, typeof(TrashGeneratingUnitAdjustment).Name, typeof(TreatmentBMP).Name, typeof(WaterQualityManagementPlanVerify).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.People.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            Delete(dbContext);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in AuditLogs.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in DelineationsWhereYouAreTheVerifiedByPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in DelineationStagingsWhereYouAreTheUploadedByPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in FieldVisitsWhereYouAreThePerformedByPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in FileResourcesWhereYouAreTheCreatePerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in LandUseBlockStagingsWhereYouAreTheUploadedByPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in Notifications.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in OnlandVisualTrashAssessmentsWhereYouAreTheCreatedByPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in OrganizationsWhereYouAreThePrimaryContactPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectsWhereYouAreTheCreatePerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectsWhereYouAreThePrimaryContactPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectNetworkSolveHistoriesWhereYouAreTheRequestedByPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in RegionalSubbasinRevisionRequestsWhereYouAreTheClosedByPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in RegionalSubbasinRevisionRequestsWhereYouAreTheRequestPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in StormwaterJurisdictionPeople.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in SupportRequestLogsWhereYouAreTheRequestPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TrashGeneratingUnitAdjustmentsWhereYouAreTheAdjustedByPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPsWhereYouAreTheInventoryVerifiedByPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in WaterQualityManagementPlanVerifiesWhereYouAreTheLastEditedByPerson.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int PersonID { get; set; }
        public Guid PersonGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public bool IsActive { get; set; }
        public int OrganizationID { get; set; }
        public bool ReceiveSupportEmails { get; set; }
        public string LoginName { get; set; }
        public bool ReceiveRSBRevisionRequestEmails { get; set; }
        public Guid WebServiceAccessToken { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return PersonID; } set { PersonID = value; } }

        public virtual ICollection<AuditLog> AuditLogs { get; set; }
        public virtual ICollection<Delineation> DelineationsWhereYouAreTheVerifiedByPerson { get; set; }
        public virtual ICollection<DelineationStaging> DelineationStagingsWhereYouAreTheUploadedByPerson { get; set; }
        public virtual ICollection<FieldVisit> FieldVisitsWhereYouAreThePerformedByPerson { get; set; }
        public virtual ICollection<FileResource> FileResourcesWhereYouAreTheCreatePerson { get; set; }
        public virtual ICollection<LandUseBlockStaging> LandUseBlockStagingsWhereYouAreTheUploadedByPerson { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<OnlandVisualTrashAssessment> OnlandVisualTrashAssessmentsWhereYouAreTheCreatedByPerson { get; set; }
        public virtual ICollection<Organization> OrganizationsWhereYouAreThePrimaryContactPerson { get; set; }
        public virtual ICollection<Project> ProjectsWhereYouAreTheCreatePerson { get; set; }
        public virtual ICollection<Project> ProjectsWhereYouAreThePrimaryContactPerson { get; set; }
        public virtual ICollection<ProjectNetworkSolveHistory> ProjectNetworkSolveHistoriesWhereYouAreTheRequestedByPerson { get; set; }
        public virtual ICollection<RegionalSubbasinRevisionRequest> RegionalSubbasinRevisionRequestsWhereYouAreTheClosedByPerson { get; set; }
        public virtual ICollection<RegionalSubbasinRevisionRequest> RegionalSubbasinRevisionRequestsWhereYouAreTheRequestPerson { get; set; }
        public virtual ICollection<StormwaterJurisdictionPerson> StormwaterJurisdictionPeople { get; set; }
        public virtual ICollection<SupportRequestLog> SupportRequestLogsWhereYouAreTheRequestPerson { get; set; }
        public virtual ICollection<TrashGeneratingUnitAdjustment> TrashGeneratingUnitAdjustmentsWhereYouAreTheAdjustedByPerson { get; set; }
        public virtual ICollection<TreatmentBMP> TreatmentBMPsWhereYouAreTheInventoryVerifiedByPerson { get; set; }
        public virtual ICollection<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifiesWhereYouAreTheLastEditedByPerson { get; set; }
        public Role Role { get { return Role.AllLookupDictionary[RoleID]; } }
        public virtual Organization Organization { get; set; }

        public static class FieldLengths
        {
            public const int FirstName = 100;
            public const int LastName = 100;
            public const int Email = 255;
            public const int Phone = 30;
            public const int LoginName = 128;
        }
    }
}