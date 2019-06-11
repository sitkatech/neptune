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
        public vTrashGeneratingUnitLoadStatistic(int primaryKey, int trashGeneratingUnitID, int? treatmentBMPID, int stormwaterJurisdictionID, decimal baselineLoadingRate, int isFullTrashCapture, int partialTrashCaptureEffectivenessPercentage, decimal? currentLoadingRate, decimal? loadingRateDelta) : this()
        {
            this.PrimaryKey = primaryKey;
            this.TrashGeneratingUnitID = trashGeneratingUnitID;
            this.TreatmentBMPID = treatmentBMPID;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.BaselineLoadingRate = baselineLoadingRate;
            this.IsFullTrashCapture = isFullTrashCapture;
            this.PartialTrashCaptureEffectivenessPercentage = partialTrashCaptureEffectivenessPercentage;
            this.CurrentLoadingRate = currentLoadingRate;
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
            this.StormwaterJurisdictionID = vTrashGeneratingUnitLoadStatistic.StormwaterJurisdictionID;
            this.BaselineLoadingRate = vTrashGeneratingUnitLoadStatistic.BaselineLoadingRate;
            this.IsFullTrashCapture = vTrashGeneratingUnitLoadStatistic.IsFullTrashCapture;
            this.PartialTrashCaptureEffectivenessPercentage = vTrashGeneratingUnitLoadStatistic.PartialTrashCaptureEffectivenessPercentage;
            this.CurrentLoadingRate = vTrashGeneratingUnitLoadStatistic.CurrentLoadingRate;
            this.LoadingRateDelta = vTrashGeneratingUnitLoadStatistic.LoadingRateDelta;
            CallAfterConstructor(vTrashGeneratingUnitLoadStatistic);
        }

        partial void CallAfterConstructor(vTrashGeneratingUnitLoadStatistic vTrashGeneratingUnitLoadStatistic);

        public int PrimaryKey { get; set; }
        public int TrashGeneratingUnitID { get; set; }
        public int? TreatmentBMPID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public decimal BaselineLoadingRate { get; set; }
        public int IsFullTrashCapture { get; set; }
        public int PartialTrashCaptureEffectivenessPercentage { get; set; }
        public decimal? CurrentLoadingRate { get; set; }
        public decimal? LoadingRateDelta { get; set; }
    }
}