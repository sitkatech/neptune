//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vPowerBILandUseStatistic]
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
    public partial class vPowerBILandUseStatistic
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vPowerBILandUseStatistic()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vPowerBILandUseStatistic(int primaryKey, int hRUCharacteristicID, string hydrologicSoilGroup, int slopePercentage, double imperviousAcres, double area, string hRUCharacteristicLandUseCodeDisplayName, int? lSPCBasinID, string watershedName, int? treatmentBMPID, int? waterQualityManagementPlanID, int? regionalSubbasinID, int? loadGeneratingUnitID) : this()
        {
            this.PrimaryKey = primaryKey;
            this.HRUCharacteristicID = hRUCharacteristicID;
            this.HydrologicSoilGroup = hydrologicSoilGroup;
            this.SlopePercentage = slopePercentage;
            this.ImperviousAcres = imperviousAcres;
            this.Area = area;
            this.HRUCharacteristicLandUseCodeDisplayName = hRUCharacteristicLandUseCodeDisplayName;
            this.LSPCBasinID = lSPCBasinID;
            this.WatershedName = watershedName;
            this.TreatmentBMPID = treatmentBMPID;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.RegionalSubbasinID = regionalSubbasinID;
            this.LoadGeneratingUnitID = loadGeneratingUnitID;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vPowerBILandUseStatistic(vPowerBILandUseStatistic vPowerBILandUseStatistic) : this()
        {
            this.PrimaryKey = vPowerBILandUseStatistic.PrimaryKey;
            this.HRUCharacteristicID = vPowerBILandUseStatistic.HRUCharacteristicID;
            this.HydrologicSoilGroup = vPowerBILandUseStatistic.HydrologicSoilGroup;
            this.SlopePercentage = vPowerBILandUseStatistic.SlopePercentage;
            this.ImperviousAcres = vPowerBILandUseStatistic.ImperviousAcres;
            this.Area = vPowerBILandUseStatistic.Area;
            this.HRUCharacteristicLandUseCodeDisplayName = vPowerBILandUseStatistic.HRUCharacteristicLandUseCodeDisplayName;
            this.LSPCBasinID = vPowerBILandUseStatistic.LSPCBasinID;
            this.WatershedName = vPowerBILandUseStatistic.WatershedName;
            this.TreatmentBMPID = vPowerBILandUseStatistic.TreatmentBMPID;
            this.WaterQualityManagementPlanID = vPowerBILandUseStatistic.WaterQualityManagementPlanID;
            this.RegionalSubbasinID = vPowerBILandUseStatistic.RegionalSubbasinID;
            this.LoadGeneratingUnitID = vPowerBILandUseStatistic.LoadGeneratingUnitID;
            CallAfterConstructor(vPowerBILandUseStatistic);
        }

        partial void CallAfterConstructor(vPowerBILandUseStatistic vPowerBILandUseStatistic);

        public int PrimaryKey { get; set; }
        public int HRUCharacteristicID { get; set; }
        public string HydrologicSoilGroup { get; set; }
        public int SlopePercentage { get; set; }
        public double ImperviousAcres { get; set; }
        public double Area { get; set; }
        public string HRUCharacteristicLandUseCodeDisplayName { get; set; }
        public int? LSPCBasinID { get; set; }
        public string WatershedName { get; set; }
        public int? TreatmentBMPID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? LoadGeneratingUnitID { get; set; }
    }
}