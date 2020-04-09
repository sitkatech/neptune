//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vNereidRegionalSubbasinCentralizedBMP]
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
    public partial class vNereidRegionalSubbasinCentralizedBMP
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vNereidRegionalSubbasinCentralizedBMP()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vNereidRegionalSubbasinCentralizedBMP(int primaryKey, int regionalSubbasinID, int oCSurveyCatchmentID, int treatmentBMPID, long? rowNumber) : this()
        {
            this.PrimaryKey = primaryKey;
            this.RegionalSubbasinID = regionalSubbasinID;
            this.OCSurveyCatchmentID = oCSurveyCatchmentID;
            this.TreatmentBMPID = treatmentBMPID;
            this.RowNumber = rowNumber;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vNereidRegionalSubbasinCentralizedBMP(vNereidRegionalSubbasinCentralizedBMP vNereidRegionalSubbasinCentralizedBMP) : this()
        {
            this.PrimaryKey = vNereidRegionalSubbasinCentralizedBMP.PrimaryKey;
            this.RegionalSubbasinID = vNereidRegionalSubbasinCentralizedBMP.RegionalSubbasinID;
            this.OCSurveyCatchmentID = vNereidRegionalSubbasinCentralizedBMP.OCSurveyCatchmentID;
            this.TreatmentBMPID = vNereidRegionalSubbasinCentralizedBMP.TreatmentBMPID;
            this.RowNumber = vNereidRegionalSubbasinCentralizedBMP.RowNumber;
            CallAfterConstructor(vNereidRegionalSubbasinCentralizedBMP);
        }

        partial void CallAfterConstructor(vNereidRegionalSubbasinCentralizedBMP vNereidRegionalSubbasinCentralizedBMP);

        public int PrimaryKey { get; set; }
        public int RegionalSubbasinID { get; set; }
        public int OCSurveyCatchmentID { get; set; }
        public int TreatmentBMPID { get; set; }
        public long? RowNumber { get; set; }
    }
}