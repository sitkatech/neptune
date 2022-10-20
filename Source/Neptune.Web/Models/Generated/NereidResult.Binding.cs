//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NereidResult]
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
    // Table [dbo].[NereidResult] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[NereidResult]")]
    public partial class NereidResult : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected NereidResult()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public NereidResult(int nereidResultID, int? treatmentBMPID, int? waterQualityManagementPlanID, int? regionalSubbasinID, int? delineationID, string nodeID, string fullResponse, DateTime? lastUpdate, bool isBaselineCondition) : this()
        {
            this.NereidResultID = nereidResultID;
            this.TreatmentBMPID = treatmentBMPID;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.RegionalSubbasinID = regionalSubbasinID;
            this.DelineationID = delineationID;
            this.NodeID = nodeID;
            this.FullResponse = fullResponse;
            this.LastUpdate = lastUpdate;
            this.IsBaselineCondition = isBaselineCondition;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public NereidResult(string fullResponse, bool isBaselineCondition) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.NereidResultID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.FullResponse = fullResponse;
            this.IsBaselineCondition = isBaselineCondition;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static NereidResult CreateNewBlank()
        {
            return new NereidResult(default(string), default(bool));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(NereidResult).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.NereidResults.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int NereidResultID { get; set; }
        public int? TreatmentBMPID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        public string NodeID { get; set; }
        public string FullResponse { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool IsBaselineCondition { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return NereidResultID; } set { NereidResultID = value; } }

        public virtual TreatmentBMP TreatmentBMP { get; set; }
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
        public virtual RegionalSubbasin RegionalSubbasin { get; set; }
        public virtual Delineation Delineation { get; set; }

        public static class FieldLengths
        {

        }
    }
}