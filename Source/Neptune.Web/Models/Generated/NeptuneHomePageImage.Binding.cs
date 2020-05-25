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
    // Table [dbo].[NeptuneHomePageImage] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[NeptuneHomePageImage]")]
    public partial class NeptuneHomePageImage : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected NeptuneHomePageImage()
        {

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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(NeptuneHomePageImage).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.NeptuneHomePageImages.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int NeptuneHomePageImageID { get; set; }
        public int FileResourceID { get; set; }
        public string Caption { get; set; }
        public int SortOrder { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return NeptuneHomePageImageID; } set { NeptuneHomePageImageID = value; } }

        public virtual FileResource FileResource { get; set; }

        public static class FieldLengths
        {
            public const int Caption = 300;
        }
    }
}