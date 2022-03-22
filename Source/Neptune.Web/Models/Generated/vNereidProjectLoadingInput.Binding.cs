//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vNereidProjectLoadingInput]
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
    public partial class vNereidProjectLoadingInput
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vNereidProjectLoadingInput()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vNereidProjectLoadingInput(int primaryKey, int projectID, int? delineationID, int? waterQualityManagementPlanID, int regionalSubbasinID, int oCSurveyCatchmentID, int modelBasinKey, string landUseCode, string baselineLandUseCode, string hydrologicSoilGroup, int slopePercentage, double area, double imperviousAcres, double baselineImperviousAcres, bool? delineationIsVerified, int? spatiallyAssociatedModelingApproach, int? relationallyAssociatedModelingApproach) : this()
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
        public vNereidProjectLoadingInput(vNereidProjectLoadingInput vNereidProjectLoadingInput) : this()
        {
            this.PrimaryKey = vNereidProjectLoadingInput.PrimaryKey;
            this.ProjectID = vNereidProjectLoadingInput.ProjectID;
            this.DelineationID = vNereidProjectLoadingInput.DelineationID;
            this.WaterQualityManagementPlanID = vNereidProjectLoadingInput.WaterQualityManagementPlanID;
            this.RegionalSubbasinID = vNereidProjectLoadingInput.RegionalSubbasinID;
            this.OCSurveyCatchmentID = vNereidProjectLoadingInput.OCSurveyCatchmentID;
            this.ModelBasinKey = vNereidProjectLoadingInput.ModelBasinKey;
            this.LandUseCode = vNereidProjectLoadingInput.LandUseCode;
            this.BaselineLandUseCode = vNereidProjectLoadingInput.BaselineLandUseCode;
            this.HydrologicSoilGroup = vNereidProjectLoadingInput.HydrologicSoilGroup;
            this.SlopePercentage = vNereidProjectLoadingInput.SlopePercentage;
            this.Area = vNereidProjectLoadingInput.Area;
            this.ImperviousAcres = vNereidProjectLoadingInput.ImperviousAcres;
            this.BaselineImperviousAcres = vNereidProjectLoadingInput.BaselineImperviousAcres;
            this.DelineationIsVerified = vNereidProjectLoadingInput.DelineationIsVerified;
            this.SpatiallyAssociatedModelingApproach = vNereidProjectLoadingInput.SpatiallyAssociatedModelingApproach;
            this.RelationallyAssociatedModelingApproach = vNereidProjectLoadingInput.RelationallyAssociatedModelingApproach;
            CallAfterConstructor(vNereidProjectLoadingInput);
        }

        partial void CallAfterConstructor(vNereidProjectLoadingInput vNereidProjectLoadingInput);

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