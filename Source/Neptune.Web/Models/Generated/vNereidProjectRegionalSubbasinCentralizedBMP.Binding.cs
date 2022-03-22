//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vNereidProjectRegionalSubbasinCentralizedBMP]
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
    public partial class vNereidProjectRegionalSubbasinCentralizedBMP
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vNereidProjectRegionalSubbasinCentralizedBMP()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vNereidProjectRegionalSubbasinCentralizedBMP(int primaryKey, int regionalSubbasinID, int oCSurveyCatchmentID, int? projectID, int treatmentBMPID, int? upstreamBMPID, long? rowNumber) : this()
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
        public vNereidProjectRegionalSubbasinCentralizedBMP(vNereidProjectRegionalSubbasinCentralizedBMP vNereidProjectRegionalSubbasinCentralizedBMP) : this()
        {
            this.PrimaryKey = vNereidProjectRegionalSubbasinCentralizedBMP.PrimaryKey;
            this.RegionalSubbasinID = vNereidProjectRegionalSubbasinCentralizedBMP.RegionalSubbasinID;
            this.OCSurveyCatchmentID = vNereidProjectRegionalSubbasinCentralizedBMP.OCSurveyCatchmentID;
            this.ProjectID = vNereidProjectRegionalSubbasinCentralizedBMP.ProjectID;
            this.TreatmentBMPID = vNereidProjectRegionalSubbasinCentralizedBMP.TreatmentBMPID;
            this.UpstreamBMPID = vNereidProjectRegionalSubbasinCentralizedBMP.UpstreamBMPID;
            this.RowNumber = vNereidProjectRegionalSubbasinCentralizedBMP.RowNumber;
            CallAfterConstructor(vNereidProjectRegionalSubbasinCentralizedBMP);
        }

        partial void CallAfterConstructor(vNereidProjectRegionalSubbasinCentralizedBMP vNereidProjectRegionalSubbasinCentralizedBMP);

        public int PrimaryKey { get; set; }
        public int RegionalSubbasinID { get; set; }
        public int OCSurveyCatchmentID { get; set; }
        public int? ProjectID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int? UpstreamBMPID { get; set; }
        public long? RowNumber { get; set; }
    }
}