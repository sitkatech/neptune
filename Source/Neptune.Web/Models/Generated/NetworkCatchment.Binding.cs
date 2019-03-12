//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NetworkCatchment]
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
    // Table [dbo].[NetworkCatchment] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[NetworkCatchment]")]
    public partial class NetworkCatchment : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected NetworkCatchment()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public NetworkCatchment(int networkCatchmentID, string oCSurveyCatchmentID, string downstreamCatchmentID, string drainID, string watershed, DbGeometry catchmentGeometry) : this()
        {
            this.NetworkCatchmentID = networkCatchmentID;
            this.OCSurveyCatchmentID = oCSurveyCatchmentID;
            this.DownstreamCatchmentID = downstreamCatchmentID;
            this.DrainID = drainID;
            this.Watershed = watershed;
            this.CatchmentGeometry = catchmentGeometry;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public NetworkCatchment(string oCSurveyCatchmentID, string downstreamCatchmentID, string drainID, string watershed, DbGeometry catchmentGeometry) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.NetworkCatchmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.OCSurveyCatchmentID = oCSurveyCatchmentID;
            this.DownstreamCatchmentID = downstreamCatchmentID;
            this.DrainID = drainID;
            this.Watershed = watershed;
            this.CatchmentGeometry = catchmentGeometry;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static NetworkCatchment CreateNewBlank()
        {
            return new NetworkCatchment(default(string), default(string), default(string), default(string), default(DbGeometry));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(NetworkCatchment).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.NetworkCatchments.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int NetworkCatchmentID { get; set; }
        public string OCSurveyCatchmentID { get; set; }
        public string DownstreamCatchmentID { get; set; }
        public string DrainID { get; set; }
        public string Watershed { get; set; }
        public DbGeometry CatchmentGeometry { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return NetworkCatchmentID; } set { NetworkCatchmentID = value; } }



        public static class FieldLengths
        {
            public const int OCSurveyCatchmentID = 10;
            public const int DownstreamCatchmentID = 10;
            public const int DrainID = 10;
            public const int Watershed = 100;
        }
    }
}