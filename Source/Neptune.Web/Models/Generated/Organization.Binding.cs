//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Organization]
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
    // Table [dbo].[Organization] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[Organization]")]
    public partial class Organization : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected Organization()
        {
            this.FundingSources = new HashSet<FundingSource>();
            this.People = new HashSet<Person>();
            this.StormwaterJurisdictions = new HashSet<StormwaterJurisdiction>();
            this.TreatmentBMPsWhereYouAreTheOwnerOrganization = new HashSet<TreatmentBMP>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Organization(int organizationID, Guid? organizationGuid, string organizationName, string organizationShortName, int? primaryContactPersonID, bool isActive, string organizationUrl, int? logoFileResourceID, int organizationTypeID) : this()
        {
            this.OrganizationID = organizationID;
            this.OrganizationGuid = organizationGuid;
            this.OrganizationName = organizationName;
            this.OrganizationShortName = organizationShortName;
            this.PrimaryContactPersonID = primaryContactPersonID;
            this.IsActive = isActive;
            this.OrganizationUrl = organizationUrl;
            this.LogoFileResourceID = logoFileResourceID;
            this.OrganizationTypeID = organizationTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Organization(string organizationName, bool isActive, int organizationTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OrganizationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.OrganizationName = organizationName;
            this.IsActive = isActive;
            this.OrganizationTypeID = organizationTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public Organization(string organizationName, bool isActive, OrganizationType organizationType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OrganizationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.OrganizationName = organizationName;
            this.IsActive = isActive;
            this.OrganizationTypeID = organizationType.OrganizationTypeID;
            this.OrganizationType = organizationType;
            organizationType.Organizations.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Organization CreateNewBlank(OrganizationType organizationType)
        {
            return new Organization(default(string), default(bool), organizationType);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return FundingSources.Any() || People.Any() || (StormwaterJurisdiction != null) || TreatmentBMPsWhereYouAreTheOwnerOrganization.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Organization).Name, typeof(FundingSource).Name, typeof(Person).Name, typeof(StormwaterJurisdiction).Name, typeof(TreatmentBMP).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            dbContext.Organizations.Remove(this);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in FundingSources.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in People.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in StormwaterJurisdictions.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPsWhereYouAreTheOwnerOrganization.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int OrganizationID { get; set; }
        public Guid? OrganizationGuid { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationShortName { get; set; }
        public int? PrimaryContactPersonID { get; set; }
        public bool IsActive { get; set; }
        public string OrganizationUrl { get; set; }
        public int? LogoFileResourceID { get; set; }
        public int OrganizationTypeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return OrganizationID; } set { OrganizationID = value; } }

        public virtual ICollection<FundingSource> FundingSources { get; set; }
        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<StormwaterJurisdiction> StormwaterJurisdictions { get; set; }
        [NotMapped]
        public StormwaterJurisdiction StormwaterJurisdiction { get { return StormwaterJurisdictions.SingleOrDefault(); } set { StormwaterJurisdictions = new List<StormwaterJurisdiction>{value};} }
        public virtual ICollection<TreatmentBMP> TreatmentBMPsWhereYouAreTheOwnerOrganization { get; set; }
        public virtual Person PrimaryContactPerson { get; set; }
        public virtual FileResource LogoFileResource { get; set; }
        public virtual OrganizationType OrganizationType { get; set; }

        public static class FieldLengths
        {
            public const int OrganizationName = 200;
            public const int OrganizationShortName = 50;
            public const int OrganizationUrl = 200;
        }
    }
}