//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationType]
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
    [Table("[dbo].[ObservationType]")]
    public partial class ObservationType : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ObservationType()
        {
            this.TreatmentBMPBenchmarkAndThresholds = new HashSet<TreatmentBMPBenchmarkAndThreshold>();
            this.TreatmentBMPObservations = new HashSet<TreatmentBMPObservation>();
            this.TreatmentBMPTypeObservationTypes = new HashSet<TreatmentBMPTypeObservationType>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ObservationType(int observationTypeID, string observationTypeName, int measurementUnitTypeID, int observationTypeSpecificationID, string benchmarkDescription, string thresholdDescription, string assessmentDescription, string observationSchema) : this()
        {
            this.ObservationTypeID = observationTypeID;
            this.ObservationTypeName = observationTypeName;
            this.MeasurementUnitTypeID = measurementUnitTypeID;
            this.ObservationTypeSpecificationID = observationTypeSpecificationID;
            this.BenchmarkDescription = benchmarkDescription;
            this.ThresholdDescription = thresholdDescription;
            this.AssessmentDescription = assessmentDescription;
            this.ObservationSchema = observationSchema;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ObservationType(string observationTypeName, int measurementUnitTypeID, int observationTypeSpecificationID, string assessmentDescription, string observationSchema) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ObservationTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ObservationTypeName = observationTypeName;
            this.MeasurementUnitTypeID = measurementUnitTypeID;
            this.ObservationTypeSpecificationID = observationTypeSpecificationID;
            this.AssessmentDescription = assessmentDescription;
            this.ObservationSchema = observationSchema;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ObservationType(string observationTypeName, MeasurementUnitType measurementUnitType, ObservationTypeSpecification observationTypeSpecification, string assessmentDescription, string observationSchema) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ObservationTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ObservationTypeName = observationTypeName;
            this.MeasurementUnitTypeID = measurementUnitType.MeasurementUnitTypeID;
            this.ObservationTypeSpecificationID = observationTypeSpecification.ObservationTypeSpecificationID;
            this.AssessmentDescription = assessmentDescription;
            this.ObservationSchema = observationSchema;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ObservationType CreateNewBlank(MeasurementUnitType measurementUnitType, ObservationTypeSpecification observationTypeSpecification)
        {
            return new ObservationType(default(string), measurementUnitType, observationTypeSpecification, default(string), default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return TreatmentBMPBenchmarkAndThresholds.Any() || TreatmentBMPObservations.Any() || TreatmentBMPTypeObservationTypes.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ObservationType).Name, typeof(TreatmentBMPBenchmarkAndThreshold).Name, typeof(TreatmentBMPObservation).Name, typeof(TreatmentBMPTypeObservationType).Name};

        [Key]
        public int ObservationTypeID { get; set; }
        public int TenantID { get; private set; }
        public string ObservationTypeName { get; set; }
        public int MeasurementUnitTypeID { get; set; }
        public int ObservationTypeSpecificationID { get; set; }
        public string BenchmarkDescription { get; set; }
        public string ThresholdDescription { get; set; }
        public string AssessmentDescription { get; set; }
        public string ObservationSchema { get; set; }
        public int PrimaryKey { get { return ObservationTypeID; } set { ObservationTypeID = value; } }

        public virtual ICollection<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get; set; }
        public virtual ICollection<TreatmentBMPObservation> TreatmentBMPObservations { get; set; }
        public virtual ICollection<TreatmentBMPTypeObservationType> TreatmentBMPTypeObservationTypes { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public MeasurementUnitType MeasurementUnitType { get { return MeasurementUnitType.AllLookupDictionary[MeasurementUnitTypeID]; } }
        public ObservationTypeSpecification ObservationTypeSpecification { get { return ObservationTypeSpecification.AllLookupDictionary[ObservationTypeSpecificationID]; } }

        public static class FieldLengths
        {
            public const int ObservationTypeName = 100;
            public const int BenchmarkDescription = 300;
            public const int ThresholdDescription = 300;
            public const int AssessmentDescription = 300;
        }
    }
}