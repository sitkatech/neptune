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
            this.TreatmentBMPObservations = new HashSet<TreatmentBMPObservation>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPAssessment(int treatmentBMPAssessmentID, int treatmentBMPID, int treatmentBMPTypeID, int stormwaterAssessmentTypeID, DateTime assessmentDate, int personID, double? alternateAssessmentScore, string alternateAssessmentRationale, bool isPrivate, string notes, bool isPostMaintenanceAssessment) : this()
        {
            this.TreatmentBMPAssessmentID = treatmentBMPAssessmentID;
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.StormwaterAssessmentTypeID = stormwaterAssessmentTypeID;
            this.AssessmentDate = assessmentDate;
            this.PersonID = personID;
            this.AlternateAssessmentScore = alternateAssessmentScore;
            this.AlternateAssessmentRationale = alternateAssessmentRationale;
            this.IsPrivate = isPrivate;
            this.Notes = notes;
            this.IsPostMaintenanceAssessment = isPostMaintenanceAssessment;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPAssessment(int treatmentBMPID, int treatmentBMPTypeID, int stormwaterAssessmentTypeID, DateTime assessmentDate, int personID, bool isPrivate, bool isPostMaintenanceAssessment) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPAssessmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.StormwaterAssessmentTypeID = stormwaterAssessmentTypeID;
            this.AssessmentDate = assessmentDate;
            this.PersonID = personID;
            this.IsPrivate = isPrivate;
            this.IsPostMaintenanceAssessment = isPostMaintenanceAssessment;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPAssessment(TreatmentBMP treatmentBMP, TreatmentBMPType treatmentBMPType, StormwaterAssessmentType stormwaterAssessmentType, DateTime assessmentDate, Person person, bool isPrivate, bool isPostMaintenanceAssessment) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPAssessmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
            treatmentBMP.TreatmentBMPAssessments.Add(this);
            this.TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID;
            this.TreatmentBMPType = treatmentBMPType;
            treatmentBMPType.TreatmentBMPAssessments.Add(this);
            this.StormwaterAssessmentTypeID = stormwaterAssessmentType.StormwaterAssessmentTypeID;
            this.AssessmentDate = assessmentDate;
            this.PersonID = person.PersonID;
            this.Person = person;
            person.TreatmentBMPAssessments.Add(this);
            this.IsPrivate = isPrivate;
            this.IsPostMaintenanceAssessment = isPostMaintenanceAssessment;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPAssessment CreateNewBlank(TreatmentBMP treatmentBMP, TreatmentBMPType treatmentBMPType, StormwaterAssessmentType stormwaterAssessmentType, Person person)
        {
            return new TreatmentBMPAssessment(treatmentBMP, treatmentBMPType, stormwaterAssessmentType, default(DateTime), person, default(bool), default(bool));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return TreatmentBMPObservations.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPAssessment).Name, typeof(TreatmentBMPObservation).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {

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
        public int StormwaterAssessmentTypeID { get; set; }
        public DateTime AssessmentDate { get; set; }
        public int PersonID { get; set; }
        public double? AlternateAssessmentScore { get; set; }
        public string AlternateAssessmentRationale { get; set; }
        public bool IsPrivate { get; set; }
        public string Notes { get; set; }
        public bool IsPostMaintenanceAssessment { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPAssessmentID; } set { TreatmentBMPAssessmentID = value; } }

        public virtual ICollection<TreatmentBMPObservation> TreatmentBMPObservations { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        public StormwaterAssessmentType StormwaterAssessmentType { get { return StormwaterAssessmentType.AllLookupDictionary[StormwaterAssessmentTypeID]; } }
        public virtual Person Person { get; set; }

        public static class FieldLengths
        {
            public const int AlternateAssessmentRationale = 1000;
            public const int Notes = 1000;
        }
    }
}