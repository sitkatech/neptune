//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePageImage]
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
    [Table("[dbo].[NeptunePageImage]")]
    public partial class NeptunePageImage : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected NeptunePageImage()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public NeptunePageImage(int neptunePageImageID, int neptunePageID, int fileResourceID) : this()
        {
            this.NeptunePageImageID = neptunePageImageID;
            this.NeptunePageID = neptunePageID;
            this.FileResourceID = fileResourceID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public NeptunePageImage(int neptunePageID, int fileResourceID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.NeptunePageImageID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.NeptunePageID = neptunePageID;
            this.FileResourceID = fileResourceID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public NeptunePageImage(NeptunePage neptunePage, FileResource fileResource) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.NeptunePageImageID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.NeptunePageID = neptunePage.NeptunePageID;
            this.NeptunePage = neptunePage;
            neptunePage.NeptunePageImages.Add(this);
            this.FileResourceID = fileResource.FileResourceID;
            this.FileResource = fileResource;
            fileResource.NeptunePageImages.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static NeptunePageImage CreateNewBlank(NeptunePage neptunePage, FileResource fileResource)
        {
            return new NeptunePageImage(neptunePage, fileResource);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(NeptunePageImage).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            dbContext.NeptunePageImages.Remove(this);
        }

        [Key]
        public int NeptunePageImageID { get; set; }
        public int NeptunePageID { get; set; }
        public int FileResourceID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return NeptunePageImageID; } set { NeptunePageImageID = value; } }

        public virtual NeptunePage NeptunePage { get; set; }
        public virtual FileResource FileResource { get; set; }

        public static class FieldLengths
        {

        }
    }
}