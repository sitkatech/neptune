//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyQuickBMP]
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
    // Table [dbo].[WaterQualityManagementPlanVerifyQuickBMP] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[WaterQualityManagementPlanVerifyQuickBMP]")]
    public partial class WaterQualityManagementPlanVerifyQuickBMP : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected WaterQualityManagementPlanVerifyQuickBMP()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanVerifyQuickBMP(int waterQualityManagementPlanVerifyQuickBMPID, int waterQualityManagementPlanVerifyID, int quickBMPID, bool? isAdequate, string waterQualityManagementPlanVerifyQuickBMPNote) : this()
        {
            this.WaterQualityManagementPlanVerifyQuickBMPID = waterQualityManagementPlanVerifyQuickBMPID;
            this.WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerifyID;
            this.QuickBMPID = quickBMPID;
            this.IsAdequate = isAdequate;
            this.WaterQualityManagementPlanVerifyQuickBMPNote = waterQualityManagementPlanVerifyQuickBMPNote;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanVerifyQuickBMP(int waterQualityManagementPlanVerifyID, int quickBMPID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanVerifyQuickBMPID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerifyID;
            this.QuickBMPID = quickBMPID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public WaterQualityManagementPlanVerifyQuickBMP(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, QuickBMP quickBMP) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanVerifyQuickBMPID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID;
            this.WaterQualityManagementPlanVerify = waterQualityManagementPlanVerify;
            waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyQuickBMPs.Add(this);
            this.QuickBMPID = quickBMP.QuickBMPID;
            this.QuickBMP = quickBMP;
            quickBMP.WaterQualityManagementPlanVerifyQuickBMPs.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static WaterQualityManagementPlanVerifyQuickBMP CreateNewBlank(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, QuickBMP quickBMP)
        {
            return new WaterQualityManagementPlanVerifyQuickBMP(waterQualityManagementPlanVerify, quickBMP);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(WaterQualityManagementPlanVerifyQuickBMP).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.WaterQualityManagementPlanVerifyQuickBMPs.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int WaterQualityManagementPlanVerifyQuickBMPID { get; set; }
        public int WaterQualityManagementPlanVerifyID { get; set; }
        public int QuickBMPID { get; set; }
        public bool? IsAdequate { get; set; }
        public string WaterQualityManagementPlanVerifyQuickBMPNote { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanVerifyQuickBMPID; } set { WaterQualityManagementPlanVerifyQuickBMPID = value; } }

        public virtual WaterQualityManagementPlanVerify WaterQualityManagementPlanVerify { get; set; }
        public virtual QuickBMP QuickBMP { get; set; }

        public static class FieldLengths
        {
            public const int WaterQualityManagementPlanVerifyQuickBMPNote = 500;
        }
    }
}