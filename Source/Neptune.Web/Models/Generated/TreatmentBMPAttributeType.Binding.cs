//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAttributeType]
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
    [Table("[dbo].[TreatmentBMPAttributeType]")]
    public partial class TreatmentBMPAttributeType : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPAttributeType()
        {
            this.MaintenanceRecordObservations = new HashSet<MaintenanceRecordObservation>();
            this.TreatmentBMPAttributes = new HashSet<TreatmentBMPAttribute>();
            this.TreatmentBMPTypeAttributeTypes = new HashSet<TreatmentBMPTypeAttributeType>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPAttributeType(int treatmentBMPAttributeTypeID, string treatmentBMPAttributeTypeName, int treatmentBMPAttributeDataTypeID, int? measurementUnitTypeID, bool isRequired, string treatmentBMPAttributeTypeDescription, int treatmentBMPAttributeTypePurposeID, string treatmentBMPAttributeTypeOptionsSchema) : this()
        {
            this.TreatmentBMPAttributeTypeID = treatmentBMPAttributeTypeID;
            this.TreatmentBMPAttributeTypeName = treatmentBMPAttributeTypeName;
            this.TreatmentBMPAttributeDataTypeID = treatmentBMPAttributeDataTypeID;
            this.MeasurementUnitTypeID = measurementUnitTypeID;
            this.IsRequired = isRequired;
            this.TreatmentBMPAttributeTypeDescription = treatmentBMPAttributeTypeDescription;
            this.TreatmentBMPAttributeTypePurposeID = treatmentBMPAttributeTypePurposeID;
            this.TreatmentBMPAttributeTypeOptionsSchema = treatmentBMPAttributeTypeOptionsSchema;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPAttributeType(string treatmentBMPAttributeTypeName, int treatmentBMPAttributeDataTypeID, bool isRequired, int treatmentBMPAttributeTypePurposeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPAttributeTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPAttributeTypeName = treatmentBMPAttributeTypeName;
            this.TreatmentBMPAttributeDataTypeID = treatmentBMPAttributeDataTypeID;
            this.IsRequired = isRequired;
            this.TreatmentBMPAttributeTypePurposeID = treatmentBMPAttributeTypePurposeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPAttributeType(string treatmentBMPAttributeTypeName, TreatmentBMPAttributeDataType treatmentBMPAttributeDataType, bool isRequired, TreatmentBMPAttributeTypePurpose treatmentBMPAttributeTypePurpose) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPAttributeTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPAttributeTypeName = treatmentBMPAttributeTypeName;
            this.TreatmentBMPAttributeDataTypeID = treatmentBMPAttributeDataType.TreatmentBMPAttributeDataTypeID;
            this.IsRequired = isRequired;
            this.TreatmentBMPAttributeTypePurposeID = treatmentBMPAttributeTypePurpose.TreatmentBMPAttributeTypePurposeID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPAttributeType CreateNewBlank(TreatmentBMPAttributeDataType treatmentBMPAttributeDataType, TreatmentBMPAttributeTypePurpose treatmentBMPAttributeTypePurpose)
        {
            return new TreatmentBMPAttributeType(default(string), treatmentBMPAttributeDataType, default(bool), treatmentBMPAttributeTypePurpose);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return MaintenanceRecordObservations.Any() || TreatmentBMPAttributes.Any() || TreatmentBMPTypeAttributeTypes.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPAttributeType).Name, typeof(MaintenanceRecordObservation).Name, typeof(TreatmentBMPAttribute).Name, typeof(TreatmentBMPTypeAttributeType).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {

            foreach(var x in MaintenanceRecordObservations.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in TreatmentBMPAttributes.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in TreatmentBMPTypeAttributeTypes.ToList())
            {
                x.DeleteFull();
            }
            HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAttributeTypes.Remove(this);                
        }

        [Key]
        public int TreatmentBMPAttributeTypeID { get; set; }
        public int TenantID { get; private set; }
        public string TreatmentBMPAttributeTypeName { get; set; }
        public int TreatmentBMPAttributeDataTypeID { get; set; }
        public int? MeasurementUnitTypeID { get; set; }
        public bool IsRequired { get; set; }
        public string TreatmentBMPAttributeTypeDescription { get; set; }
        public int TreatmentBMPAttributeTypePurposeID { get; set; }
        public string TreatmentBMPAttributeTypeOptionsSchema { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPAttributeTypeID; } set { TreatmentBMPAttributeTypeID = value; } }

        public virtual ICollection<MaintenanceRecordObservation> MaintenanceRecordObservations { get; set; }
        public virtual ICollection<TreatmentBMPAttribute> TreatmentBMPAttributes { get; set; }
        public virtual ICollection<TreatmentBMPTypeAttributeType> TreatmentBMPTypeAttributeTypes { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public TreatmentBMPAttributeDataType TreatmentBMPAttributeDataType { get { return TreatmentBMPAttributeDataType.AllLookupDictionary[TreatmentBMPAttributeDataTypeID]; } }
        public MeasurementUnitType MeasurementUnitType { get { return MeasurementUnitTypeID.HasValue ? MeasurementUnitType.AllLookupDictionary[MeasurementUnitTypeID.Value] : null; } }
        public TreatmentBMPAttributeTypePurpose TreatmentBMPAttributeTypePurpose { get { return TreatmentBMPAttributeTypePurpose.AllLookupDictionary[TreatmentBMPAttributeTypePurposeID]; } }

        public static class FieldLengths
        {
            public const int TreatmentBMPAttributeTypeName = 100;
            public const int TreatmentBMPAttributeTypeDescription = 200;
        }
    }
}