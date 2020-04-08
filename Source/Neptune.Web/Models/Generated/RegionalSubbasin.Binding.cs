//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasin]
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
    // Table [dbo].[RegionalSubbasin] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[RegionalSubbasin]")]
    public partial class RegionalSubbasin : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected RegionalSubbasin()
        {
            this.LoadGeneratingUnits = new HashSet<LoadGeneratingUnit>();
            this.RegionalSubbasinsWhereYouAreTheOCSurveyDownstreamCatchment = new HashSet<RegionalSubbasin>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public RegionalSubbasin(int regionalSubbasinID, string drainID, string watershed, DbGeometry catchmentGeometry, int oCSurveyCatchmentID, int? oCSurveyDownstreamCatchmentID, DbGeometry catchmentGeometry4326, DateTime? lastUpdate, bool? isWaitingForLGURefresh, bool? isInLSPCBasin) : this()
        {
            this.RegionalSubbasinID = regionalSubbasinID;
            this.DrainID = drainID;
            this.Watershed = watershed;
            this.CatchmentGeometry = catchmentGeometry;
            this.OCSurveyCatchmentID = oCSurveyCatchmentID;
            this.OCSurveyDownstreamCatchmentID = oCSurveyDownstreamCatchmentID;
            this.CatchmentGeometry4326 = catchmentGeometry4326;
            this.LastUpdate = lastUpdate;
            this.IsWaitingForLGURefresh = isWaitingForLGURefresh;
            this.IsInLSPCBasin = isInLSPCBasin;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public RegionalSubbasin(DbGeometry catchmentGeometry, int oCSurveyCatchmentID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.RegionalSubbasinID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.CatchmentGeometry = catchmentGeometry;
            this.OCSurveyCatchmentID = oCSurveyCatchmentID;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static RegionalSubbasin CreateNewBlank()
        {
            return new RegionalSubbasin(default(DbGeometry), default(int));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return LoadGeneratingUnits.Any() || RegionalSubbasinsWhereYouAreTheOCSurveyDownstreamCatchment.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(RegionalSubbasin).Name, typeof(LoadGeneratingUnit).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.RegionalSubbasins.Remove(this);
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

            foreach(var x in LoadGeneratingUnits.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in RegionalSubbasinsWhereYouAreTheOCSurveyDownstreamCatchment.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int RegionalSubbasinID { get; set; }
        public string DrainID { get; set; }
        public string Watershed { get; set; }
        public DbGeometry CatchmentGeometry { get; set; }
        public int OCSurveyCatchmentID { get; set; }
        public int? OCSurveyDownstreamCatchmentID { get; set; }
        public DbGeometry CatchmentGeometry4326 { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool? IsWaitingForLGURefresh { get; set; }
        public bool? IsInLSPCBasin { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return RegionalSubbasinID; } set { RegionalSubbasinID = value; } }

        public virtual ICollection<LoadGeneratingUnit> LoadGeneratingUnits { get; set; }
        public virtual ICollection<RegionalSubbasin> RegionalSubbasinsWhereYouAreTheOCSurveyDownstreamCatchment { get; set; }
        public virtual RegionalSubbasin OCSurveyDownstreamCatchment { get; set; }

        public static class FieldLengths
        {
            public const int DrainID = 10;
            public const int Watershed = 100;
        }
    }
}