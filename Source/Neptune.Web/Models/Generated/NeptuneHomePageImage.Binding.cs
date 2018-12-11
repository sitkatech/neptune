//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptuneHomePageImage]
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
    [Table("[dbo].[NeptuneHomePageImage]")]
    public partial class NeptuneHomePageImage : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected NeptuneHomePageImage()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public NeptuneHomePageImage(int neptuneHomePageImageID, int fileResourceID, string caption, int sortOrder) : this()
        {
            this.NeptuneHomePageImageID = neptuneHomePageImageID;
            this.FileResourceID = fileResourceID;
            this.Caption = caption;
            this.SortOrder = sortOrder;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public NeptuneHomePageImage(int fileResourceID, string caption, int sortOrder) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.NeptuneHomePageImageID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.FileResourceID = fileResourceID;
            this.Caption = caption;
            this.SortOrder = sortOrder;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public NeptuneHomePageImage(FileResource fileResource, string caption, int sortOrder) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.NeptuneHomePageImageID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.FileResourceID = fileResource.FileResourceID;
            this.FileResource = fileResource;
            fileResource.NeptuneHomePageImages.Add(this);
            this.Caption = caption;
            this.SortOrder = sortOrder;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static NeptuneHomePageImage CreateNewBlank(FileResource fileResource)
        {
            return new NeptuneHomePageImage(fileResource, default(string), default(int));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(NeptuneHomePageImage).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(HttpRequestStorage.DatabaseEntities);
            dbContext.AllNeptuneHomePageImages.Remove(this);
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

        }

        [Key]
        public int NeptuneHomePageImageID { get; set; }
        public int TenantID { get; private set; }
        public int FileResourceID { get; set; }
        public string Caption { get; set; }
        public int SortOrder { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return NeptuneHomePageImageID; } set { NeptuneHomePageImageID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual FileResource FileResource { get; set; }

        public static class FieldLengths
        {
            public const int Caption = 300;
        }
    }
}