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
    [Table("[dbo].[FieldVisit]")]
    public partial class FieldVisit : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected FieldVisit()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public FieldVisit(int fieldVisitID, int? fieldVisitStatusID, int? initialAssessmentID, int? maintenanceRecordID, int? postMaintenanceAssessmentID, int performedByPersonID, DateTime visitDate) : this()
        {
            this.FieldVisitID = fieldVisitID;
            this.FieldVisitStatusID = fieldVisitStatusID;
            this.InitialAssessmentID = initialAssessmentID;
            this.MaintenanceRecordID = maintenanceRecordID;
            this.PostMaintenanceAssessmentID = postMaintenanceAssessmentID;
            this.PerformedByPersonID = performedByPersonID;
            this.VisitDate = visitDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public FieldVisit(int performedByPersonID, DateTime visitDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FieldVisitID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.PerformedByPersonID = performedByPersonID;
            this.VisitDate = visitDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public FieldVisit(Person performedByPerson, DateTime visitDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FieldVisitID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.PerformedByPersonID = performedByPerson.PersonID;
            this.PerformedByPerson = performedByPerson;
            performedByPerson.FieldVisitsWhereYouAreThePerformedByPerson.Add(this);
            this.VisitDate = visitDate;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static FieldVisit CreateNewBlank(Person performedByPerson)
        {
            return new FieldVisit(performedByPerson, default(DateTime));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(FieldVisit).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {
            HttpRequestStorage.DatabaseEntities.AllFieldVisits.Remove(this);                
        }

        [Key]
        public int FieldVisitID { get; set; }
        public int TenantID { get; private set; }
        public int? FieldVisitStatusID { get; set; }
        public int? InitialAssessmentID { get; set; }
        public int? MaintenanceRecordID { get; set; }
        public int? PostMaintenanceAssessmentID { get; set; }
        public int PerformedByPersonID { get; set; }
        public DateTime VisitDate { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return FieldVisitID; } set { FieldVisitID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual FieldVisitStatus FieldVisitStatus { get; set; }
        public virtual TreatmentBMPAssessment InitialAssessment { get; set; }
        public virtual TreatmentBMPAssessment PostMaintenanceAssessment { get; set; }
        public virtual MaintenanceRecord MaintenanceRecord { get; set; }
        public virtual Person PerformedByPerson { get; set; }

        public static class FieldLengths
        {

        }
    }
}