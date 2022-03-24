//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectNetworkSolveHistory]
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
    // Table [dbo].[ProjectNetworkSolveHistory] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectNetworkSolveHistory]")]
    public partial class ProjectNetworkSolveHistory : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectNetworkSolveHistory()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectNetworkSolveHistory(int projectNetworkSolveHistoryID, int projectID, int requestedByPersonID, int projectNetworkSolveHistoryStatusTypeID, DateTime lastUpdated, string errorMessage) : this()
        {
            this.ProjectNetworkSolveHistoryID = projectNetworkSolveHistoryID;
            this.ProjectID = projectID;
            this.RequestedByPersonID = requestedByPersonID;
            this.ProjectNetworkSolveHistoryStatusTypeID = projectNetworkSolveHistoryStatusTypeID;
            this.LastUpdated = lastUpdated;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectNetworkSolveHistory(int projectID, int requestedByPersonID, int projectNetworkSolveHistoryStatusTypeID, DateTime lastUpdated) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectNetworkSolveHistoryID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectID = projectID;
            this.RequestedByPersonID = requestedByPersonID;
            this.ProjectNetworkSolveHistoryStatusTypeID = projectNetworkSolveHistoryStatusTypeID;
            this.LastUpdated = lastUpdated;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProjectNetworkSolveHistory(Project project, Person requestedByPerson, ProjectNetworkSolveHistoryStatusType projectNetworkSolveHistoryStatusType, DateTime lastUpdated) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectNetworkSolveHistoryID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectID = project.ProjectID;
            this.Project = project;
            project.ProjectNetworkSolveHistories.Add(this);
            this.RequestedByPersonID = requestedByPerson.PersonID;
            this.RequestedByPerson = requestedByPerson;
            requestedByPerson.ProjectNetworkSolveHistoriesWhereYouAreTheRequestedByPerson.Add(this);
            this.ProjectNetworkSolveHistoryStatusTypeID = projectNetworkSolveHistoryStatusType.ProjectNetworkSolveHistoryStatusTypeID;
            this.LastUpdated = lastUpdated;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectNetworkSolveHistory CreateNewBlank(Project project, Person requestedByPerson, ProjectNetworkSolveHistoryStatusType projectNetworkSolveHistoryStatusType)
        {
            return new ProjectNetworkSolveHistory(project, requestedByPerson, projectNetworkSolveHistoryStatusType, default(DateTime));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectNetworkSolveHistory).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProjectNetworkSolveHistories.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ProjectNetworkSolveHistoryID { get; set; }
        public int ProjectID { get; set; }
        public int RequestedByPersonID { get; set; }
        public int ProjectNetworkSolveHistoryStatusTypeID { get; set; }
        public DateTime LastUpdated { get; set; }
        public string ErrorMessage { get; set; }
        [NotMapped]
        public HtmlString ErrorMessageHtmlString
        { 
            get { return ErrorMessage == null ? null : new HtmlString(ErrorMessage); }
            set { ErrorMessage = value?.ToString(); }
        }
        [NotMapped]
        public int PrimaryKey { get { return ProjectNetworkSolveHistoryID; } set { ProjectNetworkSolveHistoryID = value; } }

        public virtual Project Project { get; set; }
        public virtual Person RequestedByPerson { get; set; }
        public ProjectNetworkSolveHistoryStatusType ProjectNetworkSolveHistoryStatusType { get { return ProjectNetworkSolveHistoryStatusType.AllLookupDictionary[ProjectNetworkSolveHistoryStatusTypeID]; } }

        public static class FieldLengths
        {

        }
    }
}