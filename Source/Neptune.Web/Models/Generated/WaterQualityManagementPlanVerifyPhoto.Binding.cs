//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyPhoto]
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
    // Table [dbo].[WaterQualityManagementPlanVerifyPhoto] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[WaterQualityManagementPlanVerifyPhoto]")]
    public partial class WaterQualityManagementPlanVerifyPhoto : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected WaterQualityManagementPlanVerifyPhoto()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanVerifyPhoto(int waterQualityManagementPlanVerifyPhotoID, int waterQualityManagementPlanVerifyID, int waterQualityManagementPlanPhotoID) : this()
        {
            this.WaterQualityManagementPlanVerifyPhotoID = waterQualityManagementPlanVerifyPhotoID;
            this.WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerifyID;
            this.WaterQualityManagementPlanPhotoID = waterQualityManagementPlanPhotoID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanVerifyPhoto(int waterQualityManagementPlanVerifyID, int waterQualityManagementPlanPhotoID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanVerifyPhotoID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerifyID;
            this.WaterQualityManagementPlanPhotoID = waterQualityManagementPlanPhotoID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public WaterQualityManagementPlanVerifyPhoto(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, WaterQualityManagementPlanPhoto waterQualityManagementPlanPhoto) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanVerifyPhotoID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID;
            this.WaterQualityManagementPlanVerify = waterQualityManagementPlanVerify;
            waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyPhotos.Add(this);
            this.WaterQualityManagementPlanPhotoID = waterQualityManagementPlanPhoto.WaterQualityManagementPlanPhotoID;
            this.WaterQualityManagementPlanPhoto = waterQualityManagementPlanPhoto;
            waterQualityManagementPlanPhoto.WaterQualityManagementPlanVerifyPhotos.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static WaterQualityManagementPlanVerifyPhoto CreateNewBlank(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, WaterQualityManagementPlanPhoto waterQualityManagementPlanPhoto)
        {
            return new WaterQualityManagementPlanVerifyPhoto(waterQualityManagementPlanVerify, waterQualityManagementPlanPhoto);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(WaterQualityManagementPlanVerifyPhoto).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.WaterQualityManagementPlanVerifyPhotos.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int WaterQualityManagementPlanVerifyPhotoID { get; set; }
        public int WaterQualityManagementPlanVerifyID { get; set; }
        public int WaterQualityManagementPlanPhotoID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanVerifyPhotoID; } set { WaterQualityManagementPlanVerifyPhotoID = value; } }

        public virtual WaterQualityManagementPlanVerify WaterQualityManagementPlanVerify { get; set; }
        public virtual WaterQualityManagementPlanPhoto WaterQualityManagementPlanPhoto { get; set; }

        public static class FieldLengths
        {

        }
    }
}