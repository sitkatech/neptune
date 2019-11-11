//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NetworkCatchmentStaging]
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
    // Table [dbo].[NetworkCatchmentStaging] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[NetworkCatchmentStaging]")]
    public partial class NetworkCatchmentStaging : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected NetworkCatchmentStaging()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public NetworkCatchmentStaging(int networkCatchmentStagingID, string drainID, string watershed, DbGeometry catchmentGeometry, int oCSurveyCatchmentID, int? oCSurveyDownstreamCatchmentID) : this()
        {
            this.NetworkCatchmentStagingID = networkCatchmentStagingID;
            this.DrainID = drainID;
            this.Watershed = watershed;
            this.CatchmentGeometry = catchmentGeometry;
            this.OCSurveyCatchmentID = oCSurveyCatchmentID;
            this.OCSurveyDownstreamCatchmentID = oCSurveyDownstreamCatchmentID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public NetworkCatchmentStaging(string drainID, string watershed, DbGeometry catchmentGeometry, int oCSurveyCatchmentID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.NetworkCatchmentStagingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.DrainID = drainID;
            this.Watershed = watershed;
            this.CatchmentGeometry = catchmentGeometry;
            this.OCSurveyCatchmentID = oCSurveyCatchmentID;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static NetworkCatchmentStaging CreateNewBlank()
        {
            return new NetworkCatchmentStaging(default(string), default(string), default(DbGeometry), default(int));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(NetworkCatchmentStaging).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.NetworkCatchmentStagings.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int NetworkCatchmentStagingID { get; set; }
        public string DrainID { get; set; }
        public string Watershed { get; set; }
        public DbGeometry CatchmentGeometry { get; set; }
        public int OCSurveyCatchmentID { get; set; }
        public int? OCSurveyDownstreamCatchmentID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return NetworkCatchmentStagingID; } set { NetworkCatchmentStagingID = value; } }



        public static class FieldLengths
        {
            public const int DrainID = 10;
            public const int Watershed = 100;
        }
    }
}