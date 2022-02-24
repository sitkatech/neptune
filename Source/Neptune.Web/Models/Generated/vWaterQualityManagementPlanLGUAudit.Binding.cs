//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vWaterQualityManagementPlanLGUAudit]
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
    public partial class vWaterQualityManagementPlanLGUAudit
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vWaterQualityManagementPlanLGUAudit()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vWaterQualityManagementPlanLGUAudit(int primaryKey, int waterQualityManagementPlanID, string waterQualityManagementPlanName, bool? loadGeneratingUnitsPopulated, bool? boundaryIsDefined, int countOfIntersectingModelBasins) : this()
        {
            this.PrimaryKey = primaryKey;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.WaterQualityManagementPlanName = waterQualityManagementPlanName;
            this.LoadGeneratingUnitsPopulated = loadGeneratingUnitsPopulated;
            this.BoundaryIsDefined = boundaryIsDefined;
            this.CountOfIntersectingModelBasins = countOfIntersectingModelBasins;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vWaterQualityManagementPlanLGUAudit(vWaterQualityManagementPlanLGUAudit vWaterQualityManagementPlanLGUAudit) : this()
        {
            this.PrimaryKey = vWaterQualityManagementPlanLGUAudit.PrimaryKey;
            this.WaterQualityManagementPlanID = vWaterQualityManagementPlanLGUAudit.WaterQualityManagementPlanID;
            this.WaterQualityManagementPlanName = vWaterQualityManagementPlanLGUAudit.WaterQualityManagementPlanName;
            this.LoadGeneratingUnitsPopulated = vWaterQualityManagementPlanLGUAudit.LoadGeneratingUnitsPopulated;
            this.BoundaryIsDefined = vWaterQualityManagementPlanLGUAudit.BoundaryIsDefined;
            this.CountOfIntersectingModelBasins = vWaterQualityManagementPlanLGUAudit.CountOfIntersectingModelBasins;
            CallAfterConstructor(vWaterQualityManagementPlanLGUAudit);
        }

        partial void CallAfterConstructor(vWaterQualityManagementPlanLGUAudit vWaterQualityManagementPlanLGUAudit);

        public int PrimaryKey { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        public string WaterQualityManagementPlanName { get; set; }
        public bool? LoadGeneratingUnitsPopulated { get; set; }
        public bool? BoundaryIsDefined { get; set; }
        public int CountOfIntersectingModelBasins { get; set; }
    }
}