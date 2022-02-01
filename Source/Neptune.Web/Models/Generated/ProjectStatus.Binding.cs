//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectStatus]
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
    // Table [dbo].[ProjectStatus] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectStatus]")]
    public partial class ProjectStatus : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectStatus()
        {
            this.Projects = new HashSet<Project>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectStatus(int projectStatusID, string projectStatusName, string projectStatusDisplayName, int projectStatusSortOrder) : this()
        {
            this.ProjectStatusID = projectStatusID;
            this.ProjectStatusName = projectStatusName;
            this.ProjectStatusDisplayName = projectStatusDisplayName;
            this.ProjectStatusSortOrder = projectStatusSortOrder;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectStatus(string projectStatusName, string projectStatusDisplayName, int projectStatusSortOrder) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectStatusID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectStatusName = projectStatusName;
            this.ProjectStatusDisplayName = projectStatusDisplayName;
            this.ProjectStatusSortOrder = projectStatusSortOrder;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectStatus CreateNewBlank()
        {
            return new ProjectStatus(default(string), default(string), default(int));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return Projects.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(Projects.Any())
            {
                dependentObjects.Add(typeof(Project).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectStatus).Name, typeof(Project).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProjectStatuses.Remove(this);
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

            foreach(var x in Projects.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int ProjectStatusID { get; set; }
        public string ProjectStatusName { get; set; }
        public string ProjectStatusDisplayName { get; set; }
        public int ProjectStatusSortOrder { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectStatusID; } set { ProjectStatusID = value; } }

        public virtual ICollection<Project> Projects { get; set; }

        public static class FieldLengths
        {
            public const int ProjectStatusName = 50;
            public const int ProjectStatusDisplayName = 50;
        }
    }
}