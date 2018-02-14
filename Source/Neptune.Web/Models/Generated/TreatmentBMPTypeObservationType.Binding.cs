//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPTypeObservationType]
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
    [Table("[dbo].[TreatmentBMPTypeObservationType]")]
    public partial class TreatmentBMPTypeObservationType : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPTypeObservationType()
        {
            this.TreatmentBMPBenchmarkAndThresholds = new HashSet<TreatmentBMPBenchmarkAndThreshold>();
            this.TreatmentBMPObservations = new HashSet<TreatmentBMPObservation>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPTypeObservationType(int treatmentBMPTypeObservationTypeID, int treatmentBMPTypeID, int observationTypeID, decimal? assessmentScoreWeight, double? defaultThresholdValue, double? defaultBenchmarkValue, bool overrideAssessmentScoreIfFailing) : this()
        {
            this.TreatmentBMPTypeObservationTypeID = treatmentBMPTypeObservationTypeID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.ObservationTypeID = observationTypeID;
            this.AssessmentScoreWeight = assessmentScoreWeight;
            this.DefaultThresholdValue = defaultThresholdValue;
            this.DefaultBenchmarkValue = defaultBenchmarkValue;
            this.OverrideAssessmentScoreIfFailing = overrideAssessmentScoreIfFailing;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPTypeObservationType(int treatmentBMPTypeID, int observationTypeID, bool overrideAssessmentScoreIfFailing) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPTypeObservationTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.ObservationTypeID = observationTypeID;
            this.OverrideAssessmentScoreIfFailing = overrideAssessmentScoreIfFailing;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPTypeObservationType(TreatmentBMPType treatmentBMPType, ObservationType observationType, bool overrideAssessmentScoreIfFailing) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPTypeObservationTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID;
            this.TreatmentBMPType = treatmentBMPType;
            treatmentBMPType.TreatmentBMPTypeObservationTypes.Add(this);
            this.ObservationTypeID = observationType.ObservationTypeID;
            this.ObservationType = observationType;
            observationType.TreatmentBMPTypeObservationTypes.Add(this);
            this.OverrideAssessmentScoreIfFailing = overrideAssessmentScoreIfFailing;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPTypeObservationType CreateNewBlank(TreatmentBMPType treatmentBMPType, ObservationType observationType)
        {
            return new TreatmentBMPTypeObservationType(treatmentBMPType, observationType, default(bool));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPTypeObservationType).Name, typeof(TreatmentBMPBenchmarkAndThreshold).Name, typeof(TreatmentBMPObservation).Name};

        [Key]
        public int TreatmentBMPTypeObservationTypeID { get; set; }
        public int TenantID { get; private set; }
        public int TreatmentBMPTypeID { get; set; }
        public int ObservationTypeID { get; set; }
        public decimal? AssessmentScoreWeight { get; set; }
        public double? DefaultThresholdValue { get; set; }
        public double? DefaultBenchmarkValue { get; set; }
        public bool OverrideAssessmentScoreIfFailing { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPTypeObservationTypeID; } set { TreatmentBMPTypeObservationTypeID = value; } }

        public virtual ICollection<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get; set; }
        public virtual ICollection<TreatmentBMPObservation> TreatmentBMPObservations { get; set; }
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        public virtual ObservationType ObservationType { get; set; }

        public static class FieldLengths
        {

        }
    }
}