//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPTypeAttributeType]
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
    [Table("[dbo].[TreatmentBMPTypeAttributeType]")]
    public partial class TreatmentBMPTypeAttributeType : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPTypeAttributeType()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPTypeAttributeType(int treatmentBMPTypeAttributeTypeID, int treatmentBMPTypeID, int treatmentBMPAttributeTypeID) : this()
        {
            this.TreatmentBMPTypeAttributeTypeID = treatmentBMPTypeAttributeTypeID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.TreatmentBMPAttributeTypeID = treatmentBMPAttributeTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPTypeAttributeType(int treatmentBMPTypeID, int treatmentBMPAttributeTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPTypeAttributeTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.TreatmentBMPAttributeTypeID = treatmentBMPAttributeTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPTypeAttributeType(TreatmentBMPType treatmentBMPType, TreatmentBMPAttributeType treatmentBMPAttributeType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPTypeAttributeTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID;
            this.TreatmentBMPType = treatmentBMPType;
            treatmentBMPType.TreatmentBMPTypeAttributeTypes.Add(this);
            this.TreatmentBMPAttributeTypeID = treatmentBMPAttributeType.TreatmentBMPAttributeTypeID;
            this.TreatmentBMPAttributeType = treatmentBMPAttributeType;
            treatmentBMPAttributeType.TreatmentBMPTypeAttributeTypes.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPTypeAttributeType CreateNewBlank(TreatmentBMPType treatmentBMPType, TreatmentBMPAttributeType treatmentBMPAttributeType)
        {
            return new TreatmentBMPTypeAttributeType(treatmentBMPType, treatmentBMPAttributeType);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPTypeAttributeType).Name};

        [Key]
        public int TreatmentBMPTypeAttributeTypeID { get; set; }
        public int TenantID { get; private set; }
        public int TreatmentBMPTypeID { get; set; }
        public int TreatmentBMPAttributeTypeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPTypeAttributeTypeID; } set { TreatmentBMPTypeAttributeTypeID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        public virtual TreatmentBMPAttributeType TreatmentBMPAttributeType { get; set; }

        public static class FieldLengths
        {

        }
    }
}