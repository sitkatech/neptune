//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPTypeAssessmentObservationType]
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
    [Table("[dbo].[TreatmentBMPTypeAssessmentObservationType]")]
    public partial class TreatmentBMPTypeAssessmentObservationType : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPTypeAssessmentObservationType()
        {
            this.TreatmentBMPBenchmarkAndThresholds = new HashSet<TreatmentBMPBenchmarkAndThreshold>();
            this.TreatmentBMPObservations = new HashSet<TreatmentBMPObservation>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPTypeAssessmentObservationType(int treatmentBMPTypeAssessmentObservationTypeID, int treatmentBMPTypeID, int treatmentBMPAssessmentObservationTypeID, decimal? assessmentScoreWeight, double? defaultThresholdValue, double? defaultBenchmarkValue, bool overrideAssessmentScoreIfFailing, int? sortOrder) : this()
        {
            this.TreatmentBMPTypeAssessmentObservationTypeID = treatmentBMPTypeAssessmentObservationTypeID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.TreatmentBMPAssessmentObservationTypeID = treatmentBMPAssessmentObservationTypeID;
            this.AssessmentScoreWeight = assessmentScoreWeight;
            this.DefaultThresholdValue = defaultThresholdValue;
            this.DefaultBenchmarkValue = defaultBenchmarkValue;
            this.OverrideAssessmentScoreIfFailing = overrideAssessmentScoreIfFailing;
            this.SortOrder = sortOrder;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPTypeAssessmentObservationType(int treatmentBMPTypeID, int treatmentBMPAssessmentObservationTypeID, bool overrideAssessmentScoreIfFailing) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPTypeAssessmentObservationTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.TreatmentBMPAssessmentObservationTypeID = treatmentBMPAssessmentObservationTypeID;
            this.OverrideAssessmentScoreIfFailing = overrideAssessmentScoreIfFailing;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPTypeAssessmentObservationType(TreatmentBMPType treatmentBMPType, TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType, bool overrideAssessmentScoreIfFailing) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPTypeAssessmentObservationTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID;
            this.TreatmentBMPType = treatmentBMPType;
            treatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes.Add(this);
            this.TreatmentBMPAssessmentObservationTypeID = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID;
            this.TreatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationType;
            treatmentBMPAssessmentObservationType.TreatmentBMPTypeAssessmentObservationTypes.Add(this);
            this.OverrideAssessmentScoreIfFailing = overrideAssessmentScoreIfFailing;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPTypeAssessmentObservationType CreateNewBlank(TreatmentBMPType treatmentBMPType, TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            return new TreatmentBMPTypeAssessmentObservationType(treatmentBMPType, treatmentBMPAssessmentObservationType, default(bool));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return TreatmentBMPBenchmarkAndThresholds.Any() || TreatmentBMPObservations.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPTypeAssessmentObservationType).Name, typeof(TreatmentBMPBenchmarkAndThreshold).Name, typeof(TreatmentBMPObservation).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            dbContext.TreatmentBMPTypeAssessmentObservationTypes.Remove(this);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in TreatmentBMPBenchmarkAndThresholds.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPObservations.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int TreatmentBMPTypeAssessmentObservationTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int TreatmentBMPAssessmentObservationTypeID { get; set; }
        public decimal? AssessmentScoreWeight { get; set; }
        public double? DefaultThresholdValue { get; set; }
        public double? DefaultBenchmarkValue { get; set; }
        public bool OverrideAssessmentScoreIfFailing { get; set; }
        public int? SortOrder { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPTypeAssessmentObservationTypeID; } set { TreatmentBMPTypeAssessmentObservationTypeID = value; } }

        public virtual ICollection<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get; set; }
        public virtual ICollection<TreatmentBMPObservation> TreatmentBMPObservations { get; set; }
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        public virtual TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType { get; set; }

        public static class FieldLengths
        {

        }
    }
}