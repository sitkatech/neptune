//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vGeoServerWaterQualityManagementPlan]
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
    public partial class vGeoServerWaterQualityManagementPlan
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vGeoServerWaterQualityManagementPlan()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vGeoServerWaterQualityManagementPlan(int primaryKey, int waterQualityManagementPlanID, int stormwaterJurisdictionID, string organizationName, int trashCaptureEffectiveness, string trashCaptureStatusTypeDisplayName) : this()
        {
            this.PrimaryKey = primaryKey;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.OrganizationName = organizationName;
            this.TrashCaptureEffectiveness = trashCaptureEffectiveness;
            this.TrashCaptureStatusTypeDisplayName = trashCaptureStatusTypeDisplayName;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vGeoServerWaterQualityManagementPlan(vGeoServerWaterQualityManagementPlan vGeoServerWaterQualityManagementPlan) : this()
        {
            this.PrimaryKey = vGeoServerWaterQualityManagementPlan.PrimaryKey;
            this.WaterQualityManagementPlanID = vGeoServerWaterQualityManagementPlan.WaterQualityManagementPlanID;
            this.StormwaterJurisdictionID = vGeoServerWaterQualityManagementPlan.StormwaterJurisdictionID;
            this.OrganizationName = vGeoServerWaterQualityManagementPlan.OrganizationName;
            this.TrashCaptureEffectiveness = vGeoServerWaterQualityManagementPlan.TrashCaptureEffectiveness;
            this.TrashCaptureStatusTypeDisplayName = vGeoServerWaterQualityManagementPlan.TrashCaptureStatusTypeDisplayName;
            CallAfterConstructor(vGeoServerWaterQualityManagementPlan);
        }

        partial void CallAfterConstructor(vGeoServerWaterQualityManagementPlan vGeoServerWaterQualityManagementPlan);

        public int PrimaryKey { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public string OrganizationName { get; set; }
        public int TrashCaptureEffectiveness { get; set; }
        public string TrashCaptureStatusTypeDisplayName { get; set; }
    }
}