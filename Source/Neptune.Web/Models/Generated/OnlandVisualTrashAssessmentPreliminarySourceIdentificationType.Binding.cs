//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentPreliminarySourceIdentificationType]
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
    // Table [dbo].[OnlandVisualTrashAssessmentPreliminarySourceIdentificationType] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[OnlandVisualTrashAssessmentPreliminarySourceIdentificationType]")]
    public partial class OnlandVisualTrashAssessmentPreliminarySourceIdentificationType : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected OnlandVisualTrashAssessmentPreliminarySourceIdentificationType()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public OnlandVisualTrashAssessmentPreliminarySourceIdentificationType(int onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID, int onlandVisualTrashAssessmentID, int preliminarySourceIdentificationTypeID, string explanationIfTypeIsOther) : this()
        {
            this.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID = onlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID;
            this.OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentID;
            this.PreliminarySourceIdentificationTypeID = preliminarySourceIdentificationTypeID;
            this.ExplanationIfTypeIsOther = explanationIfTypeIsOther;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public OnlandVisualTrashAssessmentPreliminarySourceIdentificationType(int onlandVisualTrashAssessmentID, int preliminarySourceIdentificationTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentID;
            this.PreliminarySourceIdentificationTypeID = preliminarySourceIdentificationTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public OnlandVisualTrashAssessmentPreliminarySourceIdentificationType(OnlandVisualTrashAssessment onlandVisualTrashAssessment, PreliminarySourceIdentificationType preliminarySourceIdentificationType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.OnlandVisualTrashAssessmentID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID;
            this.OnlandVisualTrashAssessment = onlandVisualTrashAssessment;
            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.Add(this);
            this.PreliminarySourceIdentificationTypeID = preliminarySourceIdentificationType.PreliminarySourceIdentificationTypeID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static OnlandVisualTrashAssessmentPreliminarySourceIdentificationType CreateNewBlank(OnlandVisualTrashAssessment onlandVisualTrashAssessment, PreliminarySourceIdentificationType preliminarySourceIdentificationType)
        {
            return new OnlandVisualTrashAssessmentPreliminarySourceIdentificationType(onlandVisualTrashAssessment, preliminarySourceIdentificationType);
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
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(OnlandVisualTrashAssessmentPreliminarySourceIdentificationType).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID { get; set; }
        public int OnlandVisualTrashAssessmentID { get; set; }
        public int PreliminarySourceIdentificationTypeID { get; set; }
        public string ExplanationIfTypeIsOther { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID; } set { OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID = value; } }

        public virtual OnlandVisualTrashAssessment OnlandVisualTrashAssessment { get; set; }
        public PreliminarySourceIdentificationType PreliminarySourceIdentificationType { get { return PreliminarySourceIdentificationType.AllLookupDictionary[PreliminarySourceIdentificationTypeID]; } }

        public static class FieldLengths
        {
            public const int ExplanationIfTypeIsOther = 500;
        }
    }
}