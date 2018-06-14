//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlan]
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
    [Table("[dbo].[WaterQualityManagementPlan]")]
    public partial class WaterQualityManagementPlan : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected WaterQualityManagementPlan()
        {
            this.TreatmentBMPs = new HashSet<TreatmentBMP>();
            this.WaterQualityManagementPlanDocuments = new HashSet<WaterQualityManagementPlanDocument>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlan(int waterQualityManagementPlanID, int stormwaterJurisdictionID, int waterQualityManagementPlanLandUseID, int waterQualityManagementPlanPriorityID, int waterQualityManagementPlanStatusID, int waterQualityManagementPlanDevelopmentTypeID, string waterQualityManagementPlanName, DateTime? approvalDate, string maintenanceContactName, string maintenanceContactOrganization, string maintenanceContactPhone, string maintenanceContactAddress1, string maintenanceContactAddress2, string maintenanceContactCity, string maintenanceContactState, string maintenanceContactZip) : this()
        {
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.WaterQualityManagementPlanLandUseID = waterQualityManagementPlanLandUseID;
            this.WaterQualityManagementPlanPriorityID = waterQualityManagementPlanPriorityID;
            this.WaterQualityManagementPlanStatusID = waterQualityManagementPlanStatusID;
            this.WaterQualityManagementPlanDevelopmentTypeID = waterQualityManagementPlanDevelopmentTypeID;
            this.WaterQualityManagementPlanName = waterQualityManagementPlanName;
            this.ApprovalDate = approvalDate;
            this.MaintenanceContactName = maintenanceContactName;
            this.MaintenanceContactOrganization = maintenanceContactOrganization;
            this.MaintenanceContactPhone = maintenanceContactPhone;
            this.MaintenanceContactAddress1 = maintenanceContactAddress1;
            this.MaintenanceContactAddress2 = maintenanceContactAddress2;
            this.MaintenanceContactCity = maintenanceContactCity;
            this.MaintenanceContactState = maintenanceContactState;
            this.MaintenanceContactZip = maintenanceContactZip;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlan(int stormwaterJurisdictionID, int waterQualityManagementPlanLandUseID, int waterQualityManagementPlanPriorityID, int waterQualityManagementPlanStatusID, int waterQualityManagementPlanDevelopmentTypeID, string waterQualityManagementPlanName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.WaterQualityManagementPlanLandUseID = waterQualityManagementPlanLandUseID;
            this.WaterQualityManagementPlanPriorityID = waterQualityManagementPlanPriorityID;
            this.WaterQualityManagementPlanStatusID = waterQualityManagementPlanStatusID;
            this.WaterQualityManagementPlanDevelopmentTypeID = waterQualityManagementPlanDevelopmentTypeID;
            this.WaterQualityManagementPlanName = waterQualityManagementPlanName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public WaterQualityManagementPlan(StormwaterJurisdiction stormwaterJurisdiction, WaterQualityManagementPlanLandUse waterQualityManagementPlanLandUse, WaterQualityManagementPlanPriority waterQualityManagementPlanPriority, WaterQualityManagementPlanStatus waterQualityManagementPlanStatus, WaterQualityManagementPlanDevelopmentType waterQualityManagementPlanDevelopmentType, string waterQualityManagementPlanName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID;
            this.StormwaterJurisdiction = stormwaterJurisdiction;
            stormwaterJurisdiction.WaterQualityManagementPlans.Add(this);
            this.WaterQualityManagementPlanLandUseID = waterQualityManagementPlanLandUse.WaterQualityManagementPlanLandUseID;
            this.WaterQualityManagementPlanLandUse = waterQualityManagementPlanLandUse;
            waterQualityManagementPlanLandUse.WaterQualityManagementPlans.Add(this);
            this.WaterQualityManagementPlanPriorityID = waterQualityManagementPlanPriority.WaterQualityManagementPlanPriorityID;
            this.WaterQualityManagementPlanPriority = waterQualityManagementPlanPriority;
            waterQualityManagementPlanPriority.WaterQualityManagementPlans.Add(this);
            this.WaterQualityManagementPlanStatusID = waterQualityManagementPlanStatus.WaterQualityManagementPlanStatusID;
            this.WaterQualityManagementPlanStatus = waterQualityManagementPlanStatus;
            waterQualityManagementPlanStatus.WaterQualityManagementPlans.Add(this);
            this.WaterQualityManagementPlanDevelopmentTypeID = waterQualityManagementPlanDevelopmentType.WaterQualityManagementPlanDevelopmentTypeID;
            this.WaterQualityManagementPlanDevelopmentType = waterQualityManagementPlanDevelopmentType;
            waterQualityManagementPlanDevelopmentType.WaterQualityManagementPlans.Add(this);
            this.WaterQualityManagementPlanName = waterQualityManagementPlanName;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static WaterQualityManagementPlan CreateNewBlank(StormwaterJurisdiction stormwaterJurisdiction, WaterQualityManagementPlanLandUse waterQualityManagementPlanLandUse, WaterQualityManagementPlanPriority waterQualityManagementPlanPriority, WaterQualityManagementPlanStatus waterQualityManagementPlanStatus, WaterQualityManagementPlanDevelopmentType waterQualityManagementPlanDevelopmentType)
        {
            return new WaterQualityManagementPlan(stormwaterJurisdiction, waterQualityManagementPlanLandUse, waterQualityManagementPlanPriority, waterQualityManagementPlanStatus, waterQualityManagementPlanDevelopmentType, default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return TreatmentBMPs.Any() || WaterQualityManagementPlanDocuments.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(WaterQualityManagementPlan).Name, typeof(TreatmentBMP).Name, typeof(WaterQualityManagementPlanDocument).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {

            foreach(var x in TreatmentBMPs.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in WaterQualityManagementPlanDocuments.ToList())
            {
                x.DeleteFull();
            }
            HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlans.Remove(this);                
        }

        [Key]
        public int WaterQualityManagementPlanID { get; set; }
        public int TenantID { get; private set; }
        public int StormwaterJurisdictionID { get; set; }
        public int WaterQualityManagementPlanLandUseID { get; set; }
        public int WaterQualityManagementPlanPriorityID { get; set; }
        public int WaterQualityManagementPlanStatusID { get; set; }
        public int WaterQualityManagementPlanDevelopmentTypeID { get; set; }
        public string WaterQualityManagementPlanName { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string MaintenanceContactName { get; set; }
        public string MaintenanceContactOrganization { get; set; }
        public string MaintenanceContactPhone { get; set; }
        public string MaintenanceContactAddress1 { get; set; }
        public string MaintenanceContactAddress2 { get; set; }
        public string MaintenanceContactCity { get; set; }
        public string MaintenanceContactState { get; set; }
        public string MaintenanceContactZip { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanID; } set { WaterQualityManagementPlanID = value; } }

        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
        public virtual ICollection<WaterQualityManagementPlanDocument> WaterQualityManagementPlanDocuments { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        public virtual WaterQualityManagementPlanLandUse WaterQualityManagementPlanLandUse { get; set; }
        public virtual WaterQualityManagementPlanPriority WaterQualityManagementPlanPriority { get; set; }
        public virtual WaterQualityManagementPlanStatus WaterQualityManagementPlanStatus { get; set; }
        public virtual WaterQualityManagementPlanDevelopmentType WaterQualityManagementPlanDevelopmentType { get; set; }

        public static class FieldLengths
        {
            public const int WaterQualityManagementPlanName = 100;
            public const int MaintenanceContactName = 100;
            public const int MaintenanceContactOrganization = 100;
            public const int MaintenanceContactPhone = 100;
            public const int MaintenanceContactAddress1 = 100;
            public const int MaintenanceContactAddress2 = 100;
            public const int MaintenanceContactCity = 100;
            public const int MaintenanceContactState = 100;
            public const int MaintenanceContactZip = 100;
        }
    }
}