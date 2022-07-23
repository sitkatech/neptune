//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ParcelStaging]
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
    // Table [dbo].[ParcelStaging] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ParcelStaging]")]
    public partial class ParcelStaging : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ParcelStaging()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ParcelStaging(int parcelStagingID, string parcelNumber, DbGeometry parcelStagingGeometry, double parcelStagingAreaSquareFeet, string ownerName, string parcelStreetNumber, string parcelAddress, string parcelZipCode, string landUse, int? squareFeetHome, int? squareFeetLot, int uploadedByPersonID) : this()
        {
            this.ParcelStagingID = parcelStagingID;
            this.ParcelNumber = parcelNumber;
            this.ParcelStagingGeometry = parcelStagingGeometry;
            this.ParcelStagingAreaSquareFeet = parcelStagingAreaSquareFeet;
            this.OwnerName = ownerName;
            this.ParcelStreetNumber = parcelStreetNumber;
            this.ParcelAddress = parcelAddress;
            this.ParcelZipCode = parcelZipCode;
            this.LandUse = landUse;
            this.SquareFeetHome = squareFeetHome;
            this.SquareFeetLot = squareFeetLot;
            this.UploadedByPersonID = uploadedByPersonID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ParcelStaging(string parcelNumber, DbGeometry parcelStagingGeometry, double parcelStagingAreaSquareFeet, int uploadedByPersonID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ParcelStagingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ParcelNumber = parcelNumber;
            this.ParcelStagingGeometry = parcelStagingGeometry;
            this.ParcelStagingAreaSquareFeet = parcelStagingAreaSquareFeet;
            this.UploadedByPersonID = uploadedByPersonID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ParcelStaging(string parcelNumber, DbGeometry parcelStagingGeometry, double parcelStagingAreaSquareFeet, Person uploadedByPerson) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ParcelStagingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ParcelNumber = parcelNumber;
            this.ParcelStagingGeometry = parcelStagingGeometry;
            this.ParcelStagingAreaSquareFeet = parcelStagingAreaSquareFeet;
            this.UploadedByPersonID = uploadedByPerson.PersonID;
            this.UploadedByPerson = uploadedByPerson;
            uploadedByPerson.ParcelStagingsWhereYouAreTheUploadedByPerson.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ParcelStaging CreateNewBlank(Person uploadedByPerson)
        {
            return new ParcelStaging(default(string), default(DbGeometry), default(double), uploadedByPerson);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ParcelStaging).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ParcelStagings.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ParcelStagingID { get; set; }
        public string ParcelNumber { get; set; }
        public DbGeometry ParcelStagingGeometry { get; set; }
        public double ParcelStagingAreaSquareFeet { get; set; }
        public string OwnerName { get; set; }
        public string ParcelStreetNumber { get; set; }
        public string ParcelAddress { get; set; }
        public string ParcelZipCode { get; set; }
        public string LandUse { get; set; }
        public int? SquareFeetHome { get; set; }
        public int? SquareFeetLot { get; set; }
        public int UploadedByPersonID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ParcelStagingID; } set { ParcelStagingID = value; } }

        public virtual Person UploadedByPerson { get; set; }

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