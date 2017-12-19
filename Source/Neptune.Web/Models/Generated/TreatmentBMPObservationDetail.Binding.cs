//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPObservationDetail]
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
    [Table("[dbo].[TreatmentBMPObservationDetail]")]
    public partial class TreatmentBMPObservationDetail : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPObservationDetail()
        {
            this.TreatmentBMPInfiltrationReadings = new HashSet<TreatmentBMPInfiltrationReading>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPObservationDetail(int treatmentBMPObservationDetailID, int treatmentBMPObservationID, int treatmentBMPObservationDetailTypeID, double treatmentBMPObservationValue, string notes) : this()
        {
            this.TreatmentBMPObservationDetailID = treatmentBMPObservationDetailID;
            this.TreatmentBMPObservationID = treatmentBMPObservationID;
            this.TreatmentBMPObservationDetailTypeID = treatmentBMPObservationDetailTypeID;
            this.TreatmentBMPObservationValue = treatmentBMPObservationValue;
            this.Notes = notes;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPObservationDetail(int treatmentBMPObservationID, int treatmentBMPObservationDetailTypeID, double treatmentBMPObservationValue) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPObservationDetailID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPObservationID = treatmentBMPObservationID;
            this.TreatmentBMPObservationDetailTypeID = treatmentBMPObservationDetailTypeID;
            this.TreatmentBMPObservationValue = treatmentBMPObservationValue;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPObservationDetail(TreatmentBMPObservation treatmentBMPObservation, TreatmentBMPObservationDetailType treatmentBMPObservationDetailType, double treatmentBMPObservationValue) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPObservationDetailID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPObservationID = treatmentBMPObservation.TreatmentBMPObservationID;
            this.TreatmentBMPObservation = treatmentBMPObservation;
            treatmentBMPObservation.TreatmentBMPObservationDetails.Add(this);
            this.TreatmentBMPObservationDetailTypeID = treatmentBMPObservationDetailType.TreatmentBMPObservationDetailTypeID;
            this.TreatmentBMPObservationValue = treatmentBMPObservationValue;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPObservationDetail CreateNewBlank(TreatmentBMPObservation treatmentBMPObservation, TreatmentBMPObservationDetailType treatmentBMPObservationDetailType)
        {
            return new TreatmentBMPObservationDetail(treatmentBMPObservation, treatmentBMPObservationDetailType, default(double));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return TreatmentBMPInfiltrationReadings.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPObservationDetail).Name, typeof(TreatmentBMPInfiltrationReading).Name};

        [Key]
        public int TreatmentBMPObservationDetailID { get; set; }
        public int TenantID { get; private set; }
        public int TreatmentBMPObservationID { get; set; }
        public int TreatmentBMPObservationDetailTypeID { get; set; }
        public double TreatmentBMPObservationValue { get; set; }
        public string Notes { get; set; }
        public int PrimaryKey { get { return TreatmentBMPObservationDetailID; } set { TreatmentBMPObservationDetailID = value; } }

        public virtual ICollection<TreatmentBMPInfiltrationReading> TreatmentBMPInfiltrationReadings { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual TreatmentBMPObservation TreatmentBMPObservation { get; set; }
        public TreatmentBMPObservationDetailType TreatmentBMPObservationDetailType { get { return TreatmentBMPObservationDetailType.AllLookupDictionary[TreatmentBMPObservationDetailTypeID]; } }

        public static class FieldLengths
        {
            public const int Notes = 300;
        }
    }
}