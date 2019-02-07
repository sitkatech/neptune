//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldVisit]
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
    // Table [dbo].[FieldVisit] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[FieldVisit]")]
    public partial class FieldVisit : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected FieldVisit()
        {
            this.MaintenanceRecords = new HashSet<MaintenanceRecord>();
            this.TreatmentBMPAssessments = new HashSet<TreatmentBMPAssessment>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public FieldVisit(int fieldVisitID, int treatmentBMPID, int fieldVisitStatusID, int performedByPersonID, DateTime visitDate, bool inventoryUpdated, int fieldVisitTypeID, bool isFieldVisitVerified) : this()
        {
            this.FieldVisitID = fieldVisitID;
            this.TreatmentBMPID = treatmentBMPID;
            this.FieldVisitStatusID = fieldVisitStatusID;
            this.PerformedByPersonID = performedByPersonID;
            this.VisitDate = visitDate;
            this.InventoryUpdated = inventoryUpdated;
            this.FieldVisitTypeID = fieldVisitTypeID;
            this.IsFieldVisitVerified = isFieldVisitVerified;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public FieldVisit(int treatmentBMPID, int fieldVisitStatusID, int performedByPersonID, DateTime visitDate, bool inventoryUpdated, int fieldVisitTypeID, bool isFieldVisitVerified) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FieldVisitID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPID = treatmentBMPID;
            this.FieldVisitStatusID = fieldVisitStatusID;
            this.PerformedByPersonID = performedByPersonID;
            this.VisitDate = visitDate;
            this.InventoryUpdated = inventoryUpdated;
            this.FieldVisitTypeID = fieldVisitTypeID;
            this.IsFieldVisitVerified = isFieldVisitVerified;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public FieldVisit(TreatmentBMP treatmentBMP, FieldVisitStatus fieldVisitStatus, Person performedByPerson, DateTime visitDate, bool inventoryUpdated, FieldVisitType fieldVisitType, bool isFieldVisitVerified) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FieldVisitID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
            treatmentBMP.FieldVisits.Add(this);
            this.FieldVisitStatusID = fieldVisitStatus.FieldVisitStatusID;
            this.PerformedByPersonID = performedByPerson.PersonID;
            this.PerformedByPerson = performedByPerson;
            performedByPerson.FieldVisitsWhereYouAreThePerformedByPerson.Add(this);
            this.VisitDate = visitDate;
            this.InventoryUpdated = inventoryUpdated;
            this.FieldVisitTypeID = fieldVisitType.FieldVisitTypeID;
            this.IsFieldVisitVerified = isFieldVisitVerified;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static FieldVisit CreateNewBlank(TreatmentBMP treatmentBMP, FieldVisitStatus fieldVisitStatus, Person performedByPerson, FieldVisitType fieldVisitType)
        {
            return new FieldVisit(treatmentBMP, fieldVisitStatus, performedByPerson, default(DateTime), default(bool), fieldVisitType, default(bool));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return MaintenanceRecords.Any() || TreatmentBMPAssessments.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(FieldVisit).Name, typeof(MaintenanceRecord).Name, typeof(TreatmentBMPAssessment).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.FieldVisits.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            Delete(dbContext);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in MaintenanceRecords.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPAssessments.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int FieldVisitID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int FieldVisitStatusID { get; set; }
        public int PerformedByPersonID { get; set; }
        public DateTime VisitDate { get; set; }
        public bool InventoryUpdated { get; set; }
        public int FieldVisitTypeID { get; set; }
        public bool IsFieldVisitVerified { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return FieldVisitID; } set { FieldVisitID = value; } }

        public virtual ICollection<MaintenanceRecord> MaintenanceRecords { get; set; }
        public virtual ICollection<TreatmentBMPAssessment> TreatmentBMPAssessments { get; set; }
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        public FieldVisitStatus FieldVisitStatus { get { return FieldVisitStatus.AllLookupDictionary[FieldVisitStatusID]; } }
        public virtual Person PerformedByPerson { get; set; }
        public FieldVisitType FieldVisitType { get { return FieldVisitType.AllLookupDictionary[FieldVisitTypeID]; } }

        public static class FieldLengths
        {

        }
    }
}