//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVisitStatus]
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
    // Table [dbo].[WaterQualityManagementPlanVisitStatus] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[WaterQualityManagementPlanVisitStatus]")]
    public partial class WaterQualityManagementPlanVisitStatus : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected WaterQualityManagementPlanVisitStatus()
        {
            this.WaterQualityManagementPlanVerifies = new HashSet<WaterQualityManagementPlanVerify>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanVisitStatus(int waterQualityManagementPlanVisitStatusID, string waterQualityManagementPlanVisitStatusName) : this()
        {
            this.WaterQualityManagementPlanVisitStatusID = waterQualityManagementPlanVisitStatusID;
            this.WaterQualityManagementPlanVisitStatusName = waterQualityManagementPlanVisitStatusName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanVisitStatus(string waterQualityManagementPlanVisitStatusName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanVisitStatusID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.WaterQualityManagementPlanVisitStatusName = waterQualityManagementPlanVisitStatusName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static WaterQualityManagementPlanVisitStatus CreateNewBlank()
        {
            return new WaterQualityManagementPlanVisitStatus(default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return WaterQualityManagementPlanVerifies.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(WaterQualityManagementPlanVisitStatus).Name, typeof(WaterQualityManagementPlanVerify).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.WaterQualityManagementPlanVisitStatuses.Remove(this);
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

            foreach(var x in WaterQualityManagementPlanVerifies.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int WaterQualityManagementPlanVisitStatusID { get; set; }
        public string WaterQualityManagementPlanVisitStatusName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanVisitStatusID; } set { WaterQualityManagementPlanVisitStatusID = value; } }

        public virtual ICollection<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifies { get; set; }

        public static class FieldLengths
        {
            public const int WaterQualityManagementPlanVisitStatusName = 100;
        }
    }
}