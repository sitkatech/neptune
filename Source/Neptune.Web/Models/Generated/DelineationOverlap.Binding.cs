//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DelineationOverlap]
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
    // Table [dbo].[DelineationOverlap] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[DelineationOverlap]")]
    public partial class DelineationOverlap : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected DelineationOverlap()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public DelineationOverlap(int delineationOverlapID, int delineationID, int overlappingDelineationID, DbGeometry overlappingGeometry) : this()
        {
            this.DelineationOverlapID = delineationOverlapID;
            this.DelineationID = delineationID;
            this.OverlappingDelineationID = overlappingDelineationID;
            this.OverlappingGeometry = overlappingGeometry;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public DelineationOverlap(int delineationID, int overlappingDelineationID, DbGeometry overlappingGeometry) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.DelineationOverlapID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.DelineationID = delineationID;
            this.OverlappingDelineationID = overlappingDelineationID;
            this.OverlappingGeometry = overlappingGeometry;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public DelineationOverlap(Delineation delineation, Delineation overlappingDelineation, DbGeometry overlappingGeometry) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.DelineationOverlapID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.DelineationID = delineation.DelineationID;
            this.Delineation = delineation;
            delineation.DelineationOverlaps.Add(this);
            this.OverlappingDelineationID = overlappingDelineation.DelineationID;
            this.OverlappingDelineation = overlappingDelineation;
            overlappingDelineation.DelineationOverlapsWhereYouAreTheOverlappingDelineation.Add(this);
            this.OverlappingGeometry = overlappingGeometry;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static DelineationOverlap CreateNewBlank(Delineation delineation, Delineation overlappingDelineation)
        {
            return new DelineationOverlap(delineation, overlappingDelineation, default(DbGeometry));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(DelineationOverlap).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.DelineationOverlaps.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int DelineationOverlapID { get; set; }
        public int DelineationID { get; set; }
        public int OverlappingDelineationID { get; set; }
        public DbGeometry OverlappingGeometry { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return DelineationOverlapID; } set { DelineationOverlapID = value; } }

        public virtual Delineation Delineation { get; set; }
        public virtual Delineation OverlappingDelineation { get; set; }

        public static class FieldLengths
        {

        }
    }
}