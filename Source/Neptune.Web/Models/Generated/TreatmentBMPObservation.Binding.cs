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
        public TreatmentBMPObservation(int treatmentBMPObservationID, int treatmentBMPAssessmentID, int treatmentBMPTypeAssessmentObservationTypeID, int treatmentBMPTypeID, int observationTypeID, string observationData) : this()
        {
            this.TreatmentBMPObservationID = treatmentBMPObservationID;
            this.TreatmentBMPAssessmentID = treatmentBMPAssessmentID;
            this.TreatmentBMPTypeAssessmentObservationTypeID = treatmentBMPTypeAssessmentObservationTypeID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.ObservationTypeID = observationTypeID;
            this.ObservationData = observationData;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPObservation(int treatmentBMPAssessmentID, int treatmentBMPTypeAssessmentObservationTypeID, int treatmentBMPTypeID, int observationTypeID, string observationData) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPObservationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPAssessmentID = treatmentBMPAssessmentID;
            this.TreatmentBMPTypeAssessmentObservationTypeID = treatmentBMPTypeAssessmentObservationTypeID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.ObservationTypeID = observationTypeID;
            this.ObservationData = observationData;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPObservation(TreatmentBMPAssessment treatmentBMPAssessment, TreatmentBMPTypeAssessmentObservationType treatmentBMPTypeAssessmentObservationType, TreatmentBMPType treatmentBMPType, TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType, string observationData) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPObservationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPAssessmentID = treatmentBMPAssessment.TreatmentBMPAssessmentID;
            this.TreatmentBMPAssessment = treatmentBMPAssessment;
            treatmentBMPAssessment.TreatmentBMPObservations.Add(this);
            this.TreatmentBMPTypeAssessmentObservationTypeID = treatmentBMPTypeAssessmentObservationType.TreatmentBMPTypeAssessmentObservationTypeID;
            this.TreatmentBMPTypeAssessmentObservationType = treatmentBMPTypeAssessmentObservationType;
            treatmentBMPTypeAssessmentObservationType.TreatmentBMPObservations.Add(this);
            this.TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID;
            this.TreatmentBMPType = treatmentBMPType;
            treatmentBMPType.TreatmentBMPObservations.Add(this);
            this.ObservationTypeID = TreatmentBMPAssessmentObservationType.ObservationTypeID;
            this.TreatmentBMPAssessmentObservationType = TreatmentBMPAssessmentObservationType;
            TreatmentBMPAssessmentObservationType.TreatmentBMPObservations.Add(this);
            this.ObservationData = observationData;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPObservation CreateNewBlank(TreatmentBMPAssessment treatmentBMPAssessment, TreatmentBMPTypeAssessmentObservationType treatmentBMPTypeAssessmentObservationType, TreatmentBMPType treatmentBMPType, TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            return new TreatmentBMPObservation(treatmentBMPAssessment, treatmentBMPTypeAssessmentObservationType, treatmentBMPType, TreatmentBMPAssessmentObservationType, default(string));
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


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {
            HttpRequestStorage.DatabaseEntities.AllTreatmentBMPObservations.Remove(this);                
        }

        [Key]
        public int TreatmentBMPObservationID { get; set; }
        public int TenantID { get; private set; }
        public int TreatmentBMPAssessmentID { get; set; }
        public int TreatmentBMPTypeAssessmentObservationTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int ObservationTypeID { get; set; }
        public string ObservationData { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPObservationID; } set { TreatmentBMPObservationID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual TreatmentBMPAssessment TreatmentBMPAssessment { get; set; }
        public virtual TreatmentBMPTypeAssessmentObservationType TreatmentBMPTypeAssessmentObservationType { get; set; }
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        public virtual TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType { get; set; }

        public static class FieldLengths
        {

        }
    }
}