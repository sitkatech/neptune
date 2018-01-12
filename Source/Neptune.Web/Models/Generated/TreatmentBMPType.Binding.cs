//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPType]
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
    [Table("[dbo].[TreatmentBMPType]")]
    public partial class TreatmentBMPType : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPType()
        {
            this.TreatmentBMPs = new HashSet<TreatmentBMP>();
            this.TreatmentBMPTypeObservationTypes = new HashSet<TreatmentBMPTypeObservationType>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPType(int treatmentBMPTypeID, string treatmentBMPTypeName, string treatmentBMPTypeDescription) : this()
        {
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.TreatmentBMPTypeName = treatmentBMPTypeName;
            this.TreatmentBMPTypeDescription = treatmentBMPTypeDescription;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPType(string treatmentBMPTypeName, string treatmentBMPTypeDescription) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPTypeName = treatmentBMPTypeName;
            this.TreatmentBMPTypeDescription = treatmentBMPTypeDescription;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPType CreateNewBlank()
        {
            return new TreatmentBMPType(default(string), default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return TreatmentBMPs.Any() || TreatmentBMPTypeObservationTypes.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPType).Name, typeof(TreatmentBMP).Name, typeof(TreatmentBMPTypeObservationType).Name};

        [Key]
        public int TreatmentBMPTypeID { get; set; }
        public int TenantID { get; private set; }
        public string TreatmentBMPTypeName { get; set; }
        public string TreatmentBMPTypeDescription { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPTypeID; } set { TreatmentBMPTypeID = value; } }

        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
        public virtual ICollection<TreatmentBMPTypeObservationType> TreatmentBMPTypeObservationTypes { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }

        public static class FieldLengths
        {
            public const int TreatmentBMPTypeName = 100;
            public const int TreatmentBMPTypeDescription = 1000;
        }
    }
}