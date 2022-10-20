//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentArea]
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
    // Table [dbo].[OnlandVisualTrashAssessmentArea] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[OnlandVisualTrashAssessmentArea]")]
    public partial class OnlandVisualTrashAssessmentArea : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected OnlandVisualTrashAssessmentArea()
        {
            this.OnlandVisualTrashAssessments = new HashSet<OnlandVisualTrashAssessment>();
            this.TrashGeneratingUnit4326s = new HashSet<TrashGeneratingUnit4326>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public OnlandVisualTrashAssessmentArea(int onlandVisualTrashAssessmentAreaID, string onlandVisualTrashAssessmentAreaName, int stormwaterJurisdictionID, DbGeometry onlandVisualTrashAssessmentAreaGeometry, int? onlandVisualTrashAssessmentBaselineScoreID, string assessmentAreaDescription, DbGeometry transectLine, int? onlandVisualTrashAssessmentProgressScoreID, DbGeometry onlandVisualTrashAssessmentAreaGeometry4326, DbGeometry transectLine4326) : this()
        {
            this.OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentAreaID;
            this.OnlandVisualTrashAssessmentAreaName = onlandVisualTrashAssessmentAreaName;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.OnlandVisualTrashAssessmentAreaGeometry = onlandVisualTrashAssessmentAreaGeometry;
            this.OnlandVisualTrashAssessmentBaselineScoreID = onlandVisualTrashAssessmentBaselineScoreID;
            this.AssessmentAreaDescription = assessmentAreaDescription;
            this.TransectLine = transectLine;
            this.OnlandVisualTrashAssessmentProgressScoreID = onlandVisualTrashAssessmentProgressScoreID;
            this.OnlandVisualTrashAssessmentAreaGeometry4326 = onlandVisualTrashAssessmentAreaGeometry4326;
            this.TransectLine4326 = transectLine4326;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public OnlandVisualTrashAssessmentArea(string onlandVisualTrashAssessmentAreaName, int stormwaterJurisdictionID, DbGeometry onlandVisualTrashAssessmentAreaGeometry) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OnlandVisualTrashAssessmentAreaID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.OnlandVisualTrashAssessmentAreaName = onlandVisualTrashAssessmentAreaName;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.OnlandVisualTrashAssessmentAreaGeometry = onlandVisualTrashAssessmentAreaGeometry;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public OnlandVisualTrashAssessmentArea(string onlandVisualTrashAssessmentAreaName, StormwaterJurisdiction stormwaterJurisdiction, DbGeometry onlandVisualTrashAssessmentAreaGeometry) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OnlandVisualTrashAssessmentAreaID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.OnlandVisualTrashAssessmentAreaName = onlandVisualTrashAssessmentAreaName;
            this.StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID;
            this.StormwaterJurisdiction = stormwaterJurisdiction;
            stormwaterJurisdiction.OnlandVisualTrashAssessmentAreas.Add(this);
            this.OnlandVisualTrashAssessmentAreaGeometry = onlandVisualTrashAssessmentAreaGeometry;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static OnlandVisualTrashAssessmentArea CreateNewBlank(StormwaterJurisdiction stormwaterJurisdiction)
        {
            return new OnlandVisualTrashAssessmentArea(default(string), stormwaterJurisdiction, default(DbGeometry));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return OnlandVisualTrashAssessments.Any() || TrashGeneratingUnit4326s.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(OnlandVisualTrashAssessments.Any())
            {
                dependentObjects.Add(typeof(OnlandVisualTrashAssessment).Name);
            }

            if(TrashGeneratingUnit4326s.Any())
            {
                dependentObjects.Add(typeof(TrashGeneratingUnit4326).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(OnlandVisualTrashAssessmentArea).Name, typeof(OnlandVisualTrashAssessment).Name, typeof(TrashGeneratingUnit4326).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.OnlandVisualTrashAssessmentAreas.Remove(this);
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

            foreach(var x in OnlandVisualTrashAssessments.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TrashGeneratingUnit4326s.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int OnlandVisualTrashAssessmentAreaID { get; set; }
        public string OnlandVisualTrashAssessmentAreaName { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public DbGeometry OnlandVisualTrashAssessmentAreaGeometry { get; set; }
        public int? OnlandVisualTrashAssessmentBaselineScoreID { get; set; }
        public string AssessmentAreaDescription { get; set; }
        public DbGeometry TransectLine { get; set; }
        public int? OnlandVisualTrashAssessmentProgressScoreID { get; set; }
        public DbGeometry OnlandVisualTrashAssessmentAreaGeometry4326 { get; set; }
        public DbGeometry TransectLine4326 { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return OnlandVisualTrashAssessmentAreaID; } set { OnlandVisualTrashAssessmentAreaID = value; } }

        public virtual ICollection<OnlandVisualTrashAssessment> OnlandVisualTrashAssessments { get; set; }
        public virtual ICollection<TrashGeneratingUnit4326> TrashGeneratingUnit4326s { get; set; }
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        public OnlandVisualTrashAssessmentScore OnlandVisualTrashAssessmentBaselineScore { get { return OnlandVisualTrashAssessmentBaselineScoreID.HasValue ? OnlandVisualTrashAssessmentScore.AllLookupDictionary[OnlandVisualTrashAssessmentBaselineScoreID.Value] : null; } }
        public OnlandVisualTrashAssessmentScore OnlandVisualTrashAssessmentProgressScore { get { return OnlandVisualTrashAssessmentProgressScoreID.HasValue ? OnlandVisualTrashAssessmentScore.AllLookupDictionary[OnlandVisualTrashAssessmentProgressScoreID.Value] : null; } }

        public static class FieldLengths
        {
            public const int OnlandVisualTrashAssessmentAreaName = 100;
            public const int AssessmentAreaDescription = 500;
        }
    }
}