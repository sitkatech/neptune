//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPObservation]
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
    [Table("[dbo].[TreatmentBMPObservation]")]
    public partial class TreatmentBMPObservation : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPObservation()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPObservation(int treatmentBMPObservationID, int treatmentBMPAssessmentID, int observationTypeID, string observationData) : this()
        {
            this.TreatmentBMPObservationID = treatmentBMPObservationID;
            this.TreatmentBMPAssessmentID = treatmentBMPAssessmentID;
            this.ObservationTypeID = observationTypeID;
            this.ObservationData = observationData;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPObservation(int treatmentBMPAssessmentID, int observationTypeID, string observationData) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPObservationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPAssessmentID = treatmentBMPAssessmentID;
            this.ObservationTypeID = observationTypeID;
            this.ObservationData = observationData;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPObservation(TreatmentBMPAssessment treatmentBMPAssessment, ObservationType observationType, string observationData) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPObservationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPAssessmentID = treatmentBMPAssessment.TreatmentBMPAssessmentID;
            this.TreatmentBMPAssessment = treatmentBMPAssessment;
            treatmentBMPAssessment.TreatmentBMPObservations.Add(this);
            this.ObservationTypeID = observationType.ObservationTypeID;
            this.ObservationType = observationType;
            observationType.TreatmentBMPObservations.Add(this);
            this.ObservationData = observationData;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPObservation CreateNewBlank(TreatmentBMPAssessment treatmentBMPAssessment, ObservationType observationType)
        {
            return new TreatmentBMPObservation(treatmentBMPAssessment, observationType, default(string));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPObservation).Name};

        [Key]
        public int TreatmentBMPObservationID { get; set; }
        public int TenantID { get; private set; }
        public int TreatmentBMPAssessmentID { get; set; }
        public int ObservationTypeID { get; set; }
        public string ObservationData { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPObservationID; } set { TreatmentBMPObservationID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual TreatmentBMPAssessment TreatmentBMPAssessment { get; set; }
        public virtual ObservationType ObservationType { get; set; }

        public static class FieldLengths
        {

        }
    }
}