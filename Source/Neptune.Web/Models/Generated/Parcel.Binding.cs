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
    [Table("[dbo].[Parcel]")]
    public partial class Parcel : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected Parcel()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Parcel(int parcelID, string parcelNumber, DbGeometry parcelGeometry, string ownerName, string parcelStreetNumber, string parcelAddress, string parcelZipCode, string landUse) : this()
        {
            this.ParcelID = parcelID;
            this.ParcelNumber = parcelNumber;
            this.ParcelGeometry = parcelGeometry;
            this.OwnerName = ownerName;
            this.ParcelStreetNumber = parcelStreetNumber;
            this.ParcelAddress = parcelAddress;
            this.ParcelZipCode = parcelZipCode;
            this.LandUse = landUse;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Parcel(string parcelNumber, DbGeometry parcelGeometry) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ParcelID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ParcelNumber = parcelNumber;
            this.ParcelGeometry = parcelGeometry;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Parcel CreateNewBlank()
        {
            return new Parcel(default(string), default(DbGeometry));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Parcel).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {
            HttpRequestStorage.DatabaseEntities.AllParcels.Remove(this);                
        }

        [Key]
        public int ParcelID { get; set; }
        public int TenantID { get; private set; }
        public string ParcelNumber { get; set; }
        public DbGeometry ParcelGeometry { get; set; }
        public string OwnerName { get; set; }
        public string ParcelStreetNumber { get; set; }
        public string ParcelAddress { get; set; }
        public string ParcelZipCode { get; set; }
        public string LandUse { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ParcelID; } set { ParcelID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }

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