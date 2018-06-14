//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanLandUse]
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
    [Table("[dbo].[WaterQualityManagementPlanLandUse]")]
    public partial class WaterQualityManagementPlanLandUse : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected WaterQualityManagementPlanLandUse()
        {
            this.WaterQualityManagementPlans = new HashSet<WaterQualityManagementPlan>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanLandUse(int waterQualityManagementPlanLandUseID, string waterQualityManagementPlanLandUseName, string waterQualityManagementPlanLandUseDisplayName, int sortOrder) : this()
        {
            this.WaterQualityManagementPlanLandUseID = waterQualityManagementPlanLandUseID;
            this.WaterQualityManagementPlanLandUseName = waterQualityManagementPlanLandUseName;
            this.WaterQualityManagementPlanLandUseDisplayName = waterQualityManagementPlanLandUseDisplayName;
            this.SortOrder = sortOrder;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanLandUse(string waterQualityManagementPlanLandUseName, string waterQualityManagementPlanLandUseDisplayName, int sortOrder) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanLandUseID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.WaterQualityManagementPlanLandUseName = waterQualityManagementPlanLandUseName;
            this.WaterQualityManagementPlanLandUseDisplayName = waterQualityManagementPlanLandUseDisplayName;
            this.SortOrder = sortOrder;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static WaterQualityManagementPlanLandUse CreateNewBlank()
        {
            return new WaterQualityManagementPlanLandUse(default(string), default(string), default(int));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(WaterQualityManagementPlanLandUse).Name, typeof(WaterQualityManagementPlan).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {

            foreach(var x in WaterQualityManagementPlans.ToList())
            {
                x.DeleteFull();
            }
            HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanLandUses.Remove(this);                
        }

        [Key]
        public int WaterQualityManagementPlanLandUseID { get; set; }
        public string WaterQualityManagementPlanLandUseName { get; set; }
        public string WaterQualityManagementPlanLandUseDisplayName { get; set; }
        public int SortOrder { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanLandUseID; } set { WaterQualityManagementPlanLandUseID = value; } }

        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }

        public static class FieldLengths
        {
            public const int WaterQualityManagementPlanLandUseName = 100;
            public const int WaterQualityManagementPlanLandUseDisplayName = 100;
        }
    }
}