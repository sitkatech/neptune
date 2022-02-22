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
        public vPowerBILandUseStatistic(int primaryKey, int hRUCharacteristicID, string hydrologicSoilGroup, int slopePercentage, double imperviousAcres, double area, string hRUCharacteristicLandUseCodeDisplayName, int? modelBasinID, string watershedName, int? catchIDN, int? downCatchIDN, int? treatmentBMPID, int? delineationID, int? waterQualityManagementPlanID, int? regionalSubbasinID, int? loadGeneratingUnitID, string modelBasinName, string landUse, string surfaceKey) : this()
        {
            this.PrimaryKey = primaryKey;
            this.HRUCharacteristicID = hRUCharacteristicID;
            this.HydrologicSoilGroup = hydrologicSoilGroup;
            this.SlopePercentage = slopePercentage;
            this.ImperviousAcres = imperviousAcres;
            this.Area = area;
            this.HRUCharacteristicLandUseCodeDisplayName = hRUCharacteristicLandUseCodeDisplayName;
            this.ModelBasinID = modelBasinID;
            this.WatershedName = watershedName;
            this.CatchIDN = catchIDN;
            this.DownCatchIDN = downCatchIDN;
            this.TreatmentBMPID = treatmentBMPID;
            this.DelineationID = delineationID;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.RegionalSubbasinID = regionalSubbasinID;
            this.LoadGeneratingUnitID = loadGeneratingUnitID;
            this.ModelBasinName = modelBasinName;
            this.LandUse = landUse;
            this.SurfaceKey = surfaceKey;
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
            this.ModelBasinID = vPowerBILandUseStatistic.ModelBasinID;
            this.WatershedName = vPowerBILandUseStatistic.WatershedName;
            this.CatchIDN = vPowerBILandUseStatistic.CatchIDN;
            this.DownCatchIDN = vPowerBILandUseStatistic.DownCatchIDN;
            this.TreatmentBMPID = vPowerBILandUseStatistic.TreatmentBMPID;
            this.DelineationID = vPowerBILandUseStatistic.DelineationID;
            this.WaterQualityManagementPlanID = vPowerBILandUseStatistic.WaterQualityManagementPlanID;
            this.RegionalSubbasinID = vPowerBILandUseStatistic.RegionalSubbasinID;
            this.LoadGeneratingUnitID = vPowerBILandUseStatistic.LoadGeneratingUnitID;
            this.ModelBasinName = vPowerBILandUseStatistic.ModelBasinName;
            this.LandUse = vPowerBILandUseStatistic.LandUse;
            this.SurfaceKey = vPowerBILandUseStatistic.SurfaceKey;
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
        public int? ModelBasinID { get; set; }
        public string WatershedName { get; set; }
        public int? CatchIDN { get; set; }
        public int? DownCatchIDN { get; set; }
        public int? TreatmentBMPID { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? LoadGeneratingUnitID { get; set; }
        public string ModelBasinName { get; set; }
        public string LandUse { get; set; }
        public string SurfaceKey { get; set; }
    }
}