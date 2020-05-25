//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMPAttribute]
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
    // Table [dbo].[SourceControlBMPAttribute] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[SourceControlBMPAttribute]")]
    public partial class SourceControlBMPAttribute : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected SourceControlBMPAttribute()
        {
            this.SourceControlBMPs = new HashSet<SourceControlBMP>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public SourceControlBMPAttribute(int sourceControlBMPAttributeID, int sourceControlBMPAttributeCategoryID, string sourceControlBMPAttributeName) : this()
        {
            this.SourceControlBMPAttributeID = sourceControlBMPAttributeID;
            this.SourceControlBMPAttributeCategoryID = sourceControlBMPAttributeCategoryID;
            this.SourceControlBMPAttributeName = sourceControlBMPAttributeName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public SourceControlBMPAttribute(int sourceControlBMPAttributeCategoryID, string sourceControlBMPAttributeName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.SourceControlBMPAttributeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.SourceControlBMPAttributeCategoryID = sourceControlBMPAttributeCategoryID;
            this.SourceControlBMPAttributeName = sourceControlBMPAttributeName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public SourceControlBMPAttribute(SourceControlBMPAttributeCategory sourceControlBMPAttributeCategory, string sourceControlBMPAttributeName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.SourceControlBMPAttributeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.SourceControlBMPAttributeCategoryID = sourceControlBMPAttributeCategory.SourceControlBMPAttributeCategoryID;
            this.SourceControlBMPAttributeCategory = sourceControlBMPAttributeCategory;
            sourceControlBMPAttributeCategory.SourceControlBMPAttributes.Add(this);
            this.SourceControlBMPAttributeName = sourceControlBMPAttributeName;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static SourceControlBMPAttribute CreateNewBlank(SourceControlBMPAttributeCategory sourceControlBMPAttributeCategory)
        {
            return new SourceControlBMPAttribute(sourceControlBMPAttributeCategory, default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return SourceControlBMPs.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(SourceControlBMPs.Any())
            {
                dependentObjects.Add(typeof(SourceControlBMP).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(SourceControlBMPAttribute).Name, typeof(SourceControlBMP).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.SourceControlBMPAttributes.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            Delete(dbContext);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in SourceControlBMPs.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int SourceControlBMPAttributeID { get; set; }
        public int SourceControlBMPAttributeCategoryID { get; set; }
        public string SourceControlBMPAttributeName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return SourceControlBMPAttributeID; } set { SourceControlBMPAttributeID = value; } }

        public virtual ICollection<SourceControlBMP> SourceControlBMPs { get; set; }
        public virtual SourceControlBMPAttributeCategory SourceControlBMPAttributeCategory { get; set; }

        public static class FieldLengths
        {
            public const int SourceControlBMPAttributeName = 100;
        }
    }
}