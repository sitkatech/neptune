//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vNereidPlannedProjectLoadingInput]
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
    public partial class vNereidPlannedProjectLoadingInput
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vNereidPlannedProjectLoadingInput()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vNereidPlannedProjectLoadingInput(int primaryKey, int projectID, int? delineationID, int? waterQualityManagementPlanID, int regionalSubbasinID, int oCSurveyCatchmentID, int modelBasinKey, string landUseCode, string baselineLandUseCode, string hydrologicSoilGroup, int slopePercentage, double area, double imperviousAcres, double baselineImperviousAcres, bool? delineationIsVerified, int? spatiallyAssociatedModelingApproach, int? relationallyAssociatedModelingApproach) : this()
        {
            this.PrimaryKey = primaryKey;
            this.ProjectID = projectID;
            this.DelineationID = delineationID;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.RegionalSubbasinID = regionalSubbasinID;
            this.OCSurveyCatchmentID = oCSurveyCatchmentID;
            this.ModelBasinKey = modelBasinKey;
            this.LandUseCode = landUseCode;
            this.BaselineLandUseCode = baselineLandUseCode;
            this.HydrologicSoilGroup = hydrologicSoilGroup;
            this.SlopePercentage = slopePercentage;
            this.Area = area;
            this.ImperviousAcres = imperviousAcres;
            this.BaselineImperviousAcres = baselineImperviousAcres;
            this.DelineationIsVerified = delineationIsVerified;
            this.SpatiallyAssociatedModelingApproach = spatiallyAssociatedModelingApproach;
            this.RelationallyAssociatedModelingApproach = relationallyAssociatedModelingApproach;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vNereidPlannedProjectLoadingInput(vNereidPlannedProjectLoadingInput vNereidPlannedProjectLoadingInput) : this()
        {
            this.PrimaryKey = vNereidPlannedProjectLoadingInput.PrimaryKey;
            this.ProjectID = vNereidPlannedProjectLoadingInput.ProjectID;
            this.DelineationID = vNereidPlannedProjectLoadingInput.DelineationID;
            this.WaterQualityManagementPlanID = vNereidPlannedProjectLoadingInput.WaterQualityManagementPlanID;
            this.RegionalSubbasinID = vNereidPlannedProjectLoadingInput.RegionalSubbasinID;
            this.OCSurveyCatchmentID = vNereidPlannedProjectLoadingInput.OCSurveyCatchmentID;
            this.ModelBasinKey = vNereidPlannedProjectLoadingInput.ModelBasinKey;
            this.LandUseCode = vNereidPlannedProjectLoadingInput.LandUseCode;
            this.BaselineLandUseCode = vNereidPlannedProjectLoadingInput.BaselineLandUseCode;
            this.HydrologicSoilGroup = vNereidPlannedProjectLoadingInput.HydrologicSoilGroup;
            this.SlopePercentage = vNereidPlannedProjectLoadingInput.SlopePercentage;
            this.Area = vNereidPlannedProjectLoadingInput.Area;
            this.ImperviousAcres = vNereidPlannedProjectLoadingInput.ImperviousAcres;
            this.BaselineImperviousAcres = vNereidPlannedProjectLoadingInput.BaselineImperviousAcres;
            this.DelineationIsVerified = vNereidPlannedProjectLoadingInput.DelineationIsVerified;
            this.SpatiallyAssociatedModelingApproach = vNereidPlannedProjectLoadingInput.SpatiallyAssociatedModelingApproach;
            this.RelationallyAssociatedModelingApproach = vNereidPlannedProjectLoadingInput.RelationallyAssociatedModelingApproach;
            CallAfterConstructor(vNereidPlannedProjectLoadingInput);
        }

        partial void CallAfterConstructor(vNereidPlannedProjectLoadingInput vNereidPlannedProjectLoadingInput);

        public int PrimaryKey { get; set; }
        public int ProjectID { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int RegionalSubbasinID { get; set; }
        public int OCSurveyCatchmentID { get; set; }
        public int ModelBasinKey { get; set; }
        public string LandUseCode { get; set; }
        public string BaselineLandUseCode { get; set; }
        public string HydrologicSoilGroup { get; set; }
        public int SlopePercentage { get; set; }
        public double Area { get; set; }
        public double ImperviousAcres { get; set; }
        public double BaselineImperviousAcres { get; set; }
        public bool? DelineationIsVerified { get; set; }
        public int? SpatiallyAssociatedModelingApproach { get; set; }
        public int? RelationallyAssociatedModelingApproach { get; set; }
    }
}