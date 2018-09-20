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
    [Table("[dbo].[QuickBMP]")]
    public partial class QuickBMP : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected QuickBMP()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public QuickBMP(int quickBMPID, int waterQualityManagementPlanID, int treatmentBMPTypeID, string quickBMPName, string quickBMPNote) : this()
        {
            this.QuickBMPID = quickBMPID;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.QuickBMPName = quickBMPName;
            this.QuickBMPNote = quickBMPNote;
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
            return false;
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(QuickBMP).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {
            HttpRequestStorage.DatabaseEntities.QuickBMPs.Remove(this);                
        }

        [Key]
        public int QuickBMPID { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public string QuickBMPName { get; set; }
        public string QuickBMPNote { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return QuickBMPID; } set { QuickBMPID = value; } }

        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }

        public static class FieldLengths
        {
            public const int QuickBMPName = 100;
            public const int QuickBMPNote = 100;
        }
    }
}