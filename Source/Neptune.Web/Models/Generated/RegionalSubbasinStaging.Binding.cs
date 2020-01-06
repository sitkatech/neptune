//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasinStaging]
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
    // Table [dbo].[RegionalSubbasinStaging] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[RegionalSubbasinStaging]")]
    public partial class RegionalSubbasinStaging : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected RegionalSubbasinStaging()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public RegionalSubbasinStaging(int regionalSubbasinStagingID, string drainID, string watershed, DbGeometry catchmentGeometry, int? oCSurveyCatchmentID, int? oCSurveyDownstreamCatchmentID) : this()
        {
            this.RegionalSubbasinStagingID = regionalSubbasinStagingID;
            this.DrainID = drainID;
            this.Watershed = watershed;
            this.CatchmentGeometry = catchmentGeometry;
            this.OCSurveyCatchmentID = oCSurveyCatchmentID;
            this.OCSurveyDownstreamCatchmentID = oCSurveyDownstreamCatchmentID;
        }



        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static RegionalSubbasinStaging CreateNewBlank()
        {
            return new RegionalSubbasinStaging();
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(RegionalSubbasinStaging).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.RegionalSubbasinStagings.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int RegionalSubbasinStagingID { get; set; }
        public string DrainID { get; set; }
        public string Watershed { get; set; }
        public DbGeometry CatchmentGeometry { get; set; }
        public int? OCSurveyCatchmentID { get; set; }
        public int? OCSurveyDownstreamCatchmentID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return RegionalSubbasinStagingID; } set { RegionalSubbasinStagingID = value; } }



        public static class FieldLengths
        {
            public const int DrainID = 10;
            public const int Watershed = 100;
        }
    }
}