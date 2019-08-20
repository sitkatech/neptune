//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[BackboneSegment]
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
    // Table [dbo].[BackboneSegment] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[BackboneSegment]")]
    public partial class BackboneSegment : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected BackboneSegment()
        {
            this.BackboneSegmentsWhereYouAreTheDownstreamBackboneSegment = new HashSet<BackboneSegment>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public BackboneSegment(int backboneSegmentID, DbGeometry backboneSegmentGeometry, int catchIDN, int? networkCatchmentID, int backboneSegmentTypeID, int? downstreamBackboneSegmentID, string streamName, DbGeometry backboneSegmentGeometry4326) : this()
        {
            this.BackboneSegmentID = backboneSegmentID;
            this.BackboneSegmentGeometry = backboneSegmentGeometry;
            this.CatchIDN = catchIDN;
            this.NetworkCatchmentID = networkCatchmentID;
            this.BackboneSegmentTypeID = backboneSegmentTypeID;
            this.DownstreamBackboneSegmentID = downstreamBackboneSegmentID;
            this.StreamName = streamName;
            this.BackboneSegmentGeometry4326 = backboneSegmentGeometry4326;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public BackboneSegment(DbGeometry backboneSegmentGeometry, int catchIDN, int backboneSegmentTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.BackboneSegmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.BackboneSegmentGeometry = backboneSegmentGeometry;
            this.CatchIDN = catchIDN;
            this.BackboneSegmentTypeID = backboneSegmentTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public BackboneSegment(DbGeometry backboneSegmentGeometry, int catchIDN, BackboneSegmentType backboneSegmentType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.BackboneSegmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.BackboneSegmentGeometry = backboneSegmentGeometry;
            this.CatchIDN = catchIDN;
            this.BackboneSegmentTypeID = backboneSegmentType.BackboneSegmentTypeID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static BackboneSegment CreateNewBlank(BackboneSegmentType backboneSegmentType)
        {
            return new BackboneSegment(default(DbGeometry), default(int), backboneSegmentType);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return BackboneSegmentsWhereYouAreTheDownstreamBackboneSegment.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(BackboneSegment).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.BackboneSegments.Remove(this);
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

            foreach(var x in BackboneSegmentsWhereYouAreTheDownstreamBackboneSegment.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int BackboneSegmentID { get; set; }
        public DbGeometry BackboneSegmentGeometry { get; set; }
        public int CatchIDN { get; set; }
        public int? NetworkCatchmentID { get; set; }
        public int BackboneSegmentTypeID { get; set; }
        public int? DownstreamBackboneSegmentID { get; set; }
        public string StreamName { get; set; }
        public DbGeometry BackboneSegmentGeometry4326 { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return BackboneSegmentID; } set { BackboneSegmentID = value; } }

        public virtual ICollection<BackboneSegment> BackboneSegmentsWhereYouAreTheDownstreamBackboneSegment { get; set; }
        public virtual BackboneSegment DownstreamBackboneSegment { get; set; }
        public virtual NetworkCatchment NetworkCatchment { get; set; }
        public BackboneSegmentType BackboneSegmentType { get { return BackboneSegmentType.AllLookupDictionary[BackboneSegmentTypeID]; } }

        public static class FieldLengths
        {

        }
    }
}