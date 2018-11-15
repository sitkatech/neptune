//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessmentObservationType]
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
    [Table("[dbo].[TreatmentBMPAssessmentObservationType]")]
    public partial class TreatmentBMPAssessmentObservationType : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPAssessmentObservationType()
        {
            this.TreatmentBMPBenchmarkAndThresholds = new HashSet<TreatmentBMPBenchmarkAndThreshold>();
            this.TreatmentBMPObservations = new HashSet<TreatmentBMPObservation>();
            this.TreatmentBMPTypeAssessmentObservationTypes = new HashSet<TreatmentBMPTypeAssessmentObservationType>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPAssessmentObservationType(int treatmentBMPAssessmentObservationTypeID, string treatmentBMPAssessmentObservationTypeName, int observationTypeSpecificationID, string treatmentBMPAssessmentObservationTypeSchema) : this()
        {
            this.TreatmentBMPAssessmentObservationTypeID = treatmentBMPAssessmentObservationTypeID;
            this.TreatmentBMPAssessmentObservationTypeName = treatmentBMPAssessmentObservationTypeName;
            this.ObservationTypeSpecificationID = observationTypeSpecificationID;
            this.TreatmentBMPAssessmentObservationTypeSchema = treatmentBMPAssessmentObservationTypeSchema;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPAssessmentObservationType(string treatmentBMPAssessmentObservationTypeName, int observationTypeSpecificationID, string treatmentBMPAssessmentObservationTypeSchema) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPAssessmentObservationTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPAssessmentObservationTypeName = treatmentBMPAssessmentObservationTypeName;
            this.ObservationTypeSpecificationID = observationTypeSpecificationID;
            this.TreatmentBMPAssessmentObservationTypeSchema = treatmentBMPAssessmentObservationTypeSchema;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPAssessmentObservationType(string treatmentBMPAssessmentObservationTypeName, ObservationTypeSpecification observationTypeSpecification, string treatmentBMPAssessmentObservationTypeSchema) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPAssessmentObservationTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPAssessmentObservationTypeName = treatmentBMPAssessmentObservationTypeName;
            this.ObservationTypeSpecificationID = observationTypeSpecification.ObservationTypeSpecificationID;
            this.TreatmentBMPAssessmentObservationTypeSchema = treatmentBMPAssessmentObservationTypeSchema;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPAssessmentObservationType CreateNewBlank(ObservationTypeSpecification observationTypeSpecification)
        {
            return new TreatmentBMPAssessmentObservationType(default(string), observationTypeSpecification, default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return TreatmentBMPBenchmarkAndThresholds.Any() || TreatmentBMPObservations.Any() || TreatmentBMPTypeAssessmentObservationTypes.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPAssessmentObservationType).Name, typeof(TreatmentBMPBenchmarkAndThreshold).Name, typeof(TreatmentBMPObservation).Name, typeof(TreatmentBMPTypeAssessmentObservationType).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {
            DeleteFull(HttpRequestStorage.DatabaseEntities);
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {

            foreach(var x in TreatmentBMPBenchmarkAndThresholds.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPObservations.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPTypeAssessmentObservationTypes.ToList())
            {
                x.DeleteFull(dbContext);
            }
            dbContext.AllTreatmentBMPAssessmentObservationTypes.Remove(this);
        }

        [Key]
        public int TreatmentBMPAssessmentObservationTypeID { get; set; }
        public int TenantID { get; private set; }
        public string TreatmentBMPAssessmentObservationTypeName { get; set; }
        public int ObservationTypeSpecificationID { get; set; }
        public string TreatmentBMPAssessmentObservationTypeSchema { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPAssessmentObservationTypeID; } set { TreatmentBMPAssessmentObservationTypeID = value; } }

        public virtual ICollection<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get; set; }
        public virtual ICollection<TreatmentBMPObservation> TreatmentBMPObservations { get; set; }
        public virtual ICollection<TreatmentBMPTypeAssessmentObservationType> TreatmentBMPTypeAssessmentObservationTypes { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public ObservationTypeSpecification ObservationTypeSpecification { get { return ObservationTypeSpecification.AllLookupDictionary[ObservationTypeSpecificationID]; } }

        public static class FieldLengths
        {
            public const int TreatmentBMPAssessmentObservationTypeName = 100;
        }
    }
}