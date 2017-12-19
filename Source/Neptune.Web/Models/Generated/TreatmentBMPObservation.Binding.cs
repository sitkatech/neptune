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
            this.TreatmentBMPObservationDetails = new HashSet<TreatmentBMPObservationDetail>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPObservation(int treatmentBMPObservationID, int treatmentBMPAssessmentID, int observationTypeID, int observationValueTypeID) : this()
        {
            this.TreatmentBMPObservationID = treatmentBMPObservationID;
            this.TreatmentBMPAssessmentID = treatmentBMPAssessmentID;
            this.ObservationTypeID = observationTypeID;
            this.ObservationValueTypeID = observationValueTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPObservation(int treatmentBMPAssessmentID, int observationTypeID, int observationValueTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPObservationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPAssessmentID = treatmentBMPAssessmentID;
            this.ObservationTypeID = observationTypeID;
            this.ObservationValueTypeID = observationValueTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPObservation(TreatmentBMPAssessment treatmentBMPAssessment, ObservationType observationType, ObservationValueType observationValueType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPObservationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPAssessmentID = treatmentBMPAssessment.TreatmentBMPAssessmentID;
            this.TreatmentBMPAssessment = treatmentBMPAssessment;
            treatmentBMPAssessment.TreatmentBMPObservations.Add(this);
            this.ObservationTypeID = observationType.ObservationTypeID;
            this.ObservationValueTypeID = observationValueType.ObservationValueTypeID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPObservation CreateNewBlank(TreatmentBMPAssessment treatmentBMPAssessment, ObservationType observationType, ObservationValueType observationValueType)
        {
            return new TreatmentBMPObservation(treatmentBMPAssessment, observationType, observationValueType);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return TreatmentBMPObservationDetails.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPObservation).Name, typeof(TreatmentBMPObservationDetail).Name};

        [Key]
        public int TreatmentBMPObservationID { get; set; }
        public int TenantID { get; private set; }
        public int TreatmentBMPAssessmentID { get; set; }
        public int ObservationTypeID { get; set; }
        public int ObservationValueTypeID { get; set; }
        public int PrimaryKey { get { return TreatmentBMPObservationID; } set { TreatmentBMPObservationID = value; } }

        public virtual ICollection<TreatmentBMPObservationDetail> TreatmentBMPObservationDetails { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual TreatmentBMPAssessment TreatmentBMPAssessment { get; set; }
        public ObservationType ObservationType { get { return ObservationType.AllLookupDictionary[ObservationTypeID]; } }
        public ObservationValueType ObservationValueType { get { return ObservationValueType.AllLookupDictionary[ObservationValueTypeID]; } }

        public static class FieldLengths
        {

        }
    }
}