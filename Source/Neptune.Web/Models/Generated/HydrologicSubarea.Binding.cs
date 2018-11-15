//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HydrologicSubarea]
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
    [Table("[dbo].[HydrologicSubarea]")]
    public partial class HydrologicSubarea : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected HydrologicSubarea()
        {
            this.WaterQualityManagementPlans = new HashSet<WaterQualityManagementPlan>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public HydrologicSubarea(int hydrologicSubareaID, string hydrologicSubareaName) : this()
        {
            this.HydrologicSubareaID = hydrologicSubareaID;
            this.HydrologicSubareaName = hydrologicSubareaName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public HydrologicSubarea(string hydrologicSubareaName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.HydrologicSubareaID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.HydrologicSubareaName = hydrologicSubareaName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static HydrologicSubarea CreateNewBlank()
        {
            return new HydrologicSubarea(default(string));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(HydrologicSubarea).Name, typeof(WaterQualityManagementPlan).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {
            DeleteFull(HttpRequestStorage.DatabaseEntities);
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {

            foreach(var x in WaterQualityManagementPlans.ToList())
            {
                x.DeleteFull(dbContext);
            }
            dbContext.AllHydrologicSubareas.Remove(this);
        }

        [Key]
        public int HydrologicSubareaID { get; set; }
        public int TenantID { get; private set; }
        public string HydrologicSubareaName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return HydrologicSubareaID; } set { HydrologicSubareaID = value; } }

        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }

        public static class FieldLengths
        {
            public const int HydrologicSubareaName = 100;
        }
    }
}