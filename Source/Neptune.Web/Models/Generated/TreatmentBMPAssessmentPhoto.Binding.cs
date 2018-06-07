//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessmentPhoto]
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
    [Table("[dbo].[TreatmentBMPAssessmentPhoto]")]
    public partial class TreatmentBMPAssessmentPhoto : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPAssessmentPhoto()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPAssessmentPhoto(int treatmentBMPAssessmentPhotoID, int fileResourceID, int treatmentBMPAssessmentID, string caption) : this()
        {
            this.TreatmentBMPAssessmentPhotoID = treatmentBMPAssessmentPhotoID;
            this.FileResourceID = fileResourceID;
            this.TreatmentBMPAssessmentID = treatmentBMPAssessmentID;
            this.Caption = caption;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPAssessmentPhoto(int fileResourceID, int treatmentBMPAssessmentID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPAssessmentPhotoID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.FileResourceID = fileResourceID;
            this.TreatmentBMPAssessmentID = treatmentBMPAssessmentID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPAssessmentPhoto(FileResource fileResource, TreatmentBMPAssessment treatmentBMPAssessment) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPAssessmentPhotoID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.FileResourceID = fileResource.FileResourceID;
            this.FileResource = fileResource;
            fileResource.TreatmentBMPAssessmentPhotos.Add(this);
            this.TreatmentBMPAssessmentID = treatmentBMPAssessment.TreatmentBMPAssessmentID;
            this.TreatmentBMPAssessment = treatmentBMPAssessment;
            treatmentBMPAssessment.TreatmentBMPAssessmentPhotos.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPAssessmentPhoto CreateNewBlank(FileResource fileResource, TreatmentBMPAssessment treatmentBMPAssessment)
        {
            return new TreatmentBMPAssessmentPhoto(fileResource, treatmentBMPAssessment);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPAssessmentPhoto).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {
            HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAssessmentPhotos.Remove(this);                
        }

        [Key]
        public int TreatmentBMPAssessmentPhotoID { get; set; }
        public int TenantID { get; private set; }
        public int FileResourceID { get; set; }
        public int TreatmentBMPAssessmentID { get; set; }
        public string Caption { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPAssessmentPhotoID; } set { TreatmentBMPAssessmentPhotoID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual FileResource FileResource { get; set; }
        public virtual TreatmentBMPAssessment TreatmentBMPAssessment { get; set; }

        public static class FieldLengths
        {
            public const int Caption = 1000;
        }
    }
}