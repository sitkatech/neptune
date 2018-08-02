//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanDocument]
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
    [Table("[dbo].[WaterQualityManagementPlanDocument]")]
    public partial class WaterQualityManagementPlanDocument : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected WaterQualityManagementPlanDocument()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanDocument(int waterQualityManagementPlanDocumentID, int waterQualityManagementPlanID, int fileResourceID, string displayName, string description, DateTime uploadDate, int waterQualityManagementPlanDocumentTypeID) : this()
        {
            this.WaterQualityManagementPlanDocumentID = waterQualityManagementPlanDocumentID;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.FileResourceID = fileResourceID;
            this.DisplayName = displayName;
            this.Description = description;
            this.UploadDate = uploadDate;
            this.WaterQualityManagementPlanDocumentTypeID = waterQualityManagementPlanDocumentTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanDocument(int waterQualityManagementPlanID, int fileResourceID, string displayName, DateTime uploadDate, int waterQualityManagementPlanDocumentTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanDocumentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.FileResourceID = fileResourceID;
            this.DisplayName = displayName;
            this.UploadDate = uploadDate;
            this.WaterQualityManagementPlanDocumentTypeID = waterQualityManagementPlanDocumentTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public WaterQualityManagementPlanDocument(WaterQualityManagementPlan waterQualityManagementPlan, FileResource fileResource, string displayName, DateTime uploadDate, WaterQualityManagementPlanDocumentType waterQualityManagementPlanDocumentType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanDocumentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID;
            this.WaterQualityManagementPlan = waterQualityManagementPlan;
            waterQualityManagementPlan.WaterQualityManagementPlanDocuments.Add(this);
            this.FileResourceID = fileResource.FileResourceID;
            this.FileResource = fileResource;
            fileResource.WaterQualityManagementPlanDocuments.Add(this);
            this.DisplayName = displayName;
            this.UploadDate = uploadDate;
            this.WaterQualityManagementPlanDocumentTypeID = waterQualityManagementPlanDocumentType.WaterQualityManagementPlanDocumentTypeID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static WaterQualityManagementPlanDocument CreateNewBlank(WaterQualityManagementPlan waterQualityManagementPlan, FileResource fileResource, WaterQualityManagementPlanDocumentType waterQualityManagementPlanDocumentType)
        {
            return new WaterQualityManagementPlanDocument(waterQualityManagementPlan, fileResource, default(string), default(DateTime), waterQualityManagementPlanDocumentType);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(WaterQualityManagementPlanDocument).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {
            HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanDocuments.Remove(this);                
        }

        [Key]
        public int WaterQualityManagementPlanDocumentID { get; set; }
        public int TenantID { get; private set; }
        public int WaterQualityManagementPlanID { get; set; }
        public int FileResourceID { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }
        public int WaterQualityManagementPlanDocumentTypeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanDocumentID; } set { WaterQualityManagementPlanDocumentID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
        public virtual FileResource FileResource { get; set; }
        public WaterQualityManagementPlanDocumentType WaterQualityManagementPlanDocumentType { get { return WaterQualityManagementPlanDocumentType.AllLookupDictionary[WaterQualityManagementPlanDocumentTypeID]; } }

        public static class FieldLengths
        {
            public const int DisplayName = 100;
            public const int Description = 1000;
        }
    }
}