//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vTrashGeneratingUnitForLoadCalculation]
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
    public partial class vTrashGeneratingUnitForLoadCalculation
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vTrashGeneratingUnitForLoadCalculation()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vTrashGeneratingUnitForLoadCalculation(int primaryKey, int trashGeneratingUnitID, int stormwaterJurisdictionID, int? treatmentBMPID, int? onlandVisualTrashAssessmentAreaID, int? landUseBlockID, DateTime? lastUpdateDate) : this()
        {
            this.PrimaryKey = primaryKey;
            this.TrashGeneratingUnitID = trashGeneratingUnitID;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.TreatmentBMPID = treatmentBMPID;
            this.OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentAreaID;
            this.LandUseBlockID = landUseBlockID;
            this.LastUpdateDate = lastUpdateDate;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vTrashGeneratingUnitForLoadCalculation(vTrashGeneratingUnitForLoadCalculation vTrashGeneratingUnitForLoadCalculation) : this()
        {
            this.PrimaryKey = vTrashGeneratingUnitForLoadCalculation.PrimaryKey;
            this.TrashGeneratingUnitID = vTrashGeneratingUnitForLoadCalculation.TrashGeneratingUnitID;
            this.StormwaterJurisdictionID = vTrashGeneratingUnitForLoadCalculation.StormwaterJurisdictionID;
            this.TreatmentBMPID = vTrashGeneratingUnitForLoadCalculation.TreatmentBMPID;
            this.OnlandVisualTrashAssessmentAreaID = vTrashGeneratingUnitForLoadCalculation.OnlandVisualTrashAssessmentAreaID;
            this.LandUseBlockID = vTrashGeneratingUnitForLoadCalculation.LandUseBlockID;
            this.LastUpdateDate = vTrashGeneratingUnitForLoadCalculation.LastUpdateDate;
            CallAfterConstructor(vTrashGeneratingUnitForLoadCalculation);
        }

        partial void CallAfterConstructor(vTrashGeneratingUnitForLoadCalculation vTrashGeneratingUnitForLoadCalculation);

        public int PrimaryKey { get; set; }
        public int TrashGeneratingUnitID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int? TreatmentBMPID { get; set; }
        public int? OnlandVisualTrashAssessmentAreaID { get; set; }
        public int? LandUseBlockID { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}