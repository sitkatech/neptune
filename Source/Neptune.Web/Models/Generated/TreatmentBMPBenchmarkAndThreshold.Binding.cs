//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPBenchmarkAndThreshold]
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
    // Table [dbo].[TreatmentBMPBenchmarkAndThreshold] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[TreatmentBMPBenchmarkAndThreshold]")]
    public partial class TreatmentBMPBenchmarkAndThreshold : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPBenchmarkAndThreshold()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPBenchmarkAndThreshold(int treatmentBMPBenchmarkAndThresholdID, int treatmentBMPID, int treatmentBMPTypeAssessmentObservationTypeID, int treatmentBMPTypeID, int treatmentBMPAssessmentObservationTypeID, double benchmarkValue, double thresholdValue) : this()
        {
            this.TreatmentBMPBenchmarkAndThresholdID = treatmentBMPBenchmarkAndThresholdID;
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPTypeAssessmentObservationTypeID = treatmentBMPTypeAssessmentObservationTypeID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.TreatmentBMPAssessmentObservationTypeID = treatmentBMPAssessmentObservationTypeID;
            this.BenchmarkValue = benchmarkValue;
            this.ThresholdValue = thresholdValue;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPBenchmarkAndThreshold(int treatmentBMPID, int treatmentBMPTypeAssessmentObservationTypeID, int treatmentBMPTypeID, int treatmentBMPAssessmentObservationTypeID, double benchmarkValue, double thresholdValue) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPBenchmarkAndThresholdID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPTypeAssessmentObservationTypeID = treatmentBMPTypeAssessmentObservationTypeID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.TreatmentBMPAssessmentObservationTypeID = treatmentBMPAssessmentObservationTypeID;
            this.BenchmarkValue = benchmarkValue;
            this.ThresholdValue = thresholdValue;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPBenchmarkAndThreshold(TreatmentBMP treatmentBMP, TreatmentBMPTypeAssessmentObservationType treatmentBMPTypeAssessmentObservationType, TreatmentBMPType treatmentBMPType, TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType, double benchmarkValue, double thresholdValue) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPBenchmarkAndThresholdID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
            treatmentBMP.TreatmentBMPBenchmarkAndThresholds.Add(this);
            this.TreatmentBMPTypeAssessmentObservationTypeID = treatmentBMPTypeAssessmentObservationType.TreatmentBMPTypeAssessmentObservationTypeID;
            this.TreatmentBMPTypeAssessmentObservationType = treatmentBMPTypeAssessmentObservationType;
            treatmentBMPTypeAssessmentObservationType.TreatmentBMPBenchmarkAndThresholds.Add(this);
            this.TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID;
            this.TreatmentBMPType = treatmentBMPType;
            treatmentBMPType.TreatmentBMPBenchmarkAndThresholds.Add(this);
            this.TreatmentBMPAssessmentObservationTypeID = treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID;
            this.TreatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationType;
            treatmentBMPAssessmentObservationType.TreatmentBMPBenchmarkAndThresholds.Add(this);
            this.BenchmarkValue = benchmarkValue;
            this.ThresholdValue = thresholdValue;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPBenchmarkAndThreshold CreateNewBlank(TreatmentBMP treatmentBMP, TreatmentBMPTypeAssessmentObservationType treatmentBMPTypeAssessmentObservationType, TreatmentBMPType treatmentBMPType, TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            return new TreatmentBMPBenchmarkAndThreshold(treatmentBMP, treatmentBMPTypeAssessmentObservationType, treatmentBMPType, treatmentBMPAssessmentObservationType, default(double), default(double));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPBenchmarkAndThreshold).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.TreatmentBMPBenchmarkAndThresholds.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int TreatmentBMPBenchmarkAndThresholdID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPTypeAssessmentObservationTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int TreatmentBMPAssessmentObservationTypeID { get; set; }
        public double BenchmarkValue { get; set; }
        public double ThresholdValue { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPBenchmarkAndThresholdID; } set { TreatmentBMPBenchmarkAndThresholdID = value; } }

        public virtual TreatmentBMP TreatmentBMP { get; set; }
        public virtual TreatmentBMPTypeAssessmentObservationType TreatmentBMPTypeAssessmentObservationType { get; set; }
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        public virtual TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType { get; set; }

        public static class FieldLengths
        {

        }
    }
}