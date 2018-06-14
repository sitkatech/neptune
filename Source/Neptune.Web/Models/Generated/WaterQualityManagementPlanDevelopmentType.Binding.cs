//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanDevelopmentType]
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
    [Table("[dbo].[WaterQualityManagementPlanDevelopmentType]")]
    public partial class WaterQualityManagementPlanDevelopmentType : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected WaterQualityManagementPlanDevelopmentType()
        {
            this.WaterQualityManagementPlans = new HashSet<WaterQualityManagementPlan>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanDevelopmentType(int waterQualityManagementPlanDevelopmentTypeID, string waterQualityManagementPlanDevelopmentTypeName, string waterQualityManagementPlanDevelopmentTypeDisplayName, int sortOrder) : this()
        {
            this.WaterQualityManagementPlanDevelopmentTypeID = waterQualityManagementPlanDevelopmentTypeID;
            this.WaterQualityManagementPlanDevelopmentTypeName = waterQualityManagementPlanDevelopmentTypeName;
            this.WaterQualityManagementPlanDevelopmentTypeDisplayName = waterQualityManagementPlanDevelopmentTypeDisplayName;
            this.SortOrder = sortOrder;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanDevelopmentType(string waterQualityManagementPlanDevelopmentTypeName, string waterQualityManagementPlanDevelopmentTypeDisplayName, int sortOrder) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanDevelopmentTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.WaterQualityManagementPlanDevelopmentTypeName = waterQualityManagementPlanDevelopmentTypeName;
            this.WaterQualityManagementPlanDevelopmentTypeDisplayName = waterQualityManagementPlanDevelopmentTypeDisplayName;
            this.SortOrder = sortOrder;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static WaterQualityManagementPlanDevelopmentType CreateNewBlank()
        {
            return new WaterQualityManagementPlanDevelopmentType(default(string), default(string), default(int));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(WaterQualityManagementPlanDevelopmentType).Name, typeof(WaterQualityManagementPlan).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {

            foreach(var x in WaterQualityManagementPlans.ToList())
            {
                x.DeleteFull();
            }
            HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanDevelopmentTypes.Remove(this);                
        }

        [Key]
        public int WaterQualityManagementPlanDevelopmentTypeID { get; set; }
        public string WaterQualityManagementPlanDevelopmentTypeName { get; set; }
        public string WaterQualityManagementPlanDevelopmentTypeDisplayName { get; set; }
        public int SortOrder { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanDevelopmentTypeID; } set { WaterQualityManagementPlanDevelopmentTypeID = value; } }

        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }

        public static class FieldLengths
        {
            public const int WaterQualityManagementPlanDevelopmentTypeName = 100;
            public const int WaterQualityManagementPlanDevelopmentTypeDisplayName = 100;
        }
    }
}