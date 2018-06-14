//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanPriority]
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
    [Table("[dbo].[WaterQualityManagementPlanPriority]")]
    public partial class WaterQualityManagementPlanPriority : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected WaterQualityManagementPlanPriority()
        {
            this.WaterQualityManagementPlans = new HashSet<WaterQualityManagementPlan>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanPriority(int waterQualityManagementPlanPriorityID, string waterQualityManagementPlanPriorityName, string waterQualityManagementPlanPriorityDisplayName, int sortOrder) : this()
        {
            this.WaterQualityManagementPlanPriorityID = waterQualityManagementPlanPriorityID;
            this.WaterQualityManagementPlanPriorityName = waterQualityManagementPlanPriorityName;
            this.WaterQualityManagementPlanPriorityDisplayName = waterQualityManagementPlanPriorityDisplayName;
            this.SortOrder = sortOrder;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanPriority(string waterQualityManagementPlanPriorityName, string waterQualityManagementPlanPriorityDisplayName, int sortOrder) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanPriorityID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.WaterQualityManagementPlanPriorityName = waterQualityManagementPlanPriorityName;
            this.WaterQualityManagementPlanPriorityDisplayName = waterQualityManagementPlanPriorityDisplayName;
            this.SortOrder = sortOrder;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static WaterQualityManagementPlanPriority CreateNewBlank()
        {
            return new WaterQualityManagementPlanPriority(default(string), default(string), default(int));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return WaterQualityManagementPlans.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(WaterQualityManagementPlanPriority).Name, typeof(WaterQualityManagementPlan).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {

            foreach(var x in WaterQualityManagementPlans.ToList())
            {
                x.DeleteFull();
            }
            HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanPriorities.Remove(this);                
        }

        [Key]
        public int WaterQualityManagementPlanPriorityID { get; set; }
        public string WaterQualityManagementPlanPriorityName { get; set; }
        public string WaterQualityManagementPlanPriorityDisplayName { get; set; }
        public int SortOrder { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanPriorityID; } set { WaterQualityManagementPlanPriorityID = value; } }

        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }

        public static class FieldLengths
        {
            public const int WaterQualityManagementPlanPriorityName = 100;
            public const int WaterQualityManagementPlanPriorityDisplayName = 100;
        }
    }
}