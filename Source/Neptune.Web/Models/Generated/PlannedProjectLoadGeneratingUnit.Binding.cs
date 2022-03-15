//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PlannedProjectLoadGeneratingUnit]
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
    // Table [dbo].[PlannedProjectLoadGeneratingUnit] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[PlannedProjectLoadGeneratingUnit]")]
    public partial class PlannedProjectLoadGeneratingUnit : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected PlannedProjectLoadGeneratingUnit()
        {
            this.PlannedProjectHRUCharacteristics = new HashSet<PlannedProjectHRUCharacteristic>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public PlannedProjectLoadGeneratingUnit(int plannedProjectLoadGeneratingUnitID, DbGeometry plannedProjectLoadGeneratingUnitGeometry, int projectID, int? modelBasinID, int? regionalSubbasinID, int? delineationID, int? waterQualityManagementPlanID) : this()
        {
            this.PlannedProjectLoadGeneratingUnitID = plannedProjectLoadGeneratingUnitID;
            this.PlannedProjectLoadGeneratingUnitGeometry = plannedProjectLoadGeneratingUnitGeometry;
            this.ProjectID = projectID;
            this.ModelBasinID = modelBasinID;
            this.RegionalSubbasinID = regionalSubbasinID;
            this.DelineationID = delineationID;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public PlannedProjectLoadGeneratingUnit(DbGeometry plannedProjectLoadGeneratingUnitGeometry, int projectID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.PlannedProjectLoadGeneratingUnitID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.PlannedProjectLoadGeneratingUnitGeometry = plannedProjectLoadGeneratingUnitGeometry;
            this.ProjectID = projectID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public PlannedProjectLoadGeneratingUnit(DbGeometry plannedProjectLoadGeneratingUnitGeometry, Project project) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.PlannedProjectLoadGeneratingUnitID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.PlannedProjectLoadGeneratingUnitGeometry = plannedProjectLoadGeneratingUnitGeometry;
            this.ProjectID = project.ProjectID;
            this.Project = project;
            project.PlannedProjectLoadGeneratingUnits.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static PlannedProjectLoadGeneratingUnit CreateNewBlank(Project project)
        {
            return new PlannedProjectLoadGeneratingUnit(default(DbGeometry), project);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return PlannedProjectHRUCharacteristics.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(PlannedProjectHRUCharacteristics.Any())
            {
                dependentObjects.Add(typeof(PlannedProjectHRUCharacteristic).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(PlannedProjectLoadGeneratingUnit).Name, typeof(PlannedProjectHRUCharacteristic).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.PlannedProjectLoadGeneratingUnits.Remove(this);
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

            foreach(var x in PlannedProjectHRUCharacteristics.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int PlannedProjectLoadGeneratingUnitID { get; set; }
        public DbGeometry PlannedProjectLoadGeneratingUnitGeometry { get; set; }
        public int ProjectID { get; set; }
        public int? ModelBasinID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return PlannedProjectLoadGeneratingUnitID; } set { PlannedProjectLoadGeneratingUnitID = value; } }

        public virtual ICollection<PlannedProjectHRUCharacteristic> PlannedProjectHRUCharacteristics { get; set; }
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