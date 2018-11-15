//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldDefinitionDataImage]
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
    [Table("[dbo].[FieldDefinitionDataImage]")]
    public partial class FieldDefinitionDataImage : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected FieldDefinitionDataImage()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public FieldDefinitionDataImage(int fieldDefinitionDataImageID, int fieldDefinitionDataID, int fileResourceID) : this()
        {
            this.FieldDefinitionDataImageID = fieldDefinitionDataImageID;
            this.FieldDefinitionDataID = fieldDefinitionDataID;
            this.FileResourceID = fileResourceID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public FieldDefinitionDataImage(int fieldDefinitionDataID, int fileResourceID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FieldDefinitionDataImageID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.FieldDefinitionDataID = fieldDefinitionDataID;
            this.FileResourceID = fileResourceID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public FieldDefinitionDataImage(FieldDefinitionData fieldDefinitionData, FileResource fileResource) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FieldDefinitionDataImageID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.FieldDefinitionDataID = fieldDefinitionData.FieldDefinitionDataID;
            this.FieldDefinitionData = fieldDefinitionData;
            fieldDefinitionData.FieldDefinitionDataImages.Add(this);
            this.FileResourceID = fileResource.FileResourceID;
            this.FileResource = fileResource;
            fileResource.FieldDefinitionDataImages.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static FieldDefinitionDataImage CreateNewBlank(FieldDefinitionData fieldDefinitionData, FileResource fileResource)
        {
            return new FieldDefinitionDataImage(fieldDefinitionData, fileResource);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(FieldDefinitionDataImage).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {
            DeleteFull(HttpRequestStorage.DatabaseEntities);
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            dbContext.AllFieldDefinitionDataImages.Remove(this);
        }

        [Key]
        public int FieldDefinitionDataImageID { get; set; }
        public int TenantID { get; private set; }
        public int FieldDefinitionDataID { get; set; }
        public int FileResourceID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return FieldDefinitionDataImageID; } set { FieldDefinitionDataImageID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual FieldDefinitionData FieldDefinitionData { get; set; }
        public virtual FileResource FileResource { get; set; }

        public static class FieldLengths
        {

        }
    }
}