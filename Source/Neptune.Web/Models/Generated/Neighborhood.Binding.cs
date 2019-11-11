//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Neighborhood]
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
    // Table [dbo].[Neighborhood] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[Neighborhood]")]
    public partial class Neighborhood : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected Neighborhood()
        {
            this.BackboneSegments = new HashSet<BackboneSegment>();
            this.NeighborhoodsWhereYouAreTheOCSurveyDownstreamNeighborhood = new HashSet<Neighborhood>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Neighborhood(int neighborhoodID, string drainID, string watershed, DbGeometry neighborhoodGeometry, int oCSurveyNeighborhoodID, int? oCSurveyDownstreamNeighborhoodID, DbGeometry neighborhoodGeometry4326) : this()
        {
            this.NeighborhoodID = neighborhoodID;
            this.DrainID = drainID;
            this.Watershed = watershed;
            this.NeighborhoodGeometry = neighborhoodGeometry;
            this.OCSurveyNeighborhoodID = oCSurveyNeighborhoodID;
            this.OCSurveyDownstreamNeighborhoodID = oCSurveyDownstreamNeighborhoodID;
            this.NeighborhoodGeometry4326 = neighborhoodGeometry4326;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Neighborhood(string drainID, string watershed, DbGeometry neighborhoodGeometry, int oCSurveyNeighborhoodID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.NeighborhoodID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.DrainID = drainID;
            this.Watershed = watershed;
            this.NeighborhoodGeometry = neighborhoodGeometry;
            this.OCSurveyNeighborhoodID = oCSurveyNeighborhoodID;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Neighborhood CreateNewBlank()
        {
            return new Neighborhood(default(string), default(string), default(DbGeometry), default(int));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return BackboneSegments.Any() || NeighborhoodsWhereYouAreTheOCSurveyDownstreamNeighborhood.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Neighborhood).Name, typeof(BackboneSegment).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.Neighborhoods.Remove(this);
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

            foreach(var x in BackboneSegments.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in NeighborhoodsWhereYouAreTheOCSurveyDownstreamNeighborhood.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int NeighborhoodID { get; set; }
        public string DrainID { get; set; }
        public string Watershed { get; set; }
        public DbGeometry NeighborhoodGeometry { get; set; }
        public int OCSurveyNeighborhoodID { get; set; }
        public int? OCSurveyDownstreamNeighborhoodID { get; set; }
        public DbGeometry NeighborhoodGeometry4326 { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return NeighborhoodID; } set { NeighborhoodID = value; } }

        public virtual ICollection<BackboneSegment> BackboneSegments { get; set; }
        public virtual ICollection<Neighborhood> NeighborhoodsWhereYouAreTheOCSurveyDownstreamNeighborhood { get; set; }
        public virtual Neighborhood OCSurveyDownstreamNeighborhood { get; set; }

        public static class FieldLengths
        {
            public const int DrainID = 10;
            public const int Watershed = 100;
        }
    }
}