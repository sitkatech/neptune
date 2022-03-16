//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vNereidPlannedProjectTreatmentBMPRegionalSubbasin]
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
    public partial class vNereidPlannedProjectTreatmentBMPRegionalSubbasin
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vNereidPlannedProjectTreatmentBMPRegionalSubbasin()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vNereidPlannedProjectTreatmentBMPRegionalSubbasin(long? primaryKey, int treatmentBMPID, int? projectID, int regionalSubbasinID, int oCSurveyCatchmentID) : this()
        {
            this.PrimaryKey = primaryKey;
            this.TreatmentBMPID = treatmentBMPID;
            this.ProjectID = projectID;
            this.RegionalSubbasinID = regionalSubbasinID;
            this.OCSurveyCatchmentID = oCSurveyCatchmentID;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vNereidPlannedProjectTreatmentBMPRegionalSubbasin(vNereidPlannedProjectTreatmentBMPRegionalSubbasin vNereidPlannedProjectTreatmentBMPRegionalSubbasin) : this()
        {
            this.PrimaryKey = vNereidPlannedProjectTreatmentBMPRegionalSubbasin.PrimaryKey;
            this.TreatmentBMPID = vNereidPlannedProjectTreatmentBMPRegionalSubbasin.TreatmentBMPID;
            this.ProjectID = vNereidPlannedProjectTreatmentBMPRegionalSubbasin.ProjectID;
            this.RegionalSubbasinID = vNereidPlannedProjectTreatmentBMPRegionalSubbasin.RegionalSubbasinID;
            this.OCSurveyCatchmentID = vNereidPlannedProjectTreatmentBMPRegionalSubbasin.OCSurveyCatchmentID;
            CallAfterConstructor(vNereidPlannedProjectTreatmentBMPRegionalSubbasin);
        }

        partial void CallAfterConstructor(vNereidPlannedProjectTreatmentBMPRegionalSubbasin vNereidPlannedProjectTreatmentBMPRegionalSubbasin);

        public long? PrimaryKey { get; set; }
        public int TreatmentBMPID { get; set; }
        public int? ProjectID { get; set; }
        public int RegionalSubbasinID { get; set; }
        public int OCSurveyCatchmentID { get; set; }
    }
}