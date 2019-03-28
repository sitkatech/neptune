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
    // Table [dbo].[TreatmentBMPAssessment] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[TreatmentBMPAssessment]")]
    public partial class TreatmentBMPAssessment : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPAssessment()
        {
            this.TreatmentBMPAssessmentPhotos = new HashSet<TreatmentBMPAssessmentPhoto>();
            this.TreatmentBMPObservations = new HashSet<TreatmentBMPObservation>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPAssessment(int treatmentBMPAssessmentID, int treatmentBMPID, int treatmentBMPTypeID, int fieldVisitID, int treatmentBMPAssessmentTypeID, string notes, double? assessmentScore, bool? isAssessmentComplete) : this()
        {
            this.TreatmentBMPAssessmentID = treatmentBMPAssessmentID;
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.FieldVisitID = fieldVisitID;
            this.TreatmentBMPAssessmentTypeID = treatmentBMPAssessmentTypeID;
            this.Notes = notes;
            this.AssessmentScore = assessmentScore;
            this.IsAssessmentComplete = isAssessmentComplete;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPAssessment(int treatmentBMPID, int treatmentBMPTypeID, int fieldVisitID, int treatmentBMPAssessmentTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPAssessmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.FieldVisitID = fieldVisitID;
            this.TreatmentBMPAssessmentTypeID = treatmentBMPAssessmentTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPAssessment(TreatmentBMP treatmentBMP, TreatmentBMPType treatmentBMPType, FieldVisit fieldVisit, TreatmentBMPAssessmentType treatmentBMPAssessmentType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPAssessmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
            treatmentBMP.TreatmentBMPAssessments.Add(this);
            this.TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID;
            this.TreatmentBMPType = treatmentBMPType;
            treatmentBMPType.TreatmentBMPAssessments.Add(this);
            this.FieldVisitID = fieldVisit.FieldVisitID;
            this.FieldVisit = fieldVisit;
            fieldVisit.TreatmentBMPAssessments.Add(this);
            this.TreatmentBMPAssessmentTypeID = treatmentBMPAssessmentType.TreatmentBMPAssessmentTypeID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPAssessment CreateNewBlank(TreatmentBMP treatmentBMP, TreatmentBMPType treatmentBMPType, FieldVisit fieldVisit, TreatmentBMPAssessmentType treatmentBMPAssessmentType)
        {
            return new TreatmentBMPAssessment(treatmentBMP, treatmentBMPType, fieldVisit, treatmentBMPAssessmentType);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return TreatmentBMPAssessmentPhotos.Any() || TreatmentBMPObservations.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPAssessment).Name, typeof(TreatmentBMPAssessmentPhoto).Name, typeof(TreatmentBMPObservation).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.TreatmentBMPAssessments.Remove(this);
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

            foreach(var x in TreatmentBMPAssessmentPhotos.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPObservations.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int TreatmentBMPAssessmentID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int FieldVisitID { get; set; }
        public int TreatmentBMPAssessmentTypeID { get; set; }
        public string Notes { get; set; }
        public double? AssessmentScore { get; set; }
        public bool? IsAssessmentComplete { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPAssessmentID; } set { TreatmentBMPAssessmentID = value; } }

        public virtual ICollection<TreatmentBMPAssessmentPhoto> TreatmentBMPAssessmentPhotos { get; set; }
        public virtual ICollection<TreatmentBMPObservation> TreatmentBMPObservations { get; set; }
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        public virtual FieldVisit FieldVisit { get; set; }
        public TreatmentBMPAssessmentType TreatmentBMPAssessmentType { get { return TreatmentBMPAssessmentType.AllLookupDictionary[TreatmentBMPAssessmentTypeID]; } }

        public static class FieldLengths
        {
            public const int Notes = 1000;
        }
    }
}