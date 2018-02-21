//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceActivity]
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
    [Table("[dbo].[MaintenanceActivity]")]
    public partial class MaintenanceActivity : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected MaintenanceActivity()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public MaintenanceActivity(int maintenanceActivityID, int treatmentBMPID, DateTime maintenanceActivityDate, int performedByPersonID, string maintenanceActivityDescription, int maintenanceActivityTypeID) : this()
        {
            this.MaintenanceActivityID = maintenanceActivityID;
            this.TreatmentBMPID = treatmentBMPID;
            this.MaintenanceActivityDate = maintenanceActivityDate;
            this.PerformedByPersonID = performedByPersonID;
            this.MaintenanceActivityDescription = maintenanceActivityDescription;
            this.MaintenanceActivityTypeID = maintenanceActivityTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public MaintenanceActivity(int treatmentBMPID, DateTime maintenanceActivityDate, int performedByPersonID, int maintenanceActivityTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.MaintenanceActivityID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPID = treatmentBMPID;
            this.MaintenanceActivityDate = maintenanceActivityDate;
            this.PerformedByPersonID = performedByPersonID;
            this.MaintenanceActivityTypeID = maintenanceActivityTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public MaintenanceActivity(TreatmentBMP treatmentBMP, DateTime maintenanceActivityDate, Person performedByPerson, MaintenanceActivityType maintenanceActivityType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.MaintenanceActivityID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
            treatmentBMP.MaintenanceActivities.Add(this);
            this.MaintenanceActivityDate = maintenanceActivityDate;
            this.PerformedByPersonID = performedByPerson.PersonID;
            this.PerformedByPerson = performedByPerson;
            performedByPerson.MaintenanceActivitiesWhereYouAreThePerformedByPerson.Add(this);
            this.MaintenanceActivityTypeID = maintenanceActivityType.MaintenanceActivityTypeID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static MaintenanceActivity CreateNewBlank(TreatmentBMP treatmentBMP, Person performedByPerson, MaintenanceActivityType maintenanceActivityType)
        {
            return new MaintenanceActivity(treatmentBMP, default(DateTime), performedByPerson, maintenanceActivityType);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(MaintenanceActivity).Name};

        [Key]
        public int MaintenanceActivityID { get; set; }
        public int TenantID { get; private set; }
        public int TreatmentBMPID { get; set; }
        public DateTime MaintenanceActivityDate { get; set; }
        public int PerformedByPersonID { get; set; }
        public string MaintenanceActivityDescription { get; set; }
        public int MaintenanceActivityTypeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return MaintenanceActivityID; } set { MaintenanceActivityID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        public virtual Person PerformedByPerson { get; set; }
        public MaintenanceActivityType MaintenanceActivityType { get { return MaintenanceActivityType.AllLookupDictionary[MaintenanceActivityTypeID]; } }

        public static class FieldLengths
        {
            public const int MaintenanceActivityDescription = 500;
        }
    }
}