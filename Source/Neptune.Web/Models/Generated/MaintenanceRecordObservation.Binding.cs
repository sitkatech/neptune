//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecordObservation]
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
    [Table("[dbo].[MaintenanceRecordObservation]")]
    public partial class MaintenanceRecordObservation : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected MaintenanceRecordObservation()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public MaintenanceRecordObservation(int maintenanceRecordObservationID, int maintenanceRecordID, int treatmentBMPTypeAttributeTypeID, int treatmentBMPTypeID, int treatmentBMPAttributeTypeID) : this()
        {
            this.MaintenanceRecordObservationID = maintenanceRecordObservationID;
            this.MaintenanceRecordID = maintenanceRecordID;
            this.TreatmentBMPTypeAttributeTypeID = treatmentBMPTypeAttributeTypeID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.TreatmentBMPAttributeTypeID = treatmentBMPAttributeTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public MaintenanceRecordObservation(int maintenanceRecordID, int treatmentBMPTypeAttributeTypeID, int treatmentBMPTypeID, int treatmentBMPAttributeTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.MaintenanceRecordObservationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.MaintenanceRecordID = maintenanceRecordID;
            this.TreatmentBMPTypeAttributeTypeID = treatmentBMPTypeAttributeTypeID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.TreatmentBMPAttributeTypeID = treatmentBMPAttributeTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public MaintenanceRecordObservation(MaintenanceRecord maintenanceRecord, TreatmentBMPTypeAttributeType treatmentBMPTypeAttributeType, TreatmentBMPType treatmentBMPType, TreatmentBMPAttributeType treatmentBMPAttributeType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.MaintenanceRecordObservationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.MaintenanceRecordID = maintenanceRecord.MaintenanceRecordID;
            this.MaintenanceRecord = maintenanceRecord;
            maintenanceRecord.MaintenanceRecordObservations.Add(this);
            this.TreatmentBMPTypeAttributeTypeID = treatmentBMPTypeAttributeType.TreatmentBMPTypeAttributeTypeID;
            this.TreatmentBMPTypeAttributeType = treatmentBMPTypeAttributeType;
            treatmentBMPTypeAttributeType.MaintenanceRecordObservations.Add(this);
            this.TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID;
            this.TreatmentBMPType = treatmentBMPType;
            treatmentBMPType.MaintenanceRecordObservations.Add(this);
            this.TreatmentBMPAttributeTypeID = treatmentBMPAttributeType.TreatmentBMPAttributeTypeID;
            this.TreatmentBMPAttributeType = treatmentBMPAttributeType;
            treatmentBMPAttributeType.MaintenanceRecordObservations.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static MaintenanceRecordObservation CreateNewBlank(MaintenanceRecord maintenanceRecord, TreatmentBMPTypeAttributeType treatmentBMPTypeAttributeType, TreatmentBMPType treatmentBMPType, TreatmentBMPAttributeType treatmentBMPAttributeType)
        {
            return new MaintenanceRecordObservation(maintenanceRecord, treatmentBMPTypeAttributeType, treatmentBMPType, treatmentBMPAttributeType);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(MaintenanceRecordObservation).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {
            HttpRequestStorage.DatabaseEntities.AllMaintenanceRecordObservations.Remove(this);                
        }

        [Key]
        public int MaintenanceRecordObservationID { get; set; }
        public int TenantID { get; private set; }
        public int MaintenanceRecordID { get; set; }
        public int TreatmentBMPTypeAttributeTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int TreatmentBMPAttributeTypeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return MaintenanceRecordObservationID; } set { MaintenanceRecordObservationID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual MaintenanceRecord MaintenanceRecord { get; set; }
        public virtual TreatmentBMPTypeAttributeType TreatmentBMPTypeAttributeType { get; set; }
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        public virtual TreatmentBMPAttributeType TreatmentBMPAttributeType { get; set; }

        public static class FieldLengths
        {

        }
    }
}