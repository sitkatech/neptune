//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentObservationPhotoStaging]
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
    // Table [dbo].[OnlandVisualTrashAssessmentObservationPhotoStaging] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[OnlandVisualTrashAssessmentObservationPhotoStaging]")]
    public partial class OnlandVisualTrashAssessmentObservationPhotoStaging : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected OnlandVisualTrashAssessmentObservationPhotoStaging()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public OnlandVisualTrashAssessmentObservationPhotoStaging(int onlandVisualTrashAssessmentObservationPhotoStagingID, int fileResourceID, int onlandVisualTrashAssessmentID) : this()
        {
            this.OnlandVisualTrashAssessmentObservationPhotoStagingID = onlandVisualTrashAssessmentObservationPhotoStagingID;
            this.FileResourceID = fileResourceID;
            this.OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public OnlandVisualTrashAssessmentObservationPhotoStaging(int fileResourceID, int onlandVisualTrashAssessmentID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OnlandVisualTrashAssessmentObservationPhotoStagingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.FileResourceID = fileResourceID;
            this.OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public OnlandVisualTrashAssessmentObservationPhotoStaging(FileResource fileResource, OnlandVisualTrashAssessment onlandVisualTrashAssessment) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OnlandVisualTrashAssessmentObservationPhotoStagingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.FileResourceID = fileResource.FileResourceID;
            this.FileResource = fileResource;
            fileResource.OnlandVisualTrashAssessmentObservationPhotoStagings.Add(this);
            this.OnlandVisualTrashAssessmentID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID;
            this.OnlandVisualTrashAssessment = onlandVisualTrashAssessment;
            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservationPhotoStagings.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static OnlandVisualTrashAssessmentObservationPhotoStaging CreateNewBlank(FileResource fileResource, OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
            return new OnlandVisualTrashAssessmentObservationPhotoStaging(fileResource, onlandVisualTrashAssessment);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(OnlandVisualTrashAssessmentObservationPhotoStaging).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            dbContext.OnlandVisualTrashAssessmentObservationPhotoStagings.Remove(this);
        }

        [Key]
        public int OnlandVisualTrashAssessmentObservationPhotoStagingID { get; set; }
        public int FileResourceID { get; set; }
        public int OnlandVisualTrashAssessmentID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return OnlandVisualTrashAssessmentObservationPhotoStagingID; } set { OnlandVisualTrashAssessmentObservationPhotoStagingID = value; } }

        public virtual FileResource FileResource { get; set; }
        public virtual OnlandVisualTrashAssessment OnlandVisualTrashAssessment { get; set; }

        public static class FieldLengths
        {

        }
    }
}