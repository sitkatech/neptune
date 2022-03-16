//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vNereidPlannedProjectRegionalSubbasinCentralizedBMP]
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
    public partial class vNereidPlannedProjectRegionalSubbasinCentralizedBMP
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vNereidPlannedProjectRegionalSubbasinCentralizedBMP()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vNereidPlannedProjectRegionalSubbasinCentralizedBMP(int primaryKey, int regionalSubbasinID, int oCSurveyCatchmentID, int? projectID, int treatmentBMPID, int? upstreamBMPID, long? rowNumber) : this()
        {
            this.PrimaryKey = primaryKey;
            this.RegionalSubbasinID = regionalSubbasinID;
            this.OCSurveyCatchmentID = oCSurveyCatchmentID;
            this.ProjectID = projectID;
            this.TreatmentBMPID = treatmentBMPID;
            this.UpstreamBMPID = upstreamBMPID;
            this.RowNumber = rowNumber;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vNereidPlannedProjectRegionalSubbasinCentralizedBMP(vNereidPlannedProjectRegionalSubbasinCentralizedBMP vNereidPlannedProjectRegionalSubbasinCentralizedBMP) : this()
        {
            this.PrimaryKey = vNereidPlannedProjectRegionalSubbasinCentralizedBMP.PrimaryKey;
            this.RegionalSubbasinID = vNereidPlannedProjectRegionalSubbasinCentralizedBMP.RegionalSubbasinID;
            this.OCSurveyCatchmentID = vNereidPlannedProjectRegionalSubbasinCentralizedBMP.OCSurveyCatchmentID;
            this.ProjectID = vNereidPlannedProjectRegionalSubbasinCentralizedBMP.ProjectID;
            this.TreatmentBMPID = vNereidPlannedProjectRegionalSubbasinCentralizedBMP.TreatmentBMPID;
            this.UpstreamBMPID = vNereidPlannedProjectRegionalSubbasinCentralizedBMP.UpstreamBMPID;
            this.RowNumber = vNereidPlannedProjectRegionalSubbasinCentralizedBMP.RowNumber;
            CallAfterConstructor(vNereidPlannedProjectRegionalSubbasinCentralizedBMP);
        }

        partial void CallAfterConstructor(vNereidPlannedProjectRegionalSubbasinCentralizedBMP vNereidPlannedProjectRegionalSubbasinCentralizedBMP);

        public int PrimaryKey { get; set; }
        public int RegionalSubbasinID { get; set; }
        public int OCSurveyCatchmentID { get; set; }
        public int? ProjectID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int? UpstreamBMPID { get; set; }
        public long? RowNumber { get; set; }
    }
}