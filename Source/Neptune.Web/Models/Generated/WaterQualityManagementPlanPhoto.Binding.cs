//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanPhoto]
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
    [Table("[dbo].[WaterQualityManagementPlanPhoto]")]
    public partial class WaterQualityManagementPlanPhoto : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected WaterQualityManagementPlanPhoto()
        {
            this.WaterQualityManagementPlanVerifyPhotos = new HashSet<WaterQualityManagementPlanVerifyPhoto>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanPhoto(int waterQualityManagementPlanPhotoID, int fileResourceID, string caption, DateTime uploadDate) : this()
        {
            this.WaterQualityManagementPlanPhotoID = waterQualityManagementPlanPhotoID;
            this.FileResourceID = fileResourceID;
            this.Caption = caption;
            this.UploadDate = uploadDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanPhoto(int fileResourceID, DateTime uploadDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanPhotoID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.FileResourceID = fileResourceID;
            this.UploadDate = uploadDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public WaterQualityManagementPlanPhoto(FileResource fileResource, DateTime uploadDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanPhotoID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.FileResourceID = fileResource.FileResourceID;
            this.FileResource = fileResource;
            fileResource.WaterQualityManagementPlanPhotos.Add(this);
            this.UploadDate = uploadDate;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static WaterQualityManagementPlanPhoto CreateNewBlank(FileResource fileResource)
        {
            return new WaterQualityManagementPlanPhoto(fileResource, default(DateTime));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return WaterQualityManagementPlanVerifyPhotos.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(WaterQualityManagementPlanPhoto).Name, typeof(WaterQualityManagementPlanVerifyPhoto).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(HttpRequestStorage.DatabaseEntities);
            dbContext.AllWaterQualityManagementPlanPhotos.Remove(this);
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in WaterQualityManagementPlanVerifyPhotos.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int WaterQualityManagementPlanPhotoID { get; set; }
        public int TenantID { get; private set; }
        public int FileResourceID { get; set; }
        public string Caption { get; set; }
        public DateTime UploadDate { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanPhotoID; } set { WaterQualityManagementPlanPhotoID = value; } }

        public virtual ICollection<WaterQualityManagementPlanVerifyPhoto> WaterQualityManagementPlanVerifyPhotos { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual FileResource FileResource { get; set; }

        public static class FieldLengths
        {
            public const int Caption = 500;
        }
    }
}