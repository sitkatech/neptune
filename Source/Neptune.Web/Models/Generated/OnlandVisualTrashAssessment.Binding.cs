//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessment]
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
    [Table("[dbo].[OnlandVisualTrashAssessment]")]
    public partial class OnlandVisualTrashAssessment : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected OnlandVisualTrashAssessment()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public OnlandVisualTrashAssessment(int onlandVisualTrashAssessmentID, int createdByPersonID, DateTime createdDate) : this()
        {
            this.OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentID;
            this.CreatedByPersonID = createdByPersonID;
            this.CreatedDate = createdDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public OnlandVisualTrashAssessment(int createdByPersonID, DateTime createdDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OnlandVisualTrashAssessmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.CreatedByPersonID = createdByPersonID;
            this.CreatedDate = createdDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public OnlandVisualTrashAssessment(Person createdByPerson, DateTime createdDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OnlandVisualTrashAssessmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.CreatedByPersonID = createdByPerson.PersonID;
            this.CreatedByPerson = createdByPerson;
            createdByPerson.OnlandVisualTrashAssessmentsWhereYouAreTheCreatedByPerson.Add(this);
            this.CreatedDate = createdDate;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static OnlandVisualTrashAssessment CreateNewBlank(Person createdByPerson)
        {
            return new OnlandVisualTrashAssessment(createdByPerson, default(DateTime));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(OnlandVisualTrashAssessment).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            dbContext.OnlandVisualTrashAssessments.Remove(this);
        }

        [Key]
        public int OnlandVisualTrashAssessmentID { get; set; }
        public int CreatedByPersonID { get; set; }
        public DateTime CreatedDate { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return OnlandVisualTrashAssessmentID; } set { OnlandVisualTrashAssessmentID = value; } }

        public virtual Person CreatedByPerson { get; set; }

        public static class FieldLengths
        {

        }
    }
}