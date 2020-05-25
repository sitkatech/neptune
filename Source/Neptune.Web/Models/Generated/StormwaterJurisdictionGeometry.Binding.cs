//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdictionGeometry]
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
    // Table [dbo].[StormwaterJurisdictionGeometry] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[StormwaterJurisdictionGeometry]")]
    public partial class StormwaterJurisdictionGeometry : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected StormwaterJurisdictionGeometry()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public StormwaterJurisdictionGeometry(int stormwaterJurisdictionGeometryID, int stormwaterJurisdictionID, DbGeometry geometryNative, DbGeometry geometry4326) : this()
        {
            this.StormwaterJurisdictionGeometryID = stormwaterJurisdictionGeometryID;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.GeometryNative = geometryNative;
            this.Geometry4326 = geometry4326;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public StormwaterJurisdictionGeometry(int stormwaterJurisdictionID, DbGeometry geometryNative, DbGeometry geometry4326) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.StormwaterJurisdictionGeometryID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.GeometryNative = geometryNative;
            this.Geometry4326 = geometry4326;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public StormwaterJurisdictionGeometry(StormwaterJurisdiction stormwaterJurisdiction, DbGeometry geometryNative, DbGeometry geometry4326) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.StormwaterJurisdictionGeometryID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID;
            this.StormwaterJurisdiction = stormwaterJurisdiction;
            this.GeometryNative = geometryNative;
            this.Geometry4326 = geometry4326;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static StormwaterJurisdictionGeometry CreateNewBlank(StormwaterJurisdiction stormwaterJurisdiction)
        {
            return new StormwaterJurisdictionGeometry(stormwaterJurisdiction, default(DbGeometry), default(DbGeometry));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(StormwaterJurisdictionGeometry).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.StormwaterJurisdictionGeometries.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int StormwaterJurisdictionGeometryID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public DbGeometry GeometryNative { get; set; }
        public DbGeometry Geometry4326 { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return StormwaterJurisdictionGeometryID; } set { StormwaterJurisdictionGeometryID = value; } }

        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }

        public static class FieldLengths
        {

        }
    }
}