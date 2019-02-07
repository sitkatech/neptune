//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Parcel]
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
    // Table [dbo].[Parcel] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[Parcel]")]
    public partial class Parcel : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected Parcel()
        {
            this.WaterQualityManagementPlanParcels = new HashSet<WaterQualityManagementPlanParcel>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Parcel(int parcelID, string parcelNumber, DbGeometry parcelGeometry, string ownerName, string parcelStreetNumber, string parcelAddress, string parcelZipCode, string landUse, int? squareFeetHome, int? squareFeetLot, double parcelAreaInAcres) : this()
        {
            this.ParcelID = parcelID;
            this.ParcelNumber = parcelNumber;
            this.ParcelGeometry = parcelGeometry;
            this.OwnerName = ownerName;
            this.ParcelStreetNumber = parcelStreetNumber;
            this.ParcelAddress = parcelAddress;
            this.ParcelZipCode = parcelZipCode;
            this.LandUse = landUse;
            this.SquareFeetHome = squareFeetHome;
            this.SquareFeetLot = squareFeetLot;
            this.ParcelAreaInAcres = parcelAreaInAcres;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Parcel(string parcelNumber, DbGeometry parcelGeometry, double parcelAreaInAcres) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ParcelID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ParcelNumber = parcelNumber;
            this.ParcelGeometry = parcelGeometry;
            this.ParcelAreaInAcres = parcelAreaInAcres;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Parcel CreateNewBlank()
        {
            return new Parcel(default(string), default(DbGeometry), default(double));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return WaterQualityManagementPlanParcels.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Parcel).Name, typeof(WaterQualityManagementPlanParcel).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.Parcels.Remove(this);
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

            foreach(var x in WaterQualityManagementPlanParcels.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int ParcelID { get; set; }
        public string ParcelNumber { get; set; }
        public DbGeometry ParcelGeometry { get; set; }
        public string OwnerName { get; set; }
        public string ParcelStreetNumber { get; set; }
        public string ParcelAddress { get; set; }
        public string ParcelZipCode { get; set; }
        public string LandUse { get; set; }
        public int? SquareFeetHome { get; set; }
        public int? SquareFeetLot { get; set; }
        public double ParcelAreaInAcres { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ParcelID; } set { ParcelID = value; } }

        public virtual ICollection<WaterQualityManagementPlanParcel> WaterQualityManagementPlanParcels { get; set; }

        public static class FieldLengths
        {
            public const int ParcelNumber = 22;
            public const int OwnerName = 100;
            public const int ParcelStreetNumber = 10;
            public const int ParcelAddress = 150;
            public const int ParcelZipCode = 5;
            public const int LandUse = 4;
        }
    }
}