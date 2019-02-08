//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SystemAttribute]
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
    // Table [dbo].[SystemAttribute] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[SystemAttribute]")]
    public partial class SystemAttribute : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected SystemAttribute()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public SystemAttribute(int systemAttributeID, DbGeometry defaultBoundingBox, int minimumYear, int? primaryContactPersonID, int? tenantSquareLogoFileResourceID, int? tenantBannerLogoFileResourceID, int? tenantStyleSheetFileResourceID, string tenantDisplayName, string toolDisplayName, string recaptchaPublicKey, string recaptchaPrivateKey, string mapServiceUrl, string parcelLayerName) : this()
        {
            this.SystemAttributeID = systemAttributeID;
            this.DefaultBoundingBox = defaultBoundingBox;
            this.MinimumYear = minimumYear;
            this.PrimaryContactPersonID = primaryContactPersonID;
            this.TenantSquareLogoFileResourceID = tenantSquareLogoFileResourceID;
            this.TenantBannerLogoFileResourceID = tenantBannerLogoFileResourceID;
            this.TenantStyleSheetFileResourceID = tenantStyleSheetFileResourceID;
            this.TenantDisplayName = tenantDisplayName;
            this.ToolDisplayName = toolDisplayName;
            this.RecaptchaPublicKey = recaptchaPublicKey;
            this.RecaptchaPrivateKey = recaptchaPrivateKey;
            this.MapServiceUrl = mapServiceUrl;
            this.ParcelLayerName = parcelLayerName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public SystemAttribute(DbGeometry defaultBoundingBox, int minimumYear, string tenantDisplayName, string toolDisplayName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.SystemAttributeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.DefaultBoundingBox = defaultBoundingBox;
            this.MinimumYear = minimumYear;
            this.TenantDisplayName = tenantDisplayName;
            this.ToolDisplayName = toolDisplayName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static SystemAttribute CreateNewBlank()
        {
            return new SystemAttribute(default(DbGeometry), default(int), default(string), default(string));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(SystemAttribute).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.SystemAttributes.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int SystemAttributeID { get; set; }
        public DbGeometry DefaultBoundingBox { get; set; }
        public int MinimumYear { get; set; }
        public int? PrimaryContactPersonID { get; set; }
        public int? TenantSquareLogoFileResourceID { get; set; }
        public int? TenantBannerLogoFileResourceID { get; set; }
        public int? TenantStyleSheetFileResourceID { get; set; }
        public string TenantDisplayName { get; set; }
        public string ToolDisplayName { get; set; }
        public string RecaptchaPublicKey { get; set; }
        public string RecaptchaPrivateKey { get; set; }
        public string MapServiceUrl { get; set; }
        public string ParcelLayerName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return SystemAttributeID; } set { SystemAttributeID = value; } }

        public virtual Person PrimaryContactPerson { get; set; }
        public virtual FileResource TenantBannerLogoFileResource { get; set; }
        public virtual FileResource TenantSquareLogoFileResource { get; set; }
        public virtual FileResource TenantStyleSheetFileResource { get; set; }

        public static class FieldLengths
        {
            public const int TenantDisplayName = 100;
            public const int ToolDisplayName = 100;
            public const int RecaptchaPublicKey = 100;
            public const int RecaptchaPrivateKey = 100;
            public const int MapServiceUrl = 255;
            public const int ParcelLayerName = 255;
        }
    }
}