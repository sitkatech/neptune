//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPImage]
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
    [Table("[dbo].[TreatmentBMPImage]")]
    public partial class TreatmentBMPImage : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPImage()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPImage(int treatmentBMPImageID, int fileResourceID, int treatmentBMPID, string caption, DateTime uploadDate) : this()
        {
            this.TreatmentBMPImageID = treatmentBMPImageID;
            this.FileResourceID = fileResourceID;
            this.TreatmentBMPID = treatmentBMPID;
            this.Caption = caption;
            this.UploadDate = uploadDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPImage(int fileResourceID, int treatmentBMPID, string caption, DateTime uploadDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPImageID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.FileResourceID = fileResourceID;
            this.TreatmentBMPID = treatmentBMPID;
            this.Caption = caption;
            this.UploadDate = uploadDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPImage(FileResource fileResource, TreatmentBMP treatmentBMP, string caption, DateTime uploadDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPImageID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.FileResourceID = fileResource.FileResourceID;
            this.FileResource = fileResource;
            fileResource.TreatmentBMPImages.Add(this);
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
            treatmentBMP.TreatmentBMPImages.Add(this);
            this.Caption = caption;
            this.UploadDate = uploadDate;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPImage CreateNewBlank(FileResource fileResource, TreatmentBMP treatmentBMP)
        {
            return new TreatmentBMPImage(fileResource, treatmentBMP, default(string), default(DateTime));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPImage).Name};

        [Key]
        public int TreatmentBMPImageID { get; set; }
        public int TenantID { get; private set; }
        public int FileResourceID { get; set; }
        public int TreatmentBMPID { get; set; }
        public string Caption { get; set; }
        public DateTime UploadDate { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPImageID; } set { TreatmentBMPImageID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual FileResource FileResource { get; set; }
        public virtual TreatmentBMP TreatmentBMP { get; set; }

        public static class FieldLengths
        {
            public const int Caption = 500;
        }
    }
}