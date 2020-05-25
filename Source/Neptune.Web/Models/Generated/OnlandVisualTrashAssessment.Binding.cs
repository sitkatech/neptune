//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessment]
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
    // Table [dbo].[OnlandVisualTrashAssessment] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[OnlandVisualTrashAssessment]")]
    public partial class OnlandVisualTrashAssessment : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected OnlandVisualTrashAssessment()
        {
            this.OnlandVisualTrashAssessmentObservations = new HashSet<OnlandVisualTrashAssessmentObservation>();
            this.OnlandVisualTrashAssessmentObservationPhotoStagings = new HashSet<OnlandVisualTrashAssessmentObservationPhotoStaging>();
            this.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes = new HashSet<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public OnlandVisualTrashAssessment(int onlandVisualTrashAssessmentID, int createdByPersonID, DateTime createdDate, int? onlandVisualTrashAssessmentAreaID, string notes, int stormwaterJurisdictionID, bool? assessingNewArea, int onlandVisualTrashAssessmentStatusID, DbGeometry draftGeometry, bool? isDraftGeometryManuallyRefined, int? onlandVisualTrashAssessmentScoreID, DateTime? completedDate, string draftAreaName, string draftAreaDescription, bool isTransectBackingAssessment, bool isProgressAssessment) : this()
        {
            this.OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentID;
            this.CreatedByPersonID = createdByPersonID;
            this.CreatedDate = createdDate;
            this.OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentAreaID;
            this.Notes = notes;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.AssessingNewArea = assessingNewArea;
            this.OnlandVisualTrashAssessmentStatusID = onlandVisualTrashAssessmentStatusID;
            this.DraftGeometry = draftGeometry;
            this.IsDraftGeometryManuallyRefined = isDraftGeometryManuallyRefined;
            this.OnlandVisualTrashAssessmentScoreID = onlandVisualTrashAssessmentScoreID;
            this.CompletedDate = completedDate;
            this.DraftAreaName = draftAreaName;
            this.DraftAreaDescription = draftAreaDescription;
            this.IsTransectBackingAssessment = isTransectBackingAssessment;
            this.IsProgressAssessment = isProgressAssessment;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public OnlandVisualTrashAssessment(int createdByPersonID, DateTime createdDate, int stormwaterJurisdictionID, int onlandVisualTrashAssessmentStatusID, bool isTransectBackingAssessment, bool isProgressAssessment) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OnlandVisualTrashAssessmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.CreatedByPersonID = createdByPersonID;
            this.CreatedDate = createdDate;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.OnlandVisualTrashAssessmentStatusID = onlandVisualTrashAssessmentStatusID;
            this.IsTransectBackingAssessment = isTransectBackingAssessment;
            this.IsProgressAssessment = isProgressAssessment;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public OnlandVisualTrashAssessment(Person createdByPerson, DateTime createdDate, StormwaterJurisdiction stormwaterJurisdiction, OnlandVisualTrashAssessmentStatus onlandVisualTrashAssessmentStatus, bool isTransectBackingAssessment, bool isProgressAssessment) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OnlandVisualTrashAssessmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.CreatedByPersonID = createdByPerson.PersonID;
            this.CreatedByPerson = createdByPerson;
            createdByPerson.OnlandVisualTrashAssessmentsWhereYouAreTheCreatedByPerson.Add(this);
            this.CreatedDate = createdDate;
            this.StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID;
            this.StormwaterJurisdiction = stormwaterJurisdiction;
            stormwaterJurisdiction.OnlandVisualTrashAssessments.Add(this);
            this.OnlandVisualTrashAssessmentStatusID = onlandVisualTrashAssessmentStatus.OnlandVisualTrashAssessmentStatusID;
            this.IsTransectBackingAssessment = isTransectBackingAssessment;
            this.IsProgressAssessment = isProgressAssessment;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static OnlandVisualTrashAssessment CreateNewBlank(Person createdByPerson, StormwaterJurisdiction stormwaterJurisdiction, OnlandVisualTrashAssessmentStatus onlandVisualTrashAssessmentStatus)
        {
            return new OnlandVisualTrashAssessment(createdByPerson, default(DateTime), stormwaterJurisdiction, onlandVisualTrashAssessmentStatus, default(bool), default(bool));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return OnlandVisualTrashAssessmentObservations.Any() || OnlandVisualTrashAssessmentObservationPhotoStagings.Any() || OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(OnlandVisualTrashAssessmentObservations.Any())
            {
                dependentObjects.Add(typeof(OnlandVisualTrashAssessmentObservation).Name);
            }

            if(OnlandVisualTrashAssessmentObservationPhotoStagings.Any())
            {
                dependentObjects.Add(typeof(OnlandVisualTrashAssessmentObservationPhotoStaging).Name);
            }

            if(OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.Any())
            {
                dependentObjects.Add(typeof(OnlandVisualTrashAssessmentPreliminarySourceIdentificationType).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(OnlandVisualTrashAssessment).Name, typeof(OnlandVisualTrashAssessmentObservation).Name, typeof(OnlandVisualTrashAssessmentObservationPhotoStaging).Name, typeof(OnlandVisualTrashAssessmentPreliminarySourceIdentificationType).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.OnlandVisualTrashAssessments.Remove(this);
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

            foreach(var x in OnlandVisualTrashAssessmentObservations.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in OnlandVisualTrashAssessmentObservationPhotoStagings.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int OnlandVisualTrashAssessmentID { get; set; }
        public int CreatedByPersonID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? OnlandVisualTrashAssessmentAreaID { get; set; }
        public string Notes { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public bool? AssessingNewArea { get; set; }
        public int OnlandVisualTrashAssessmentStatusID { get; set; }
        public DbGeometry DraftGeometry { get; set; }
        public bool? IsDraftGeometryManuallyRefined { get; set; }
        public int? OnlandVisualTrashAssessmentScoreID { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string DraftAreaName { get; set; }
        public string DraftAreaDescription { get; set; }
        public bool IsTransectBackingAssessment { get; set; }
        public bool IsProgressAssessment { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return OnlandVisualTrashAssessmentID; } set { OnlandVisualTrashAssessmentID = value; } }

        public virtual ICollection<OnlandVisualTrashAssessmentObservation> OnlandVisualTrashAssessmentObservations { get; set; }
        public virtual ICollection<OnlandVisualTrashAssessmentObservationPhotoStaging> OnlandVisualTrashAssessmentObservationPhotoStagings { get; set; }
        public virtual ICollection<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType> OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes { get; set; }
        public virtual Person CreatedByPerson { get; set; }
        public virtual OnlandVisualTrashAssessmentArea OnlandVisualTrashAssessmentArea { get; set; }
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        public OnlandVisualTrashAssessmentStatus OnlandVisualTrashAssessmentStatus { get { return OnlandVisualTrashAssessmentStatus.AllLookupDictionary[OnlandVisualTrashAssessmentStatusID]; } }
        public OnlandVisualTrashAssessmentScore OnlandVisualTrashAssessmentScore { get { return OnlandVisualTrashAssessmentScoreID.HasValue ? OnlandVisualTrashAssessmentScore.AllLookupDictionary[OnlandVisualTrashAssessmentScoreID.Value] : null; } }

        public static class FieldLengths
        {
            public const int Notes = 500;
            public const int DraftAreaName = 100;
            public const int DraftAreaDescription = 500;
        }
    }
}