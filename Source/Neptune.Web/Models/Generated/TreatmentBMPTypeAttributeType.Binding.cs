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
            this.CustomAttributes = new HashSet<CustomAttribute>();
            this.MaintenanceRecordObservations = new HashSet<MaintenanceRecordObservation>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPTypeAttributeType(int treatmentBMPTypeAttributeTypeID, int treatmentBMPTypeID, int customAttributeTypeID) : this()
        {
            this.TreatmentBMPTypeAttributeTypeID = treatmentBMPTypeAttributeTypeID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.CustomAttributeTypeID = customAttributeTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPTypeAttributeType(int treatmentBMPTypeID, int customAttributeTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPTypeAttributeTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.CustomAttributeTypeID = customAttributeTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPTypeAttributeType(TreatmentBMPType treatmentBMPType, CustomAttributeType customAttributeType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPTypeAttributeTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID;
            this.TreatmentBMPType = treatmentBMPType;
            treatmentBMPType.TreatmentBMPTypeAttributeTypes.Add(this);
            this.CustomAttributeTypeID = customAttributeType.CustomAttributeTypeID;
            this.CustomAttributeType = customAttributeType;
            customAttributeType.TreatmentBMPTypeAttributeTypes.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPTypeAttributeType CreateNewBlank(TreatmentBMPType treatmentBMPType, CustomAttributeType customAttributeType)
        {
            return new TreatmentBMPTypeAttributeType(treatmentBMPType, customAttributeType);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return CustomAttributes.Any() || MaintenanceRecordObservations.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPTypeAttributeType).Name, typeof(CustomAttribute).Name, typeof(MaintenanceRecordObservation).Name};


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
            HttpRequestStorage.DatabaseEntities.AllTreatmentBMPTypeAttributeTypes.Remove(this);                
        }

        [Key]
        public int TreatmentBMPTypeAttributeTypeID { get; set; }
        public int TenantID { get; private set; }
        public int TreatmentBMPTypeID { get; set; }
        public int CustomAttributeTypeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPTypeAttributeTypeID; } set { TreatmentBMPTypeAttributeTypeID = value; } }

        public virtual ICollection<CustomAttribute> CustomAttributes { get; set; }
        public virtual ICollection<MaintenanceRecordObservation> MaintenanceRecordObservations { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        public virtual CustomAttributeType CustomAttributeType { get; set; }

        public static class FieldLengths
        {

        }
    }
}