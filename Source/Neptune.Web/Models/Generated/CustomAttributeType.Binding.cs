//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeType]
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
    [Table("[dbo].[CustomAttributeType]")]
    public partial class CustomAttributeType : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected CustomAttributeType()
        {
            this.CustomAttributes = new HashSet<CustomAttribute>();
            this.MaintenanceRecordObservations = new HashSet<MaintenanceRecordObservation>();
            this.TreatmentBMPTypeAttributeTypes = new HashSet<TreatmentBMPTypeAttributeType>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public CustomAttributeType(int customAttributeTypeID, string customAttributeTypeName, int customAttributeDataTypeID, int? measurementUnitTypeID, bool isRequired, string customAttributeTypeDescription, int customAttributeTypePurposeID, string customAttributeTypeOptionsSchema) : this()
        {
            this.CustomAttributeTypeID = customAttributeTypeID;
            this.CustomAttributeTypeName = customAttributeTypeName;
            this.CustomAttributeDataTypeID = customAttributeDataTypeID;
            this.MeasurementUnitTypeID = measurementUnitTypeID;
            this.IsRequired = isRequired;
            this.CustomAttributeTypeDescription = customAttributeTypeDescription;
            this.CustomAttributeTypePurposeID = customAttributeTypePurposeID;
            this.CustomAttributeTypeOptionsSchema = customAttributeTypeOptionsSchema;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public CustomAttributeType(string customAttributeTypeName, int customAttributeDataTypeID, bool isRequired, int customAttributeTypePurposeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.CustomAttributeTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.CustomAttributeTypeName = customAttributeTypeName;
            this.CustomAttributeDataTypeID = customAttributeDataTypeID;
            this.IsRequired = isRequired;
            this.CustomAttributeTypePurposeID = customAttributeTypePurposeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public CustomAttributeType(string customAttributeTypeName, CustomAttributeDataType customAttributeDataType, bool isRequired, CustomAttributeTypePurpose customAttributeTypePurpose) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.CustomAttributeTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.CustomAttributeTypeName = customAttributeTypeName;
            this.CustomAttributeDataTypeID = customAttributeDataType.CustomAttributeDataTypeID;
            this.IsRequired = isRequired;
            this.CustomAttributeTypePurposeID = customAttributeTypePurpose.CustomAttributeTypePurposeID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static CustomAttributeType CreateNewBlank(CustomAttributeDataType customAttributeDataType, CustomAttributeTypePurpose customAttributeTypePurpose)
        {
            return new CustomAttributeType(default(string), customAttributeDataType, default(bool), customAttributeTypePurpose);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return CustomAttributes.Any() || MaintenanceRecordObservations.Any() || TreatmentBMPTypeAttributeTypes.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(CustomAttributeType).Name, typeof(CustomAttribute).Name, typeof(MaintenanceRecordObservation).Name, typeof(TreatmentBMPTypeAttributeType).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {

            foreach(var x in CustomAttributes.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in MaintenanceRecordObservations.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in TreatmentBMPTypeAttributeTypes.ToList())
            {
                x.DeleteFull();
            }
            HttpRequestStorage.DatabaseEntities.AllCustomAttributeTypes.Remove(this);                
        }

        [Key]
        public int CustomAttributeTypeID { get; set; }
        public int TenantID { get; private set; }
        public string CustomAttributeTypeName { get; set; }
        public int CustomAttributeDataTypeID { get; set; }
        public int? MeasurementUnitTypeID { get; set; }
        public bool IsRequired { get; set; }
        public string CustomAttributeTypeDescription { get; set; }
        public int CustomAttributeTypePurposeID { get; set; }
        public string CustomAttributeTypeOptionsSchema { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return CustomAttributeTypeID; } set { CustomAttributeTypeID = value; } }

        public virtual ICollection<CustomAttribute> CustomAttributes { get; set; }
        public virtual ICollection<MaintenanceRecordObservation> MaintenanceRecordObservations { get; set; }
        public virtual ICollection<TreatmentBMPTypeAttributeType> TreatmentBMPTypeAttributeTypes { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public CustomAttributeDataType CustomAttributeDataType { get { return CustomAttributeDataType.AllLookupDictionary[CustomAttributeDataTypeID]; } }
        public MeasurementUnitType MeasurementUnitType { get { return MeasurementUnitTypeID.HasValue ? MeasurementUnitType.AllLookupDictionary[MeasurementUnitTypeID.Value] : null; } }
        public CustomAttributeTypePurpose CustomAttributeTypePurpose { get { return CustomAttributeTypePurpose.AllLookupDictionary[CustomAttributeTypePurposeID]; } }

        public static class FieldLengths
        {
            public const int CustomAttributeTypeName = 100;
            public const int CustomAttributeTypeDescription = 200;
        }
    }
}