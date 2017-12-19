//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPInfiltrationReading]
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
    [Table("[dbo].[TreatmentBMPInfiltrationReading]")]
    public partial class TreatmentBMPInfiltrationReading : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPInfiltrationReading()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPInfiltrationReading(int treatmentBMPInfiltrationReadingID, int treatmentBMPObservationDetailID, double readingValue, double readingTime) : this()
        {
            this.TreatmentBMPInfiltrationReadingID = treatmentBMPInfiltrationReadingID;
            this.TreatmentBMPObservationDetailID = treatmentBMPObservationDetailID;
            this.ReadingValue = readingValue;
            this.ReadingTime = readingTime;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPInfiltrationReading(int treatmentBMPObservationDetailID, double readingValue, double readingTime) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPInfiltrationReadingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPObservationDetailID = treatmentBMPObservationDetailID;
            this.ReadingValue = readingValue;
            this.ReadingTime = readingTime;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPInfiltrationReading(TreatmentBMPObservationDetail treatmentBMPObservationDetail, double readingValue, double readingTime) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPInfiltrationReadingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPObservationDetailID = treatmentBMPObservationDetail.TreatmentBMPObservationDetailID;
            this.TreatmentBMPObservationDetail = treatmentBMPObservationDetail;
            treatmentBMPObservationDetail.TreatmentBMPInfiltrationReadings.Add(this);
            this.ReadingValue = readingValue;
            this.ReadingTime = readingTime;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPInfiltrationReading CreateNewBlank(TreatmentBMPObservationDetail treatmentBMPObservationDetail)
        {
            return new TreatmentBMPInfiltrationReading(treatmentBMPObservationDetail, default(double), default(double));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPInfiltrationReading).Name};

        [Key]
        public int TreatmentBMPInfiltrationReadingID { get; set; }
        public int TenantID { get; private set; }
        public int TreatmentBMPObservationDetailID { get; set; }
        public double ReadingValue { get; set; }
        public double ReadingTime { get; set; }
        public int PrimaryKey { get { return TreatmentBMPInfiltrationReadingID; } set { TreatmentBMPInfiltrationReadingID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual TreatmentBMPObservationDetail TreatmentBMPObservationDetail { get; set; }

        public static class FieldLengths
        {

        }
    }
}