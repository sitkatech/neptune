//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vPowerBIWaterQualityManagementPlan]
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public partial class vPowerBIWaterQualityManagementPlan
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vPowerBIWaterQualityManagementPlan()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vPowerBIWaterQualityManagementPlan(int primaryKey, int waterQualityManagementPlanID, string waterQualityManagementPlanName, string organizationName, string waterQualityManagementPlanStatusDisplayName, string waterQualityManagementPlanDevelopmentTypeDisplayName, string waterQualityManagementPlanLandUseDisplayName, string waterQualityManagementPlanPermitTermDisplayName, DateTime? approvalDate, DateTime? dateOfContruction, string hydromodificationAppliesDisplayName, string hydrologicSubareaName, decimal? recordedWQMPAreaInAcres, string trashCaptureStatusTypeDisplayName, int? trashCaptureEffectiveness) : this()
        {
            this.PrimaryKey = primaryKey;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.WaterQualityManagementPlanName = waterQualityManagementPlanName;
            this.OrganizationName = organizationName;
            this.WaterQualityManagementPlanStatusDisplayName = waterQualityManagementPlanStatusDisplayName;
            this.WaterQualityManagementPlanDevelopmentTypeDisplayName = waterQualityManagementPlanDevelopmentTypeDisplayName;
            this.WaterQualityManagementPlanLandUseDisplayName = waterQualityManagementPlanLandUseDisplayName;
            this.WaterQualityManagementPlanPermitTermDisplayName = waterQualityManagementPlanPermitTermDisplayName;
            this.ApprovalDate = approvalDate;
            this.DateOfContruction = dateOfContruction;
            this.HydromodificationAppliesDisplayName = hydromodificationAppliesDisplayName;
            this.HydrologicSubareaName = hydrologicSubareaName;
            this.RecordedWQMPAreaInAcres = recordedWQMPAreaInAcres;
            this.TrashCaptureStatusTypeDisplayName = trashCaptureStatusTypeDisplayName;
            this.TrashCaptureEffectiveness = trashCaptureEffectiveness;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vPowerBIWaterQualityManagementPlan(vPowerBIWaterQualityManagementPlan vPowerBIWaterQualityManagementPlan) : this()
        {
            this.PrimaryKey = vPowerBIWaterQualityManagementPlan.PrimaryKey;
            this.WaterQualityManagementPlanID = vPowerBIWaterQualityManagementPlan.WaterQualityManagementPlanID;
            this.WaterQualityManagementPlanName = vPowerBIWaterQualityManagementPlan.WaterQualityManagementPlanName;
            this.OrganizationName = vPowerBIWaterQualityManagementPlan.OrganizationName;
            this.WaterQualityManagementPlanStatusDisplayName = vPowerBIWaterQualityManagementPlan.WaterQualityManagementPlanStatusDisplayName;
            this.WaterQualityManagementPlanDevelopmentTypeDisplayName = vPowerBIWaterQualityManagementPlan.WaterQualityManagementPlanDevelopmentTypeDisplayName;
            this.WaterQualityManagementPlanLandUseDisplayName = vPowerBIWaterQualityManagementPlan.WaterQualityManagementPlanLandUseDisplayName;
            this.WaterQualityManagementPlanPermitTermDisplayName = vPowerBIWaterQualityManagementPlan.WaterQualityManagementPlanPermitTermDisplayName;
            this.ApprovalDate = vPowerBIWaterQualityManagementPlan.ApprovalDate;
            this.DateOfContruction = vPowerBIWaterQualityManagementPlan.DateOfContruction;
            this.HydromodificationAppliesDisplayName = vPowerBIWaterQualityManagementPlan.HydromodificationAppliesDisplayName;
            this.HydrologicSubareaName = vPowerBIWaterQualityManagementPlan.HydrologicSubareaName;
            this.RecordedWQMPAreaInAcres = vPowerBIWaterQualityManagementPlan.RecordedWQMPAreaInAcres;
            this.TrashCaptureStatusTypeDisplayName = vPowerBIWaterQualityManagementPlan.TrashCaptureStatusTypeDisplayName;
            this.TrashCaptureEffectiveness = vPowerBIWaterQualityManagementPlan.TrashCaptureEffectiveness;
            CallAfterConstructor(vPowerBIWaterQualityManagementPlan);
        }

        partial void CallAfterConstructor(vPowerBIWaterQualityManagementPlan vPowerBIWaterQualityManagementPlan);

        public int PrimaryKey { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        public string WaterQualityManagementPlanName { get; set; }
        public string OrganizationName { get; set; }
        public string WaterQualityManagementPlanStatusDisplayName { get; set; }
        public string WaterQualityManagementPlanDevelopmentTypeDisplayName { get; set; }
        public string WaterQualityManagementPlanLandUseDisplayName { get; set; }
        public string WaterQualityManagementPlanPermitTermDisplayName { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? DateOfContruction { get; set; }
        public string HydromodificationAppliesDisplayName { get; set; }
        public string HydrologicSubareaName { get; set; }
        public decimal? RecordedWQMPAreaInAcres { get; set; }
        public string TrashCaptureStatusTypeDisplayName { get; set; }
        public int? TrashCaptureEffectiveness { get; set; }
    }
}