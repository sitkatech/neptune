//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentObservationPhoto]
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
    // Table [dbo].[OnlandVisualTrashAssessmentObservationPhoto] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[OnlandVisualTrashAssessmentObservationPhoto]")]
    public partial class OnlandVisualTrashAssessmentObservationPhoto : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected OnlandVisualTrashAssessmentObservationPhoto()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public OnlandVisualTrashAssessmentObservationPhoto(int onlandVisualTrashAssessmentObservationPhotoID, int fileResourceID, int onlandVisualTrashAssessmentObservationID) : this()
        {
            this.OnlandVisualTrashAssessmentObservationPhotoID = onlandVisualTrashAssessmentObservationPhotoID;
            this.FileResourceID = fileResourceID;
            this.OnlandVisualTrashAssessmentObservationID = onlandVisualTrashAssessmentObservationID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public OnlandVisualTrashAssessmentObservationPhoto(int fileResourceID, int onlandVisualTrashAssessmentObservationID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OnlandVisualTrashAssessmentObservationPhotoID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.FileResourceID = fileResourceID;
            this.OnlandVisualTrashAssessmentObservationID = onlandVisualTrashAssessmentObservationID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public OnlandVisualTrashAssessmentObservationPhoto(FileResource fileResource, OnlandVisualTrashAssessmentObservation onlandVisualTrashAssessmentObservation) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OnlandVisualTrashAssessmentObservationPhotoID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.FileResourceID = fileResource.FileResourceID;
            this.FileResource = fileResource;
            fileResource.OnlandVisualTrashAssessmentObservationPhotos.Add(this);
            this.OnlandVisualTrashAssessmentObservationID = onlandVisualTrashAssessmentObservation.OnlandVisualTrashAssessmentObservationID;
            this.OnlandVisualTrashAssessmentObservation = onlandVisualTrashAssessmentObservation;
            onlandVisualTrashAssessmentObservation.OnlandVisualTrashAssessmentObservationPhotos.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static OnlandVisualTrashAssessmentObservationPhoto CreateNewBlank(FileResource fileResource, OnlandVisualTrashAssessmentObservation onlandVisualTrashAssessmentObservation)
        {
            return new OnlandVisualTrashAssessmentObservationPhoto(fileResource, onlandVisualTrashAssessmentObservation);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(OnlandVisualTrashAssessmentObservationPhoto).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.OnlandVisualTrashAssessmentObservationPhotos.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int OnlandVisualTrashAssessmentObservationPhotoID { get; set; }
        public int FileResourceID { get; set; }
        public int OnlandVisualTrashAssessmentObservationID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return OnlandVisualTrashAssessmentObservationPhotoID; } set { OnlandVisualTrashAssessmentObservationPhotoID = value; } }

        public virtual FileResource FileResource { get; set; }
        public virtual OnlandVisualTrashAssessmentObservation OnlandVisualTrashAssessmentObservation { get; set; }

        public static class FieldLengths
        {

        }
    }
}