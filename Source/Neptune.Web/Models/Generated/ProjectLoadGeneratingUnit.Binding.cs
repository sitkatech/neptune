//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectLoadGeneratingUnit]
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
    // Table [dbo].[ProjectLoadGeneratingUnit] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectLoadGeneratingUnit]")]
    public partial class ProjectLoadGeneratingUnit : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectLoadGeneratingUnit()
        {
            this.ProjectHRUCharacteristics = new HashSet<ProjectHRUCharacteristic>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectLoadGeneratingUnit(int projectLoadGeneratingUnitID, DbGeometry projectLoadGeneratingUnitGeometry, int projectID, int? modelBasinID, int? regionalSubbasinID, int? delineationID, int? waterQualityManagementPlanID, bool? isEmptyResponseFromHRUService) : this()
        {
            this.ProjectLoadGeneratingUnitID = projectLoadGeneratingUnitID;
            this.ProjectLoadGeneratingUnitGeometry = projectLoadGeneratingUnitGeometry;
            this.ProjectID = projectID;
            this.ModelBasinID = modelBasinID;
            this.RegionalSubbasinID = regionalSubbasinID;
            this.DelineationID = delineationID;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.IsEmptyResponseFromHRUService = isEmptyResponseFromHRUService;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectLoadGeneratingUnit(DbGeometry projectLoadGeneratingUnitGeometry, int projectID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectLoadGeneratingUnitID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectLoadGeneratingUnitGeometry = projectLoadGeneratingUnitGeometry;
            this.ProjectID = projectID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProjectLoadGeneratingUnit(DbGeometry projectLoadGeneratingUnitGeometry, Project project) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectLoadGeneratingUnitID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectLoadGeneratingUnitGeometry = projectLoadGeneratingUnitGeometry;
            this.ProjectID = project.ProjectID;
            this.Project = project;
            project.ProjectLoadGeneratingUnits.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectLoadGeneratingUnit CreateNewBlank(Project project)
        {
            return new ProjectLoadGeneratingUnit(default(DbGeometry), project);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return ProjectHRUCharacteristics.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(ProjectHRUCharacteristics.Any())
            {
                dependentObjects.Add(typeof(ProjectHRUCharacteristic).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectLoadGeneratingUnit).Name, typeof(ProjectHRUCharacteristic).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProjectLoadGeneratingUnits.Remove(this);
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

            foreach(var x in ProjectHRUCharacteristics.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int ProjectLoadGeneratingUnitID { get; set; }
        public DbGeometry ProjectLoadGeneratingUnitGeometry { get; set; }
        public int ProjectID { get; set; }
        public int? ModelBasinID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public bool? IsEmptyResponseFromHRUService { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectLoadGeneratingUnitID; } set { ProjectLoadGeneratingUnitID = value; } }

        public virtual ICollection<ProjectHRUCharacteristic> ProjectHRUCharacteristics { get; set; }
        public virtual Project Project { get; set; }
        public virtual ModelBasin ModelBasin { get; set; }
        public virtual RegionalSubbasin RegionalSubbasin { get; set; }
        public virtual Delineation Delineation { get; set; }
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }

        public static class FieldLengths
        {

        }
    }
}