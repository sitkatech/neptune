//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashGeneratingUnitAdjustment]
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
    // Table [dbo].[TrashGeneratingUnitAdjustment] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[TrashGeneratingUnitAdjustment]")]
    public partial class TrashGeneratingUnitAdjustment : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TrashGeneratingUnitAdjustment()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TrashGeneratingUnitAdjustment(int trashGeneratingUnitAdjustmentID, int? adjustedDelineationID, int? adjustedOnlandVisualTrashAssessmentAreaID, DbGeometry deletedGeometry, DateTime adjustmentDate, int adjustedByPersonID, bool isProcessed, DateTime? processedDate) : this()
        {
            this.TrashGeneratingUnitAdjustmentID = trashGeneratingUnitAdjustmentID;
            this.AdjustedDelineationID = adjustedDelineationID;
            this.AdjustedOnlandVisualTrashAssessmentAreaID = adjustedOnlandVisualTrashAssessmentAreaID;
            this.DeletedGeometry = deletedGeometry;
            this.AdjustmentDate = adjustmentDate;
            this.AdjustedByPersonID = adjustedByPersonID;
            this.IsProcessed = isProcessed;
            this.ProcessedDate = processedDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TrashGeneratingUnitAdjustment(DateTime adjustmentDate, int adjustedByPersonID, bool isProcessed) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TrashGeneratingUnitAdjustmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.AdjustmentDate = adjustmentDate;
            this.AdjustedByPersonID = adjustedByPersonID;
            this.IsProcessed = isProcessed;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TrashGeneratingUnitAdjustment(DateTime adjustmentDate, Person adjustedByPerson, bool isProcessed) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TrashGeneratingUnitAdjustmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.AdjustmentDate = adjustmentDate;
            this.AdjustedByPersonID = adjustedByPerson.PersonID;
            this.AdjustedByPerson = adjustedByPerson;
            adjustedByPerson.TrashGeneratingUnitAdjustmentsWhereYouAreTheAdjustedByPerson.Add(this);
            this.IsProcessed = isProcessed;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TrashGeneratingUnitAdjustment CreateNewBlank(Person adjustedByPerson)
        {
            return new TrashGeneratingUnitAdjustment(default(DateTime), adjustedByPerson, default(bool));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TrashGeneratingUnitAdjustment).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.TrashGeneratingUnitAdjustments.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int TrashGeneratingUnitAdjustmentID { get; set; }
        public int? AdjustedDelineationID { get; set; }
        public int? AdjustedOnlandVisualTrashAssessmentAreaID { get; set; }
        public DbGeometry DeletedGeometry { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public int AdjustedByPersonID { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime? ProcessedDate { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TrashGeneratingUnitAdjustmentID; } set { TrashGeneratingUnitAdjustmentID = value; } }

        public virtual Delineation AdjustedDelineation { get; set; }
        public virtual OnlandVisualTrashAssessmentArea AdjustedOnlandVisualTrashAssessmentArea { get; set; }
        public virtual Person AdjustedByPerson { get; set; }

        public static class FieldLengths
        {

        }
    }
}