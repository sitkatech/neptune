//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAttribute]
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
    [Table("[dbo].[TreatmentBMPAttribute]")]
    public partial class TreatmentBMPAttribute : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPAttribute()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPAttribute(int treatmentBMPAttributeID, int treatmentBMPID, int treatmentBMPTypeAttributeTypeID, int treatmentBMPTypeID, int treatmentBMPAttributeTypeID, string treatmentBMPAttributeValue) : this()
        {
            this.TreatmentBMPAttributeID = treatmentBMPAttributeID;
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPTypeAttributeTypeID = treatmentBMPTypeAttributeTypeID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.TreatmentBMPAttributeTypeID = treatmentBMPAttributeTypeID;
            this.TreatmentBMPAttributeValue = treatmentBMPAttributeValue;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPAttribute(int treatmentBMPID, int treatmentBMPTypeAttributeTypeID, int treatmentBMPTypeID, int treatmentBMPAttributeTypeID, string treatmentBMPAttributeValue) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPAttributeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPTypeAttributeTypeID = treatmentBMPTypeAttributeTypeID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.TreatmentBMPAttributeTypeID = treatmentBMPAttributeTypeID;
            this.TreatmentBMPAttributeValue = treatmentBMPAttributeValue;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPAttribute(TreatmentBMP treatmentBMP, TreatmentBMPTypeAttributeType treatmentBMPTypeAttributeType, TreatmentBMPType treatmentBMPType, TreatmentBMPAttributeType treatmentBMPAttributeType, string treatmentBMPAttributeValue) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPAttributeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
            treatmentBMP.TreatmentBMPAttributes.Add(this);
            this.TreatmentBMPTypeAttributeTypeID = treatmentBMPTypeAttributeType.TreatmentBMPTypeAttributeTypeID;
            this.TreatmentBMPTypeAttributeType = treatmentBMPTypeAttributeType;
            treatmentBMPTypeAttributeType.TreatmentBMPAttributes.Add(this);
            this.TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID;
            this.TreatmentBMPType = treatmentBMPType;
            treatmentBMPType.TreatmentBMPAttributes.Add(this);
            this.TreatmentBMPAttributeTypeID = treatmentBMPAttributeType.TreatmentBMPAttributeTypeID;
            this.TreatmentBMPAttributeType = treatmentBMPAttributeType;
            treatmentBMPAttributeType.TreatmentBMPAttributes.Add(this);
            this.TreatmentBMPAttributeValue = treatmentBMPAttributeValue;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPAttribute CreateNewBlank(TreatmentBMP treatmentBMP, TreatmentBMPTypeAttributeType treatmentBMPTypeAttributeType, TreatmentBMPType treatmentBMPType, TreatmentBMPAttributeType treatmentBMPAttributeType)
        {
            return new TreatmentBMPAttribute(treatmentBMP, treatmentBMPTypeAttributeType, treatmentBMPType, treatmentBMPAttributeType, default(string));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPAttribute).Name};

        [Key]
        public int TreatmentBMPAttributeID { get; set; }
        public int TenantID { get; private set; }
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPTypeAttributeTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int TreatmentBMPAttributeTypeID { get; set; }
        public string TreatmentBMPAttributeValue { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPAttributeID; } set { TreatmentBMPAttributeID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        public virtual TreatmentBMPTypeAttributeType TreatmentBMPTypeAttributeType { get; set; }
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        public virtual TreatmentBMPAttributeType TreatmentBMPAttributeType { get; set; }

        public static class FieldLengths
        {
            public const int TreatmentBMPAttributeValue = 1000;
        }
    }
}