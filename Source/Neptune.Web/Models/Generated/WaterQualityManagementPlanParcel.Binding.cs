//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanParcel]
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
    [Table("[dbo].[WaterQualityManagementPlanParcel]")]
    public partial class WaterQualityManagementPlanParcel : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected WaterQualityManagementPlanParcel()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanParcel(int waterQualityManagementPlanParcelID, int waterQualityManagementPlanID, int parcelID) : this()
        {
            this.WaterQualityManagementPlanParcelID = waterQualityManagementPlanParcelID;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.ParcelID = parcelID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanParcel(int waterQualityManagementPlanID, int parcelID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanParcelID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.ParcelID = parcelID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public WaterQualityManagementPlanParcel(WaterQualityManagementPlan waterQualityManagementPlan, Parcel parcel) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanParcelID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID;
            this.WaterQualityManagementPlan = waterQualityManagementPlan;
            waterQualityManagementPlan.WaterQualityManagementPlanParcels.Add(this);
            this.ParcelID = parcel.ParcelID;
            this.Parcel = parcel;
            parcel.WaterQualityManagementPlanParcels.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static WaterQualityManagementPlanParcel CreateNewBlank(WaterQualityManagementPlan waterQualityManagementPlan, Parcel parcel)
        {
            return new WaterQualityManagementPlanParcel(waterQualityManagementPlan, parcel);
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
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(WaterQualityManagementPlanParcel).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(HttpRequestStorage.DatabaseEntities);
            dbContext.AllWaterQualityManagementPlanParcels.Remove(this);
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

        }

        [Key]
        public int WaterQualityManagementPlanParcelID { get; set; }
        public int TenantID { get; private set; }
        public int WaterQualityManagementPlanID { get; set; }
        public int ParcelID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanParcelID; } set { WaterQualityManagementPlanParcelID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
        public virtual Parcel Parcel { get; set; }

        public static class FieldLengths
        {

        }
    }
}