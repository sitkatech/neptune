//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectDocument]
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
    // Table [dbo].[ProjectDocument] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectDocument]")]
    public partial class ProjectDocument : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectDocument()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectDocument(int projectDocumentID, int fileResourceID, int projectID, string displayName, DateTime uploadDate, string documentDescription) : this()
        {
            this.ProjectDocumentID = projectDocumentID;
            this.FileResourceID = fileResourceID;
            this.ProjectID = projectID;
            this.DisplayName = displayName;
            this.UploadDate = uploadDate;
            this.DocumentDescription = documentDescription;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectDocument(int fileResourceID, int projectID, string displayName, DateTime uploadDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectDocumentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.FileResourceID = fileResourceID;
            this.ProjectID = projectID;
            this.DisplayName = displayName;
            this.UploadDate = uploadDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProjectDocument(FileResource fileResource, Project project, string displayName, DateTime uploadDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectDocumentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.FileResourceID = fileResource.FileResourceID;
            this.FileResource = fileResource;
            fileResource.ProjectDocuments.Add(this);
            this.ProjectID = project.ProjectID;
            this.Project = project;
            project.ProjectDocuments.Add(this);
            this.DisplayName = displayName;
            this.UploadDate = uploadDate;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectDocument CreateNewBlank(FileResource fileResource, Project project)
        {
            return new ProjectDocument(fileResource, project, default(string), default(DateTime));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectDocument).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProjectDocuments.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ProjectDocumentID { get; set; }
        public int FileResourceID { get; set; }
        public int ProjectID { get; set; }
        public string DisplayName { get; set; }
        public DateTime UploadDate { get; set; }
        public string DocumentDescription { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectDocumentID; } set { ProjectDocumentID = value; } }

        public virtual FileResource FileResource { get; set; }
        public virtual Project Project { get; set; }

        public static class FieldLengths
        {
            public const int DisplayName = 200;
            public const int DocumentDescription = 500;
        }
    }
}