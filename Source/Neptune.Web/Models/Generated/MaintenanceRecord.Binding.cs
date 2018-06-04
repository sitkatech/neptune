//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecord]
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
    [Table("[dbo].[MaintenanceRecord]")]
    public partial class MaintenanceRecord : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected MaintenanceRecord()
        {
            this.FieldVisits = new HashSet<FieldVisit>();
            this.MaintenanceRecordObservations = new HashSet<MaintenanceRecordObservation>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public MaintenanceRecord(int maintenanceRecordID, int treatmentBMPID, string maintenanceRecordDescription, int maintenanceRecordTypeID) : this()
        {
            this.MaintenanceRecordID = maintenanceRecordID;
            this.TreatmentBMPID = treatmentBMPID;
            this.MaintenanceRecordDescription = maintenanceRecordDescription;
            this.MaintenanceRecordTypeID = maintenanceRecordTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public MaintenanceRecord(int treatmentBMPID, int maintenanceRecordTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.MaintenanceRecordID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPID = treatmentBMPID;
            this.MaintenanceRecordTypeID = maintenanceRecordTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public MaintenanceRecord(TreatmentBMP treatmentBMP, MaintenanceRecordType maintenanceRecordType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.MaintenanceRecordID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
            treatmentBMP.MaintenanceRecords.Add(this);
            this.MaintenanceRecordTypeID = maintenanceRecordType.MaintenanceRecordTypeID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static MaintenanceRecord CreateNewBlank(TreatmentBMP treatmentBMP, MaintenanceRecordType maintenanceRecordType)
        {
            return new MaintenanceRecord(treatmentBMP, maintenanceRecordType);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return (FieldVisit != null) || MaintenanceRecordObservations.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(MaintenanceRecord).Name, typeof(FieldVisit).Name, typeof(MaintenanceRecordObservation).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {

            foreach(var x in FieldVisits.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in MaintenanceRecordObservations.ToList())
            {
                x.DeleteFull();
            }
            HttpRequestStorage.DatabaseEntities.AllMaintenanceRecords.Remove(this);                
        }

        [Key]
        public int MaintenanceRecordID { get; set; }
        public int TenantID { get; private set; }
        public int TreatmentBMPID { get; set; }
        public string MaintenanceRecordDescription { get; set; }
        public int MaintenanceRecordTypeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return MaintenanceRecordID; } set { MaintenanceRecordID = value; } }

        protected virtual ICollection<FieldVisit> FieldVisits { get; set; }
        [NotMapped]
        public FieldVisit FieldVisit { get { return FieldVisits.SingleOrDefault(); } set { FieldVisits = new List<FieldVisit>{value};} }
        public virtual ICollection<MaintenanceRecordObservation> MaintenanceRecordObservations { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        public MaintenanceRecordType MaintenanceRecordType { get { return MaintenanceRecordType.AllLookupDictionary[MaintenanceRecordTypeID]; } }

        public static class FieldLengths
        {
            public const int MaintenanceRecordDescription = 500;
        }
    }
}