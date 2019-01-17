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
    // Table [dbo].[MaintenanceRecord] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[MaintenanceRecord]")]
    public partial class MaintenanceRecord : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected MaintenanceRecord()
        {
            this.MaintenanceRecordObservations = new HashSet<MaintenanceRecordObservation>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public MaintenanceRecord(int maintenanceRecordID, int treatmentBMPID, int treatmentBMPTypeID, int fieldVisitID, string maintenanceRecordDescription, int? maintenanceRecordTypeID) : this()
        {
            this.MaintenanceRecordID = maintenanceRecordID;
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.FieldVisitID = fieldVisitID;
            this.MaintenanceRecordDescription = maintenanceRecordDescription;
            this.MaintenanceRecordTypeID = maintenanceRecordTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public MaintenanceRecord(int treatmentBMPID, int treatmentBMPTypeID, int fieldVisitID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.MaintenanceRecordID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.FieldVisitID = fieldVisitID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public MaintenanceRecord(TreatmentBMP treatmentBMP, int treatmentBMPTypeID, FieldVisit fieldVisit) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.MaintenanceRecordID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
            treatmentBMP.MaintenanceRecords.Add(this);
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.FieldVisitID = fieldVisit.FieldVisitID;
            this.FieldVisit = fieldVisit;
            fieldVisit.MaintenanceRecords.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static MaintenanceRecord CreateNewBlank(TreatmentBMP treatmentBMP, FieldVisit fieldVisit)
        {
            return new MaintenanceRecord(treatmentBMP, default(int), fieldVisit);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return MaintenanceRecordObservations.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(MaintenanceRecord).Name, typeof(MaintenanceRecordObservation).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            dbContext.MaintenanceRecords.Remove(this);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in MaintenanceRecordObservations.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int MaintenanceRecordID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int FieldVisitID { get; set; }
        public string MaintenanceRecordDescription { get; set; }
        public int? MaintenanceRecordTypeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return MaintenanceRecordID; } set { MaintenanceRecordID = value; } }

        public virtual ICollection<MaintenanceRecordObservation> MaintenanceRecordObservations { get; set; }
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        public virtual FieldVisit FieldVisit { get; set; }
        public MaintenanceRecordType MaintenanceRecordType { get { return MaintenanceRecordTypeID.HasValue ? MaintenanceRecordType.AllLookupDictionary[MaintenanceRecordTypeID.Value] : null; } }

        public static class FieldLengths
        {
            public const int MaintenanceRecordDescription = 500;
        }
    }
}