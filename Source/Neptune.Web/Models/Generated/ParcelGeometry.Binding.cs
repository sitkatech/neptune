//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ParcelGeometry]
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
    // Table [dbo].[ParcelGeometry] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ParcelGeometry]")]
    public partial class ParcelGeometry : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ParcelGeometry()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ParcelGeometry(int parcelGeometryID, int parcelID, DbGeometry geometryNative, DbGeometry geometry4326) : this()
        {
            this.ParcelGeometryID = parcelGeometryID;
            this.ParcelID = parcelID;
            this.GeometryNative = geometryNative;
            this.Geometry4326 = geometry4326;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ParcelGeometry(int parcelID, DbGeometry geometryNative) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ParcelGeometryID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ParcelID = parcelID;
            this.GeometryNative = geometryNative;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ParcelGeometry(Parcel parcel, DbGeometry geometryNative) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ParcelGeometryID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ParcelID = parcel.ParcelID;
            this.Parcel = parcel;
            this.GeometryNative = geometryNative;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ParcelGeometry CreateNewBlank(Parcel parcel)
        {
            return new ParcelGeometry(parcel, default(DbGeometry));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ParcelGeometry).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ParcelGeometries.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ParcelGeometryID { get; set; }
        public int ParcelID { get; set; }
        public DbGeometry GeometryNative { get; set; }
        public DbGeometry Geometry4326 { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ParcelGeometryID; } set { ParcelGeometryID = value; } }

        public virtual Parcel Parcel { get; set; }

        public static class FieldLengths
        {

        }
    }
}