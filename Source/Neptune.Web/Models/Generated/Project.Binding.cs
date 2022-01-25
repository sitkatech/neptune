//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Project]
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
    // Table [dbo].[Project] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[Project]")]
    public partial class Project : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected Project()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Project(int projectID, string projectName, int organizationID, int stormwaterJurisdictionID, int projectStatusID, int primaryContactPersonID, int createPersonID, DateTime dateCreated, string projectDescription, string additionalContactInformation) : this()
        {
            this.ProjectID = projectID;
            this.ProjectName = projectName;
            this.OrganizationID = organizationID;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.ProjectStatusID = projectStatusID;
            this.PrimaryContactPersonID = primaryContactPersonID;
            this.CreatePersonID = createPersonID;
            this.DateCreated = dateCreated;
            this.ProjectDescription = projectDescription;
            this.AdditionalContactInformation = additionalContactInformation;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Project(string projectName, int organizationID, int stormwaterJurisdictionID, int projectStatusID, int primaryContactPersonID, int createPersonID, DateTime dateCreated) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectName = projectName;
            this.OrganizationID = organizationID;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.ProjectStatusID = projectStatusID;
            this.PrimaryContactPersonID = primaryContactPersonID;
            this.CreatePersonID = createPersonID;
            this.DateCreated = dateCreated;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public Project(string projectName, Organization organization, StormwaterJurisdiction stormwaterJurisdiction, ProjectStatus projectStatus, Person primaryContactPerson, Person createPerson, DateTime dateCreated) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectName = projectName;
            this.OrganizationID = organization.OrganizationID;
            this.Organization = organization;
            organization.Projects.Add(this);
            this.StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID;
            this.StormwaterJurisdiction = stormwaterJurisdiction;
            stormwaterJurisdiction.Projects.Add(this);
            this.ProjectStatusID = projectStatus.ProjectStatusID;
            this.PrimaryContactPersonID = primaryContactPerson.PersonID;
            this.PrimaryContactPerson = primaryContactPerson;
            primaryContactPerson.ProjectsWhereYouAreThePrimaryContactPerson.Add(this);
            this.CreatePersonID = createPerson.PersonID;
            this.CreatePerson = createPerson;
            createPerson.ProjectsWhereYouAreTheCreatePerson.Add(this);
            this.DateCreated = dateCreated;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Project CreateNewBlank(Organization organization, StormwaterJurisdiction stormwaterJurisdiction, ProjectStatus projectStatus, Person primaryContactPerson, Person createPerson)
        {
            return new Project(default(string), organization, stormwaterJurisdiction, projectStatus, primaryContactPerson, createPerson, default(DateTime));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return false;
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Project).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.Projects.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int OrganizationID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int ProjectStatusID { get; set; }
        public int PrimaryContactPersonID { get; set; }
        public int CreatePersonID { get; set; }
        public DateTime DateCreated { get; set; }
        public string ProjectDescription { get; set; }
        public string AdditionalContactInformation { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectID; } set { ProjectID = value; } }

        public virtual Organization Organization { get; set; }
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        public ProjectStatus ProjectStatus { get { return ProjectStatus.AllLookupDictionary[ProjectStatusID]; } }
        public virtual Person CreatePerson { get; set; }
        public virtual Person PrimaryContactPerson { get; set; }

        public static class FieldLengths
        {
            public const int ProjectName = 200;
            public const int ProjectDescription = 500;
            public const int AdditionalContactInformation = 500;
        }
    }
}