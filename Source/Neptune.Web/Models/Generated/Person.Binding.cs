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
    [Table("[dbo].[Person]")]
    public partial class Person : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected Person()
        {
            this.AuditLogs = new HashSet<AuditLog>();
            this.FieldVisitsWhereYouAreThePerformedByPerson = new HashSet<FieldVisit>();
            this.FileResourcesWhereYouAreTheCreatePerson = new HashSet<FileResource>();
            this.ModeledCatchmentGeometryStagings = new HashSet<ModeledCatchmentGeometryStaging>();
            this.Notifications = new HashSet<Notification>();
            this.OrganizationsWhereYouAreThePrimaryContactPerson = new HashSet<Organization>();
            this.StormwaterJurisdictionPeople = new HashSet<StormwaterJurisdictionPerson>();
            this.SupportRequestLogsWhereYouAreTheRequestPerson = new HashSet<SupportRequestLog>();
            this.TenantAttributesWhereYouAreThePrimaryContactPerson = new HashSet<TenantAttribute>();
            this.TreatmentBMPsWhereYouAreTheInventoryVerifiedByPerson = new HashSet<TreatmentBMP>();
            this.WaterQualityManagementPlanVerifiesWhereYouAreTheLastEditedByPerson = new HashSet<WaterQualityManagementPlanVerify>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Person(int personID, Guid personGuid, string firstName, string lastName, string email, string phone, int roleID, DateTime createDate, DateTime? updateDate, DateTime? lastActivityDate, bool isActive, int organizationID, bool receiveSupportEmails, string loginName) : this()
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
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Person(Guid personGuid, string firstName, string lastName, string email, int roleID, DateTime createDate, bool isActive, int organizationID, bool receiveSupportEmails, string loginName) : this()
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
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public Person(Guid personGuid, string firstName, string lastName, string email, Role role, DateTime createDate, bool isActive, Organization organization, bool receiveSupportEmails, string loginName) : this()
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
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Person CreateNewBlank(Role role, Organization organization)
        {
            return new Person(default(Guid), default(string), default(string), default(string), role, default(DateTime), default(bool), organization, default(bool), default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return AuditLogs.Any() || FieldVisitsWhereYouAreThePerformedByPerson.Any() || FileResourcesWhereYouAreTheCreatePerson.Any() || ModeledCatchmentGeometryStagings.Any() || Notifications.Any() || OrganizationsWhereYouAreThePrimaryContactPerson.Any() || StormwaterJurisdictionPeople.Any() || SupportRequestLogsWhereYouAreTheRequestPerson.Any() || TenantAttributesWhereYouAreThePrimaryContactPerson.Any() || TreatmentBMPsWhereYouAreTheInventoryVerifiedByPerson.Any() || WaterQualityManagementPlanVerifiesWhereYouAreTheLastEditedByPerson.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Person).Name, typeof(AuditLog).Name, typeof(FieldVisit).Name, typeof(FileResource).Name, typeof(ModeledCatchmentGeometryStaging).Name, typeof(Notification).Name, typeof(Organization).Name, typeof(StormwaterJurisdictionPerson).Name, typeof(SupportRequestLog).Name, typeof(TenantAttribute).Name, typeof(TreatmentBMP).Name, typeof(WaterQualityManagementPlanVerify).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {

            foreach(var x in AuditLogs.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in FieldVisitsWhereYouAreThePerformedByPerson.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in FileResourcesWhereYouAreTheCreatePerson.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in ModeledCatchmentGeometryStagings.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in Notifications.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in OrganizationsWhereYouAreThePrimaryContactPerson.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in StormwaterJurisdictionPeople.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in SupportRequestLogsWhereYouAreTheRequestPerson.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in TenantAttributesWhereYouAreThePrimaryContactPerson.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in TreatmentBMPsWhereYouAreTheInventoryVerifiedByPerson.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in WaterQualityManagementPlanVerifiesWhereYouAreTheLastEditedByPerson.ToList())
            {
                x.DeleteFull();
            }
            HttpRequestStorage.DatabaseEntities.AllPeople.Remove(this);                
        }

        [Key]
        public int PersonID { get; set; }
        public int TenantID { get; private set; }
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
        [NotMapped]
        public int PrimaryKey { get { return PersonID; } set { PersonID = value; } }

        public virtual ICollection<AuditLog> AuditLogs { get; set; }
        public virtual ICollection<FieldVisit> FieldVisitsWhereYouAreThePerformedByPerson { get; set; }
        public virtual ICollection<FileResource> FileResourcesWhereYouAreTheCreatePerson { get; set; }
        public virtual ICollection<ModeledCatchmentGeometryStaging> ModeledCatchmentGeometryStagings { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Organization> OrganizationsWhereYouAreThePrimaryContactPerson { get; set; }
        public virtual ICollection<StormwaterJurisdictionPerson> StormwaterJurisdictionPeople { get; set; }
        public virtual ICollection<SupportRequestLog> SupportRequestLogsWhereYouAreTheRequestPerson { get; set; }
        public virtual ICollection<TenantAttribute> TenantAttributesWhereYouAreThePrimaryContactPerson { get; set; }
        public virtual ICollection<TreatmentBMP> TreatmentBMPsWhereYouAreTheInventoryVerifiedByPerson { get; set; }
        public virtual ICollection<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifiesWhereYouAreTheLastEditedByPerson { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
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