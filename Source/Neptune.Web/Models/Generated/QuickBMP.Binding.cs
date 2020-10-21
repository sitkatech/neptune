//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[QuickBMP]
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
    // Table [dbo].[QuickBMP] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[QuickBMP]")]
    public partial class QuickBMP : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected QuickBMP()
        {
            this.WaterQualityManagementPlanVerifyQuickBMPs = new HashSet<WaterQualityManagementPlanVerifyQuickBMP>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public QuickBMP(int quickBMPID, int waterQualityManagementPlanID, int treatmentBMPTypeID, string quickBMPName, string quickBMPNote, decimal? percentOfSiteTreated, decimal? percentCaptured, decimal? percentRetained, int? dryWeatherFlowOverrideID) : this()
        {
            this.QuickBMPID = quickBMPID;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.QuickBMPName = quickBMPName;
            this.QuickBMPNote = quickBMPNote;
            this.PercentOfSiteTreated = percentOfSiteTreated;
            this.PercentCaptured = percentCaptured;
            this.PercentRetained = percentRetained;
            this.DryWeatherFlowOverrideID = dryWeatherFlowOverrideID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public QuickBMP(int waterQualityManagementPlanID, int treatmentBMPTypeID, string quickBMPName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.QuickBMPID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.QuickBMPName = quickBMPName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public QuickBMP(WaterQualityManagementPlan waterQualityManagementPlan, TreatmentBMPType treatmentBMPType, string quickBMPName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.QuickBMPID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID;
            this.WaterQualityManagementPlan = waterQualityManagementPlan;
            waterQualityManagementPlan.QuickBMPs.Add(this);
            this.TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID;
            this.TreatmentBMPType = treatmentBMPType;
            treatmentBMPType.QuickBMPs.Add(this);
            this.QuickBMPName = quickBMPName;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static QuickBMP CreateNewBlank(WaterQualityManagementPlan waterQualityManagementPlan, TreatmentBMPType treatmentBMPType)
        {
            return new QuickBMP(waterQualityManagementPlan, treatmentBMPType, default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return WaterQualityManagementPlanVerifyQuickBMPs.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(WaterQualityManagementPlanVerifyQuickBMPs.Any())
            {
                dependentObjects.Add(typeof(WaterQualityManagementPlanVerifyQuickBMP).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(QuickBMP).Name, typeof(WaterQualityManagementPlanVerifyQuickBMP).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.QuickBMPs.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            Delete(dbContext);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in WaterQualityManagementPlanVerifyQuickBMPs.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int QuickBMPID { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public string QuickBMPName { get; set; }
        public string QuickBMPNote { get; set; }
        public decimal? PercentOfSiteTreated { get; set; }
        public decimal? PercentCaptured { get; set; }
        public decimal? PercentRetained { get; set; }
        public int? DryWeatherFlowOverrideID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return QuickBMPID; } set { QuickBMPID = value; } }

        public virtual ICollection<WaterQualityManagementPlanVerifyQuickBMP> WaterQualityManagementPlanVerifyQuickBMPs { get; set; }
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        public DryWeatherFlowOverride DryWeatherFlowOverride { get { return DryWeatherFlowOverrideID.HasValue ? DryWeatherFlowOverride.AllLookupDictionary[DryWeatherFlowOverrideID.Value] : null; } }

        public static class FieldLengths
        {
            public const int QuickBMPName = 100;
            public const int QuickBMPNote = 200;
        }
    }
}