//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FileResource]
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
    // Table [dbo].[FileResource] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[FileResource]")]
    public partial class FileResource : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected FileResource()
        {
            this.FieldDefinitionDataImages = new HashSet<FieldDefinitionDataImage>();
            this.NeptuneHomePageImages = new HashSet<NeptuneHomePageImage>();
            this.NeptunePageImages = new HashSet<NeptunePageImage>();
            this.OnlandVisualTrashAssessmentObservationPhotos = new HashSet<OnlandVisualTrashAssessmentObservationPhoto>();
            this.OnlandVisualTrashAssessmentObservationPhotoStagings = new HashSet<OnlandVisualTrashAssessmentObservationPhotoStaging>();
            this.OrganizationsWhereYouAreTheLogoFileResource = new HashSet<Organization>();
            this.SystemAttributesWhereYouAreTheTenantBannerLogoFileResource = new HashSet<SystemAttribute>();
            this.SystemAttributesWhereYouAreTheTenantSquareLogoFileResource = new HashSet<SystemAttribute>();
            this.SystemAttributesWhereYouAreTheTenantStyleSheetFileResource = new HashSet<SystemAttribute>();
            this.TreatmentBMPAssessmentPhotos = new HashSet<TreatmentBMPAssessmentPhoto>();
            this.TreatmentBMPDocuments = new HashSet<TreatmentBMPDocument>();
            this.TreatmentBMPImages = new HashSet<TreatmentBMPImage>();
            this.WaterQualityManagementPlanDocuments = new HashSet<WaterQualityManagementPlanDocument>();
            this.WaterQualityManagementPlanPhotos = new HashSet<WaterQualityManagementPlanPhoto>();
            this.WaterQualityManagementPlanVerifies = new HashSet<WaterQualityManagementPlanVerify>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public FileResource(int fileResourceID, int fileResourceMimeTypeID, string originalBaseFilename, string originalFileExtension, Guid fileResourceGUID, byte[] fileResourceData, int createPersonID, DateTime createDate) : this()
        {
            this.FileResourceID = fileResourceID;
            this.FileResourceMimeTypeID = fileResourceMimeTypeID;
            this.OriginalBaseFilename = originalBaseFilename;
            this.OriginalFileExtension = originalFileExtension;
            this.FileResourceGUID = fileResourceGUID;
            this.FileResourceData = fileResourceData;
            this.CreatePersonID = createPersonID;
            this.CreateDate = createDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public FileResource(int fileResourceMimeTypeID, string originalBaseFilename, string originalFileExtension, Guid fileResourceGUID, byte[] fileResourceData, int createPersonID, DateTime createDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FileResourceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.FileResourceMimeTypeID = fileResourceMimeTypeID;
            this.OriginalBaseFilename = originalBaseFilename;
            this.OriginalFileExtension = originalFileExtension;
            this.FileResourceGUID = fileResourceGUID;
            this.FileResourceData = fileResourceData;
            this.CreatePersonID = createPersonID;
            this.CreateDate = createDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public FileResource(FileResourceMimeType fileResourceMimeType, string originalBaseFilename, string originalFileExtension, Guid fileResourceGUID, byte[] fileResourceData, Person createPerson, DateTime createDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FileResourceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.FileResourceMimeTypeID = fileResourceMimeType.FileResourceMimeTypeID;
            this.OriginalBaseFilename = originalBaseFilename;
            this.OriginalFileExtension = originalFileExtension;
            this.FileResourceGUID = fileResourceGUID;
            this.FileResourceData = fileResourceData;
            this.CreatePersonID = createPerson.PersonID;
            this.CreatePerson = createPerson;
            createPerson.FileResourcesWhereYouAreTheCreatePerson.Add(this);
            this.CreateDate = createDate;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static FileResource CreateNewBlank(FileResourceMimeType fileResourceMimeType, Person createPerson)
        {
            return new FileResource(fileResourceMimeType, default(string), default(string), default(Guid), default(byte[]), createPerson, default(DateTime));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return FieldDefinitionDataImages.Any() || NeptuneHomePageImages.Any() || NeptunePageImages.Any() || OnlandVisualTrashAssessmentObservationPhotos.Any() || OnlandVisualTrashAssessmentObservationPhotoStagings.Any() || OrganizationsWhereYouAreTheLogoFileResource.Any() || SystemAttributesWhereYouAreTheTenantBannerLogoFileResource.Any() || SystemAttributesWhereYouAreTheTenantSquareLogoFileResource.Any() || SystemAttributesWhereYouAreTheTenantStyleSheetFileResource.Any() || TreatmentBMPAssessmentPhotos.Any() || TreatmentBMPDocuments.Any() || TreatmentBMPImages.Any() || WaterQualityManagementPlanDocuments.Any() || WaterQualityManagementPlanPhotos.Any() || WaterQualityManagementPlanVerifies.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(FileResource).Name, typeof(FieldDefinitionDataImage).Name, typeof(NeptuneHomePageImage).Name, typeof(NeptunePageImage).Name, typeof(OnlandVisualTrashAssessmentObservationPhoto).Name, typeof(OnlandVisualTrashAssessmentObservationPhotoStaging).Name, typeof(Organization).Name, typeof(SystemAttribute).Name, typeof(TreatmentBMPAssessmentPhoto).Name, typeof(TreatmentBMPDocument).Name, typeof(TreatmentBMPImage).Name, typeof(WaterQualityManagementPlanDocument).Name, typeof(WaterQualityManagementPlanPhoto).Name, typeof(WaterQualityManagementPlanVerify).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            dbContext.FileResources.Remove(this);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in FieldDefinitionDataImages.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in NeptuneHomePageImages.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in NeptunePageImages.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in OnlandVisualTrashAssessmentObservationPhotos.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in OnlandVisualTrashAssessmentObservationPhotoStagings.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in OrganizationsWhereYouAreTheLogoFileResource.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in SystemAttributesWhereYouAreTheTenantBannerLogoFileResource.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in SystemAttributesWhereYouAreTheTenantSquareLogoFileResource.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in SystemAttributesWhereYouAreTheTenantStyleSheetFileResource.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPAssessmentPhotos.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPDocuments.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPImages.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in WaterQualityManagementPlanDocuments.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in WaterQualityManagementPlanPhotos.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in WaterQualityManagementPlanVerifies.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int FileResourceID { get; set; }
        public int FileResourceMimeTypeID { get; set; }
        public string OriginalBaseFilename { get; set; }
        public string OriginalFileExtension { get; set; }
        public Guid FileResourceGUID { get; set; }
        public byte[] FileResourceData { get; set; }
        public int CreatePersonID { get; set; }
        public DateTime CreateDate { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return FileResourceID; } set { FileResourceID = value; } }

        public virtual ICollection<FieldDefinitionDataImage> FieldDefinitionDataImages { get; set; }
        public virtual ICollection<NeptuneHomePageImage> NeptuneHomePageImages { get; set; }
        public virtual ICollection<NeptunePageImage> NeptunePageImages { get; set; }
        public virtual ICollection<OnlandVisualTrashAssessmentObservationPhoto> OnlandVisualTrashAssessmentObservationPhotos { get; set; }
        public virtual ICollection<OnlandVisualTrashAssessmentObservationPhotoStaging> OnlandVisualTrashAssessmentObservationPhotoStagings { get; set; }
        public virtual ICollection<Organization> OrganizationsWhereYouAreTheLogoFileResource { get; set; }
        public virtual ICollection<SystemAttribute> SystemAttributesWhereYouAreTheTenantBannerLogoFileResource { get; set; }
        public virtual ICollection<SystemAttribute> SystemAttributesWhereYouAreTheTenantSquareLogoFileResource { get; set; }
        public virtual ICollection<SystemAttribute> SystemAttributesWhereYouAreTheTenantStyleSheetFileResource { get; set; }
        public virtual ICollection<TreatmentBMPAssessmentPhoto> TreatmentBMPAssessmentPhotos { get; set; }
        public virtual ICollection<TreatmentBMPDocument> TreatmentBMPDocuments { get; set; }
        public virtual ICollection<TreatmentBMPImage> TreatmentBMPImages { get; set; }
        public virtual ICollection<WaterQualityManagementPlanDocument> WaterQualityManagementPlanDocuments { get; set; }
        public virtual ICollection<WaterQualityManagementPlanPhoto> WaterQualityManagementPlanPhotos { get; set; }
        public virtual ICollection<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifies { get; set; }
        public FileResourceMimeType FileResourceMimeType { get { return FileResourceMimeType.AllLookupDictionary[FileResourceMimeTypeID]; } }
        public virtual Person CreatePerson { get; set; }

        public static class FieldLengths
        {
            public const int OriginalBaseFilename = 255;
            public const int OriginalFileExtension = 255;
        }
    }
}