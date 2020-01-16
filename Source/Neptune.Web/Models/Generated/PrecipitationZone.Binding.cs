//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PrecipitationZone]
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
    // Table [dbo].[PrecipitationZone] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[PrecipitationZone]")]
    public partial class PrecipitationZone : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected PrecipitationZone()
        {
            this.TreatmentBMPs = new HashSet<TreatmentBMP>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public PrecipitationZone(int precipitationZoneID, int precipitationZoneKey, double designStormwaterDepthInInches, DbGeometry precipitationZoneGeometry, DateTime lastUpdate) : this()
        {
            this.PrecipitationZoneID = precipitationZoneID;
            this.PrecipitationZoneKey = precipitationZoneKey;
            this.DesignStormwaterDepthInInches = designStormwaterDepthInInches;
            this.PrecipitationZoneGeometry = precipitationZoneGeometry;
            this.LastUpdate = lastUpdate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public PrecipitationZone(int precipitationZoneKey, double designStormwaterDepthInInches, DbGeometry precipitationZoneGeometry, DateTime lastUpdate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.PrecipitationZoneID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.PrecipitationZoneKey = precipitationZoneKey;
            this.DesignStormwaterDepthInInches = designStormwaterDepthInInches;
            this.PrecipitationZoneGeometry = precipitationZoneGeometry;
            this.LastUpdate = lastUpdate;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static PrecipitationZone CreateNewBlank()
        {
            return new PrecipitationZone(default(int), default(double), default(DbGeometry), default(DateTime));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return TreatmentBMPs.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(PrecipitationZone).Name, typeof(TreatmentBMP).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.PrecipitationZones.Remove(this);
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

            foreach(var x in TreatmentBMPs.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int PrecipitationZoneID { get; set; }
        public int PrecipitationZoneKey { get; set; }
        public double DesignStormwaterDepthInInches { get; set; }
        public DbGeometry PrecipitationZoneGeometry { get; set; }
        public DateTime LastUpdate { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return PrecipitationZoneID; } set { PrecipitationZoneID = value; } }

        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }

        public static class FieldLengths
        {

        }
    }
}