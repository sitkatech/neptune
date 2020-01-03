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
            this.HRUCharacteristics = new HashSet<HRUCharacteristic>();
            this.NetworkCatchmentsWhereYouAreTheOCSurveyDownstreamCatchment = new HashSet<NetworkCatchment>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public NetworkCatchment(int networkCatchmentID, string drainID, string watershed, DbGeometry catchmentGeometry, int oCSurveyCatchmentID, int? oCSurveyDownstreamCatchmentID, DbGeometry catchmentGeometry4326, DateTime? lastUpdate) : this()
        {
            this.NetworkCatchmentID = networkCatchmentID;
            this.DrainID = drainID;
            this.Watershed = watershed;
            this.CatchmentGeometry = catchmentGeometry;
            this.OCSurveyCatchmentID = oCSurveyCatchmentID;
            this.OCSurveyDownstreamCatchmentID = oCSurveyDownstreamCatchmentID;
            this.CatchmentGeometry4326 = catchmentGeometry4326;
            this.LastUpdate = lastUpdate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public NetworkCatchment(DbGeometry catchmentGeometry, int oCSurveyCatchmentID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.NetworkCatchmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.CatchmentGeometry = catchmentGeometry;
            this.OCSurveyCatchmentID = oCSurveyCatchmentID;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static NetworkCatchment CreateNewBlank()
        {
            return new NetworkCatchment(default(DbGeometry), default(int));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return HRUCharacteristics.Any() || NetworkCatchmentsWhereYouAreTheOCSurveyDownstreamCatchment.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(NetworkCatchment).Name, typeof(HRUCharacteristic).Name};


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
            DeleteChildren(dbContext);
            Delete(dbContext);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in HRUCharacteristics.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in NetworkCatchmentsWhereYouAreTheOCSurveyDownstreamCatchment.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int NetworkCatchmentID { get; set; }
        public string DrainID { get; set; }
        public string Watershed { get; set; }
        public DbGeometry CatchmentGeometry { get; set; }
        public int OCSurveyCatchmentID { get; set; }
        [ForeignKey(nameof(OCSurveyDownstreamCatchment))]
        public int? OCSurveyDownstreamCatchmentID { get; set; }
        public DbGeometry CatchmentGeometry4326 { get; set; }
        public DateTime? LastUpdate { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return NetworkCatchmentID; } set { NetworkCatchmentID = value; } }

        public virtual ICollection<HRUCharacteristic> HRUCharacteristics { get; set; }
        [InverseProperty(nameof(OCSurveyDownstreamCatchment))]
        public virtual ICollection<NetworkCatchment> NetworkCatchmentsWhereYouAreTheOCSurveyDownstreamCatchment { get; set; }
        [ForeignKey(nameof(OCSurveyDownstreamCatchmentID))]
        public virtual NetworkCatchment OCSurveyDownstreamCatchment { get; set; }

        public static class FieldLengths
        {
            public const int DrainID = 10;
            public const int Watershed = 100;
        }
    }
}