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
    // Table [dbo].[WaterQualityManagementPlanVerifySourceControlBMP] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[WaterQualityManagementPlanVerifySourceControlBMP]")]
    public partial class WaterQualityManagementPlanVerifySourceControlBMP : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected WaterQualityManagementPlanVerifySourceControlBMP()
        {

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
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(WaterQualityManagementPlanVerifySourceControlBMP).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.WaterQualityManagementPlanVerifySourceControlBMPs.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int WaterQualityManagementPlanVerifySourceControlBMPID { get; set; }
        public int WaterQualityManagementPlanVerifyID { get; set; }
        public int SourceControlBMPID { get; set; }
        public string WaterQualityManagementPlanSourceControlCondition { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanVerifySourceControlBMPID; } set { WaterQualityManagementPlanVerifySourceControlBMPID = value; } }

        public virtual WaterQualityManagementPlanVerify WaterQualityManagementPlanVerify { get; set; }
        public virtual SourceControlBMP SourceControlBMP { get; set; }

        public static class FieldLengths
        {
            public const int WaterQualityManagementPlanSourceControlCondition = 1000;
        }
    }
}