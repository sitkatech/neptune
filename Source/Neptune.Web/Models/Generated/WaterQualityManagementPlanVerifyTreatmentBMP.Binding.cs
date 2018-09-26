//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyTreatmentBMP]
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
    [Table("[dbo].[WaterQualityManagementPlanVerifyTreatmentBMP]")]
    public partial class WaterQualityManagementPlanVerifyTreatmentBMP : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected WaterQualityManagementPlanVerifyTreatmentBMP()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanVerifyTreatmentBMP(int waterQualityManagementPlanVerifyTreatmentBMPID, int waterQualityManagementPlanVerifyID, int treatmentBMPID, bool? isAdequate, string waterQualityManagementPlanVerifyQuickBMPNote) : this()
        {
            this.WaterQualityManagementPlanVerifyTreatmentBMPID = waterQualityManagementPlanVerifyTreatmentBMPID;
            this.WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerifyID;
            this.TreatmentBMPID = treatmentBMPID;
            this.IsAdequate = isAdequate;
            this.WaterQualityManagementPlanVerifyQuickBMPNote = waterQualityManagementPlanVerifyQuickBMPNote;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanVerifyTreatmentBMP(int waterQualityManagementPlanVerifyID, int treatmentBMPID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanVerifyTreatmentBMPID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerifyID;
            this.TreatmentBMPID = treatmentBMPID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public WaterQualityManagementPlanVerifyTreatmentBMP(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, TreatmentBMP treatmentBMP) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanVerifyTreatmentBMPID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID;
            this.WaterQualityManagementPlanVerify = waterQualityManagementPlanVerify;
            waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyTreatmentBMPs.Add(this);
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
            treatmentBMP.WaterQualityManagementPlanVerifyTreatmentBMPs.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static WaterQualityManagementPlanVerifyTreatmentBMP CreateNewBlank(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, TreatmentBMP treatmentBMP)
        {
            return new WaterQualityManagementPlanVerifyTreatmentBMP(waterQualityManagementPlanVerify, treatmentBMP);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(WaterQualityManagementPlanVerifyTreatmentBMP).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {
            HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifyTreatmentBMPs.Remove(this);                
        }

        [Key]
        public int WaterQualityManagementPlanVerifyTreatmentBMPID { get; set; }
        public int TenantID { get; private set; }
        public int WaterQualityManagementPlanVerifyID { get; set; }
        public int TreatmentBMPID { get; set; }
        public bool? IsAdequate { get; set; }
        public string WaterQualityManagementPlanVerifyQuickBMPNote { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanVerifyTreatmentBMPID; } set { WaterQualityManagementPlanVerifyTreatmentBMPID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual WaterQualityManagementPlanVerify WaterQualityManagementPlanVerify { get; set; }
        public virtual TreatmentBMP TreatmentBMP { get; set; }

        public static class FieldLengths
        {
            public const int WaterQualityManagementPlanVerifyQuickBMPNote = 500;
        }
    }
}