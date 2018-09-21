//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMPAttributeCategory]
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
    [Table("[dbo].[SourceControlBMPAttributeCategory]")]
    public partial class SourceControlBMPAttributeCategory : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected SourceControlBMPAttributeCategory()
        {
            this.SourceControlBMPAttributes = new HashSet<SourceControlBMPAttribute>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public SourceControlBMPAttributeCategory(int sourceControlBMPAttributeCategoryID, string sourceControlBMPAttributeCategoryName) : this()
        {
            this.SourceControlBMPAttributeCategoryID = sourceControlBMPAttributeCategoryID;
            this.SourceControlBMPAttributeCategoryName = sourceControlBMPAttributeCategoryName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public SourceControlBMPAttributeCategory(string sourceControlBMPAttributeCategoryName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.SourceControlBMPAttributeCategoryID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.SourceControlBMPAttributeCategoryName = sourceControlBMPAttributeCategoryName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static SourceControlBMPAttributeCategory CreateNewBlank()
        {
            return new SourceControlBMPAttributeCategory(default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return SourceControlBMPAttributes.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(SourceControlBMPAttributeCategory).Name, typeof(SourceControlBMPAttribute).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {

            foreach(var x in SourceControlBMPAttributes.ToList())
            {
                x.DeleteFull();
            }
            HttpRequestStorage.DatabaseEntities.AllSourceControlBMPAttributeCategories.Remove(this);                
        }

        [Key]
        public int SourceControlBMPAttributeCategoryID { get; set; }
        public int TenantID { get; private set; }
        public string SourceControlBMPAttributeCategoryName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return SourceControlBMPAttributeCategoryID; } set { SourceControlBMPAttributeCategoryID = value; } }

        public virtual ICollection<SourceControlBMPAttribute> SourceControlBMPAttributes { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }

        public static class FieldLengths
        {
            public const int SourceControlBMPAttributeCategoryName = 100;
        }
    }
}