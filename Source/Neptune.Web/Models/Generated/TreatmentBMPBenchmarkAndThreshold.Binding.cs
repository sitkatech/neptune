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
    [Table("[dbo].[TreatmentBMPBenchmarkAndThreshold]")]
    public partial class TreatmentBMPBenchmarkAndThreshold : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPBenchmarkAndThreshold()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPBenchmarkAndThreshold(int treatmentBMPBenchmarkAndThresholdID, int treatmentBMPID, int treatmentBMPTypeObservationTypeID, int treatmentBMPTypeID, int observationTypeID, double benchmarkValue, double thresholdValue) : this()
        {
            this.TreatmentBMPBenchmarkAndThresholdID = treatmentBMPBenchmarkAndThresholdID;
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPTypeObservationTypeID = treatmentBMPTypeObservationTypeID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.ObservationTypeID = observationTypeID;
            this.BenchmarkValue = benchmarkValue;
            this.ThresholdValue = thresholdValue;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPBenchmarkAndThreshold(int treatmentBMPID, int treatmentBMPTypeObservationTypeID, int treatmentBMPTypeID, int observationTypeID, double benchmarkValue, double thresholdValue) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPBenchmarkAndThresholdID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPTypeObservationTypeID = treatmentBMPTypeObservationTypeID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.ObservationTypeID = observationTypeID;
            this.BenchmarkValue = benchmarkValue;
            this.ThresholdValue = thresholdValue;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPBenchmarkAndThreshold(TreatmentBMP treatmentBMP, TreatmentBMPTypeObservationType treatmentBMPTypeObservationType, TreatmentBMPType treatmentBMPType, ObservationType observationType, double benchmarkValue, double thresholdValue) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPBenchmarkAndThresholdID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
            treatmentBMP.TreatmentBMPBenchmarkAndThresholds.Add(this);
            this.TreatmentBMPTypeObservationTypeID = treatmentBMPTypeObservationType.TreatmentBMPTypeObservationTypeID;
            this.TreatmentBMPTypeObservationType = treatmentBMPTypeObservationType;
            treatmentBMPTypeObservationType.TreatmentBMPBenchmarkAndThresholds.Add(this);
            this.TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID;
            this.TreatmentBMPType = treatmentBMPType;
            treatmentBMPType.TreatmentBMPBenchmarkAndThresholds.Add(this);
            this.ObservationTypeID = observationType.ObservationTypeID;
            this.ObservationType = observationType;
            observationType.TreatmentBMPBenchmarkAndThresholds.Add(this);
            this.BenchmarkValue = benchmarkValue;
            this.ThresholdValue = thresholdValue;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPBenchmarkAndThreshold CreateNewBlank(TreatmentBMP treatmentBMP, TreatmentBMPTypeObservationType treatmentBMPTypeObservationType, TreatmentBMPType treatmentBMPType, ObservationType observationType)
        {
            return new TreatmentBMPBenchmarkAndThreshold(treatmentBMP, treatmentBMPTypeObservationType, treatmentBMPType, observationType, default(double), default(double));
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
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {
            HttpRequestStorage.DatabaseEntities.AllTreatmentBMPBenchmarkAndThresholds.Remove(this);                
        }

        [Key]
        public int TreatmentBMPBenchmarkAndThresholdID { get; set; }
        public int TenantID { get; private set; }
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPTypeObservationTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int ObservationTypeID { get; set; }
        public double BenchmarkValue { get; set; }
        public double ThresholdValue { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPBenchmarkAndThresholdID; } set { TreatmentBMPBenchmarkAndThresholdID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        public virtual TreatmentBMPTypeObservationType TreatmentBMPTypeObservationType { get; set; }
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        public virtual ObservationType ObservationType { get; set; }

        public static class FieldLengths
        {

        }
    }
}