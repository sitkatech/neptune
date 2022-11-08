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
            this.ProjectDocuments = new HashSet<ProjectDocument>();
            this.ProjectHRUCharacteristics = new HashSet<ProjectHRUCharacteristic>();
            this.ProjectLoadGeneratingUnits = new HashSet<ProjectLoadGeneratingUnit>();
            this.ProjectNereidResults = new HashSet<ProjectNereidResult>();
            this.ProjectNetworkSolveHistories = new HashSet<ProjectNetworkSolveHistory>();
            this.TreatmentBMPs = new HashSet<TreatmentBMP>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Project(int projectID, string projectName, int organizationID, int stormwaterJurisdictionID, int projectStatusID, int primaryContactPersonID, int createPersonID, DateTime dateCreated, string projectDescription, string additionalContactInformation, bool doesNotIncludeTreatmentBMPs, bool calculateOCTAM2Tier2Scores, bool shareOCTAM2Tier2Scores, DateTime? oCTAM2Tier2ScoresLastSharedDate, string oCTAWatersheds, double? pollutantVolume, double? pollutantMetals, double? pollutantBacteria, double? pollutantNutrients, double? pollutantTSS, double? tPI, double? sEA, double? dryWeatherWQLRI, double? wetWeatherWQLRI, double? areaTreatedAcres, double? imperviousAreaTreatedAcres) : this()
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
            this.DoesNotIncludeTreatmentBMPs = doesNotIncludeTreatmentBMPs;
            this.CalculateOCTAM2Tier2Scores = calculateOCTAM2Tier2Scores;
            this.ShareOCTAM2Tier2Scores = shareOCTAM2Tier2Scores;
            this.OCTAM2Tier2ScoresLastSharedDate = oCTAM2Tier2ScoresLastSharedDate;
            this.OCTAWatersheds = oCTAWatersheds;
            this.PollutantVolume = pollutantVolume;
            this.PollutantMetals = pollutantMetals;
            this.PollutantBacteria = pollutantBacteria;
            this.PollutantNutrients = pollutantNutrients;
            this.PollutantTSS = pollutantTSS;
            this.TPI = tPI;
            this.SEA = sEA;
            this.DryWeatherWQLRI = dryWeatherWQLRI;
            this.WetWeatherWQLRI = wetWeatherWQLRI;
            this.AreaTreatedAcres = areaTreatedAcres;
            this.ImperviousAreaTreatedAcres = imperviousAreaTreatedAcres;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Project(string projectName, int organizationID, int stormwaterJurisdictionID, int projectStatusID, int primaryContactPersonID, int createPersonID, DateTime dateCreated, bool doesNotIncludeTreatmentBMPs, bool calculateOCTAM2Tier2Scores, bool shareOCTAM2Tier2Scores) : this()
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
            this.DoesNotIncludeTreatmentBMPs = doesNotIncludeTreatmentBMPs;
            this.CalculateOCTAM2Tier2Scores = calculateOCTAM2Tier2Scores;
            this.ShareOCTAM2Tier2Scores = shareOCTAM2Tier2Scores;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public Project(string projectName, Organization organization, StormwaterJurisdiction stormwaterJurisdiction, ProjectStatus projectStatus, Person primaryContactPerson, Person createPerson, DateTime dateCreated, bool doesNotIncludeTreatmentBMPs, bool calculateOCTAM2Tier2Scores, bool shareOCTAM2Tier2Scores) : this()
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
            this.ProjectStatus = projectStatus;
            projectStatus.Projects.Add(this);
            this.PrimaryContactPersonID = primaryContactPerson.PersonID;
            this.PrimaryContactPerson = primaryContactPerson;
            primaryContactPerson.ProjectsWhereYouAreThePrimaryContactPerson.Add(this);
            this.CreatePersonID = createPerson.PersonID;
            this.CreatePerson = createPerson;
            createPerson.ProjectsWhereYouAreTheCreatePerson.Add(this);
            this.DateCreated = dateCreated;
            this.DoesNotIncludeTreatmentBMPs = doesNotIncludeTreatmentBMPs;
            this.CalculateOCTAM2Tier2Scores = calculateOCTAM2Tier2Scores;
            this.ShareOCTAM2Tier2Scores = shareOCTAM2Tier2Scores;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Project CreateNewBlank(Organization organization, StormwaterJurisdiction stormwaterJurisdiction, ProjectStatus projectStatus, Person primaryContactPerson, Person createPerson)
        {
            return new Project(default(string), organization, stormwaterJurisdiction, projectStatus, primaryContactPerson, createPerson, default(DateTime), default(bool), default(bool), default(bool));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return ProjectDocuments.Any() || ProjectHRUCharacteristics.Any() || ProjectLoadGeneratingUnits.Any() || ProjectNereidResults.Any() || ProjectNetworkSolveHistories.Any() || TreatmentBMPs.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(ProjectDocuments.Any())
            {
                dependentObjects.Add(typeof(ProjectDocument).Name);
            }

            if(ProjectHRUCharacteristics.Any())
            {
                dependentObjects.Add(typeof(ProjectHRUCharacteristic).Name);
            }

            if(ProjectLoadGeneratingUnits.Any())
            {
                dependentObjects.Add(typeof(ProjectLoadGeneratingUnit).Name);
            }

            if(ProjectNereidResults.Any())
            {
                dependentObjects.Add(typeof(ProjectNereidResult).Name);
            }

            if(ProjectNetworkSolveHistories.Any())
            {
                dependentObjects.Add(typeof(ProjectNetworkSolveHistory).Name);
            }

            if(TreatmentBMPs.Any())
            {
                dependentObjects.Add(typeof(TreatmentBMP).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Project).Name, typeof(ProjectDocument).Name, typeof(ProjectHRUCharacteristic).Name, typeof(ProjectLoadGeneratingUnit).Name, typeof(ProjectNereidResult).Name, typeof(ProjectNetworkSolveHistory).Name, typeof(TreatmentBMP).Name};


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
            DeleteChildren(dbContext);
            Delete(dbContext);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in ProjectDocuments.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectHRUCharacteristics.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectLoadGeneratingUnits.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectNereidResults.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectNetworkSolveHistories.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPs.ToList())
            {
                x.DeleteFull(dbContext);
            }
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
        public bool DoesNotIncludeTreatmentBMPs { get; set; }
        public bool CalculateOCTAM2Tier2Scores { get; set; }
        public bool ShareOCTAM2Tier2Scores { get; set; }
        public DateTime? OCTAM2Tier2ScoresLastSharedDate { get; set; }
        public string OCTAWatersheds { get; set; }
        public double? PollutantVolume { get; set; }
        public double? PollutantMetals { get; set; }
        public double? PollutantBacteria { get; set; }
        public double? PollutantNutrients { get; set; }
        public double? PollutantTSS { get; set; }
        public double? TPI { get; set; }
        public double? SEA { get; set; }
        public double? DryWeatherWQLRI { get; set; }
        public double? WetWeatherWQLRI { get; set; }
        public double? AreaTreatedAcres { get; set; }
        public double? ImperviousAreaTreatedAcres { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectID; } set { ProjectID = value; } }

        public virtual ICollection<ProjectDocument> ProjectDocuments { get; set; }
        public virtual ICollection<ProjectHRUCharacteristic> ProjectHRUCharacteristics { get; set; }
        public virtual ICollection<ProjectLoadGeneratingUnit> ProjectLoadGeneratingUnits { get; set; }
        public virtual ICollection<ProjectNereidResult> ProjectNereidResults { get; set; }
        public virtual ICollection<ProjectNetworkSolveHistory> ProjectNetworkSolveHistories { get; set; }
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        public virtual ProjectStatus ProjectStatus { get; set; }
        public virtual Person CreatePerson { get; set; }
        public virtual Person PrimaryContactPerson { get; set; }

        public static class FieldLengths
        {
            public const int ProjectName = 200;
            public const int ProjectDescription = 500;
            public const int AdditionalContactInformation = 500;
            public const int OCTAWatersheds = 500;
        }
    }
}