//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashGeneratingUnit4326]
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
    // Table [dbo].[TrashGeneratingUnit4326] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[TrashGeneratingUnit4326]")]
    public partial class TrashGeneratingUnit4326 : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TrashGeneratingUnit4326()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TrashGeneratingUnit4326(int trashGeneratingUnit4326ID, int stormwaterJurisdictionID, int? onlandVisualTrashAssessmentAreaID, int? landUseBlockID, DbGeometry trashGeneratingUnit4326Geometry, DateTime? lastUpdateDate, int? delineationID, int? waterQualityManagementPlanID) : this()
        {
            this.TrashGeneratingUnit4326ID = trashGeneratingUnit4326ID;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentAreaID;
            this.LandUseBlockID = landUseBlockID;
            this.TrashGeneratingUnit4326Geometry = trashGeneratingUnit4326Geometry;
            this.LastUpdateDate = lastUpdateDate;
            this.DelineationID = delineationID;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TrashGeneratingUnit4326(int stormwaterJurisdictionID, DbGeometry trashGeneratingUnit4326Geometry) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TrashGeneratingUnit4326ID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.TrashGeneratingUnit4326Geometry = trashGeneratingUnit4326Geometry;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TrashGeneratingUnit4326(StormwaterJurisdiction stormwaterJurisdiction, DbGeometry trashGeneratingUnit4326Geometry) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TrashGeneratingUnit4326ID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID;
            this.StormwaterJurisdiction = stormwaterJurisdiction;
            stormwaterJurisdiction.TrashGeneratingUnit4326s.Add(this);
            this.TrashGeneratingUnit4326Geometry = trashGeneratingUnit4326Geometry;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TrashGeneratingUnit4326 CreateNewBlank(StormwaterJurisdiction stormwaterJurisdiction)
        {
            return new TrashGeneratingUnit4326(stormwaterJurisdiction, default(DbGeometry));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TrashGeneratingUnit4326).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.TrashGeneratingUnit4326s.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int TrashGeneratingUnit4326ID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int? OnlandVisualTrashAssessmentAreaID { get; set; }
        public int? LandUseBlockID { get; set; }
        public DbGeometry TrashGeneratingUnit4326Geometry { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TrashGeneratingUnit4326ID; } set { TrashGeneratingUnit4326ID = value; } }

        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        public virtual LandUseBlock LandUseBlock { get; set; }

        public static class FieldLengths
        {

        }
    }
}