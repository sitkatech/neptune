//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttribute]
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
    [Table("[dbo].[CustomAttribute]")]
    public partial class CustomAttribute : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected CustomAttribute()
        {
            this.CustomAttributeValues = new HashSet<CustomAttributeValue>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public CustomAttribute(int customAttributeID, int treatmentBMPID, int treatmentBMPTypeCustomAttributeTypeID, int treatmentBMPTypeID, int customAttributeTypeID) : this()
        {
            this.CustomAttributeID = customAttributeID;
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPTypeCustomAttributeTypeID = treatmentBMPTypeCustomAttributeTypeID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.CustomAttributeTypeID = customAttributeTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public CustomAttribute(int treatmentBMPID, int treatmentBMPTypeCustomAttributeTypeID, int treatmentBMPTypeID, int customAttributeTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.CustomAttributeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPTypeCustomAttributeTypeID = treatmentBMPTypeCustomAttributeTypeID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.CustomAttributeTypeID = customAttributeTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public CustomAttribute(TreatmentBMP treatmentBMP, TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeType, TreatmentBMPType treatmentBMPType, CustomAttributeType customAttributeType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.CustomAttributeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
            treatmentBMP.CustomAttributes.Add(this);
            this.TreatmentBMPTypeCustomAttributeTypeID = treatmentBMPTypeCustomAttributeType.TreatmentBMPTypeCustomAttributeTypeID;
            this.TreatmentBMPTypeCustomAttributeType = treatmentBMPTypeCustomAttributeType;
            treatmentBMPTypeCustomAttributeType.CustomAttributes.Add(this);
            this.TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID;
            this.TreatmentBMPType = treatmentBMPType;
            treatmentBMPType.CustomAttributes.Add(this);
            this.CustomAttributeTypeID = customAttributeType.CustomAttributeTypeID;
            this.CustomAttributeType = customAttributeType;
            customAttributeType.CustomAttributes.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static CustomAttribute CreateNewBlank(TreatmentBMP treatmentBMP, TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeType, TreatmentBMPType treatmentBMPType, CustomAttributeType customAttributeType)
        {
            return new CustomAttribute(treatmentBMP, treatmentBMPTypeCustomAttributeType, treatmentBMPType, customAttributeType);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return CustomAttributeValues.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(CustomAttribute).Name, typeof(CustomAttributeValue).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {

            foreach(var x in CustomAttributeValues.ToList())
            {
                x.DeleteFull();
            }
            HttpRequestStorage.DatabaseEntities.AllCustomAttributes.Remove(this);                
        }

        [Key]
        public int CustomAttributeID { get; set; }
        public int TenantID { get; private set; }
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPTypeCustomAttributeTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int CustomAttributeTypeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return CustomAttributeID; } set { CustomAttributeID = value; } }

        public virtual ICollection<CustomAttributeValue> CustomAttributeValues { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        public virtual TreatmentBMPTypeCustomAttributeType TreatmentBMPTypeCustomAttributeType { get; set; }
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        public virtual CustomAttributeType CustomAttributeType { get; set; }

        public static class FieldLengths
        {

        }
    }
}