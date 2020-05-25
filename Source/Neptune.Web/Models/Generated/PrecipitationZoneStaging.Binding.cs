//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PrecipitationZoneStaging]
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
    // Table [dbo].[PrecipitationZoneStaging] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[PrecipitationZoneStaging]")]
    public partial class PrecipitationZoneStaging : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected PrecipitationZoneStaging()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public PrecipitationZoneStaging(int precipitationZoneStagingID, int precipitationZoneKey, double designStormwaterDepthInInches, DbGeometry precipitationZoneGeometry) : this()
        {
            this.PrecipitationZoneStagingID = precipitationZoneStagingID;
            this.PrecipitationZoneKey = precipitationZoneKey;
            this.DesignStormwaterDepthInInches = designStormwaterDepthInInches;
            this.PrecipitationZoneGeometry = precipitationZoneGeometry;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public PrecipitationZoneStaging(int precipitationZoneKey, double designStormwaterDepthInInches, DbGeometry precipitationZoneGeometry) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.PrecipitationZoneStagingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.PrecipitationZoneKey = precipitationZoneKey;
            this.DesignStormwaterDepthInInches = designStormwaterDepthInInches;
            this.PrecipitationZoneGeometry = precipitationZoneGeometry;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static PrecipitationZoneStaging CreateNewBlank()
        {
            return new PrecipitationZoneStaging(default(int), default(double), default(DbGeometry));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(PrecipitationZoneStaging).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.PrecipitationZoneStagings.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int PrecipitationZoneStagingID { get; set; }
        public int PrecipitationZoneKey { get; set; }
        public double DesignStormwaterDepthInInches { get; set; }
        public DbGeometry PrecipitationZoneGeometry { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return PrecipitationZoneStagingID; } set { PrecipitationZoneStagingID = value; } }



        public static class FieldLengths
        {

        }
    }
}