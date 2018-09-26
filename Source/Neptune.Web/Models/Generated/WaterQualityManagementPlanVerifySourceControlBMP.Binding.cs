//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifySourceControlBMP]
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
    [Table("[dbo].[WaterQualityManagementPlanVerifySourceControlBMP]")]
    public partial class WaterQualityManagementPlanVerifySourceControlBMP : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected WaterQualityManagementPlanVerifySourceControlBMP()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanVerifySourceControlBMP(int waterQualityManagementPlanVerifySourceControlBMPID, int waterQualityManagementPlanVerifyID, int sourceControlBMPID, string waterQualityManagementPlanSourceControlCondition) : this()
        {
            this.WaterQualityManagementPlanVerifySourceControlBMPID = waterQualityManagementPlanVerifySourceControlBMPID;
            this.WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerifyID;
            this.SourceControlBMPID = sourceControlBMPID;
            this.WaterQualityManagementPlanSourceControlCondition = waterQualityManagementPlanSourceControlCondition;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanVerifySourceControlBMP(int waterQualityManagementPlanVerifyID, int sourceControlBMPID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanVerifySourceControlBMPID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerifyID;
            this.SourceControlBMPID = sourceControlBMPID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public WaterQualityManagementPlanVerifySourceControlBMP(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, SourceControlBMP sourceControlBMP) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanVerifySourceControlBMPID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID;
            this.WaterQualityManagementPlanVerify = waterQualityManagementPlanVerify;
            waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifySourceControlBMPs.Add(this);
            this.SourceControlBMPID = sourceControlBMP.SourceControlBMPID;
            this.SourceControlBMP = sourceControlBMP;
            sourceControlBMP.WaterQualityManagementPlanVerifySourceControlBMPs.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static WaterQualityManagementPlanVerifySourceControlBMP CreateNewBlank(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, SourceControlBMP sourceControlBMP)
        {
            return new WaterQualityManagementPlanVerifySourceControlBMP(waterQualityManagementPlanVerify, sourceControlBMP);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(WaterQualityManagementPlanVerifySourceControlBMP).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {
            HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifySourceControlBMPs.Remove(this);                
        }

        [Key]
        public int WaterQualityManagementPlanVerifySourceControlBMPID { get; set; }
        public int TenantID { get; private set; }
        public int WaterQualityManagementPlanVerifyID { get; set; }
        public int SourceControlBMPID { get; set; }
        public string WaterQualityManagementPlanSourceControlCondition { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanVerifySourceControlBMPID; } set { WaterQualityManagementPlanVerifySourceControlBMPID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual WaterQualityManagementPlanVerify WaterQualityManagementPlanVerify { get; set; }
        public virtual SourceControlBMP SourceControlBMP { get; set; }

        public static class FieldLengths
        {
            public const int WaterQualityManagementPlanSourceControlCondition = 1000;
        }
    }
}