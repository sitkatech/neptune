//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessment]
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
    [Table("[dbo].[TreatmentBMPAssessment]")]
    public partial class TreatmentBMPAssessment : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPAssessment()
        {
            this.FieldVisitsWhereYouAreTheInitialAssessment = new HashSet<FieldVisit>();
            this.FieldVisitsWhereYouAreThePostMaintenanceAssessment = new HashSet<FieldVisit>();
            this.TreatmentBMPObservations = new HashSet<TreatmentBMPObservation>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPAssessment(int treatmentBMPAssessmentID, int treatmentBMPID, int treatmentBMPTypeID, string notes) : this()
        {
            this.TreatmentBMPAssessmentID = treatmentBMPAssessmentID;
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.Notes = notes;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPAssessment(int treatmentBMPID, int treatmentBMPTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPAssessmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPAssessment(TreatmentBMP treatmentBMP, TreatmentBMPType treatmentBMPType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPAssessmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
            treatmentBMP.TreatmentBMPAssessments.Add(this);
            this.TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID;
            this.TreatmentBMPType = treatmentBMPType;
            treatmentBMPType.TreatmentBMPAssessments.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPAssessment CreateNewBlank(TreatmentBMP treatmentBMP, TreatmentBMPType treatmentBMPType)
        {
            return new TreatmentBMPAssessment(treatmentBMP, treatmentBMPType);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return (FieldVisitWhereYouAreTheInitialAssessment != null) || (FieldVisitWhereYouAreThePostMaintenanceAssessment != null) || TreatmentBMPObservations.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPAssessment).Name, typeof(FieldVisit).Name, typeof(TreatmentBMPObservation).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {

            foreach(var x in FieldVisitsWhereYouAreTheInitialAssessment.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in FieldVisitsWhereYouAreThePostMaintenanceAssessment.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in TreatmentBMPObservations.ToList())
            {
                x.DeleteFull();
            }
            HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAssessments.Remove(this);                
        }

        [Key]
        public int TreatmentBMPAssessmentID { get; set; }
        public int TenantID { get; private set; }
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public string Notes { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPAssessmentID; } set { TreatmentBMPAssessmentID = value; } }

        protected virtual ICollection<FieldVisit> FieldVisitsWhereYouAreTheInitialAssessment { get; set; }
        [NotMapped]
        public FieldVisit FieldVisitWhereYouAreTheInitialAssessment { get { return FieldVisitsWhereYouAreTheInitialAssessment.SingleOrDefault(); } set { FieldVisitsWhereYouAreTheInitialAssessment = new List<FieldVisit>{value};} }
        protected virtual ICollection<FieldVisit> FieldVisitsWhereYouAreThePostMaintenanceAssessment { get; set; }
        [NotMapped]
        public FieldVisit FieldVisitWhereYouAreThePostMaintenanceAssessment { get { return FieldVisitsWhereYouAreThePostMaintenanceAssessment.SingleOrDefault(); } set { FieldVisitsWhereYouAreThePostMaintenanceAssessment = new List<FieldVisit>{value};} }
        public virtual ICollection<TreatmentBMPObservation> TreatmentBMPObservations { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }

        public static class FieldLengths
        {
            public const int Notes = 1000;
        }
    }
}