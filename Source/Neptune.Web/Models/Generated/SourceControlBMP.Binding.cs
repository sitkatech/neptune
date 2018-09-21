//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMP]
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
    [Table("[dbo].[SourceControlBMP]")]
    public partial class SourceControlBMP : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected SourceControlBMP()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public SourceControlBMP(int sourceControlBMPID, int sourceControlBMPAttributeID, bool isPresent, string sourceControlBMPNote) : this()
        {
            this.SourceControlBMPID = sourceControlBMPID;
            this.SourceControlBMPAttributeID = sourceControlBMPAttributeID;
            this.IsPresent = isPresent;
            this.SourceControlBMPNote = sourceControlBMPNote;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public SourceControlBMP(int sourceControlBMPAttributeID, bool isPresent) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.SourceControlBMPID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.SourceControlBMPAttributeID = sourceControlBMPAttributeID;
            this.IsPresent = isPresent;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public SourceControlBMP(SourceControlBMPAttribute sourceControlBMPAttribute, bool isPresent) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.SourceControlBMPID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.SourceControlBMPAttributeID = sourceControlBMPAttribute.SourceControlBMPAttributeID;
            this.SourceControlBMPAttribute = sourceControlBMPAttribute;
            sourceControlBMPAttribute.SourceControlBMPs.Add(this);
            this.IsPresent = isPresent;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static SourceControlBMP CreateNewBlank(SourceControlBMPAttribute sourceControlBMPAttribute)
        {
            return new SourceControlBMP(sourceControlBMPAttribute, default(bool));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(SourceControlBMP).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {
            HttpRequestStorage.DatabaseEntities.AllSourceControlBMPs.Remove(this);                
        }

        [Key]
        public int SourceControlBMPID { get; set; }
        public int TenantID { get; private set; }
        public int SourceControlBMPAttributeID { get; set; }
        public bool IsPresent { get; set; }
        public string SourceControlBMPNote { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return SourceControlBMPID; } set { SourceControlBMPID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual SourceControlBMPAttribute SourceControlBMPAttribute { get; set; }

        public static class FieldLengths
        {
            public const int SourceControlBMPNote = 200;
        }
    }
}