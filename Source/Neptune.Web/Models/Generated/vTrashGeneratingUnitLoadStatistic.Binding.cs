//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vTrashGeneratingUnitLoadStatistic]
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
    public partial class vTrashGeneratingUnitLoadStatistic
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vTrashGeneratingUnitLoadStatistic()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vTrashGeneratingUnitLoadStatistic(int primaryKey, int trashGeneratingUnitID, int? treatmentBMPID, string treatmentBMPName, double? trashGeneratingUnitArea, int stormwaterJurisdictionID, int? organizationID, string organizationName, decimal baselineLoadingRate, bool isFullTrashCapture, bool isPartialTrashCapture, int partialTrashCaptureEffectivenessPercentage, string landUseType, int? priorityLandUseTypeID, bool? hasBaselineScore, bool? hasProgressScore, decimal? currentLoadingRate, decimal progressLoadingRate, bool delineationIsVerified, DateTime? lastCalculatedDate, string priorityLandUseTypeDisplayName, int? onlandVisualTrashAssessmentAreaID, int? waterQualityManagementPlanID, string waterQualityManagementPlanName, int? landUseBlockID, DateTime? lastUpdateDate, double area, decimal? loadingRateDelta) : this()
        {
            this.PrimaryKey = primaryKey;
            this.TrashGeneratingUnitID = trashGeneratingUnitID;
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPName = treatmentBMPName;
            this.TrashGeneratingUnitArea = trashGeneratingUnitArea;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.OrganizationID = organizationID;
            this.OrganizationName = organizationName;
            this.BaselineLoadingRate = baselineLoadingRate;
            this.IsFullTrashCapture = isFullTrashCapture;
            this.IsPartialTrashCapture = isPartialTrashCapture;
            this.PartialTrashCaptureEffectivenessPercentage = partialTrashCaptureEffectivenessPercentage;
            this.LandUseType = landUseType;
            this.PriorityLandUseTypeID = priorityLandUseTypeID;
            this.HasBaselineScore = hasBaselineScore;
            this.HasProgressScore = hasProgressScore;
            this.CurrentLoadingRate = currentLoadingRate;
            this.ProgressLoadingRate = progressLoadingRate;
            this.DelineationIsVerified = delineationIsVerified;
            this.LastCalculatedDate = lastCalculatedDate;
            this.PriorityLandUseTypeDisplayName = priorityLandUseTypeDisplayName;
            this.OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentAreaID;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.WaterQualityManagementPlanName = waterQualityManagementPlanName;
            this.LandUseBlockID = landUseBlockID;
            this.LastUpdateDate = lastUpdateDate;
            this.Area = area;
            this.LoadingRateDelta = loadingRateDelta;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vTrashGeneratingUnitLoadStatistic(vTrashGeneratingUnitLoadStatistic vTrashGeneratingUnitLoadStatistic) : this()
        {
            this.PrimaryKey = vTrashGeneratingUnitLoadStatistic.PrimaryKey;
            this.TrashGeneratingUnitID = vTrashGeneratingUnitLoadStatistic.TrashGeneratingUnitID;
            this.TreatmentBMPID = vTrashGeneratingUnitLoadStatistic.TreatmentBMPID;
            this.TreatmentBMPName = vTrashGeneratingUnitLoadStatistic.TreatmentBMPName;
            this.TrashGeneratingUnitArea = vTrashGeneratingUnitLoadStatistic.TrashGeneratingUnitArea;
            this.StormwaterJurisdictionID = vTrashGeneratingUnitLoadStatistic.StormwaterJurisdictionID;
            this.OrganizationID = vTrashGeneratingUnitLoadStatistic.OrganizationID;
            this.OrganizationName = vTrashGeneratingUnitLoadStatistic.OrganizationName;
            this.BaselineLoadingRate = vTrashGeneratingUnitLoadStatistic.BaselineLoadingRate;
            this.IsFullTrashCapture = vTrashGeneratingUnitLoadStatistic.IsFullTrashCapture;
            this.IsPartialTrashCapture = vTrashGeneratingUnitLoadStatistic.IsPartialTrashCapture;
            this.PartialTrashCaptureEffectivenessPercentage = vTrashGeneratingUnitLoadStatistic.PartialTrashCaptureEffectivenessPercentage;
            this.LandUseType = vTrashGeneratingUnitLoadStatistic.LandUseType;
            this.PriorityLandUseTypeID = vTrashGeneratingUnitLoadStatistic.PriorityLandUseTypeID;
            this.HasBaselineScore = vTrashGeneratingUnitLoadStatistic.HasBaselineScore;
            this.HasProgressScore = vTrashGeneratingUnitLoadStatistic.HasProgressScore;
            this.CurrentLoadingRate = vTrashGeneratingUnitLoadStatistic.CurrentLoadingRate;
            this.ProgressLoadingRate = vTrashGeneratingUnitLoadStatistic.ProgressLoadingRate;
            this.DelineationIsVerified = vTrashGeneratingUnitLoadStatistic.DelineationIsVerified;
            this.LastCalculatedDate = vTrashGeneratingUnitLoadStatistic.LastCalculatedDate;
            this.PriorityLandUseTypeDisplayName = vTrashGeneratingUnitLoadStatistic.PriorityLandUseTypeDisplayName;
            this.OnlandVisualTrashAssessmentAreaID = vTrashGeneratingUnitLoadStatistic.OnlandVisualTrashAssessmentAreaID;
            this.WaterQualityManagementPlanID = vTrashGeneratingUnitLoadStatistic.WaterQualityManagementPlanID;
            this.WaterQualityManagementPlanName = vTrashGeneratingUnitLoadStatistic.WaterQualityManagementPlanName;
            this.LandUseBlockID = vTrashGeneratingUnitLoadStatistic.LandUseBlockID;
            this.LastUpdateDate = vTrashGeneratingUnitLoadStatistic.LastUpdateDate;
            this.Area = vTrashGeneratingUnitLoadStatistic.Area;
            this.LoadingRateDelta = vTrashGeneratingUnitLoadStatistic.LoadingRateDelta;
            CallAfterConstructor(vTrashGeneratingUnitLoadStatistic);
        }

        partial void CallAfterConstructor(vTrashGeneratingUnitLoadStatistic vTrashGeneratingUnitLoadStatistic);

        public int PrimaryKey { get; set; }
        public int TrashGeneratingUnitID { get; set; }
        public int? TreatmentBMPID { get; set; }
        public string TreatmentBMPName { get; set; }
        public double? TrashGeneratingUnitArea { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int? OrganizationID { get; set; }
        public string OrganizationName { get; set; }
        public decimal BaselineLoadingRate { get; set; }
        public bool IsFullTrashCapture { get; set; }
        public bool IsPartialTrashCapture { get; set; }
        public int PartialTrashCaptureEffectivenessPercentage { get; set; }
        public string LandUseType { get; set; }
        public int? PriorityLandUseTypeID { get; set; }
        public bool? HasBaselineScore { get; set; }
        public bool? HasProgressScore { get; set; }
        public decimal? CurrentLoadingRate { get; set; }
        public decimal ProgressLoadingRate { get; set; }
        public bool DelineationIsVerified { get; set; }
        public DateTime? LastCalculatedDate { get; set; }
        public string PriorityLandUseTypeDisplayName { get; set; }
        public int? OnlandVisualTrashAssessmentAreaID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public string WaterQualityManagementPlanName { get; set; }
        public int? LandUseBlockID { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public double Area { get; set; }
        public decimal? LoadingRateDelta { get; set; }
    }
}