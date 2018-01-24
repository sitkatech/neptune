//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPDocument]
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
    [Table("[dbo].[TreatmentBMPDocument]")]
    public partial class TreatmentBMPDocument : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPDocument()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPDocument(int treatmentBMPDocumentID, int fileResourceID, int treatmentBMPID, string displayName, DateTime uploadDate, string documentDescription) : this()
        {
            this.TreatmentBMPDocumentID = treatmentBMPDocumentID;
            this.FileResourceID = fileResourceID;
            this.TreatmentBMPID = treatmentBMPID;
            this.DisplayName = displayName;
            this.UploadDate = uploadDate;
            this.DocumentDescription = documentDescription;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPDocument(int fileResourceID, int treatmentBMPID, string displayName, DateTime uploadDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPDocumentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.FileResourceID = fileResourceID;
            this.TreatmentBMPID = treatmentBMPID;
            this.DisplayName = displayName;
            this.UploadDate = uploadDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPDocument(FileResource fileResource, TreatmentBMP treatmentBMP, string displayName, DateTime uploadDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPDocumentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.FileResourceID = fileResource.FileResourceID;
            this.FileResource = fileResource;
            fileResource.TreatmentBMPDocuments.Add(this);
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
            treatmentBMP.TreatmentBMPDocuments.Add(this);
            this.DisplayName = displayName;
            this.UploadDate = uploadDate;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPDocument CreateNewBlank(FileResource fileResource, TreatmentBMP treatmentBMP)
        {
            return new TreatmentBMPDocument(fileResource, treatmentBMP, default(string), default(DateTime));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPDocument).Name};

        [Key]
        public int TreatmentBMPDocumentID { get; set; }
        public int TenantID { get; private set; }
        public int FileResourceID { get; set; }
        public int TreatmentBMPID { get; set; }
        public string DisplayName { get; set; }
        public DateTime UploadDate { get; set; }
        public string DocumentDescription { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPDocumentID; } set { TreatmentBMPDocumentID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual FileResource FileResource { get; set; }
        public virtual TreatmentBMP TreatmentBMP { get; set; }

        public static class FieldLengths
        {
            public const int DisplayName = 200;
            public const int DocumentDescription = 500;
        }
    }
}