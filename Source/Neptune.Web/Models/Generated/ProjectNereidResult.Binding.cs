//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectNereidResult]
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
    // Table [dbo].[ProjectNereidResult] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectNereidResult]")]
    public partial class ProjectNereidResult : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectNereidResult()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectNereidResult(int projectNereidResultID, int projectID, bool isBaselineCondition, int? treatmentBMPID, int? waterQualityManagementPlanID, int? regionalSubbasinID, int? delineationID, string nodeID, string fullResponse, DateTime? lastUpdate) : this()
        {
            this.ProjectNereidResultID = projectNereidResultID;
            this.ProjectID = projectID;
            this.IsBaselineCondition = isBaselineCondition;
            this.TreatmentBMPID = treatmentBMPID;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.RegionalSubbasinID = regionalSubbasinID;
            this.DelineationID = delineationID;
            this.NodeID = nodeID;
            this.FullResponse = fullResponse;
            this.LastUpdate = lastUpdate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectNereidResult(int projectID, bool isBaselineCondition) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectNereidResultID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectID = projectID;
            this.IsBaselineCondition = isBaselineCondition;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProjectNereidResult(Project project, bool isBaselineCondition) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectNereidResultID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectID = project.ProjectID;
            this.Project = project;
            project.ProjectNereidResults.Add(this);
            this.IsBaselineCondition = isBaselineCondition;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectNereidResult CreateNewBlank(Project project)
        {
            return new ProjectNereidResult(project, default(bool));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectNereidResult).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProjectNereidResults.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ProjectNereidResultID { get; set; }
        public int ProjectID { get; set; }
        public bool IsBaselineCondition { get; set; }
        public int? TreatmentBMPID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        public string NodeID { get; set; }
        public string FullResponse { get; set; }
        public DateTime? LastUpdate { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectNereidResultID; } set { ProjectNereidResultID = value; } }

        public virtual Project Project { get; set; }
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
        public virtual RegionalSubbasin RegionalSubbasin { get; set; }
        public virtual Delineation Delineation { get; set; }

        public static class FieldLengths
        {

        }
    }
}