//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vNereidLoadingInput]
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
    public partial class vNereidLoadingInput
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vNereidLoadingInput()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vNereidLoadingInput(int primaryKey, int? delineationID, int? waterQualityManagementPlanID, int regionalSubbasinID, int oCSurveyCatchmentID, int lSPCBasinKey, string hRUCharacteristicLandUseCodeName, string hydrologicSoilGroup, int slopePercentage, double area, double imperviousAcres, bool? delineationIsVerified) : this()
        {
            this.PrimaryKey = primaryKey;
            this.DelineationID = delineationID;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.RegionalSubbasinID = regionalSubbasinID;
            this.OCSurveyCatchmentID = oCSurveyCatchmentID;
            this.LSPCBasinKey = lSPCBasinKey;
            this.HRUCharacteristicLandUseCodeName = hRUCharacteristicLandUseCodeName;
            this.HydrologicSoilGroup = hydrologicSoilGroup;
            this.SlopePercentage = slopePercentage;
            this.Area = area;
            this.ImperviousAcres = imperviousAcres;
            this.DelineationIsVerified = delineationIsVerified;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vNereidLoadingInput(vNereidLoadingInput vNereidLoadingInput) : this()
        {
            this.PrimaryKey = vNereidLoadingInput.PrimaryKey;
            this.DelineationID = vNereidLoadingInput.DelineationID;
            this.WaterQualityManagementPlanID = vNereidLoadingInput.WaterQualityManagementPlanID;
            this.RegionalSubbasinID = vNereidLoadingInput.RegionalSubbasinID;
            this.OCSurveyCatchmentID = vNereidLoadingInput.OCSurveyCatchmentID;
            this.LSPCBasinKey = vNereidLoadingInput.LSPCBasinKey;
            this.HRUCharacteristicLandUseCodeName = vNereidLoadingInput.HRUCharacteristicLandUseCodeName;
            this.HydrologicSoilGroup = vNereidLoadingInput.HydrologicSoilGroup;
            this.SlopePercentage = vNereidLoadingInput.SlopePercentage;
            this.Area = vNereidLoadingInput.Area;
            this.ImperviousAcres = vNereidLoadingInput.ImperviousAcres;
            this.DelineationIsVerified = vNereidLoadingInput.DelineationIsVerified;
            CallAfterConstructor(vNereidLoadingInput);
        }

        partial void CallAfterConstructor(vNereidLoadingInput vNereidLoadingInput);

        public int PrimaryKey { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int RegionalSubbasinID { get; set; }
        public int OCSurveyCatchmentID { get; set; }
        public int LSPCBasinKey { get; set; }
        public string HRUCharacteristicLandUseCodeName { get; set; }
        public string HydrologicSoilGroup { get; set; }
        public int SlopePercentage { get; set; }
        public double Area { get; set; }
        public double ImperviousAcres { get; set; }
        public bool? DelineationIsVerified { get; set; }
    }
}